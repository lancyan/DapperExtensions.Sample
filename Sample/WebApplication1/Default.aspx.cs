using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Test.BLL;

namespace WebApplication1
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //直接调用DapperExtensions框架
            UsersBLL bll = new UsersBLL();

            var users = bll.GetUsers();


            StringBuilder sb = new StringBuilder();

            foreach (var user in users)
            {
                sb.Append(user.UserName + "  " + user.NickName);
            }
            Response.Write(sb.ToString());


        }


     
    }
}