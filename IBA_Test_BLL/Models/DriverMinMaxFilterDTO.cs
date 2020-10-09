using FluentValidation.Attributes;
using IBA_Test_BLL.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBA_Test_BLL.Models
{
    [Validator(typeof(DriverMinMaxFilterDTOValidator))]
    public class DriverMinMaxFilterDTO
    {
        public DateTime dt { get; set; }
    }
}
