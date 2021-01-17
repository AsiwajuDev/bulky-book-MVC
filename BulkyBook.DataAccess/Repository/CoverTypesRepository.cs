using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class CoverTypesRepository : Repository<CoverTypes>, ICoverTypesRepository
    {
        private readonly ApplicationDbContext _db;

        public CoverTypesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CoverTypes coverType)
        {
            var objFromDb = _db.CoverTypes.FirstOrDefault(s => s.Id == coverType.Id);
            if(objFromDb != null)
            {
                objFromDb.Name = coverType.Name;                
            }            
        }
    }
}
