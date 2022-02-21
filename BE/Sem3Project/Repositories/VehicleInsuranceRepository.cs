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
    public class VehicleInsuranceRepository : IVehicleInsuranceRepository
    {
        private readonly ApplicationDbContext _db;

        public VehicleInsuranceRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public VehicleInsurance CreateVehicleInsurance(
            VehicleInsuranceCreateDto vehicleInsuranceCreateDto, 
            User user, 
            VehiclePolicy vehiclePolicy,
            string createdBy
        ) {
            var vehicleInsurance = new VehicleInsurance();

            vehicleInsurance.PlateNumber = vehicleInsuranceCreateDto.PlateNumber;
            vehicleInsurance.EngineNumber = vehicleInsuranceCreateDto.EngineNumber;
            vehicleInsurance.ChassisNumber = vehicleInsuranceCreateDto.ChassisNumber;
            vehicleInsurance.Type = vehicleInsuranceCreateDto.Type;
            vehicleInsurance.EffectiveDate = vehicleInsuranceCreateDto.EffectiveDate;
            vehicleInsurance.ExpireDate = vehicleInsuranceCreateDto.ExpireDate;
            vehicleInsurance.CreatedDate = DateTime.Now;
            vehicleInsurance.ModifiedDate = DateTime.Now;
            vehicleInsurance.CreatedBy = createdBy;
            vehicleInsurance.Remark = null;
            vehicleInsurance.Status = "unverified";
            vehicleInsurance.Token = RandomToken.Generate();
            vehicleInsurance.User = user;
            vehicleInsurance.VehiclePolicy = vehiclePolicy;
            vehicleInsurance.Receipt = null;

            _db.VehicleInsurances.Add(vehicleInsurance);
            _db.SaveChanges();
            return vehicleInsurance;
        }

        public PagedList<VehicleInsurance> GetVehiclePolicies(PaginationFilter paginationFilter, string userId)
        {
            throw new System.NotImplementedException();
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public bool VerifyInsurance(string token)
        {
            var vehicleInsurance = _db.VehicleInsurances.FirstOrDefault(hp => hp.Token == token);

            if (vehicleInsurance == null)
            {
                return false;
            }
            else
            {
                vehicleInsurance.Token = null;
                vehicleInsurance.IsVerified = true;
                _db.VehicleInsurances.Update(vehicleInsurance);
                return Save();
            }
        }
    }
}
