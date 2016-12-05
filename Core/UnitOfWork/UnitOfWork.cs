using Core.DB;
using Core.Repositories;
using System;

namespace Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        bool IsError { get; set; }
        IDictionaryRepository DictionaryRepository { get; }
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IPermissionRepository PermissionRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }

        int Complate();
    }

    public class UnitOfWork : IUnitOfWork
    {

        private readonly DbCoreDataContext _context;

        public bool IsError { get; set; }

        public IDictionaryRepository DictionaryRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }
        public IRoleRepository RoleRepository { get; private set; }
        public IPermissionRepository PermissionRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }
        public IOrderDetailRepository OrderDetailRepository { get; private set; }

        public UnitOfWork(DbCoreDataContext context)
        {
            _context = context;

            DictionaryRepository = new DictionaryRepository(_context);
            UserRepository = new UserRepository(_context);
            RoleRepository = new RoleRepository(_context);
            PermissionRepository = new PermissionRepository(_context);
            OrderRepository = new OrderRepository(_context);
            OrderDetailRepository = new OrderDetailRepository(_context);

        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Complate()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                IsError = true;
                return -1;
            }
        }
    }
}
