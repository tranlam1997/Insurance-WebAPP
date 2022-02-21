using Sem3Project.Data;
using Sem3Project.Filters;
using Sem3Project.Helpers;
using Sem3Project.Models;
using Sem3Project.Models.Dtos;
using System;
using System.Linq;

namespace Sem3Project.Repositories
{
    public class LifePolicyRepository : ILifePolicyRepository
    {
        private readonly ApplicationDbContext _db;

        public LifePolicyRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateLifePolicy(LifePolicyCreateDto lifePolicyCreateDto, string createdBy)
        {
            var lifePolicy = new LifePolicy();

            lifePolicy.Type = lifePolicyCreateDto.Type;
            lifePolicy.Content = lifePolicyCreateDto.Content;
            lifePolicy.PersonClaim = lifePolicyCreateDto.PersonClaim;
            lifePolicy.AmountPaid = lifePolicyCreateDto.AmountPaid;
            lifePolicy.CreatedDate = DateTime.Now;
            lifePolicy.ModifiedDate = DateTime.Now;
            lifePolicy.CreatedBy = createdBy;

            _db.LifePolicies.Add(lifePolicy);
            return Save();
        }

        public PagedList<LifePolicy> GetLifePolicies(PaginationFilter paginationFilter)
        {
            return PagedList<LifePolicy>.ToPagedList(
                _db.LifePolicies.OrderBy(lp => lp.CreatedDate).Where(lp => lp.IsReleased == true).ToList(),
                paginationFilter.PageNumber,
                paginationFilter.PageSize
            );
        }

        public PagedList<LifePolicy> GetLifePoliciesForAdmin(
            PaginationFilter paginationFilter,
            LifePolicyFilter lifePolicyFilter
        ) {
            var x = _db.LifePolicies.OrderBy(lp => lp.CreatedDate);

            if (lifePolicyFilter.IsReleased == "true" || lifePolicyFilter.IsReleased == "false")
            {
                bool isReleased;

                if (lifePolicyFilter.IsReleased == "true")
                {
                    isReleased = true;
                }
                else
                {
                    isReleased = false;
                }

                x = (IOrderedQueryable<LifePolicy>)x.Where(lp => lp.IsReleased == isReleased);
            }

            return PagedList<LifePolicy>.ToPagedList(
                x.ToList(),
                paginationFilter.PageNumber,
                paginationFilter.PageSize
            );
        }

        public LifePolicy GetLifePolicy(string id)
        {
            try
            {
                Guid guid = Guid.Parse(id);
                var lifePolicy = _db.LifePolicies.FirstOrDefault(lp => lp.Id == guid && lp.IsReleased == true);
                return lifePolicy;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public LifePolicy GetLifePolicyForAdmin(string id)
        {
            try
            {
                Guid guid = Guid.Parse(id);
                var lifePolicy = _db.LifePolicies.FirstOrDefault(lp => lp.Id == guid);
                return lifePolicy;
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

        public bool UpdateLifePolicy(
            LifePolicyUpdateDto lifePolicyUpdateDto, 
            string id, 
            string modifiedBy
         ) {
            LifePolicy lifePolicy = _db.LifePolicies.Where(lp => lp.Id == Guid.Parse(id)).FirstOrDefault();
            if (lifePolicy != null)
            {
                lifePolicy.Type = lifePolicyUpdateDto.Type;
                lifePolicy.Content = lifePolicyUpdateDto.Content;
                lifePolicy.PersonClaim = lifePolicyUpdateDto.PersonClaim;
                lifePolicy.AmountPaid = lifePolicyUpdateDto.AmountPaid;
                lifePolicy.IsReleased = lifePolicyUpdateDto.IsReleased;
                lifePolicy.ModifiedDate = DateTime.Now;
                lifePolicy.ModifiedBy = modifiedBy;

                if (lifePolicyUpdateDto.IsReleased == true && lifePolicy.IsReleased == false)
                {
                    lifePolicy.ReleasedDate = DateTime.Now;
                }

                _db.LifePolicies.Update(lifePolicy);
                return Save();
            } else
            {
                return false;
            }
        }
    }
}
