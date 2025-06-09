using FleetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FleetManagement.Service
{
    public class RouteService
    {
        private readonly CarParkEntities _context = new CarParkEntities();

        public List<Routes> GetAllRoutes()
        {
            return _context.Routes.ToList();
        }

        public List<Routes> GetFilteredRoutes(DateTime? date, int? vehicleId, int? driverId)
        {
            return _context.Routes
                .Where(r => (!date.HasValue || r.StartDate.Date == date.Value.Date) &&
                            (!vehicleId.HasValue || r.VehicleId == vehicleId) &&
                            (!driverId.HasValue || r.DriverId == driverId))
                .ToList();
        }

        public void AddRoute(Routes route)
        {
            _context.Routes.Add(route);

            // Обновление статуса транспорта
            var vehicle = _context.Vehicles.Find(route.VehicleId);
            if (vehicle != null)
            {
                vehicle.Status = "In Transit";
                _context.Entry(vehicle).CurrentValues.SetValues(vehicle);
            }

            _context.SaveChanges();
        }

        public void UpdateRoute(Routes route)
        {
            var existingRoute = _context.Routes.Find(route.Id);
            if (existingRoute != null)
            {
                _context.Entry(existingRoute).CurrentValues.SetValues(route);
                _context.SaveChanges();
            }
        }

        public void DeleteRoute(int id)
        {
            var route = _context.Routes.Find(id);
            if (route != null)
            {
                _context.Routes.Remove(route);

                // Обновление статуса транспорта на "Available" после удаления маршрута
                var vehicle = _context.Vehicles.Find(route.VehicleId);
                if (vehicle != null)
                {
                    vehicle.Status = "Available";
                    _context.Entry(vehicle).CurrentValues.SetValues(vehicle);
                }

                _context.SaveChanges();
            }
        }
    }
}
