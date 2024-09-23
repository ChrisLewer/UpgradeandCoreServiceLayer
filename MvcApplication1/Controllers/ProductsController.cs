using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using MvcApplication1;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    //refer: https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/models-data/validating-with-a-service-layer-cs


    public class ProductsController : Controller
    {
        //private TestEntities db = new TestEntities(); //to remove for repo

        //private IProductRepository _repository; //remove when intro service layer

        private IProductService _service; //service var to handle validation and database 

        public ProductsController()
        { 
            //this(new ProductRepository())
            _service = new ProductService( /*this.ModelState,*/  
                new ModelStateWrapper(this.ModelState),  //<-- decouple the ModelState, ie could call the service from non mvc front end  
                new ProductRepository() );  //on creation pass the model state (using wrapper)
                                           //and ProductRepository class (which uses entity framework but could create a new class that
                                           //implements IProductRepository eg say a mysql one; new ProductRepositoryMSSQL()  ) 
        }

        //public ProductsController(/*IProductRepository repository,*/ IProductService service)
        //{
        //    //_repository = repository;

        //    
        //}

        // GET: Products
        public ActionResult Index()
        {
            //return View(db.Products.ToList());
            //return View(_repository.ListProducts());

            return View(_service.ListProducts());
        }

        

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,UnitsInStock")] Product product)
        {
            if (ModelState.IsValid)
            {
                //db.Products.Add(product);   --
                //db.SaveChanges();           -->  Removing    

                //_repository.CreateProduct(product);   --> if just using repo layer


                if (!_service.CreateProduct(product))  // --> if using a service layer to do validation and db stuff
                    return View();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                
            }
            base.Dispose(disposing);
        }
    }
}
