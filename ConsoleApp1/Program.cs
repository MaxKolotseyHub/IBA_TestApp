using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Threading.Tasks;
using IBA_Test_DAL.Repositories;

namespace ConsoleApp1
{
    class Program
    {
        private static string _path = Path.Combine(Directory.GetCurrentDirectory(), "storage.txt");
        static async Task Main(string[] args)
        {
            DriverRepository repository = new DriverRepository();
            await repository.Add(new IBA_Test_DAL.Models.DriverDAL("6141 HE-5", DateTime.Now, 78.9f));
            await repository.Add(new IBA_Test_DAL.Models.DriverDAL("9857 KM-7", DateTime.Now, 98.3f));
            await repository.Add(new IBA_Test_DAL.Models.DriverDAL("9857 KM-7", DateTime.Now.AddDays(12), 98.3f));

            InsertWithSQL(new Model("6141 HE-5", DateTime.Now, 78.9f));
            InsertWithSQL(new Model("9857 KM-7", DateTime.Now, 98.3f));
            InsertWithSQL(new Model("9857 KM-7", DateTime.Now.AddDays(12), 98.3f));
            InsertWithSQL(new Model("9857 KM-7", DateTime.Now.AddDays(-10), 98.3f));
            InsertWithSQL(new Model("1245 KB-3", DateTime.Now, 85.0f));
            GetWithSQL();
            var model = GetInfoFromStorage("1245 KB-3");
            Console.WriteLine(model.ToString());
            Console.ReadKey();
        }


        private static void AddInfoToStorage(Model model)
        {
            if (!File.Exists(_path))
            {
                using (StreamWriter sw = new StreamWriter(_path, true, Encoding.GetEncoding(1251)))
                {
                    sw.WriteLine("\"Дата\", \"Скорость\", \"Номер\"");
                }
            }
            using (StreamWriter sw = File.AppendText(_path))
            {
                sw.WriteLine(model.ToString());
            }
        }

        private static void GetWithSQL()
        {
            //Определяем подключение
            OleDbConnection StrCon = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directory.GetCurrentDirectory() + ";Extended Properties=text");
            //Строка для выборки данных
            string Select1 = "SELECT * FROM [storage.txt] WHERE Speed > '50'AND Date >= #08/10/2020#";
            //Создание объекта Command
            OleDbCommand comand1 = new OleDbCommand(Select1, StrCon);
            //Определяем объект Adapter для взаимодействия с источником данных
            OleDbDataAdapter adapter1 = new OleDbDataAdapter(comand1);
            //Определяем объект DataSet
            DataSet AllTables = new DataSet();
            //Открываем подключение
            StrCon.Open();
            //Заполняем DataSet таблицей из источника данных
            var reader = comand1.ExecuteReader();
            while (reader.Read())
            {
                var model = new Model(reader[2].ToString(), DateTime.Parse(reader[0].ToString()), float.Parse(reader[1].ToString()));
            }
            adapter1.Fill(AllTables);
            //Заполняем обект datagridview для отображения данных на форме
            StrCon.Close();
        }
        private static void InsertWithSQL(Model model)
        {
            if (!File.Exists(_path))
            {
                using (StreamWriter sw = new StreamWriter(_path, true, Encoding.GetEncoding(1251)))
                {
                    sw.WriteLine("\"Date\", \"Speed\", \"Number\"");
                }
            }
            //Определяем подключение
            OleDbConnection StrCon = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directory.GetCurrentDirectory() + ";Extended Properties=text");
            //Строка для выборки данных
            string Select1 = $"INSERT INTO [storage.txt] ([Date],[Speed],[Number]) values (@date, @speed, @number)";
            //Создание объекта Command 
            OleDbCommand comand1 = new OleDbCommand(Select1, StrCon);
            comand1.Parameters.AddWithValue("@date", model.DTime);
            comand1.Parameters.AddWithValue("@speed", model.Speed);
            comand1.Parameters.AddWithValue("@number", model.Number);
            //Определяем объект Adapter для взаимодействия с источником данных
            StrCon.Open();
            comand1.ExecuteNonQuery();
            //Заполняем обект datagridview для отображения данных на форме
            StrCon.Close();
        }

        private static async Task<Model> GetInfoFromStorage(string number)
        {
            Model model = null;

            using (StreamReader sr = new StreamReader(_path, Encoding.GetEncoding(1251)))
            {
                string line = "";
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    if (line.Contains(number))
                    {
                        string[] array = line.Split('\t');
                        model = new Model(array[0], DateTime.Parse(array[1]), float.Parse(array[2]));
                        return model;
                    }
                }
            }

            return model;
        }
    }

    class Model
    {
        public string Number { get; set; }
        public DateTime DTime { get; set; }
        public float Speed { get; set; }

        public Model(string number, DateTime dTime, float speed)
        {
            Number = number;
            DTime = dTime;
            Speed = speed;
        }

        public override string ToString()
        {
            return $"{DTime.ToString("dd.MM.yyyy HH:mm:ss")},{Speed},'{Number}'";
        }
    }
}

