using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SampleDDDWebApiApp.Business.Repositories;
using SampleDDDWebApiApp.Models.Entities;
using SampleDDDWebApiApp.Models.ViewModel;

namespace SampleDDDWebApiApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepo;
        private readonly IMapper _mapper;

        public UserController(IRepository<User> userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        [HttpGet("/get-users")]
        public IActionResult GetUsers()
        {
            var users = _userRepo.GetAll().ToList();
            return Ok(_mapper.Map<List<UserView>>(users));
        }
    }
}