using SampleDDDWebApiApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleDDDWebApiApp.Models.ValueObjects
{
    public class Balance
    {
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
