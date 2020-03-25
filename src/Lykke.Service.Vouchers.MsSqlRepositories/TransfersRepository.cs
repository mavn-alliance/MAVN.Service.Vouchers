using System;
using System.Threading.Tasks;
using AutoMapper;
using Lykke.Common.MsSql;
using Lykke.Service.Vouchers.Domain.Entities;
using Lykke.Service.Vouchers.Domain.Repositories;
using Lykke.Service.Vouchers.MsSqlRepositories.Context;
using Lykke.Service.Vouchers.MsSqlRepositories.Entities;

namespace Lykke.Service.Vouchers.MsSqlRepositories
{
    public class TransfersRepository : ITransfersRepository
    {
        private readonly MsSqlContextFactory<DataContext> _contextFactory;
        private readonly IMapper _mapper;

        public TransfersRepository(MsSqlContextFactory<DataContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<Transfer> GetByIdAsync(Guid transferId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.Transfers.FindAsync(transferId);

                return _mapper.Map<Transfer>(entity);
            }
        }

        public async Task InsertAsync(Transfer transfer)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = _mapper.Map<TransferEntity>(transfer);

                context.Transfers.Add(entity);

                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Transfer transfer)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.Transfers.FindAsync(transfer.Id);

                _mapper.Map(transfer, entity);

                await context.SaveChangesAsync();
            }
        }
    }
}
