﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Grand.Web.Services;
using System.Linq;
using Grand.Web.Models.Checkout;
using Grand.Web.Models.ShoppingCart;
using Grand.Core.Domain.Orders;
using Grand.Core;
using Grand.Services.Orders;

namespace Grand.Web.ViewComponents
{
    public class OrderTotalsViewComponent : ViewComponent
    {
        private readonly IShoppingCartWebService _shoppingCartWebService;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        public OrderTotalsViewComponent(IShoppingCartWebService shoppingCartWebService, IWorkContext workContext, IStoreContext storeContext)
        {
            this._shoppingCartWebService = shoppingCartWebService;
            this._workContext = workContext;
            this._storeContext = storeContext;
        }

        public IViewComponentResult Invoke(bool isEditable)
        {
            var cart = _workContext.CurrentCustomer.ShoppingCartItems
                .Where(sci => sci.ShoppingCartType == ShoppingCartType.ShoppingCart)
                .LimitPerStore(_storeContext.CurrentStore.Id)
                .ToList();
            var model = _shoppingCartWebService.PrepareOrderTotals(cart, isEditable);
            return View(model);
        }
    }
}