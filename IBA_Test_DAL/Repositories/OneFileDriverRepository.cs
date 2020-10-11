using IBA_Test_DAL.Data;
using IBA_Test_DAL.Interfaces;
using IBA_Test_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBA_Test_DAL.Repositories
{
    class OneFileDriverRepository : IDriverRepository
    {
        private readonly FileDb _db;

        public OneFileDriverRepository(FileDb db)
        {
            _db = db;
        }
        public async Task Add(DriverDAL model)
        {
            await _db.InsertAsync(model);
        }

        public async Task<IEnumerable<DriverDAL>> GetByDate(DateTime dt)
        {
            return await _db.SelectMinMaxByDate(dt);
        }

        public async Task<IEnumerable<DriverDAL>> GetByDateSpeed(DateTime dt, float speed)
        {
            return await _db.SelectByDateSpeed(dt,speed);
        }

    }
}
