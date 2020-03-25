using Autofac;
using Lykke.Common.MsSql;
using Lykke.Service.Vouchers.Domain.Repositories;
using Lykke.Service.Vouchers.MsSqlRepositories.Context;

namespace Lykke.Service.Vouchers.MsSqlRepositories
{
    public class AutofacModule : Module
    {
        private readonly string _connectionString;

        public AutofacModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterMsSql(_connectionString,
                connectionString => new DataContext(connectionString, false),
                dbConnection => new DataContext(dbConnection));

            builder.RegisterType<OperationsRepository>()
                .As<IOperationsRepository>()
                .SingleInstance();

            builder.RegisterType<TransfersRepository>()
                .As<ITransfersRepository>()
                .SingleInstance();

            builder.RegisterType<VouchersRepository>()
                .As<IVouchersRepository>()
                .SingleInstance();
        }
    }
}
