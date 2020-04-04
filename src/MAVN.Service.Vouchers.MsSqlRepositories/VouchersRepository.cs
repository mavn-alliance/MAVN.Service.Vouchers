using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Common.MsSql;
using MAVN.Service.Vouchers.Domain.Entities;
using MAVN.Service.Vouchers.Domain.Exceptions;
using MAVN.Service.Vouchers.Domain.Repositories;
using MAVN.Service.Vouchers.MsSqlRepositories.Context;
using MAVN.Service.Vouchers.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.Vouchers.MsSqlRepositories
{
    public class VouchersRepository : IVouchersRepository
    {
        private const int ViolationInUniqueIndexErrorCode = 2601;
        
        private readonly MsSqlContextFactory<DataContext> _contextFactory;
        private readonly ILog _log;
        private readonly IMapper _mapper;

        public VouchersRepository(
            MsSqlContextFactory<DataContext> contextFactory,
            ILogFactory logFactory,
            IMapper mapper)
        {
            _contextFactory = contextFactory;
            _log = logFactory.CreateLog(this);
            _mapper = mapper;
        }

        public async Task<Voucher> GetByIdAsync(Guid voucherId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.Vouchers
                    .Include(o => o.CustomerVoucher)
                    .FirstOrDefaultAsync(o => o.Id == voucherId);

                return _mapper.Map<Voucher>(entity);
            }
        }

        public async Task<Voucher> GetInStockAsync(Guid spendRuleId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.Vouchers
                    .Where(o => o.SpendRuleId == spendRuleId)
                    .FirstOrDefaultAsync(o => o.Status == VoucherStatus.InStock);

                return _mapper.Map<Voucher>(entity);
            }
        }

        public async Task<IReadOnlyList<Voucher>> GetBySpendRuleIdAsync(Guid spendRuleId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entities = await context.Vouchers
                    .Where(o => o.SpendRuleId == spendRuleId)
                    .ToListAsync();

                return _mapper.Map<List<Voucher>>(entities);
            }
        }

        public async Task<PaginatedVouchers> GetByCustomerIdAsync(
            Guid customerId,
            int skip,
            int take)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var vouchers = context.Vouchers
                    .Include(o => o.CustomerVoucher)
                    .Where(o => o.CustomerVoucher.CustomerId == customerId)
                    .OrderByDescending(o => o.CustomerVoucher.PurchaseDate);

                var totalCount = await vouchers.CountAsync();

                var result = await vouchers
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();

                return new PaginatedVouchers
                {
                    Vouchers = _mapper.Map<List<Voucher>>(result),
                    TotalCount = totalCount,
                };
            }
        }

        public async Task<int> GetTotalAsync(Guid spendRuleId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var count = await context.Vouchers
                    .Where(o => o.SpendRuleId == spendRuleId)
                    .CountAsync();

                return count;
            }
        }

        public async Task<int> GetTotalByStatusAsync(Guid spendRuleId, VoucherStatus status)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var count = await context.Vouchers
                    .Where(o => o.SpendRuleId == spendRuleId && o.Status == status)
                    .CountAsync();

                return count;
            }
        }

        public async Task InsertAsync(IReadOnlyList<Voucher> vouchers)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var entities = _mapper.Map<List<VoucherEntity>>(vouchers);

                        context.Vouchers.AddRange(entities);

                        // TODO: Change to BulkInsert
                        await context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (DbUpdateException exception)
                        when ((exception.InnerException as SqlException)?.Number == ViolationInUniqueIndexErrorCode)
                    {
                        throw new CodeAlreadyExistException();
                    }
                    catch (Exception exception)
                    {
                        _log.Error(exception, "An error occurred while inserting vouchers");

                        throw;
                    }
                }
            }
        }

        public async Task UpdateAsync(Voucher voucher)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var entity = await context.Vouchers
                            .Include(o => o.CustomerVoucher)
                            .FirstOrDefaultAsync(o => o.Id == voucher.Id);

                        _mapper.Map(voucher, entity);

                        if (voucher.CustomerVoucher == null && entity.CustomerVoucher != null)
                            context.CustomerVouchers.Remove(entity.CustomerVoucher);
                        else if (voucher.CustomerVoucher != null && entity.CustomerVoucher == null)
                            entity.CustomerVoucher = _mapper.Map<CustomerVoucherEntity>(voucher.CustomerVoucher);
                        else if (voucher.CustomerVoucher != null && entity.CustomerVoucher != null)
                            _mapper.Map(voucher.CustomerVoucher, entity.CustomerVoucher);

                        await context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception exception)
                    {
                        _log.Error(exception, "An error occurred while updating voucher", $"voucherId: {voucher.Id}");

                        throw;
                    }
                }
            }
        }
    }
}
