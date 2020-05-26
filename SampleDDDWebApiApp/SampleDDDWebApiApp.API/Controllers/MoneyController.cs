using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleDDDWebApiApp.Business.Repositories;
using SampleDDDWebApiApp.Business.Services;
using SampleDDDWebApiApp.Models.Entities;
using SampleDDDWebApiApp.Models.RequestModel;
using SampleDDDWebApiApp.Models.ViewModel;

namespace SampleDDDWebApiApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {
        private readonly IMoneyService _moneyService;
        private readonly IRepository<User> _userRepo;
        private readonly IMapper _mapper;

        public MoneyController(IMoneyService moneyService, IRepository<User> userRepo, IMapper mapper)
        {
            _moneyService = moneyService;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        [HttpPost("store-money")]
        public IActionResult StoreMoney([FromBody] StoreMoneyRequestModel requestModel)
        {
            try
            {
                _moneyService.Store(requestModel);
                return Ok("Money was uccessfully stored.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("send-money")]
        public IActionResult SendMoney([FromBody] SendMoneyRequestModel requestModel)
        {
            try
            {
                _moneyService.Send(requestModel);
                return Ok("Money was successfully sent");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ceonvert-money")]
        public IActionResult ConvertMoney([FromBody] ConvertMoneyRequestModel requestModel)
        {
            try
            {
                _moneyService.Convert(requestModel);
                return Ok("Money was successfully converted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/get-user-balances")]
        public IActionResult GetBalances(int userId)
        {
            try
            {
                var user = _userRepo.Get(userId);
                var userBalances = new UserBalancesView() {
                    User = _mapper.Map<UserView>(user),
                    Balances = _mapper.Map<List<BalanceView>>(user.Money)
                };
                return Ok(userBalances);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}