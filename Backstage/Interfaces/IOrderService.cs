﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backstage.ViewModels;

namespace Backstage.Interfaces
{
    public interface IOrderService
    {
        public IEnumerable<OrderViewModel> GetOrderData();

        public Task<IEnumerable<OrderViewModel>> UpdateOrder(OrderViewModel EditOrder);
    }
}
