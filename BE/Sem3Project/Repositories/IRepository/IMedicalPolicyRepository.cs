using Sem3Project.Filters;
using Sem3Project.Helpers;
using Sem3Project.Models;
using Sem3Project.Models.Dtos;

namespace Sem3Project.Repositories.IRepository
{
    public interface IMedicalPolicyRepository
    {
        PagedList<MedicalPolicy> GetMedicalPolicies(PaginationFilter paginationFilter);

        PagedList<MedicalPolicy> GetMedicalPoliciesForAdmin(
            PaginationFilter paginationFilter,
            MedicalPolicyFilter medicalPolicyFilter
        );

        MedicalPolicy GetMedicalPolicy(string id);

        MedicalPolicy GetMedicalPolicyForAdmin(string id);

        bool CreateMedicalPolicy(MedicalPolicyCreateDto medicalPolicyCreateDto, string createdBy);

        bool UpdateMedicalPolicy(
            MedicalPolicyUpdateDto medicalPolicyUpdateDto,
            string id,
            string modifiedBy
        );

        bool Save();
    }
}
