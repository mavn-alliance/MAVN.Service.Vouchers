using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lykke.Common.MsSql;
using Lykke.Service.Vouchers.Domain.Entities;
using Lykke.Service.Vouchers.Domain.Repositories;
using Lykke.Service.Vouchers.MsSqlRepositories.Context;
using Lykke.Service.Vouchers.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lykke.Service.Vouchers.MsSqlRepositories
{
    public class OperationsRepository : IOperationsRepository
    {
        private readonly MsSqlContextFactory<DataContext> _contextFactory;
        private readonly IMapper _mapper;

        public OperationsRepository(MsSqlContextFactory<DataContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<Operation> GetByTransferIdAsync(Guid transferId, OperationType type)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.Operations
                    .Where(i => i.TransferId == transferId && i.Type == type)
                    .FirstAsync();

                return _mapper.Map<Operation>(entity);
            }
        }

        public async Task InsertAsync(Operation operation)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = _mapper.Map<OperationEntity>(operation);

                context.Operations.Add(entity);

                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Guid operationId, OperationStatus status)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.Operations.FindAsync(operationId);

                entity.Status = status;

                await context.SaveChangesAsync();
            }
        }

        public async Task ProcessTransferAndAcceptOperationsAsync(Guid transferId, Guid acceptOperationId)
        {
            var acceptOperation = new Operation
            {
                Id = acceptOperationId,
                TransferId = transferId,
                Type = OperationType.Accept,
                Status = OperationStatus.Created,
                Created = DateTime.UtcNow
            };
            var acceptEntity = _mapper.Map<OperationEntity>(acceptOperation);

            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.Operations
                    .Where(i => i.TransferId == transferId && i.Type == OperationType.Transfer)
                    .FirstAsync();
                entity.Status = OperationStatus.Succeeded;
                context.Operations.Update(entity);
                context.Operations.Add(acceptEntity);

                await context.SaveChangesAsync();
            }
        }
    }
}
