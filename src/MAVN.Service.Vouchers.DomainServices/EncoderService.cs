using System;
using MAVN.PrivateBlockchain.Definitions;
using MAVN.Service.Vouchers.Domain.Services;
using Nethereum.ABI;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Contracts;
using Nethereum.Hex.HexConvertors.Extensions;

namespace MAVN.Service.Vouchers.DomainServices
{
    public class EncoderService : IEncoderService
    {
        private const string EmptyInvoiceId = "n/a";

        private readonly ABIEncode _abiEncode;
        private readonly FunctionCallEncoder _functionCallEncoder;

        public EncoderService()
        {
            _abiEncode = new ABIEncode();
            _functionCallEncoder = new FunctionCallEncoder();
        }

        public string EncodeTransferData(Guid spendRuleId, Guid transferId)
        {
            var parameters = new[]
            {
                new ABIValue(new StringType(), spendRuleId.ToString()),
                new ABIValue(new StringType(), EmptyInvoiceId),
                new ABIValue(new StringType(), transferId.ToString())
            };

            return _abiEncode.GetABIEncoded(parameters).ToHex(true);
        }

        public string EncodeAcceptData(Guid spendRuleId, Guid transferId)
        {
            var acceptFunction = new AcceptTransferFunction
            {
                TransferId = transferId.ToString(), InvoiceId = EmptyInvoiceId, CampaignId = spendRuleId.ToString()
            };

            var abiFunction = ABITypedRegistry.GetFunctionABI<AcceptTransferFunction>();

            return _functionCallEncoder.EncodeRequest(acceptFunction, abiFunction.Sha3Signature);
        }
    }
}
