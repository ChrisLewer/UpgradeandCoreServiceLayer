﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcApplication1.Models
{

    public class ProductRepository : MvcApplication1.Models.IProductRepository
    {
        private TestEntities _entities = new TestEntities();

        public IEnumerable<Product> ListProducts()
        {
            return _entities.Products.ToList();
        }

        public bool CreateProduct(Product productToCreate)
        {
            try
            {
                //Move logic out of controller (ie no fat controller and seperation of concerns)
                _entities.Products.Add(productToCreate);
                _entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Product GetProductById(int id)
        {
            return _entities.Products.Find(id);
        }
    }


    public interface IProductRepository
    {
        bool CreateProduct(Product productToCreate);
        IEnumerable<Product> ListProducts();

        Product GetProductById(int id);

    }



}
