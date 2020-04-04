using System;
using FluentValidation;
using JetBrains.Annotations;
using MAVN.Service.Vouchers.Client.Models.Vouchers;

namespace MAVN.Service.Vouchers.Validators.Vouchers
{
    [UsedImplicitly]
    public class VoucherBuyModelValidator : AbstractValidator<VoucherBuyModel>
    {
        public VoucherBuyModelValidator()
        {
            RuleFor(o => o.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Customer identifier required.");

            RuleFor(o => o.SpendRuleId)
                .NotEqual(Guid.Empty)
                .WithMessage("Spend rule identifier required.");
        }
    }
}
