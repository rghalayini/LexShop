using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LexShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
        public BasketController()
        {
            context = productContext;
            productCategories = productCategoryContext;
        }

        // GET: Basket
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);           
        }
    }
}