﻿using ProductManagment.Data;
using static ProductManagment.ProductManagmentModel;

namespace ProductManagment.Repository
{
    public class ProductRepository :Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)

        {
            _context = context;
        }

        //public void Save()
        //{
        //    _context.SaveChanges();
        //}

        public void Update(Product obj)
        {
            _context.Products.Update(obj);
        }

      
    }
}
