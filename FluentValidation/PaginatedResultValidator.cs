using FluentValidation;
using PB.BaseResult.Communication;

namespace PB.BaseResult.FluentValidation
{
    public class PaginatedResultValidator : AbstractValidator<PaginatedResult>
    {
        public PaginatedResultValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("O número da página deve ser maior que zero.");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("O tamanho da página deve ser maior que zero.");
        }
    }
}
