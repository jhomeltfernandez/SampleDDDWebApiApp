using System;
using System.Collections.Generic;
using System.Text;

namespace SampleDDDWebApiApp.Models.ViewModel
{
    public class UserBalancesView
    {
        public UserView User { get; set; }
        public List<BalanceView> Balances { get; set; }
    }
}
