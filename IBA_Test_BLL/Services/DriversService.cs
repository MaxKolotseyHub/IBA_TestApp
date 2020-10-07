using AutoMapper;
using IBA_Test_BLL.Interfaces;
using IBA_Test_BLL.Models;
using IBA_Test_DAL.Interfaces;
using IBA_Test_DAL.Models;
using System;
using System.Collections.Generic;
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
        public Task Add(DriverBLL model)
        {
            return _repository.Add(_mapper.Map<DriverDAL>(model));
        }

        public async Task<IEnumerable<DriverBLL>> GetByDateAndSpeed(DateTime dt, float speed)
        {
           return _mapper.Map<IEnumerable<DriverBLL>>(await _repository.GetAll()).Where(x => x.DateTime.Date == dt.Date && x.Speed > speed);
        }

        public async Task<IEnumerable<DriverBLL>> GetByDateHigherAndLower(DateTime dt)
        {
           return _mapper.Map<IEnumerable<DriverBLL>>(await _repository.GetAll()).Where(x => x.DateTime.Date == dt.Date);
        }
    }
}
