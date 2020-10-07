using AutoMapper;
using IBA_Test_BLL.Helpers;
using IBA_Test_BLL.Interfaces;
using IBA_Test_BLL.Services;
using IBA_Test_DAL.Interfaces;
using IBA_Test_DAL.Modules;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBA_Test_BLL.Modules
{
    public class MyModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Load(new MyModuleDAL());
            Kernel.Bind<IDriversService>().To<DriversService>();
            Kernel.Bind<IMapper>().ToMethod(x => Automapper.GetMapper());
        }
    }
}
