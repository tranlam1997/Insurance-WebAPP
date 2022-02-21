using Sem3Project.Data;
using Sem3Project.Filters;
using Sem3Project.Helpers;
using Sem3Project.Models;
using Sem3Project.Models.Dtos;
using Sem3Project.Repositories.IRepository;
using System;
using System.Linq;

namespace Sem3Project.Repositories
{
    public class HomePolicyRepository : IHomePolicyRepository
    {
        private readonly ApplicationDbContext _db;

        public HomePolicyRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateHomePolicy(HomePolicyCreateDto homePolicyCreateDto, string createdBy)
        {
            var homePolicy = new HomePolicy();
            homePolicy.Type = homePolicyCreateDto.Type;
            homePolicy.Content = homePolicyCreateDto.Content;
            homePolicy.AmountPaid = homePolicyCreateDto.AmountPaid;
            homePolicy.CreatedDate = DateTime.Now;
            homePolicy.ModifiedDate = DateTime.Now;
            homePolicy.CreatedBy = createdBy;

            _db.HomePolicies.Add(homePolicy);
            return Save();
        }

        public PagedList<HomePolicy> GetHomePolicies(PaginationFilter paginationFilter)
        {
            return PagedList<HomePolicy>.ToPagedList(
                _db.HomePolicies.OrderBy(hp => hp.CreatedDate).Where(hp => hp.IsReleased == true).ToList(),
                paginationFilter.PageNumber,
                paginationFilter.PageSize
            );
        }

        public PagedList<HomePolicy> GetHomePoliciesForAdmin(PaginationFilter paginationFilter, HomePolicyFilter homePolicyFilter)
        {
            var x = _db.HomePolicies.OrderBy(hp => hp.CreatedDate);

            if (homePolicyFilter.IsReleased == "true" || homePolicyFilter.IsReleased == "false")
            {
                bool isReleased;
                if (homePolicyFilter.IsReleased == "true")
                {
                    isReleased = true;
                }
                else
                {
                    isReleased = false;
                }

                x = (IOrderedQueryable<HomePolicy>)x.Where(hp => hp.IsReleased == isReleased);
            }

            return PagedList<HomePolicy>.ToPagedList(
                x.ToList(),
                paginationFilter.PageNumber,
                paginationFilter.PageSize
            );
        }

        public HomePolicy GetHomePolicy(string id)
        {
            try
            {
                Guid guid = Guid.Parse(id);
                var homePolicy = _db.HomePolicies.FirstOrDefault(hp => hp.Id == guid && hp.IsReleased == true);
                return homePolicy;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public HomePolicy GetHomePolicyForAdmin(string id)
        {
            try
            {
                Guid guid = Guid.Parse(id);
                var homePolicy = _db.HomePolicies.FirstOrDefault(hp => hp.Id == guid);
                return homePolicy;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public bool UpdateHomePolicy(HomePolicyUpdateDto homePolicyUpdateDto, string id, string modifiedBy)
        {
            HomePolicy homePolicy = _db.HomePolicies.Where(hp => hp.Id == Guid.Parse(id)).FirstOrDefault();
            homePolicy.Type = homePolicyUpdateDto.Type;
            homePolicy.Content = homePolicyUpdateDto.Content;
            homePolicy.AmountPaid = homePolicyUpdateDto.AmountPaid;
            homePolicy.IsReleased = homePolicyUpdateDto.IsReleased;
            homePolicy.ModifiedDate = DateTime.Now;
            homePolicy.ModifiedBy = modifiedBy;

            if (homePolicyUpdateDto.IsReleased == true && homePolicy.IsReleased == false)
            {
                homePolicy.ReleasedDate = DateTime.Now;
            }

            _db.HomePolicies.Update(homePolicy);
            return Save();
        }
    }
}
