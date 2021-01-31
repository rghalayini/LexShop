﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LexShop.Core.Contracts;
using LexShop.Core.Models;

namespace LexShop.WebUI.Controllers
{
    public class OrderManagerController : Controller
    {
        IOrderService orderService;
        public OrderManagerController(IOrderService OrderService)
        {
            this.orderService = OrderService;
        }



        /// <summary>
        /// Add a view to the index page
        /// View name: Index
        /// Template: List
        /// Model class: Order (LexShop.Core.Models)


        // GET: OrderManager
        // index page will return our list of orders
        public ActionResult Index()
        {

            List<Order> orders = orderService.GetOrderList();

            return View(orders);
        }
        // Get individual order
        public ActionResult UpdateOrder(string Id)
        {
            ViewBag.StatusList = new List<string>() {
                "Order Created",
                "Payment Processed",
                "Order Shipped",
                "Order Complete"
            };
            Order order = orderService.GetOrder(Id);
            return View(order);
        }
        [HttpPost]
        public ActionResult UpdateOrder(Order updatedOrder, string Id)
        {
            Order order = orderService.GetOrder(Id);

            //change order status
            order.OrderStatus = updatedOrder.OrderStatus;
            orderService.UpdateOrder(order);

            return RedirectToAction("Index");
        }
    }
}