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
    public class VehiclePolicyRepository: IVehiclePolicyRepository
    {
        private readonly ApplicationDbContext _db;

        public VehiclePolicyRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateVehiclePolicy(VehiclePolicyCreateDto vehiclePolicyCreateDto, string createdBy)
        {
            
            var vehiclePolicy = new VehiclePolicy();

            vehiclePolicy.Type = vehiclePolicyCreateDto.Type;
            vehiclePolicy.Content = vehiclePolicyCreateDto.Content;
            vehiclePolicy.PersonClaim = vehiclePolicyCreateDto.PersonClaim;
            vehiclePolicy.VehicleClaim = vehiclePolicyCreateDto.VehicleClaim;
            vehiclePolicy.AmountPaid = vehiclePolicyCreateDto.AmountPaid;
            vehiclePolicy.CreatedDate = DateTime.Now;
            vehiclePolicy.ModifiedDate = DateTime.Now;
            vehiclePolicy.CreatedBy = createdBy;
            //vehiclePolicy.ReleasedDate = null;

            _db.VehiclePolicies.Add(vehiclePolicy);
            return Save();
        }

        public PagedList<VehiclePolicy> GetVehiclePolicies(PaginationFilter paginationFilter) {
            return PagedList<VehiclePolicy>.ToPagedList(
                    _db.VehiclePolicies.OrderBy(vp => vp.CreatedDate).Where(vp => vp.IsReleased == true).ToList(),
                    paginationFilter.PageNumber,
                    paginationFilter.PageSize
            );
        }

        public PagedList<VehiclePolicy> GetVehiclePoliciesForAdmin(
            PaginationFilter paginationFilter, 
            VehiclePolicyFilter vehiclePolicyFilter
        ) {
            var x = _db.VehiclePolicies.OrderBy(vp => vp.CreatedDate);

            if (vehiclePolicyFilter.IsReleased == "true" || vehiclePolicyFilter.IsReleased == "false")
            {
                bool isReleased;

                if (vehiclePolicyFilter.IsReleased == "true")
                {
                    isReleased = true;
                } 
                else
                {
                    isReleased = false;
                }

                x = (IOrderedQueryable<VehiclePolicy>)x.Where(vp => vp.IsReleased == isReleased);
            }

            return PagedList<VehiclePolicy>.ToPagedList(
                x.ToList(),
                paginationFilter.PageNumber,
                paginationFilter.PageSize
            );
            //if (string.IsNullOrWhiteSpace(search))
            //{
            //    return PagedList<VehiclePolicy>.ToPagedList(
            //        _db.VehiclePolicies.OrderBy(vp => vp.CreatedDate).ToList(),
            //        paginationFilter.PageNumber,
            //        paginationFilter.PageSize
            //    );
            //}
            //else
            //{
            //    return PagedList<VehiclePolicy>.ToPagedList(
            //        _db.VehiclePolicies.OrderBy(vp => vp.CreatedDate).ToList(),
            //        paginationFilter.PageNumber,
            //        paginationFilter.PageSize
            //    );
            //}
        }

        public VehiclePolicy GetVehiclePolicy(string id)
        {
            try
            {
                Guid guid = Guid.Parse(id);
                var vehiclePolicy = _db.VehiclePolicies.FirstOrDefault(vp => vp.Id == guid && vp.IsReleased == true);
                return vehiclePolicy;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public VehiclePolicy GetVehiclePolicyForAdmin(string id)
        {
            try
            {
                Guid guid = Guid.Parse(id);
                var vehiclePolicy = _db.VehiclePolicies.FirstOrDefault(vp => vp.Id == guid);
                return vehiclePolicy;
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

        public bool UpdateVehiclePolicy(
            VehiclePolicyUpdateDto vehiclePolicyUpdateDto, 
            string id,
            string modifiedBy
        ) {
            VehiclePolicy vehiclePolicy = _db.VehiclePolicies.Where(vp => vp.Id == Guid.Parse(id)).FirstOrDefault();

            if (vehiclePolicy != null)
            {
                vehiclePolicy.Type = vehiclePolicyUpdateDto.Type;
                vehiclePolicy.Content = vehiclePolicyUpdateDto.Content;
                vehiclePolicy.PersonClaim = vehiclePolicyUpdateDto.PersonClaim;
                vehiclePolicy.VehicleClaim = vehiclePolicyUpdateDto.VehicleClaim;
                vehiclePolicy.AmountPaid = vehiclePolicyUpdateDto.AmountPaid;
                vehiclePolicy.IsReleased = vehiclePolicyUpdateDto.IsReleased;
                vehiclePolicy.ModifiedDate = DateTime.Now;
                vehiclePolicy.ModifiedBy = modifiedBy;

                if (vehiclePolicyUpdateDto.IsReleased == true && vehiclePolicy.IsReleased == false)
                {
                    vehiclePolicy.ReleasedDate = DateTime.Now;
                }

                _db.VehiclePolicies.Update(vehiclePolicy);
                return Save();
            } 
            else
            {
                return false;
            }
        }
    }
}
