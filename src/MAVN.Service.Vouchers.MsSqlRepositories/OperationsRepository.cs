using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MAVN.Persistence.PostgreSQL.Legacy;
using MAVN.Service.Vouchers.Domain.Entities;
using MAVN.Service.Vouchers.Domain.Repositories;
using MAVN.Service.Vouchers.MsSqlRepositories.Context;
using MAVN.Service.Vouchers.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.Vouchers.MsSqlRepositories
{
    public class OperationsRepository : IOperationsRepository
    {
        private readonly PostgreSQLContextFactory<DataContext> _contextFactory;
        private readonly IMapper _mapper;

        public OperationsRepository(PostgreSQLContextFactory<DataContext> contextFactory, IMapper mapper)
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
