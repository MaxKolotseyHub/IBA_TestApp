using CSharpFunctionalExtensions;
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
        Task<Result> Add(DriverDTO model);
        Task<Result<IEnumerable<DriverDTO>>> GetByDateAndSpeed(DateTime dt, float speed);
        Task<Result<IEnumerable<DriverDTO>>> GetByDateHigherAndLower(DateTime dt);
    }
}
