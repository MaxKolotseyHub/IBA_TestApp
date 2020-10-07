using IBA_Test_BLL.Interfaces;
using IBA_Test_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBA_Test_BLL.Services
{
    internal class DriversService : IDriversService
    {
        public Task Add(DriverBLL model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DriverBLL>> GetByDateAndSpeed(DateTime dt, float speed)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DriverBLL>> GetByDateHigherAndLower(DateTime dt)
        {
            throw new NotImplementedException();
        }
    }
}
