using SampleDDDWebApiApp.Business.Repositories;
using SampleDDDWebApiApp.Models.Entities;
using SampleDDDWebApiApp.Models.RequestModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SampleDDDWebApiApp.Business.Services
{
    public class MoneyService : IMoneyService
    {
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Currency> _currencyRepo;

        public MoneyService(IRepository<User> userRepo, IRepository<Currency> currencyRepo)
        {
            _userRepo = userRepo;
            _currencyRepo = currencyRepo;
        }

        public void Send(SendMoneyRequestModel request)
        {
            var userFrom = _userRepo.Get(request.FromUserId);
            var userTo = _userRepo.Get(request.ToUserId);
            if (userFrom == null || userTo == null) throw new Exception("The user/s you specified not found.");

            var userFrom_money = userFrom.Money.Where(s => s.Currency.Name.Equals(request.Currency)).FirstOrDefault();
            if (userFrom_money == null) throw new Exception($"The sender dont have a {request.Currency} balance.");
            if(!userFrom_money.CanSend(request.Amount)) throw new Exception($"The sender cannot send the specified amount.");

            var currency = _currencyRepo.GetAll(s => s.Name.Equals(request.Currency)).FirstOrDefault();
            if (currency == null) throw new Exception($"Selected currency is not yet available.");

            var userTo_money = userTo.Money.Where(s => s.Currency.Name.Equals(request.Currency)).FirstOrDefault();
            if (userTo_money == null)
            {
                userTo_money = new UserMoney
                {
                    Currency = currency
                };
                userTo_money.CashIn(request.Amount);


                userTo.StoreMoney(userTo_money);
            }

            userFrom_money.CashOut(request.Amount);

            _userRepo.Update(userFrom);
            _userRepo.Update(userTo);
        }

        public void Store(StoreMoneyRequestModel request)
        {
            var currency = _currencyRepo.GetAll(s => s.Name.Equals(request.Currency)).FirstOrDefault();
            if (currency == null) throw new Exception($"Selected currency is not yet available.");

            var user = _userRepo.Get(request.UserId);
            if (user == null) throw new Exception("User does not exist.");

            UserMoney money = new UserMoney {
                Currency = currency
            };
            money.CashIn(request.Amount);

            user.StoreMoney(money);

            _userRepo.Update(user);
        }

        public void Convert(ConvertMoneyRequestModel request)
        {
            if (request.FromCurrency.Equals(request.ToCurrency)) throw new Exception("Converting to the same currency is not allowed.");

            var user = _userRepo.Get(request.UserId);
            if (user == null) throw new Exception("The user you specified not found.");

            var fromCurrency = _currencyRepo.GetAll(s => s.Name.Equals(request.FromCurrency)).FirstOrDefault();
            var toCurrency = _currencyRepo.GetAll(s => s.Name.Equals(request.ToCurrency)).FirstOrDefault();
            if (toCurrency == null || fromCurrency == null) throw new Exception("Trying to convert to unavialable currecy is not allowed.");

            user.ValidateIfHasCurrencyBalance(request.FromCurrency);

            var fromMoney = user.Money.Where(s => s.Currency.Name.Equals(request.FromCurrency)).FirstOrDefault();
            var toMoney = user.Money.Where(s => s.Currency.Name.Equals(request.ToCurrency)).FirstOrDefault();

            fromMoney.ValidateIfCanConvert(toCurrency, request.Amount);

            decimal converted_FromMoney = fromMoney.ConvertToCurrency(toCurrency);

            this.Store(new StoreMoneyRequestModel()
            {
                UserId = user.Id,
                Currency = request.ToCurrency,
                Amount = request.Amount
            });

            decimal cashOutAmount = request.Amount * fromCurrency.Ratio;
            fromMoney.CashOut(cashOutAmount);

            _userRepo.Update(user);

        }
    }
}
