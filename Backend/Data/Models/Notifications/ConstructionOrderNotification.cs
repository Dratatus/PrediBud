﻿using Backend.Data.Models.Users;

namespace Backend.Data.Models.Notifications
{
    public class ConstructionOrderNotification
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
    }
}
