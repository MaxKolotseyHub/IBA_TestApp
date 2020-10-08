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
    public class DriverRepository : IDriverRepository
    {
        private readonly string _path;
        private readonly string _fileName;
        private readonly string _fullPath;
        public DriverRepository()
        {
            _fileName = $"{DateTime.Now.ToString("ddMMyyyy")}.txt";
            _path = Path.Combine(Directory.GetCurrentDirectory(),"Storage");
            _fullPath = Path.Combine(_path, _fileName);
        }

        public async Task Add(DriverDAL model)
        {
            await CheckCurrentStorageExistsAsync();
            await InsertAsync(model);
        }

        public Task<IEnumerable<DriverDAL>> GetByDateSpeed(DateTime dt, float speed)
        {
            string fileName = $"{dt.ToString("ddMMyyyy")}.txt";
            string storagePath = CheckStorageExists(dt).Replace(fileName,"") ;
            return SelectByDateSpeed(storagePath, fileName, speed);
        }

        public Task<IEnumerable<DriverDAL>> GetByDate(DateTime dt)
        {
            string fileName = $"{dt.ToString("ddMMyyyy")}.txt";
            string storagePath = CheckStorageExists(dt).Replace(fileName, "");
            return SelectMinMaxByDate(storagePath, fileName);
        }

        private async Task CheckCurrentStorageExistsAsync()
        {
            if (!File.Exists(_fullPath))
                await CreateStorageAsync();
        }

        private async Task CreateStorageAsync()
        {
            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);
            using (StreamWriter sw = new StreamWriter(_fullPath, true, Encoding.GetEncoding(1251)))
            {
                await sw.WriteLineAsync("\"Date\", \"Speed\", \"Number\"");
            }
        }

        private string CheckStorageExists(DateTime dt)
        {
            return Directory.GetFiles(_path).FirstOrDefault(x=> x.Contains(dt.ToString("ddMMyyyy")));
        }

        private async Task InsertAsync(DriverDAL model)
        {
            using (OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _path + @"\;Extended Properties=text"))
            {
                string commandStr = $"INSERT INTO [{_fileName}] ([Date],[Speed],[Number]) values (@date, @speed, @number)";
                OleDbCommand comand1 = new OleDbCommand(commandStr, connection);
                comand1.Parameters.AddWithValue("@date", model.DateTime);
                comand1.Parameters.AddWithValue("@speed", model.Speed);
                comand1.Parameters.AddWithValue("@number", model.CarNumber);
                await connection.OpenAsync();
                await comand1.ExecuteNonQueryAsync();
                connection.Close();
            }
        }

        private async Task<IEnumerable<DriverDAL>> SelectByDateSpeed(string path, string fileName, float speed)
        {
            List<DriverDAL> drivers = new List<DriverDAL>();

            using (OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=text"))
            {
                string commandStr = $"SELECT * FROM [{fileName}] WHERE Speed > '{speed}'";
                OleDbCommand command = new OleDbCommand(commandStr, connection);
                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var model = new DriverDAL(reader[2].ToString(), DateTime.Parse(reader[0].ToString()), float.Parse(reader[1].ToString()));
                    drivers.Add(model);
                }
                connection.Close();
            }

            return drivers;
        }
        private async Task<IEnumerable<DriverDAL>> SelectMinMaxByDate(string path, string fileName)
        {
            List<DriverDAL> drivers = new List<DriverDAL>();

            using (OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=text"))
            {
                string commandStr = $"SELECT * FROM [{fileName}] WHERE Speed = (Select MIN(Speed) from [{fileName}] ) OR Speed = (Select MAX(Speed) from [{fileName}] )";
                OleDbCommand command = new OleDbCommand(commandStr, connection);
                await connection.OpenAsync();
                var reader = command.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    var model = new DriverDAL(reader[2].ToString(), DateTime.Parse(reader[0].ToString()), float.Parse(reader[1].ToString()));
                    drivers.Add(model);
                }
                connection.Close();
            }

            return drivers;
        }



    }
}
