using IBA_Test_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBA_Test_DAL.Data
{
    internal class FileDb 
    {
        private readonly string _fileName;
        private readonly string _directoryPath;
        private readonly string _fullPath;
        public FileDb()
        {
            _fileName = "dbFile.txt";
            _directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Storage");
            _fullPath = Path.Combine(_directoryPath, _fileName);

        }
        private bool CheckDBExists()
        {
            return File.Exists(_fullPath);
        }

        private async Task CheckDBExistsAndCreateAsync()
        {
            if (!Directory.Exists(_directoryPath))
                Directory.CreateDirectory(_directoryPath);

            if (!File.Exists(_fullPath))
                await CreateTableAsync();
        }

        private async Task CreateTableAsync()
        {
            using (var connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _directoryPath + @";Extended Properties=text"))
            {
                string commandStr = $"CREATE TABLE [{_fileName}] ([Date] DateTime,[Speed] DOUBLE,[Number] TEXT)";
                await connection.OpenAsync();
                OleDbCommand command = new OleDbCommand(commandStr, connection);
                await command.ExecuteNonQueryAsync();
            }
        }

        internal async Task InsertAsync(DriverDAL model)
        {
            await CheckDBExistsAndCreateAsync();

            using (var connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _directoryPath + @";Extended Properties=text"))
            {
                string commandStr = $"INSERT INTO [{_fileName}] ([Date],[Speed],[Number]) values (@date, @speed, @number)";
                await connection.OpenAsync();
                OleDbCommand command = new OleDbCommand(commandStr, connection);
                command.Parameters.AddWithValue("@date", model.DateTime).OleDbType = OleDbType.Date;
                command.Parameters.AddWithValue("@speed", model.Speed).OleDbType = OleDbType.Double;
                command.Parameters.AddWithValue("@number", model.CarNumber).OleDbType = OleDbType.LongVarChar;
                await command.ExecuteNonQueryAsync();
            }
        }

        internal async Task<IEnumerable<DriverDAL>> SelectByDateSpeed(DateTime dt, float speed)
        {
            List<DriverDAL> drivers = new List<DriverDAL>();
            if (!CheckDBExists())
                return drivers;

            using (var connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _directoryPath + @";Extended Properties=text"))
            {
                string commandStr = $"SELECT * FROM [{_fileName}] WHERE Speed > @speed AND Date BETWEEN #{dt.ToString("MM/dd/yyyy")} 00:00:00# AND #{dt.ToString("MM/dd/yyyy")} 23:59:59#";
                await connection.OpenAsync();
                OleDbCommand command = new OleDbCommand(commandStr, connection);
                command.Parameters.AddWithValue("@speed", speed).OleDbType = OleDbType.Double;
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var model = new DriverDAL(reader[2].ToString(), DateTime.Parse(reader[0].ToString()), float.Parse(reader[1].ToString()));
                    drivers.Add(model);
                }
            }
            return drivers;
        }

        internal async Task<IEnumerable<DriverDAL>> SelectMinMaxByDate(DateTime dt)
        {
            List<DriverDAL> drivers = new List<DriverDAL>();
            if (!CheckDBExists())
                return drivers;

            using (var connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _directoryPath + @";Extended Properties=text"))
            {
                string commandStr = $"SELECT * FROM [{_fileName}] WHERE Speed = (Select MIN(Speed) from [{_fileName}]  where  Date BETWEEN #{dt.ToString("MM/dd/yyyy")} 00:00:00# AND #{dt.ToString("MM/dd/yyyy")} 23:59:59#)  OR Speed = (Select MAX(Speed) from [{_fileName}] where  Date BETWEEN #{dt.ToString("MM/dd/yyyy")} 00:00:00# AND #{dt.ToString("MM/dd/yyyy")} 23:59:59#)";
                await connection.OpenAsync();
                OleDbCommand command = new OleDbCommand(commandStr, connection);
                var reader = command.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    var model = new DriverDAL(reader[2].ToString(), DateTime.Parse(reader[0].ToString()), float.Parse(reader[1].ToString()));
                    drivers.Add(model);
                }
            }

            return drivers.OrderBy(x => x.Speed);
        }
    }
}
