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

        VehicleInsurance GetVehicleInsurance(string id, string userId);

        PagedList<VehicleInsurance> GetVehicleInsurances(
            PaginationFilter paginationFilter,
            string userId
        );

        PagedList<VehicleInsurance> GetVehicleInsurancesForAdmin(
            PaginationFilter paginationFilter,
            VehicleInsuranceFilter vehicleInsuranceFilter
        );

        bool VerifyInsurance(string token);

        bool Save();
    }
}
