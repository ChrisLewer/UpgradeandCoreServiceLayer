using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;  <-- decouple

namespace MvcApplication1.Models
{
    public class ProductService : IProductService
    {

        //private ModelStateDictionary _modelState;  <-- decouple from MVC
        private IValidationDictionary _validatonDictionary;

        private IProductRepository _repository;

        public ProductService(/*ModelStateDictionary modelState*/ IValidationDictionary validationDictionary,
            IProductRepository repository)
        {
            //_modelState = modelState; <-- decouple from MVC
            _validatonDictionary = validationDictionary;

            _repository = repository;   //create out repo for handling db stuff
        }


        //put all validation in one place
        protected bool ValidateProduct(Product productToValidate)
        {
            if (productToValidate.Name.Trim().Length == 0)
                //_modelState.AddModelError("Name", "Name is required.");
                _validatonDictionary.AddError("Name", "Name is required.");
            if (productToValidate.Description.Trim().Length == 0)
                //_modelState.AddModelError("Description", "Description is required.");
                _validatonDictionary.AddError("Description", "Description is required.");
            if (productToValidate.UnitsInStock < 0)
                //_modelState.AddModelError("UnitsInStock", "Units in stock cannot be less than zero.");
                _validatonDictionary.AddError("UnitsInStock", "Units in stock cannot be less than zero.");
            //return _modelState.IsValid;
            return _validatonDictionary.IsValid;
        }

        public IEnumerable<Product> ListProducts()
        {
            return _repository.ListProducts();
        }

        public bool CreateProduct(Product productToCreate)
        {
            // Validation logic
            if (!ValidateProduct(productToCreate))
                return false;

            // Database logic
            try
            {
                _repository.CreateProduct(productToCreate);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Product GetProductById(int id)
        {
            return _repository.GetProductById(id);
        }
    }

    public interface IProductService
    {
        bool CreateProduct(Product productToCreate);
        IEnumerable<Product> ListProducts();

        Product GetProductById(int id);
    }
}