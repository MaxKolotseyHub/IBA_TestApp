using FluentValidation;
using FluentValidation.Attributes;
using IBA_Test_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBA_Test_BLL.Validators
{
    public class DriverDTOValidator: AbstractValidator<DriverDTO>
    {
        public DriverDTOValidator()
        {
            RuleFor(x => x.Speed).NotNull().GreaterThan(0).WithMessage("Speed should be greater than 0");
            RuleFor(x => x.DateTime).NotNull().WithMessage("Date shouldnt be null");
            RuleFor(x => x.CarNumber).NotNull().MinimumLength(8).WithMessage("Number is invalid");
        }
    }
}
