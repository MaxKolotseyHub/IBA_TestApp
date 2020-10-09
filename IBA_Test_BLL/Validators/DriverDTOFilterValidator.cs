using FluentValidation;
using IBA_Test_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBA_Test_BLL.Validators
{
    public class DriverDTOFilterValidator : AbstractValidator<DriverFilterDTO>
    {
        public DriverDTOFilterValidator()
        {
            RuleFor(x => x.Speed).NotNull().WithMessage("Speed shouldnt be null");
            RuleFor(x => x.DateTime).NotNull().WithMessage("Date shouldnt be null");
        }
    }
}
