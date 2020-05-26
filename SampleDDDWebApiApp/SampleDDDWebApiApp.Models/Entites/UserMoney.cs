
using SampleDDDWebApiApp.Models.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleDDDWebApiApp.Models.Entities
{
    public class UserMoney : BaseEntity
    {
        public decimal Amount { get; set; }

        public virtual User User { get; set; }
        public virtual Currency Currency { get; set; }


        public bool CanConvert(Currency toCurrency, decimal toAmount)
        {
            decimal moneyBalance = this.Amount * toCurrency.Ratio;

            return (toAmount <= moneyBalance);
        }

        public void ValidateIfCanConvert(Currency toCurrency, decimal toAmount)
        {
            if (!CanConvert(toCurrency, toAmount)) throw new Exception($"Not enough balance to convert to {toAmount} {toCurrency.Name}");
        }

        public bool CanSend(decimal amount)
        {
            return (this.Amount >= amount);
        }

        public decimal ConvertToCurrency(Currency toCurrency)
        {
            return this.Amount * toCurrency.Ratio;
        }


        public void CashIn(decimal amount)
        {
            this.Amount += amount;
        }

        public void CashOut(decimal amount)
        {
            if (this.Amount >= amount)
            {
                this.Amount -= amount;
            }
            else
            {
                throw new Exception($"Dont have enough {this.Currency.Name} balance.");
            }
        }
    }
}
