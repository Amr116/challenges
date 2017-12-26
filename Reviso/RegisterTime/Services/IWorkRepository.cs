using System;
using System.Collections.Generic;
using RegisterTime.Entities;

namespace RegisterTime.Services
{
    public interface IWorkRepository
    {
        IEnumerable<Freelancer> GetFreelancers();
        IEnumerable<Project> GetFreelancer(Guid Id);

        IEnumerable<Project> GetProjects();
        IEnumerable<WorkFlow> GetProject(Guid freelancerId, Guid projectId);
        bool FreelancerExists(Guid freelancerId);
        bool ProjectExists(Guid projectId);

        IEnumerable<Project> CreateProject(Guid freelancerId, string title);

        IEnumerable<WorkFlow> RegisterWorkHours(Guid freelancerId, Guid projectId, float hours);

        IEnumerable<Project> AddFreelacerToProject(Guid freelancerId, Guid projectId);

        bool Save();

        void AddFreelancer(Freelancer freelancer);
        void DeleteFreelancer(Freelancer freelancer);

    }
}
