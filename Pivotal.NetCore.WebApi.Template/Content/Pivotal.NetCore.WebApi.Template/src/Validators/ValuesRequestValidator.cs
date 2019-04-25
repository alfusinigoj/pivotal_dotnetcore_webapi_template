using FluentValidation;
using Pivotal.NetCore.WebApi.Template.Extensions;
using Pivotal.NetCore.WebApi.Template.Models;

namespace Pivotal.NetCore.WebApi.Template.Validators
{
    public class ValuesRequestValidator : AbstractValidator<ValuesRequest>
    {
        public ValuesRequestValidator()
        {
            RuleFor(p => p.Param1)
                .IsNonEmptyEightDigitNumber();

            RuleFor(p => p.Param2)
                .NotNull()
                .NotEmpty();
        }
    }
}
