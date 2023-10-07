using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using DemandApp;

namespace DemandApp
{
    public partial class Registration : System.Web.UI.Page
    {
        private Connect connect;

        private Regex MailReg;

        public Registration()
        {
            connect = new Connect();
            connect.AuthenticatCon();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RBCust.Checked = true;
                RBCust_CheckedChanged(null, null);
                loadcompanybind();
            }
        }

        protected void ImgBtnSbmt_Click(object sender, ImageClickEventArgs e)
        {
            string loginType = RBCust.Checked ? "C" : "S";
            string RoleID = RBCust.Checked ? "1" : "2";
            string RoleName = RBCust.Checked ? "Customer Level" : "Salesperson Level";
            string LoginId = Convert.ToString(Session["UserId"]);
            string userid = TxtUserID.Text;
            string name = Txtname.Text;
            string email = TxtMail.Text;
            string phoeno = TxtPhn.Text;
            string add = txtadd.Text;
            string GSTno = txtGstNo.Text;


            //string name = DdlUser.Text; //TxtName.Text.Trim();
            string ipadd = HttpContext.Current.Request.UserHostAddress;// TxtId.Text.Trim();
            string mail = TxtMail.Text.Trim();
            string phn = TxtPhn.Text.Trim();

            //if (IdIsValid(id))
            //{
                if (IsValidEmail(mail))
                {
                    if (IsValidPhone(phn))
                    {
                        string query = "insert into MST_LOGINDETAILS(COMPANYID,USERID,PASSWORD,ROLEID,ROLENAME,NAME,ADDRESS,GSTNO,LOGINTYPE,CREATEDDATE,CREATEDBY,MODIFIEDDATE,MODIFIEDBY,IPADD) VALUES ('" + DDlcompany.SelectedValue + "', '" + userid + "', '"+ userid + "'@123, '" + RoleID + "', '" + RoleName + "','"+name+"','"+add+"','"+ GSTno+ "','"+loginType+"','"+DateTime.Now+"','Admin',null,null,'"+ipadd+"')";
                        try
                        {
                            connect.InsertValues(query, connect.ConnTestDemandApp);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            //}
            //else { }
        }
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        bool IsValidPhone(string phone)
        {
            bool valid;
            Regex pattern = new Regex(@"^(?:\(?)((?<AreaCode>\d{3})(?:[\).\s]?))(?<Prefix>\d{3})(?:[-\.\s]?)(?<Suffix>\d{4})(?!\d)", RegexOptions.IgnorePatternWhitespace);
            valid = pattern.IsMatch(phone);
            return valid;
        }
        private bool IdIsValid(string id)
        {
            bool result;
            //DataTable dt  = null;
            try
            {
                string query = "SELECT [Customer_Id] FROM [dbo].[SD_Customer_Master] WHERE [Customer_Id] = '" + id + "'";
                //dt = connect.SelQuery(query, connect.ConnTestDemandApp);
                result = connect.ExecScalar(query, connect.ConnSicon1_2);
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                //if (dt != null)
                //    dt.Dispose();
            }
        }

        protected void RBSales_CheckedChanged(object sender, EventArgs e)
        {
            if (RBSales.Checked)
            {
                RBCust.Checked = false;
                string proc = "DemandApp_Reg_GetSalesPerson";
                PopulateDdl(proc);
            }
        }

        protected void RBCust_CheckedChanged(object sender, EventArgs e)
        {
            if (RBCust.Checked)
            {
                RBSales.Checked = false;
                string proc = "DemandApp_Reg_GetCustomers";
                PopulateDdl(proc);
            }
        }

        private void PopulateDdl(string proc)
        {
            DataTable dt = connect.GetDataProc(proc, connect.ConnSicon1_2);
            DdlUser.DataSource = dt;
            DdlUser.DataValueField = RBCust.Checked ? "Customer_Id" : "Paycode";
            DdlUser.DataTextField = RBCust.Checked ? "Customer_Name" : "Name";
            DdlUser.DataBind();
        }
        protected void loadcompanybind()
        {
            string query = "Select DataAreaID, [Name] from V_COMPANY_MASTER where DataAreaID not in ('DAT') ORDER BY [Name]";

            DataTable dt = GetDataSrc(query);

            if (dt.Rows.Count > 0)
            {
                DDlcompany.Items.Add(new ListItem("--SELECT-- ", "0"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DDlcompany.Items.Add(new ListItem(dt.Rows[i]["Name"].ToString().ToUpper(), dt.Rows[i]["DataAreaID"].ToString().ToUpper()));
                }

            }
        }

        private DataTable GetDataSrc(string query)
        {
            DataTable dt;
            try
            {
                dt = connect.SelQuery(query, connect.ConnTestBBISICON);
                if (dt != null && dt.Rows.Count > 0)
                    return dt;
                return null;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", ex.Message, false);
                return null;
            }
        }

        protected void loginid_CheckedChanged(Object sender, EventArgs e)
        {
            emptyform();


        }

        protected void emptyform()
        {
            TxtUserID.Text = "";
            Txtname.Text = "";
            TxtMail.Text = "";
            TxtPhn.Text = "";
            txtadd.Text = "";
            txtGstNo.Text = "";
        }
        protected void TxtUserID_TextChanged(object sender, EventArgs e)
        {
            string loginType= RBCust.Checked? "C" : "S";

            string userid=TxtUserID.Text;
            string msgstr = "Customer Id";
            string sqlquery = "select  Customer_Id userid ,Customer_Name name,Customer_Billing_Address adress,Depot_id Depotid,Sub_Company_ID CompanyID,GSTIN from V_Customer_Master where Customer_Id='" + userid + "' and Sub_Company_ID='" + DDlcompany.SelectedValue + "'";
            if (loginType=="S")
            {
                msgstr = "Paycode";
                sqlquery = "Select Paycode userid,  name ,'' adress,EmpDepot Depotid,EmploymentLegalEntityId CompanyID,''GSTIN  from V_Employees where paycode='"+userid+"' and EmploymentLegalEntityId='"+DDlcompany.SelectedValue+"'";
            }

            DataTable dt = new DataTable();
            dt=GetDataSrc(sqlquery);
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    Txtname.Text = Convert.ToString(dt.Rows[0]["name"]);
                    txtadd.Text = Convert.ToString(dt.Rows[0]["adress"]);
                    txtGstNo.Text = Convert.ToString(dt.Rows[0]["GSTIN"]);
                    //Txtname.Text = Convert.ToString(dt.Rows[0]["name"]);
                    //Txtname.Text = Convert.ToString(dt.Rows[0]["name"]);
                    //Txtname.Text = Convert.ToString(dt.Rows[0]["name"]);
                }
            }
            else {
                //lblmsg.Text= msgstr + " is not Exist ";
                emptyform();

            }

        }
    }
}