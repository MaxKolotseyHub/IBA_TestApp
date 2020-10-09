using FluentValidation.Attributes;
using IBA_Test_BLL.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBA_Test_BLL.Models
{
    [Validator(typeof(DriverDTOValidator))]
    public class DriverDTO
    {
        public string CarNumber { get; set; }
        public DateTime DateTime { get; set; }
        public float Speed { get; set; }
    }
}
