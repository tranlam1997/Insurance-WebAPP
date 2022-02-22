using Sem3Project.Filters;
using Sem3Project.Helpers;
using Sem3Project.Models;
using Sem3Project.Models.Dtos;

namespace Sem3Project.Repositories.IRepository
{
    public interface IVehicleInsuranceRepository
    {
        VehicleInsurance CreateVehicleInsurance(
            VehicleInsuranceCreateDto vehicleInsuranceCreateDto, 
            User user, 
            VehiclePolicy vehiclePolicy,
            string createdBy
        );

        PagedList<VehicleInsurance> GetVehiclePolicies(PaginationFilter paginationFilter, string userId);

        PagedList<VehicleInsurance> GetVehiclePoliciesForAdmin(PaginationFilter paginationFilter, VehicleInsuranceFilter vehicleInsuranceFilter);

        bool VerifyInsurance(string token);

        bool Save();
    }
}
