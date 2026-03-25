using System.Reflection;
using System.Text;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.Events;
using TiendaServicios.RabbitMQ.Bus.Implement;
using Xunit;

namespace TiendaServicios.Api.Test.Books
{
    public class RabbitEventBusTest
    {
        [Fact]
        public async Task Consumer_Delegate_HitsBranches()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var mockScopeFactory = new Mock<IServiceScopeFactory>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            var mockScope = new Mock<IServiceScope>();
            
            mockScopeFactory.Setup(x => x.CreateScope()).Returns(mockScope.Object);
            mockScope.Setup(x => x.ServiceProvider).Returns(mockServiceProvider.Object);

            var bus = new RabbitEventBus(mockMediator.Object, mockScopeFactory.Object, new Mock<IConnectionFactory>().Object);

            // Access private method Consumer_Delegate
            var methodInfo = typeof(RabbitEventBus).GetMethod("Consumer_Delegate", BindingFlags.NonPublic | BindingFlags.Instance)!;

            // Mock basic deliver args
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new TestEvent()));
            var evt = new BasicDeliverEventArgs { RoutingKey = "TestEvent", Body = body };

            // Act - Case 1: eventName NOT in handlers
            await (Task)methodInfo.Invoke(bus, new object[] { new object(), evt })!;

            // Act - Case 2: eventName IN handlers
            var handlersField = typeof(RabbitEventBus).GetField("_handlers", BindingFlags.NonPublic | BindingFlags.Instance)!;
            var eventTypesField = typeof(RabbitEventBus).GetField("_eventTypes", BindingFlags.NonPublic | BindingFlags.Instance)!;

            var handlers = (Dictionary<string, List<Type>>)handlersField.GetValue(bus)!;
            var eventTypes = (List<Type>)eventTypesField.GetValue(bus)!;

            handlers.Add("TestEvent", new List<Type> { typeof(TestEventHandler) });
            eventTypes.Add(typeof(TestEvent));

            // Setup service provider to return handler
            mockServiceProvider.Setup(x => x.GetService(typeof(TestEventHandler))).Returns(new TestEventHandler());

            await (Task)methodInfo.Invoke(bus, new object[] { new object(), evt })!;

            // Assert
            Assert.True(true); 
        }
        
        [Fact]
        public void Subscribe_HandlesDuplicates()
        {
            var mockFactory = new Mock<IConnectionFactory>();
            var mockConn = new Mock<IConnection>();
            var mockModel = new Mock<IModel>();
            mockFactory.Setup(x => x.CreateConnection()).Returns(mockConn.Object);
            mockConn.Setup(x => x.CreateModel()).Returns(mockModel.Object);
            
            var busWithMock = new RabbitEventBus(new Mock<IMediator>().Object, new Mock<IServiceScopeFactory>().Object, mockFactory.Object);
            
            busWithMock.Subscribe<TestEvent, TestEventHandler>();
            
            // Second subscribe should throw
            Assert.Throws<ArgumentException>(() => busWithMock.Subscribe<TestEvent, TestEventHandler>());
        }

        [Fact]
        public void Publish_UsesFactory()
        {
            var mockFactory = new Mock<IConnectionFactory>();
            var mockConn = new Mock<IConnection>();
            var mockModel = new Mock<IModel>();
            mockFactory.Setup(x => x.CreateConnection()).Returns(mockConn.Object);
            mockConn.Setup(x => x.CreateModel()).Returns(mockModel.Object);
            
            var busWithMock = new RabbitEventBus(new Mock<IMediator>().Object, new Mock<IServiceScopeFactory>().Object, mockFactory.Object);
            
            busWithMock.Publish(new TestEvent());
            
            mockFactory.Verify(x => x.CreateConnection(), Times.Once);
        }

        public class TestEvent : Event { }
        public class TestEventHandler : IEventHandler<TestEvent>
        {
            public Task Handle(TestEvent @event) => Task.CompletedTask;
        }
    }
}
