using System;
using System.Linq;
using System.Collections.Generic;
using RegisterTime.Entities;

namespace RegisterTime.Services
{
    public class WorkRepository : IWorkRepository
    {
        private WorkFlowContext _context;
        public WorkRepository(WorkFlowContext context)
        {
            _context = context;
        }

        public WorkRepository()
        {
        }

        public IEnumerable<Freelancer> GetFreelancers()
        {
            return _context.Freelancers
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .ToList();
        }

        public IEnumerable<Project> GetFreelancer(Guid freelancerId)
        {
            if (FreelancerExists(freelancerId))
            {
                List<Guid> ids = _context.WorkFlows.Where(
                    a => a.FreelancerId == freelancerId)
                    .Select(p => p.ProjectId)
                    .Distinct().ToList();

                return _context.Projects.Where(p => ids.Contains(p.Id));

            }
            return Enumerable.Empty<Project>();
        }

        public IEnumerable<Project> GetProjects()
        {
            return _context.Projects.OrderByDescending(a => a.StartDate.Date)
                .OrderByDescending(a => a.StartDate.TimeOfDay);
        }

        public IEnumerable<WorkFlow> GetProject(Guid freelancerId, Guid projectId)
        {
            if (FreelancerExists(freelancerId) && ProjectExists(projectId))
            {
                return _context.WorkFlows.Where(
                    a => a.FreelancerId == freelancerId &&
                    a.ProjectId == projectId);
            }
            return Enumerable.Empty<WorkFlow>();
        }

        public bool FreelancerExists(Guid freelancerId)
        {
            if (_context.Freelancers.Where(a => a.Id == freelancerId).Any())
            {
                return true;
            }
            return false;
        }

        public bool ProjectExists(Guid projectId)
        {
            if (_context.Projects.Where(a => a.Id == projectId).Any())
            {
                return true;
            }
            return false;
        }

        public IEnumerable<Project> CreateProject(Guid freelancerId, string title)
        {
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Title = title,
                StartDate = DateTime.Now
            };
            _context.Projects.Add(project);

            var workFlow = new WorkFlow
            {
                FreelancerId = freelancerId,
                ProjectId = project.Id
            };
            _context.WorkFlows.Add(workFlow);

            if (Save())
            {
                return GetFreelancer(freelancerId);
            }

            return Enumerable.Empty<Project>();
        }

        public IEnumerable<Project> AddFreelacerToProject(Guid freelancerId, Guid projectId)
        {
            var freelancerHasIt = _context.WorkFlows.Where(
                a => a.FreelancerId == freelancerId &&
                a.ProjectId == projectId);

            if (freelancerHasIt.Any())
            {
                return Enumerable.Empty<Project>();
            }

            if (ProjectExists(projectId))
            {
                var workFlow = new WorkFlow
                {
                    FreelancerId = freelancerId,
                    ProjectId = projectId
                };
                _context.WorkFlows.Add(workFlow);

                if (Save())
                {
                    return GetFreelancer(freelancerId);
                }
            }
            return Enumerable.Empty<Project>();
        }

        public IEnumerable<WorkFlow> RegisterWorkHours(Guid freelancerId, Guid projectId, float hours)
        {
            if (FreelancerExists(freelancerId) && ProjectExists(projectId))
            {
                var workFlow = new WorkFlow
                {
                    FreelancerId = freelancerId,
                    ProjectId = projectId,
                    WorkHours = hours
                };
                _context.WorkFlows.Add(workFlow);

                if (Save())
                {
                    return GetProject(freelancerId, projectId);
                }
            }
            return Enumerable.Empty<WorkFlow>();
        }

        public void AddFreelancer(Freelancer freelancer)
        {
            _context.Freelancers.Add(freelancer);
        }

        public void DeleteFreelancer(Freelancer freelancer)
        {
            _context.Freelancers.Remove(freelancer);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<Freelancer> GetFreelancer(IEnumerable<Guid> freelancerId)
        {
            throw new NotImplementedException();
        }
    }
}
