﻿using Backend.Data.Models.Orders.Construction;

namespace Backend.Data.Models.Users
{
    public class Worker : User
    {
        public List<ConstructionOrder> AssignedOrders { get; set; } = new List<ConstructionOrder>();
    }
}
