using Microsoft.EntityFrameworkCore;
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
        )
        {
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

        public VehicleInsurance GetVehicleInsurance(string id, string userId)
        {
            try
            {
                Guid vehicleInsuranceGuid = Guid.Parse(id);
                Guid userGuid = Guid.Parse(userId);
                var vehicleInsurance = _db.VehicleInsurances
                    .Include(vi => vi.VehiclePolicy)
                    .FirstOrDefault(vi => vi.Id == vehicleInsuranceGuid && vi.User.Id == userGuid);
                return vehicleInsurance;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public PagedList<VehicleInsurance> GetVehicleInsurances(
            PaginationFilter paginationFilter,
            string userId
        )
        {
            return PagedList<VehicleInsurance>.ToPagedList(
                _db.VehicleInsurances
                    .OrderBy(vi => vi.CreatedDate)
                    .Include(vi => vi.VehiclePolicy)
                    .Where(vi => vi.User.Id == Guid.Parse(userId))
                    .ToList(),
                paginationFilter.PageNumber,
                paginationFilter.PageSize
            );
        }

        public PagedList<VehicleInsurance> GetVehicleInsurancesForAdmin(
            PaginationFilter paginationFilter,
            VehicleInsuranceFilter vehicleInsuranceFilter
        )
        {
            var x = _db.VehicleInsurances
                .Include(vi => vi.VehiclePolicy)
                .Include(vi => vi.User)
                .OrderBy(vi => vi.CreatedDate);

            if (!string.IsNullOrWhiteSpace(vehicleInsuranceFilter.Search))
            {
                x =
                    (IOrderedQueryable<VehicleInsurance>)x.Where(
                        u =>
                            u.User.Email
                                .ToLower()
                                .Contains(vehicleInsuranceFilter.Search.Trim().ToLower())
                            || u.User.PhoneNumber.Contains(
                                vehicleInsuranceFilter.Search.Trim().ToLower()
                            )
                    );
            }

            if (
                vehicleInsuranceFilter.IsVerified == "true"
                || vehicleInsuranceFilter.IsVerified == "false"
            )
            {
                bool isVerified;

                if (vehicleInsuranceFilter.IsVerified == "true")
                {
                    isVerified = true;
                }
                else
                {
                    isVerified = false;
                }

                x = (IOrderedQueryable<VehicleInsurance>)x.Where(vi => vi.IsVerified == isVerified);
            }

            if (vehicleInsuranceFilter.IsPaid == "false")
            {
                x = (IOrderedQueryable<VehicleInsurance>)x.Where(vi => vi.Receipt == null);
            }

            //x = (IOrderedQueryable<VehicleInsurance>)x


            return PagedList<VehicleInsurance>.ToPagedList(
                x.ToList(),
                paginationFilter.PageNumber,
                paginationFilter.PageSize
            );
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
