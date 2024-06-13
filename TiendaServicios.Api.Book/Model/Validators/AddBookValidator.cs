using FluentValidation;
using TiendaServicios.Api.Book.Model.Request;

namespace TiendaServicios.Api.Book.Model.Validators
{
    public class AddBookValidator : AbstractValidator<AddBookRequestModel>
    {
        public AddBookValidator()
        {
            RuleFor(x => x.Title).NotEmpty().Length(0, 30);
            RuleFor(x => x.Published).NotEmpty();
            RuleFor(x=>x.AuthorId).NotEmpty();
        }
    }
}
