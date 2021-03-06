﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManagerAppResourceServer.Interfaces;
using PasswordManagerAppResourceServer.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PasswordManagerAppResourceServer.Controllers
{
    [Route("api/")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        

        public StatisticsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        private int GetUserIdFromJwtToken()
        {
            int id = -1;
            try
            {
                id = Int32.Parse(HttpContext.User.Identity.Name);
                return id;
            }
            catch (Exception)
            {
                return id;
            }

        }


        [AllowAnonymous]
        // POST api/visitoragents
        [HttpPost("visitoragents")]
        public ActionResult<VisitorAgent> CreateVisitorAgent([FromBody] VisitorAgent visitorAgent)
        {
            try
            {
                _unitOfWork.Context.VisitorAgents.Add(visitorAgent);
                _unitOfWork.SaveChanges();
                
            }
            catch (Exception)
            {
                return BadRequest(new { Success = false });
            }
            return Ok(new { Success = true });


        }
        [Authorize]
        [HttpGet("statistics/user-data")]
        public Dictionary<string,int> GetUserStatisticData()
        {
            
            return _unitOfWork.GetStatisticData(GetUserIdFromJwtToken());
             

        }

    }
}
