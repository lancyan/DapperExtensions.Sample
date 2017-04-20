using Test.DAL.Base;
using Test.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.DAL
{
    public class GoodsDAL : BaseDAL<Goods>
  {
        public GoodsDAL(string connGroupName = "Test1")
            : base(connGroupName)
        {

        }
    }
}
