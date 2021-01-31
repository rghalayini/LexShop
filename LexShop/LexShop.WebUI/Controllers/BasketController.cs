using LexShop.Core.Contracts;
using LexShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LexShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IRepository<Customer> customers;
        IBasketService basketService;
        IOrderService orderService;


        public BasketController(IBasketService BasketService, IOrderService OrderService, IRepository<Customer> Customers)
        {
            this.basketService = BasketService;
            this.orderService = OrderService;
            this.customers = Customers;
        }

        // GET: BasketController2
        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext);
            return View(model);
        }
        public ActionResult AddToBasket(string Id)
        {
            basketService.AddToBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }
        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);
            return PartialView(basketSummary);
        }
        //Create a checkout view(partial view) and set the template to a create template and make sure the model class is : Order (LexShop.Core.Models)

        [Authorize]
        public ActionResult Checkout()
        {
            //retrive customer from database (ASP.NET Identity)
            Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);

            //Make sure the customer is nott null
            if (customer != null)
            {
                Order order = new Order()
                {
                    Email = customer.Email,
                    City = customer.City,
                    State = customer.State,
                    Street = customer.Street,
                    FirstName = customer.FirstName,
                    Surname = customer.LastName,
                    ZipCode = customer.ZipCode
                };

                return View(order);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult Checkout(Order order)
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";
            order.Email = User.Identity.Name;
            //TODO: process payment...

            order.OrderStatus = "Payment Processed";
            orderService.CreateOrder(order, basketItems);
            //clear the basket
            basketService.ClearBasket(this.HttpContext);

            //And finally redirect the user to the thank you page with order id.
            return RedirectToAction("Thankyou", new { OrderId = order.Id });
        }
        //create view (partial view) for the thank you page. Template: Empty
        public ActionResult ThankYou(string OrderId)
        {
            ViewBag.OrderId = OrderId;
            return View();
        }
    }
}