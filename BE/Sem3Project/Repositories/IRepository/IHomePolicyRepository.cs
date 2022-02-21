using Sem3Project.Filters;
using Sem3Project.Helpers;
using Sem3Project.Models;
using Sem3Project.Models.Dtos;

namespace Sem3Project.Repositories.IRepository
{
    public interface IHomePolicyRepository
    {
        PagedList<HomePolicy> GetHomePolicies(PaginationFilter paginationFilter);

        PagedList<HomePolicy> GetHomePoliciesForAdmin(
           PaginationFilter paginationFilter,
           HomePolicyFilter homePolicyFilter
        );

        HomePolicy GetHomePolicy(string id);

        HomePolicy GetHomePolicyForAdmin(string id);

        bool CreateHomePolicy(HomePolicyCreateDto homePolicyCreateDto, string createdBy);

        bool UpdateHomePolicy(
            HomePolicyUpdateDto homePolicyUpdateDto,
            string id,
            string modifiedBy
        );

        bool Save();
    }
}
