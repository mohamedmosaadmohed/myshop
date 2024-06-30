using Microsoft.EntityFrameworkCore.Infrastructure;
using myshop.DataAccess.Data;
using myshop.Entities.Models;
using myshop.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DataAccess.Implementation
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
            private readonly ApplicationDbContext _context;
            public ProductRepository(ApplicationDbContext context) : base(context)
            {
                _context = context;
            }

            public void Update(Product product)
            {
                var productInDb = _context.TbProduct.FirstOrDefault(x => x.Id == product.Id);
            if (productInDb != null)
                {
                productInDb.Name = product.Name;
                productInDb.Description = product.Description;
                productInDb.Price = product.Price;
                productInDb.Image = product.Image;
                productInDb.CatagoryId = product.CatagoryId;
                productInDb.CreateDate = DateTime.Now;
            }
            }
        }
    }
