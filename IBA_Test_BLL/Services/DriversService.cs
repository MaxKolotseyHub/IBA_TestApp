using AutoMapper;
using CSharpFunctionalExtensions;
using IBA_Test_BLL.Interfaces;
using IBA_Test_BLL.Models;
using IBA_Test_DAL.Interfaces;
using IBA_Test_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBA_Test_BLL.Services
{
    internal class DriversService : IDriversService
    {
        private readonly IDriverRepository _repository;
        private readonly IMapper _mapper;

        public DriversService(IDriverRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Result> Add(DriverDTO model)
        {
            try
            {
                await _repository.Add(_mapper.Map<DriverDAL>(model));
                return Result.Success();
            }
            catch (OleDbException e)
            {
                return Result.Failure<IEnumerable<DriverDTO>>(e.Message);
            }
            catch (IOException e)
            {
                return Result.Failure<IEnumerable<DriverDTO>>(e.Message);
            }
        }

        public async Task<Result<IEnumerable<DriverDTO>>> GetByDateAndSpeed(DateTime dt, float speed)
        {
            try
            {
                var drivers = _mapper.Map<IEnumerable<DriverDTO>>(await _repository.GetByDateSpeed(dt, speed));
                return Result.Success(drivers);
            }
            catch (OleDbException e)
            {
                return Result.Failure<IEnumerable<DriverDTO>>(e.Message);
            }
            catch (IOException e)
            {
                return Result.Failure<IEnumerable<DriverDTO>>(e.Message);
            }
        }

        public async Task<Result<IEnumerable<DriverDTO>>> GetByDateHigherAndLower(DateTime dt)
        {
            try
            {
                var drivers = _mapper.Map<IEnumerable<DriverDTO>>(await _repository.GetByDate(dt));
                return Result.Success(drivers);
            }
            catch (OleDbException e)
            {
                return Result.Failure<IEnumerable<DriverDTO>>(e.Message);
            }catch(IOException e)
            {
                return Result.Failure<IEnumerable<DriverDTO>>(e.Message);
            }
        }
    }
}
