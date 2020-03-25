using System;
using System.Linq;
using FluentValidation;
using JetBrains.Annotations;
using Lykke.Service.Vouchers.Client.Models.Vouchers;

namespace Lykke.Service.Vouchers.Validators.Vouchers
{
    [UsedImplicitly]
    public class VoucherCreateModelValidator : AbstractValidator<VoucherCreateModel>
    {
        public VoucherCreateModelValidator()
        {
            RuleFor(o => o.SpendRuleId)
                .NotEqual(Guid.Empty)
                .WithMessage("Spend rule identifier required.");

            RuleFor(o => o.Codes)
                .Must(o => o != null && o.Any())
                .WithMessage("At least one voucher code required.")
                .Must(o => o.All(i => !string.IsNullOrEmpty(i) && i.Length <= 15))
                .WithMessage("Voucher code should not be the empty string and longer than 15 characters.");
        }
    }
}
