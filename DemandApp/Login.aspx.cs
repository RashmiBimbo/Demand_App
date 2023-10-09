using System;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Infrastructure;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemandApp
{
    public partial class LoginPage : System.Web.UI.Page
    {
        private Connect connect = new Connect();

        public DataTable LstCmpnyDataSrc
        {
            get
            {
                if (Session["LstCmpnyDataSrc"] == null)
                {
                    // Session["LstCmpnyDataSrc"] = GetLstCmpnyDataSrc();
                }
                return (DataTable)Session["LstCmpnyDataSrc"];
            }
            set
            {
                LstCmpnyDataSrc = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            connect.AuthenticatCon();
            if (!Page.IsPostBack)
            {
                Session.Abandon();
                Session.RemoveAll();
                LstCmpny.DataBind();
                LstLocn.DataBind();
                loadcompanybind();
            }
            //connect.connect(connStr);
        }

        private DataTable GetDataSrc(string query)
        {
            DataTable dt;
            try
            {
                dt = connect.SelQuery(query, connect.ConnBBIDemand);
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

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string cmpny = LstCmpny.SelectedValue;
            string locn = LstLocn.SelectedValue;
            string UsrId = TxtUserId.Text;
            string pswrd = TxtPswrd.Text;
            try
            {
                //Session["Master_File"] = "~/Normal/Normal_.master";
                if (LstCmpny.SelectedValue == "" || LstCmpny.SelectedValue == null || LstCmpny.SelectedValue == "0")
                {
                    this.ShowPopUpMsg("Please select Company Name.");
                    LstCmpny.Focus();
                    return;
                }
                if (TxtUserId.Text == "" || TxtUserId.Text == null)
                {
                    this.ShowPopUpMsg("Please Enter User Id.");
                    TxtUserId.Focus();
                    return;
                }
                if (TxtPswrd.Text == "" && TxtPswrd.Text == null)
                {
                    this.ShowPopUpMsg("Please Enter Password.");
                    TxtPswrd.Focus();
                    return;
                }

                if (Request.Browser.Cookies)
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["UserName"].Value = UsrId;
                }

                //if (chkRemMe.Checked)
                //{
                Response.Cookies["PassWord"].Expires = DateTime.Now.AddDays(30);
                Response.Cookies["PassWord"].Value = pswrd;
                // }

                string mSqlQuery = "EXEC [dbo].[VALIDATELOGIN] '" + cmpny + "', '" + UsrId + "', '" + pswrd + "'";

                DataTable dt = connect.SelQuery(mSqlQuery, connect.ConnBBIDemand);

                if (dt != null && dt.Rows.Count > 0)
                {
                    //if (dt.Rows[0][3].ToString().ToUpper() != TxtPswrd.Text.ToUpper())
                    //{
                    //    Label1.Text = "Kindly Check Your ID And Password";
                    //    return;
                    //}
                    Session["UserId"] = dt.Rows[0][2];
                    Session["UserName"] = dt.Rows[0][6];
                    Session["User_type"] = dt.Rows[0][4];
                    Session["RoleId"] = dt.Rows[0][4];
                    Session["Designation"] = dt.Rows[0][3];
                    Session["Department"] = dt.Rows[0][4];
                    Session["Main_Company_Id"] = dt.Rows[0][2];
                    Session["CompanyId"] = dt.Rows[0][1];
                    Session["Location"] = dt.Rows[0][7];
                    Session["Sub_Location"] = dt.Rows[0][8];
                    Session["Email_id"] = dt.Rows[0][9] ?? "";
                    Session["Active"] = dt.Rows[0][10];
                    Session["Flag"] = dt.Rows[0][11];
                    Session["Permission"] = dt.Rows[0][12];
                    Response.Redirect("~/Home.aspx");
                    //this.Session["Master_File"] = "~/Normal/Normal_.master";
                }
                else
                    ShowPopUpMsg("Kindly Check Your ID And Password");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", ex.Message, false);
            }
        }

        protected void BtnClick_Click(object sender, EventArgs e)
        {
        }


        //protected void LstLocn_DataBinding(object sender, EventArgs e)
        //{
        //    string query = "Select [Location] from V_LMaster where Location is not null and TRIM(Location) !=''; ORDER BY [Location]";
        //    LstLocn.DataSource = GetDataSrc(query);
        //    LstLocn.DataTextField = "Location";
        //    LstLocn.DataValueField = "Location";
        //}

        protected void LstCmpny_DataBinding(object sender, EventArgs e)
        {
            string query = "Select DataAreaID, [Name] from V_COMPANY_MASTER where DataAreaID not in ('DAT') ORDER BY [Name]";
            LstCmpny.DataSource = GetDataSrc(query);
            LstCmpny.DataTextField = "Name";
            LstCmpny.DataValueField = "DataAreaID";
        }

        protected void loadcompanybind()
        {
            string query = "Select DataAreaID, [Name] from V_COMPANY_MASTER where DataAreaID not in ('DAT') ORDER BY [Name]";

            DataTable dt = GetDataSrc(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                LstCmpny.Items.Add(new ListItem("--SELECT-- ", "0"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    LstCmpny.Items.Add(new ListItem(dt.Rows[i]["Name"].ToString().ToUpper(), dt.Rows[i]["DataAreaID"].ToString().ToUpper()));
                }
            }
        }

        private void ShowPopUpMsg(string strMessage)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + strMessage + "');", true);
        }

    }
}