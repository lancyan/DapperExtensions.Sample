using Test.DAL.Base;
using Test.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.DAL
{
    public class BillsDAL : BaseDAL<Bills>
   {
        public BillsDAL(string connGroupName = "Test1")
            : base(connGroupName)
        {

        }
    }
}
