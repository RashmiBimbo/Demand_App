using System;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Infrastructure;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;
using System.Xml.Linq;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Runtime.InteropServices.ComTypes;

namespace DemandApp
{
    public partial class home : System.Web.UI.Page
    {
        private Connect connect = new Connect();

        protected void Page_Load(object sender, EventArgs e)
        {
            connect.AuthenticatCon();
            if (!Page.IsPostBack)
            {
                try
                {


                    if (Session["login_id"] == null && Convert.ToString(Session["login_id"]) == "")
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                    DateTime now = DateTime.Now;
                    var sdatmon =  new DateTime(now.Year, now.Month, 1);
                    var edatmon = sdatmon.AddMonths(1).AddDays(-1);

                    var sdate = new DateTime(now.Year, now.Month, 1);
               var  edate = sdatmon.AddMonths(1).AddDays(-1);

                    

                //    lblwelcome.Text = Session.Item("Login_Name") ' + ", " + Session.Item("Designation")
                //'lblwelcome.Text = lblwelcome.Text + ", " + Session.Item("Location")
                //Label1.Text = Session.Item("MacAddress")

                //t_paycode = UCase(Trim(Session.Item("Login_Id")))

                //sdate1 = Format(CDate(Date.Now), "dd-MMM-yyyy")
                //smonyr = Format(CDate(Date.Now), "dd-MM")



                }
                catch (Exception)
                {

                    throw;
                }
            }
            //connect.connect(connStr);
        }
    }
}