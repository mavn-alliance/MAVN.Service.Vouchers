using AutoMapper;
using MAVN.Service.Vouchers.Client.Models;
using MAVN.Service.Vouchers.Client.Models.Reports;
using MAVN.Service.Vouchers.Client.Models.Vouchers;
using MAVN.Service.Vouchers.Domain.Entities;

namespace MAVN.Service.Vouchers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PaginationModel, PageInfo>(MemberList.Destination);

            // Vouchers

            CreateMap<Voucher, CustomerVoucherModel>(MemberList.Destination)
                .ForMember(src => src.AmountInTokens,
                    opt => opt.MapFrom(src => src.CustomerVoucher.AmountInTokens))
                .ForMember(src => src.AmountInBaseCurrency,
                    opt => opt.MapFrom(src => src.CustomerVoucher.AmountInBaseCurrency))
                .ForMember(src => src.PurchaseDate,
                    opt => opt.MapFrom(src => src.CustomerVoucher.PurchaseDate));

            CreateMap<Voucher, VoucherModel>(MemberList.Source)
                .ForSourceMember(src => src.CustomerVoucher, opt => opt.DoNotValidate());

            CreateMap<PaginatedVouchers, PaginatedCustomerVouchersResponse>(MemberList.Destination);

            // Reports

            CreateMap<SpendRuleVouchersReport, SpendRuleVouchersReportModel>(MemberList.Destination);
        }
    }
}
