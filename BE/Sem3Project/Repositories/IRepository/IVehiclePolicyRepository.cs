using Sem3Project.Filters;
using Sem3Project.Helpers;
using Sem3Project.Models;
using Sem3Project.Models.Dtos;

namespace Sem3Project.Repositories.IRepository
{
    public interface IVehiclePolicyRepository
    {
        PagedList<VehiclePolicy> GetVehiclePolicies(PaginationFilter paginationFilter);

        PagedList<VehiclePolicy> GetVehiclePoliciesForAdmin(
            PaginationFilter paginationFilter,
            VehiclePolicyFilter vehiclePolicyFilter
        );

        VehiclePolicy GetVehiclePolicy(string id);

        VehiclePolicy GetVehiclePolicyForAdmin(string id);

        bool CreateVehiclePolicy(VehiclePolicyCreateDto vehiclePolicyCreateDto, string createdBy);

        bool UpdateVehiclePolicy(
            VehiclePolicyUpdateDto vehiclePolicyUpdateDto,
            string id,
            string modifiedBy
        );

        bool Save();
    }
}
