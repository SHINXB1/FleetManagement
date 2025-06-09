using FleetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.Service
{
    public class VehicleService
    {
        private CarParkEntities _context = new CarParkEntities();

        public List<Vehicles> GetAllVehicles()
        {
            return _context.Vehicles.ToList();
        }

        public void AddVehicle(Vehicles vehicle)
        {
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
        }

        public void UpdateVehicle(Vehicles vehicle)
        {
            var existingVehicle = _context.Vehicles.Find(vehicle.Id);
            if (existingVehicle != null)
            {
                _context.Entry(existingVehicle).CurrentValues.SetValues(vehicle);
                _context.SaveChanges();
            }
        }

        public void DeleteVehicle(int id)
        {
            var vehicle = _context.Vehicles.Find(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                _context.SaveChanges();
            }
        }
    }
}
