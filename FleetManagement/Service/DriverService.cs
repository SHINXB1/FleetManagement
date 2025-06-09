using FleetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.Service
{
    public class DriverService
    {
        private readonly CarParkEntities _context = new CarParkEntities();

        public List<Drivers> GetAllDrivers()
        {
            return _context.Drivers.ToList();
        }

        public void AddDriver(Drivers driver)
        {
            _context.Drivers.Add(driver);
            _context.SaveChanges();
        }

        public void UpdateDriver(Drivers driver)
        {
            var existingDriver = _context.Drivers.Find(driver.Id);
            if (existingDriver != null)
            {
                _context.Entry(existingDriver).CurrentValues.SetValues(driver);
                _context.SaveChanges();
            }
        }

        public void DeleteDriver(int id)
        {
            var driver = _context.Drivers.Find(id);
            if (driver != null)
            {
                _context.Drivers.Remove(driver);
                _context.SaveChanges();
            }
        }
    }
}
