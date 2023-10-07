using System;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;

namespace DemandApp
{
    public partial class PaymentCollection : System.Web.UI.Page
    {
        private Connect connect;
        private string SalesPriceGroup = "";
        //private string companyId = "";
        //private string Userid = "";


        public PaymentCollection()
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

                    if (RoleId == "4" || RoleId == "3" || RoleId == "2")
                    {
                    }
                    else
                    {
                        Response.Redirect("./Home.aspx");
                    }
                    //Userid= Convert.ToString(Session["UserId"]);
                    //Response.Redirect("./Home.aspx");
                }
                else
                {
                    Response.Redirect("./Login.aspx");
                }
            }
        }

        protected void ImgBtnSbmt_Click(object sender, ImageClickEventArgs e)
        {

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

        protected void emptyform()
        {
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            {
                MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value);

                int i;
                if (Convert.ToInt16(Session["RoleId"]) == 3 || Convert.ToInt16(Session["RoleId"])==4 || Convert.ToInt16(Session["RoleId"]) == 6)
                {
                    loadSalesPersonlist();
                    SP_DIV.Visible = true;
                }
                else
                {
                    SP_DIV.Visible = false;

                }

                //loadGroup();
                for (i = 0; i <= Menu1.Items.Count - 1; i++)

                {

                    if (i == Convert.ToInt32(e.Item.Value))

                    {

                        Menu1.Items[i].Text = Menu1.Items[i].Text;

                    }

                    else

                    {

                        Menu1.Items[i].Text = Menu1.Items[i].Text;

                    }

                }

            }
        }

        protected void loadSalesPersonlist()
        {
            string CompanyId = Convert.ToString(Session["CompanyId"]);
            string query = "EXEC GETEMPLOYEE '" + CompanyId + "'";

            DataTable dt = GetDataSrc(query);

            if (dt!= null && dt.Rows.Count > 0)
            {
                DDLSalesPerson.Items.Clear();


                DDLSalesPerson.Items.Add(new ListItem("All", "%%"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DDLSalesPerson.Items.Add(new ListItem(dt.Rows[i]["Name"].ToString().ToUpper(), dt.Rows[i]["Paycode"].ToString().ToUpper()));
                }

            }
        }


        protected void txt_demnaddate_TextChanged(object sender, EventArgs e)
        {
            //loadcustomerlist();
            //loadGroup();
        }

        protected void Collection_VIEW_Click(object sender, EventArgs e)
        {
            string SPCode = "";
            try
            {
                if (txt_startdate.Text == string.Empty || txt_startdate.Text == "")
                {
                    lblmsg.Text = "Please Select Start Date...";
                    this.ShowPopUpMsg("Please Select Start Date...");
                    //ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
                    return;
                }
                if (txt_enddate.Text == string.Empty || txt_enddate.Text == "")
                {

                    lblmsg.Text = "Please Select End Date...";
                    return;
                    //exit ;
                }

                SPCode = Convert.ToString(Session["UserId"]);
                if (Convert.ToInt16(Session["RoleId"]) == 3 || Convert.ToInt16(Session["RoleId"]) == 4)
                {
                    if (DDLSalesPerson.SelectedValue == string.Empty || DDLSalesPerson.SelectedValue == "")
                    {

                        lblmsg.Text = "Please Select Sales Person...";
                        return;
                    }
                    SPCode = DDLSalesPerson.SelectedValue;
                }

                string query = "EXEC [dbo].[GetPaymentCollection] '" + txt_startdate.Text + "','" + txt_enddate.Text + "','" + Convert.ToString(Session["CompanyId"]) + "','" + SPCode + "'";

                DataTable dt = GetDataSrc(query);


                if (dt!= null &&  dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    lblmsg.Text = "No Record Found............";
                    //"errormessgae"
                }
            }
            catch (Exception ex)
            {
                //tran.Rollback();
                //lblMessage.Text = "Error Occured  near Sr. No. = " + slno + "<br />";
                //lblMessage.Text += "No Data Is Inserted. <br />";
                ////lblMessage.Text += ex.Message;  
            }
            finally
            {
                //con.Close();
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
            string FileName = "PaymentCollection" + DateTime.Now + ".xls";
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