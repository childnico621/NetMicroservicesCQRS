using FluentValidation;
using TiendaServicios.Api.Autor.Model.Request;

namespace TiendaServicios.Api.Autor.Model.Validators
{
    public class AddAuthorValidator : AbstractValidator<AddAuthorRequestModel>
    {
        public AddAuthorValidator()
        {

            RuleFor(x => x.Name).NotEmpty().Length(0, 10);
            RuleFor(x=>x.LastName).Length(0, 20);
            RuleFor(x => x.Birthdate).NotEmpty().LessThan(DateTime.Now.AddDays(-1));
        }
    }
}
