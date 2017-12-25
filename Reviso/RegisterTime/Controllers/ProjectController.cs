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
using System.ComponentModel.DataAnnotations;

using System.Security.Claims;
using System.Net.Http;

namespace RegisterTime.Controllers
{
    [Authorize]
    [Route("api/freelancer/{freelancerId}/project")]
    public class ProjectController : Controller
    {
        private IWorkRepository _WorkRepository;
        public ProjectController(IWorkRepository workRepository)
        {
            _WorkRepository = workRepository;
        }

        [HttpGet()]
        public IActionResult GetFreelancer(Guid freelancerId)
        {
            var freelancerRep = _WorkRepository.GetFreelancer(freelancerId);

            if (!freelancerRep.Any())
            {
                return NotFound();
            }

            var projects = freelancerRep.Select(a => new ProjectDto
            {
                ProjetId = a.Id,
                Title = a.Title,
                startDate = a.StartDate
            });

            return Ok(projects);
        }

        [HttpPost("createme")]
        public IActionResult CreateProject(Guid freelancerId, [FromBody] CreateProjectDto model)
        {
            if (ModelState.IsValid)
            {
                var userId = new Guid(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (freelancerId == userId)
                {
                    var projectRep = _WorkRepository.CreateProject(freelancerId, model.Title);
                    if (!projectRep.Any())
                    {
                        return NotFound();
                    }

                    var projects = projectRep.Select(a => new ProjectDto
                    {
                        ProjetId = a.Id,
                        Title = a.Title,
                        startDate = a.StartDate
                    });

                    return Ok(projects);                    
                }
            }
            return BadRequest();
        }

        [HttpPost("addme")]
        public IActionResult AddMeToProject(Guid freelancerId, [FromBody] AddMeToProjectDto model)
        {
            if (ModelState.IsValid)
            {
                var userId = new Guid(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (freelancerId == userId)
                {
                    var projectRep = _WorkRepository.AddFreelacerToProject(freelancerId, new Guid(model.ProjectId));
                    if (!projectRep.Any())
                    {
                        return NotFound();
                    }

                    var projects = projectRep.Select(a => new ProjectDto
                    {
                        ProjetId = a.Id,
                        Title = a.Title,
                        startDate = a.StartDate
                    });
                    return Ok(projects);
                }
            }
            return BadRequest();
        }

        [HttpGet("{projectId}")]
        public IActionResult GetProject(Guid freelancerId, Guid projectId)
        {
            var projectRep = _WorkRepository.GetProject(freelancerId, projectId);
            if (!projectRep.Any())
            {
                return NotFound();
            }
            var project = Mapper.Map<IEnumerable<WorkFlowDto>>(projectRep);
            return Ok(project);
        }

        [HttpPost("{projectId}/{hours:float}")]
        public IActionResult SetTime(Guid freelancerId, Guid projectId, float hours)
        {
            var projectRep = _WorkRepository.RegisterWorkHours(freelancerId, projectId, hours);
            if (!projectRep.Any())
            {
                return NotFound();
            }

            var project = Mapper.Map<IEnumerable<WorkFlowDto>>(projectRep);
            return Ok(project);
        }
    }
}