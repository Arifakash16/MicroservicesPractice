﻿namespace Discount.API.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get;set; }
        public string Description { get;set; }
        public int Amount { get; set; }
    }
}
