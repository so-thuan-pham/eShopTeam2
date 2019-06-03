using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using Ordering.Domain;
using Ordering.Domain.AggregatesModel.OrderAggregate;
using System;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISession session;
        private readonly ITransaction tx;
        private readonly OrderRepository orderRepository;

        public IOrderRepository Orders => orderRepository;

        public UnitOfWork(ISessionFactory sessionFactory)
        {
            session = sessionFactory.OpenSession();
            tx = session.BeginTransaction();
            orderRepository = new OrderRepository(session);
        }

        public async Task CommitChanges()
        {
            await tx.CommitAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && session != null)
            {
                tx.Dispose();
                session.Dispose();
            }

        }
    }

    public class UnitOfWorkProvider : IUnitOfWorkProvider
    {
        private readonly ISessionFactory sessionFactory;

        public UnitOfWorkProvider(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork(sessionFactory);
        }
    }
}
