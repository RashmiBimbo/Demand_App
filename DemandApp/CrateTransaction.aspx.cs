using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;

namespace DemandApp
{
    public partial class CrateTransaction : System.Web.UI.Page
    {
        private Connect connect;
        private int RoleId;
        private string CompanyId, UserID;

        public CrateTransaction()
        {
            connect = new Connect();
            connect.AuthenticatCon();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UserID = Convert.ToString(Session["UserId"]);
            RoleId = Convert.ToInt16(Session["RoleId"]);
            CompanyId = Session["CompanyId"] != null ? Convert.ToString(Session["CompanyId"]) : null;

            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(CompanyId))
                {
                    if (!(RoleId == 3 || RoleId == 6))
                        Response.Redirect("./Home.aspx");
                }
                else
                    Response.Redirect("./Login.aspx");
            }
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
            MultiView1.ActiveViewIndex = int.Parse(e.Item.Value);

            Div_Filters.Visible = (RoleId == 3 || RoleId == 6);
            lblSV.Visible= DIV_SV.Visible = RoleId == 3;

            if (RoleId == 3)
                LoadSupervisors();

            if (RoleId == 6)
                //LoadSalesPerson($"EXEC GETEMPLOYEE '{CompanyId}';");
                LoadSalesPerson(UserID);
            {
                //int i;
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

        private void LoadSalesPerson(string supervisor)
        {
            DataTable dt = GetDataSrc($"EXEC [SP_GetSalesPersonBySupervisor] '{CompanyId}', '{supervisor}'");

            if (dt != null && dt.Rows.Count > 0)
                LoadDdl(DDLSalesPerson, dt);
        }

        private void LoadSupervisors()
        {
            string query = $"EXEC [SP_GetSupervisor] '{CompanyId}'";

            DataTable dt = GetDataSrc(query);

            if (dt != null && dt.Rows.Count > 0)
                LoadDdl(DdlSV, dt);
        }

        private void LoadDdl(DropDownList ddl, DataTable dt)
        {
            ddl.Items.Clear();

            ddl.Items.Add(new ListItem("All", "%%"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Add(new ListItem(dt.Rows[i]["Name"].ToString().ToUpper(), dt.Rows[i]["Paycode"].ToString().ToUpper()));
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

                //if (Convert.ToInt16(Session["RoleId"]) == 3 || Convert.ToInt16(Session["RoleId"]) == 4)
                //{
                //    if (DDLSalesPerson.SelectedValue == string.Empty || DDLSalesPerson.SelectedValue == "")
                //    {
                //        lblmsg.Text = "Please Select Sales Person...";
                //        return;
                //    }
                UserID = string.IsNullOrEmpty(DDLSalesPerson.SelectedValue) ? "" : DDLSalesPerson.SelectedValue;
                //}

                string query = $"EXEC [dbo].[SP_GetCrateTransaction] '{txt_startdate.Text}', '{txt_enddate.Text}', '{CompanyId}', '{UserID}'";
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

        protected void DdlSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSalesPerson(DdlSV.SelectedValue);
        }

    }
}