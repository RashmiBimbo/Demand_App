using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;

namespace DemandApp
{
    public partial class PaymentTransaction : System.Web.UI.Page
    {
        private Connect connect;

        public PaymentTransaction()
        {
            connect = new Connect();
            connect.AuthenticatCon();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["CompanyId"] != null && Convert.ToString(Session["CompanyId"]) != "")
                {
                    string RoleId = Convert.ToString(Session["RoleId"]);

                    if (!(RoleId == "4" || RoleId == "3" || RoleId == "2"))
                        Response.Redirect("./Home.aspx");
                }
                else
                {
                    Response.Redirect("./Login.aspx");
                }
            }
        }

        private void PopulateDdl(string proc)
        {
            DataTable dt = connect.GetDataProc(proc, connect.ConnSicon1_2);
            //DdlUser.DataSource = dt;
            //DdlUser.DataValueField = RBCust.Checked ? "Customer_Id" : "Paycode";
            //DdlUser.DataTextField = RBCust.Checked ? "Customer_Name" : "Name";
            //DdlUser.DataBind();
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

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            {
                MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value);
                int roleId = Convert.ToInt16(Session["RoleId"]);
                int i;
                if (roleId == 3 || roleId== 4 || roleId == 6)
                {
                    loadSalesPersonlist();
                    SP_DIV.Visible = true;
                }
                else
                {
                    SP_DIV.Visible = false;
                }
                {
                    //loadGroup();
                    //for (i = 0; i <= Menu1.Items.Count - 1; i++)

                    //{

                    //    if (i == Convert.ToInt32(e.Item.Value))

                    //    {

                    //        Menu1.Items[i].Text = Menu1.Items[i].Text;

                    //    }

                    //    else

                    //    {

                    //        Menu1.Items[i].Text = Menu1.Items[i].Text;

                    //    }

                    //}
                }
            }
        }

        protected void loadSalesPersonlist()
        {
            string CompanyId = Convert.ToString(Session["CompanyId"]);
            string query = "EXEC [GetEmployee] '" + CompanyId + "'";

            DataTable dt = GetDataSrc(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                DDLSalesPerson.Items.Clear();

                DDLSalesPerson.Items.Add(new ListItem("All", "%%"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DDLSalesPerson.Items.Add(new ListItem(dt.Rows[i]["Name"].ToString().ToUpper(), dt.Rows[i]["Paycode"].ToString().ToUpper()));
                }
            }
        }

        protected void Collection_VIEW_Click(object sender, EventArgs e)
        {
            lblmsg.Text = "";
            try
            {
                if (txt_startdate.Text == string.Empty || txt_startdate.Text == "")
                {
                    lblmsg.Text = "Please Select Start Date...";
                    return;
                }
                if (txt_enddate.Text == string.Empty || txt_enddate.Text == "")
                {
                    lblmsg.Text = "Please Select End Date...";
                    return;
                }

                string SPCode = Convert.ToString(Session["UserId"]);
                if (Convert.ToInt16(Session["RoleId"]) == 3 || Convert.ToInt16(Session["RoleId"]) == 4)
                {
                    if (DDLSalesPerson.SelectedValue == string.Empty || DDLSalesPerson.SelectedValue == "")
                    {

                        lblmsg.Text = "Please Select Sales Person...";
                        return;
                    }
                    SPCode = DDLSalesPerson.SelectedValue;
                }

                string query = "EXEC [dbo].[SP_GetCrateTransaction] '" + txt_startdate.Text + "','" + txt_enddate.Text + "','BBI','" + 
                    /*Convert.ToString(Session["CompanyId"]) + "','" +*/ SPCode + "'";
                //query = "SELECT * FROM [BBIDemand].[dbo].[SD_Crate_Transaction]";

                DataTable dt = GetDataSrc(query);

                if (dt != null && dt.Rows.Count > 0)
                    GridView1.DataSource = dt;
                else
                {
                    GridView1.DataSource = null;
                    lblmsg.Text = "No Record Found............";
                }
                    GridView1.DataBind();
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        protected void Excel_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
        }

        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Crate_Transaction_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView1.GridLines = GridLines.Both;
            GridView1.HeaderStyle.Font.Bold = true;
            GridView1.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        private void ShowPopUpMsg(string strMessage)
        {
            string sScript = "var t=setTimeout(hidelbl(" + lblmsg.ClientID + "),5000);function hidelbl(lbl){lbl.vale='';clearTimeout(t););}";

            ScriptManager.RegisterStartupScript(this, GetType(), "Script1", sScript, true);
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "alert('" + strMessage + "');");
        }

    }

}