using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            CoverTypes = new CoverTypesRepository(_db);
            StoredProcedure_Call = new StoredProcedure_Call(_db);
            Product = new ProductRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }
        public ICoverTypesRepository CoverTypes { get; private set; }
        public IProductRepository Product { get; private set; }
        public IStoredProcedure_Call StoredProcedure_Call { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
