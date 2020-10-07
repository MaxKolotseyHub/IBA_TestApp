using AutoMapper;
using IBA_Test_BLL.Models;
using IBA_Test_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBA_Test_BLL.Helpers
{
    internal class Automapper
    {
        private static Mapper mapper;

        public static Mapper GetMapper()
        {
            if (mapper == null)
            {
                var conf = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DriverBLL, DriverDAL>().ReverseMap();
                });

                mapper = new Mapper(conf);
            }

            return mapper;
        }
    }
}
