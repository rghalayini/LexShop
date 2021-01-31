using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LexShop.Core.Models;
using LexShop.Core.ViewModels;

namespace LexShop.Core.Contracts
{
    //5. Create the interface
    public interface IOrderService
    {
        void CreateOrder(Order baseOrder, List<BasketItemViewModel> basketItems);
    }
}
