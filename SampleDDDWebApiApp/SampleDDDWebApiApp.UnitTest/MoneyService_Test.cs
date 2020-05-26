using AutoMapper;
using Moq;
using NUnit.Framework;
using SampleDDDWebApiApp.API.Controllers;
using SampleDDDWebApiApp.Business.Repositories;
using SampleDDDWebApiApp.Business.Services;
using SampleDDDWebApiApp.Models.Entities;
using SampleDDDWebApiApp.Models.RequestModel;
using SampleDDDWebApiApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SampleDDDWebApiApp.UnitTest
{
    [TestFixture]
    public class MoneyService_Test
    {
        private Mock<IRepository<User>> _userRepoMoq;
        private Mock<IRepository<Currency>> _currencyRepoMoq;
        private Mock<IRepository<UserMoney>> _moneyRepoMoq;
        private IMoneyService _moneyServiceMoq;


        [SetUp]
        public void Setup()
        {
            _userRepoMoq = new Mock<IRepository<User>>();
            _currencyRepoMoq = new Mock<IRepository<Currency>>();
            _moneyRepoMoq = new Mock<IRepository<UserMoney>>();
            _moneyServiceMoq = new MoneyService(_userRepoMoq.Object, _currencyRepoMoq.Object);

        }
    }
}
