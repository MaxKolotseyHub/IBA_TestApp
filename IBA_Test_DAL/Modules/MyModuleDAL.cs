using IBA_Test_DAL.Interfaces;
using IBA_Test_DAL.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBA_Test_DAL.Modules
{
    public class MyModuleDAL : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IDriverRepository>().To<OneFileDriverRepository>();
        }
    }
}
