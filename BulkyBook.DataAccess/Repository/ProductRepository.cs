﻿using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var objFromDb = _db.Product.FirstOrDefault(s => s.Id == product.Id);
            if(objFromDb != null)
            {
                if(product.ImageUrl != null)
                {
                    objFromDb.ImageUrl = product.ImageUrl;
                }    
                objFromDb.Title = product.Title;
                objFromDb.ISBN = product.ISBN;
                objFromDb.Description = product.Description;
                objFromDb.Author = product.Author;
                objFromDb.Price = product.Price;
                objFromDb.ListPrice = product.ListPrice;
                objFromDb.Price50 = product.Price50;
                objFromDb.Price100 = product.Price100;
                objFromDb.CategoryId = product.CategoryId;
                objFromDb.CoverTypesId = product.CoverTypesId;
            }            
        }
    }
}