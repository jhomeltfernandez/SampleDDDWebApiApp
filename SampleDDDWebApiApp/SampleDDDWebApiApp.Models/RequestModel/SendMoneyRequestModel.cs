using System;
using System.Collections.Generic;
using System.Text;

namespace SampleDDDWebApiApp.Models.RequestModel
{
    public class SendMoneyRequestModel
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }

        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
