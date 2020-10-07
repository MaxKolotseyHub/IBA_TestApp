using IBA_Test_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBA_Test_DAL.Interfaces
{
    public interface IDriverRepository
    {
        Task Add(DriverDAL model);
        Task<IEnumerable<DriverDAL>> GetAllByDateAndSpeed(DriverDAL model);
        Task<IEnumerable<DriverDAL>> GetHigherAndLower(DriverDAL model);
    }
}
