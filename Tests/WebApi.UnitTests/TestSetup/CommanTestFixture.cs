using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DBOperations;

namespace WebApi.UnitTests.TestSetup
{
    public class CommanTestFixture
    {
        public BookStoreDbContext BookStoreDbContext { get; set; }
        public IMapper Mapper { get; set; }
        public CommanTestFixture()
        {

        }
    }
}
