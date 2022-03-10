using Sem3Project.Filters;
using Sem3Project.Helpers;
using Sem3Project.Models;
using Sem3Project.Models.Dtos;

namespace Sem3Project.Repositories
{
    public interface ILifePolicyRepository
    {
        PagedList<LifePolicy> GetLifePolicies(PaginationFilter paginationFilter);

        PagedList<LifePolicy> GetLifePoliciesForAdmin(
            PaginationFilter paginationFilter,
            LifePolicyFilter lifePolicyFilter
        );

        LifePolicy GetLifePolicy(string id);

        LifePolicy GetLifePolicyForAdmin(string id);

        bool CreateLifePolicy(LifePolicyCreateDto lifePolicyCreateDto, string createdBy);

        bool UpdateLifePolicy(
            LifePolicyUpdateDto lifePolicyUpdateDto,
            string id,
            string modifiedBy
        );

        bool Save();
    }
}
