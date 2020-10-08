using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBA_Test_DAL.Models
{
    public class DriverDAL
    {
        public string CarNumber { get; set; }
        public DateTime DateTime{ get; set; }
        public float Speed { get; set; }

        public DriverDAL(string carNumber, DateTime dateTime, float speed)
        {
            CarNumber = carNumber;
            DateTime = dateTime;
            Speed = speed;
        }
    }
}
