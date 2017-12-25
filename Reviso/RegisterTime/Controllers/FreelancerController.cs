using System;
using System.Linq;
using System.Text;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using RegisterTime.Entities;
using RegisterTime.Services;
using RegisterTime.Models;

namespace RegisterTime.Controllers
{
    [Authorize]
    [Route("api/freelancer")]
    public class FreelancerController : Controller
    {
        private IWorkRepository _WorkRepository;
        public FreelancerController(IWorkRepository workRepository)
        {
            _WorkRepository = workRepository;
        }
        [HttpGet()]
        public IActionResult GetFreelancers()
        {
            var freelancersRep = _WorkRepository.GetFreelancers();
            var freelancers = Mapper.Map<IEnumerable<FreelancerDto>>(freelancersRep);
            return Ok(freelancers);
        }

        [HttpGet("projects")]
        public IActionResult GetProjects()
        {
            var projectsRep = _WorkRepository.GetProjects();
            var projects = projectsRep.Select( a => new ProjectDto
                {
                    ProjetId = a.Id,
                    Title = a.Title,
                    startDate = a.StartDate
                });

            return Ok(projects);
        }
    }
}