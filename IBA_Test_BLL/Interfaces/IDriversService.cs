using IBA_Test_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBA_Test_BLL.Interfaces
{
    public interface IDriversService
    {
        Task Add(DriverBLL model);
        Task<IEnumerable<DriverBLL>> GetByDateAndSpeed(DateTime dt, float speed);
        Task<IEnumerable<DriverBLL>> GetByDateHigherAndLower(DateTime dt);
    }
}
