using System;

namespace Lykke.Service.Vouchers.Domain.Services
{
    public interface IEncoderService
    {
        string EncodeTransferData(Guid spendRuleId, Guid transferId);

        string EncodeAcceptData(Guid spendRuleId, Guid transferId);
    }
}
