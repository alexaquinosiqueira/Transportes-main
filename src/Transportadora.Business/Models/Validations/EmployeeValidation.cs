using FluentValidation;

namespace Transportadora.Business.Models.Validations
{
    public class EmployeeValidation : AbstractValidator<Employee>
    {
        public EmployeeValidation()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithMessage("O Campo {PropertyName} precisa ser fornecido")
                .Length(2, 100).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
