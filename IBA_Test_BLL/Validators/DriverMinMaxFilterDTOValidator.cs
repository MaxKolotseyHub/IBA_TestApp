using FluentValidation;
using IBA_Test_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBA_Test_BLL.Validators
{
    public class DriverMinMaxFilterDTOValidator: AbstractValidator<DriverMinMaxFilterDTO>
    {
        public DriverMinMaxFilterDTOValidator()
        {
            RuleFor(x => x.dt).NotNull().WithMessage("Date shouldnt be null");
        }
    }
}
