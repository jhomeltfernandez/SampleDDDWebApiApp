using SampleDDDWebApiApp.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SampleDDDWebApiApp.Models.Entites;
using System.Collections.ObjectModel;

namespace SampleDDDWebApiApp.Models.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public virtual ICollection<UserMoney> Money { get; set; }

        public List<Balance> GetBalances()
        {
            return this.Money.Select(s => new Balance { Currency = s.Currency.Name, Amount = s.Amount }).ToList();
        }

        public void StoreMoney(UserMoney money)
        {
            var userMoney = this.Money.Where(s => s.Currency.Name.Equals(money.Currency.Name)).FirstOrDefault();
            if (userMoney == null)
            {
                userMoney = money;
                this.Money.Add(userMoney);
            }
            else
            {
                userMoney.CashIn(money.Amount);
            }
        }

        public void Convert(Currency fromCurrency, Currency toCurreny, decimal amount)
        {
            var moneyFrom = this.Money.Where(s => s.Currency.Name.Equals(fromCurrency.Name)).FirstOrDefault();
            if (moneyFrom == null) throw new Exception($"The current user dont have money stored with the {fromCurrency.Name} currency.");

            var moneyTo = this.Money.Where(s => s.Currency.Name.Equals(toCurreny.Name)).FirstOrDefault();
            if (moneyTo == null)
            {
                moneyTo = new UserMoney
                {
                    Currency = toCurreny
                };
            }

            decimal cashInAmount = amount,
                cashOutAmount = 0m;
            if (moneyFrom.CanConvert(toCurreny, amount))
            {
                decimal mfConvertedAmount = moneyFrom.ConvertToCurrency(toCurreny);
                cashOutAmount = mfConvertedAmount - (mfConvertedAmount - cashInAmount);

                moneyTo.CashIn(cashInAmount);
                moneyFrom.CashOut(cashOutAmount);
            }
            else
            {
                throw new Exception($"The {moneyFrom.Currency.Name} money balance is not enough to convert to {cashInAmount} {toCurreny.Name}");
            }

            if (moneyTo == null) this.Money.Add(moneyTo);

        }


        public void ValidateIfHasCurrencyBalance(string currency)
        {
            var userMoney = this.Money.Where(s => s.Currency.Name.Equals(currency)).FirstOrDefault();
            if (userMoney == null) throw new Exception($"The user does not have {currency} balance");
        }
    }
}
