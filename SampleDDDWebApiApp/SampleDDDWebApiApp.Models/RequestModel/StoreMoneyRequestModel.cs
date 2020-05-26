using System;
using System.Collections.Generic;
using System.Text;

namespace SampleDDDWebApiApp.Models.RequestModel
{
    public class StoreMoneyRequestModel
    {
        public int UserId { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
