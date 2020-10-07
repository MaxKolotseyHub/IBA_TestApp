using IBA_Test_DAL.Interfaces;
using IBA_Test_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBA_Test_DAL.Repositories
{
    internal class DriverRepository : IDriverRepository
    {
        public Task Add(DriverDAL model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DriverDAL>> GetAllByDateAndSpeed(DriverDAL model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DriverDAL>> GetHigherAndLower(DriverDAL model)
        {
            throw new NotImplementedException();
        }
    }
}
