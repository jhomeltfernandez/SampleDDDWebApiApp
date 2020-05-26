using System;
using System.Collections.Generic;
using System.Text;

namespace SampleDDDWebApiApp.Models.RequestModel
{
    public class ConvertMoneyRequestModel
    {
        public int UserId { get; set; }
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal Amount { get; set; }
    }
}
