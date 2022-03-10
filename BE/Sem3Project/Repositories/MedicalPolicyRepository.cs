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
    public class MedicalPolicyRepository : IMedicalPolicyRepository
    {
        private readonly ApplicationDbContext _db;

        public MedicalPolicyRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateMedicalPolicy(
            MedicalPolicyCreateDto medicalPolicyCreateDto,
            string createdBy
        )
        {
            var medicalPolicy = new MedicalPolicy();
            medicalPolicy.Type = medicalPolicyCreateDto.Type;
            medicalPolicy.Content = medicalPolicyCreateDto.Content;
            medicalPolicy.AmountPaid = medicalPolicyCreateDto.AmountPaid;
            medicalPolicy.CreatedDate = DateTime.Now;
            medicalPolicy.ModifiedDate = DateTime.Now;
            medicalPolicy.CreatedBy = createdBy;

            _db.MedicalPolicies.Add(medicalPolicy);
            return Save();
        }

        public PagedList<MedicalPolicy> GetMedicalPolicies(PaginationFilter paginationFilter)
        {
            return PagedList<MedicalPolicy>.ToPagedList(
                _db.MedicalPolicies
                    .OrderBy(mp => mp.CreatedDate)
                    .Where(mp => mp.IsReleased == true)
                    .ToList(),
                paginationFilter.PageNumber,
                paginationFilter.PageSize
            );
        }

        public PagedList<MedicalPolicy> GetMedicalPoliciesForAdmin(
            PaginationFilter paginationFilter,
            MedicalPolicyFilter medicalPolicyFilter
        )
        {
            var x = _db.MedicalPolicies.OrderBy(mp => mp.CreatedDate);

            if (
                medicalPolicyFilter.IsReleased == "true"
                || medicalPolicyFilter.IsReleased == "false"
            )
            {
                bool isReleased;

                if (medicalPolicyFilter.IsReleased == "true")
                {
                    isReleased = true;
                }
                else
                {
                    isReleased = false;
                }

                x = (IOrderedQueryable<MedicalPolicy>)x.Where(mp => mp.IsReleased == isReleased);
            }

            return PagedList<MedicalPolicy>.ToPagedList(
                x.ToList(),
                paginationFilter.PageNumber,
                paginationFilter.PageSize
            );
        }

        public MedicalPolicy GetMedicalPolicy(string id)
        {
            try
            {
                Guid guid = Guid.Parse(id);
                var medicalPolicy = _db.MedicalPolicies.FirstOrDefault(
                    lp => lp.Id == guid && lp.IsReleased == true
                );
                return medicalPolicy;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public MedicalPolicy GetMedicalPolicyForAdmin(string id)
        {
            try
            {
                Guid guid = Guid.Parse(id);
                var medicalPolicy = _db.MedicalPolicies.FirstOrDefault(lp => lp.Id == guid);
                return medicalPolicy;
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

        public bool UpdateMedicalPolicy(
            MedicalPolicyUpdateDto medicalPolicyUpdateDto,
            string id,
            string modifiedBy
        )
        {
            MedicalPolicy medicalPolicy = _db.MedicalPolicies
                .Where(lp => lp.Id == Guid.Parse(id))
                .FirstOrDefault();
            if (medicalPolicy != null)
            {
                medicalPolicy.Type = medicalPolicyUpdateDto.Type;
                medicalPolicy.Content = medicalPolicyUpdateDto.Content;
                medicalPolicy.AmountPaid = medicalPolicyUpdateDto.AmountPaid;
                medicalPolicy.IsReleased = medicalPolicyUpdateDto.IsReleased;
                medicalPolicy.ModifiedDate = DateTime.Now;
                medicalPolicy.ModifiedBy = modifiedBy;

                if (
                    medicalPolicyUpdateDto.IsReleased == true
                    && medicalPolicyUpdateDto.IsReleased == false
                )
                {
                    medicalPolicy.ModifiedDate = DateTime.Now;
                }

                _db.MedicalPolicies.Update(medicalPolicy);
                return Save();
            }
            else
            {
                return false;
            }
        }
    }
}
