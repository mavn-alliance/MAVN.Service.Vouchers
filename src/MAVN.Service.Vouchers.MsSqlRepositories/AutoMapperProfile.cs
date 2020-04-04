using AutoMapper;
using MAVN.Service.Vouchers.Domain.Entities;
using MAVN.Service.Vouchers.MsSqlRepositories.Entities;

namespace MAVN.Service.Vouchers.MsSqlRepositories
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Operation, OperationEntity>(MemberList.Source);
            CreateMap<OperationEntity, Operation>(MemberList.Destination);

            CreateMap<Transfer, TransferEntity>(MemberList.Source);
            CreateMap<TransferEntity, Transfer>(MemberList.Destination);

            CreateMap<Voucher, VoucherEntity>(MemberList.Source)
                .ForMember(dest => dest.CustomerVoucher, opt => opt.Ignore());
            CreateMap<VoucherEntity, Voucher>(MemberList.Destination);

            CreateMap<CustomerVoucherEntity, CustomerVoucher>(MemberList.Destination);
            CreateMap<CustomerVoucher, CustomerVoucherEntity>(MemberList.Source);
        }
    }
}
