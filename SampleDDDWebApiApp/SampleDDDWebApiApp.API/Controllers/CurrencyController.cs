using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleDDDWebApiApp.Business.Repositories;
using SampleDDDWebApiApp.Models.Entities;

namespace SampleDDDWebApiApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IRepository<Currency> _currencyRepo;
        private readonly IMapper _mapper;

        public CurrencyController(IRepository<Currency> currencyRepo, IMapper mapper)
        {
            _currencyRepo = currencyRepo;
            _mapper = mapper;
        }

        [HttpGet("/get-available-currencies")]
        public IActionResult GetUsers()
        {
            var currencies = _currencyRepo.GetAll().ToList();
            return Ok(currencies); 
        }
    }
} 