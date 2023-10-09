using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Net;
using DemandApp.Models;
using Newtonsoft.Json;

namespace DemandApp
{
    public partial class DepotDemand : System.Web.UI.Page
    {
        private static string token = string.Empty;
        private static int RetrySeconds = 10;
        private static int MaxRetries = 10;
        //  private static string endpoint = "https://modernuat.sandbox.operations.dynamics.com";
        private static string endpoint = "https://mfprod.operations.dynamics.com/";


        private Connect connect;
        private string SalesPriceGroup = "";
        //private string companyId = "";
        //private string Userid = "";

        string UserId = "";
        string CompanyId = "";

        public DepotDemand()
        {
            connect = new Connect();
            connect.AuthenticatCon();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write("<script>alert('Data inserted successfully')</script>");
            if (!Page.IsPostBack)
            {
                CompanyId = Convert.ToString(Session["CompanyId"]);
                UserId = Convert.ToString(Session["UserId"]);

                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                if (Convert.ToString(Session["CompanyId"]) != null && Convert.ToString(Session["CompanyId"]) != "")
                {
                    string RoleId = Convert.ToString(Session["RoleId"]);
                    if (RoleId == "5" || RoleId == "3" || RoleId == "2" || RoleId == "6")
                    {
                    }
                    else
                        Response.Redirect("./Home.aspx");
                }
                else
                    Response.Redirect("./Login.aspx");
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
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", ex.Message, false);
                return null;
            }
        }

        protected void emptyformGrid1()
        {
            ddlgroupname.SelectedValue = null;
            DDLCustomer.SelectedValue = null;
            txt_demnaddate.Text = "";
            CMDSave.Visible = false;
        }

        protected void AllGridEmpty()
        {
            ddlgroupname.SelectedValue = null;
            DDLCustomer.SelectedValue = null;
            txt_demnaddate.Text = "";
            GridView1.DataSource = null;
            GridView1.DataBind();
            CMDSave.Visible = false;
            Grid1div.Visible = false;

            //ddlgroupname.SelectedValue = null;
            //DDLCustomer.SelectedValue = null;
            Vtxt_DemadDate.Text = "";
            GridView3.DataSource = null;
            GridView3.DataBind();
            rpt_strdate.Text = "";
            rpt_Enddate.Text = "";

            GridView2.DataSource = null;
            GridView2.DataBind();
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            {
                MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value);

                int i;
                loadcustomerlist();
                loadGroup();
                AllGridEmpty();
                for (i = 0; i <= Menu1.Items.Count - 1; i++)
                {
                    if (i == Convert.ToInt32(e.Item.Value))
                        Menu1.Items[i].Text = Menu1.Items[i].Text;
                    else
                        Menu1.Items[i].Text = Menu1.Items[i].Text;
                }
            }
        }

        protected void loadcustomerlist(string query)
        {
            DataTable dt = GetDataSrc(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                ShowPopUpMsg(query + "  :" + Convert.ToString(dt.Rows.Count));
                DDLCustomer.Items.Clear();

                DDLCustomer.Items.Add(new ListItem("", "0"));
                for (int i = 0; i < dt.Rows.Count; i++)
                    DDLCustomer.Items.Add(new ListItem(dt.Rows[i]["Customer_Name"].ToString().ToUpper(), dt.Rows[i]["Customer_Id"].ToString().ToUpper()));
            }
            else
                DDLCustomer.Items.Clear();
        }


        protected void loadcustomerlist()
        {
            string query = "select Customer_Id,Customer_Name from V_Customer_Master where SalespersonCode='" + UserId + "'  and Sub_Company_ID='" + CompanyId + "' order by Customer_Id";

            if (Convert.ToString(Session["User_type"]) == "6")
                query = "select Customer_Id,Customer_Name from V_Customer_Master where SupervisorCode='" + UserId + "' and Sub_Company_ID='" + CompanyId + "'  order by Customer_Id";

            DataTable dt = GetDataSrc(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                DDLCustomer.Items.Clear();

                DDLCustomer.Items.Add(new ListItem("", "0"));
                for (int i = 0; i < dt.Rows.Count; i++)
                    DDLCustomer.Items.Add(new ListItem(dt.Rows[i]["Customer_Name"].ToString().ToUpper(), dt.Rows[i]["Customer_Id"].ToString().ToUpper()));
            }
        }

        protected void loadGroup()
        {
            string selquery = "select * from V_ItemGroup";
            DataTable dt = GetDataSrc(selquery);

            ddlgroupname.Items.Clear();
            //  Rpt_ddlgname.Items.Clear();
            ddlgroupname.Items.Add(new ListItem("", "0"));
            //Rpt_ddlgname.Items.Add(new ListItem("", "0"));
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                    ddlgroupname.Items.Add(new ListItem(Convert.ToString(dt.Rows[i]["name"]), Convert.ToString(dt.Rows[i]["name"])));
            }
        }

        protected void txt_demnaddate_TextChanged(object sender, EventArgs e)
        {
            //loadcustomerlist();
            loadGroup();
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select Customer_Id,Customer_Name,SalesGroup,Depot_id,MTRouteId RouteId from V_Customer_Master where customer_Id='" + DDLCustomer.SelectedValue + "' and Sub_Company_id='" + Convert.ToString(Session["CompanyId"]) + "'  order by Customer_Id";

            DataTable dt = GetDataSrc(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                Session["DepotId"] = dt.Rows[0][3];

                Session["RouteId"] = dt.Rows[0][3];

                loaditemlist(ddlgroupname.SelectedValue);
            }
        }
        protected void ddlgroupnameIndexChanged(object sender, EventArgs e)
        {
            string query = string.Empty;
            string companyid = Convert.ToString(Session["CompanyId"]);
            if (Convert.ToString(Session["User_type"]) == "3" || Convert.ToString(Session["User_type"]) == "4")
                query = "select Customer_Id,Customer_Name,SalesGroup from V_Customer_Master where SalesGroup='" + ddlgroupname.SelectedValue + "' and Sub_Company_ID='" + companyid + "'   order by Customer_Name";
            else if (Convert.ToString(Session["User_type"]) == "2")
                query = "select Customer_Id,Customer_Name,SalesGroup from V_Customer_Master where SalespersonCode='" + Convert.ToString(Session["UserId"]) + "' and Sub_Company_ID='" + companyid + "' order by Customer_Name";
            else if (Convert.ToString(Session["User_type"]) == "6")
                query = "select Customer_Id,Customer_Name,SalesGroup from V_Customer_Master where  SupervisorCode='" + Convert.ToString(Session["UserId"]) + "' and Sub_Company_ID='" + companyid + "'  order by Customer_Name";
            //else if (Convert.ToString(Session["LoginType"]) == "1")
            //{
            //     query = "select Customer_Id,Customer_Name,SalesGroup from V_Customer_Master where customer_Id='" + DDLCustomer.SelectedValue + "'  order by Customer_Id";
            //}
            loadcustomerlist(query);
        }

        protected void loaditemlist(string SalesGroup)
        {
            string companyId = Convert.ToString(Session["CompanyId"]);

            //string query = "select top 10 item_id,item_name from v_item_master where ITEMGROUPID='" + SalesGroup + "' and Stopped='No'";
            string IsCustomerERP = "EXEC CustomerCheckforERP '" + txt_demnaddate.Text + "','" + DDLCustomer.SelectedValue + "','" + SalesGroup + "','" + companyId + "'";
            DataTable dt1 = GetDataSrc(IsCustomerERP);
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                //lblmgs.Visible= true;
                this.ShowPopUpMsg(DDLCustomer.SelectedItem.Value + " is already send to ERP Server...");
                //loadGroup();
                return;
            }

            //string query = "select top 10 item_id,item_name from v_item_master where ITEMGROUPID='" + SalesGroup + "' and Stopped='No'";
            string query = "EXEC GetDemandItemDetails '" + txt_demnaddate.Text + "','" + DDLCustomer.SelectedValue + "','" + SalesGroup + "','" + companyId + "'";
            DataTable dt = GetDataSrc(query);

            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                //GridView1.DataBind();
                Grid1div.Visible = true;
                CMDSave.Visible = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                Grid1div.Visible = false;
                CMDSave.Visible = false;
            }
        }

        protected string GenerateOrderNo()
        {
            string query = "EXEC [dbo].[GetDemandOrderNo] '" + DDLCustomer.SelectedValue + "', '" + txt_demnaddate.Text + "'";

            DataTable dt = GetDataSrc(query);

            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "0";
        }

        protected void CMDSave_Click(object sender, EventArgs e)
        {

            string slno = null;
            try
            {
                if (txt_demnaddate.Text == null || txt_demnaddate.Text == "")
                {
                    this.ShowPopUpMsg("Please select Demand Date.");
                    txt_demnaddate.Focus();
                    return;
                }
                if (ddlgroupname.SelectedValue == null || ddlgroupname.SelectedValue == "")
                {
                    this.ShowPopUpMsg("Please Select Item Group Name.");
                    ddlgroupname.Focus();
                    return;
                }
                if (DDLCustomer.SelectedValue == null || DDLCustomer.SelectedValue == "")
                {
                    this.ShowPopUpMsg("Please Select Customer Name.");
                    DDLCustomer.Focus();
                    return;
                }
                string SelectQuery = string.Empty;
                string CompanyId = Convert.ToString(Session["CompanyId"]);
                string DepotId = Convert.ToString(Session["DepotId"]);
                string RouteId = Convert.ToString(Session["RouteId"]);
                string CustomerId = DDLCustomer.SelectedValue; //Convert.ToString(Session["DepotId"]);
                string orderNo = GenerateOrderNo();
                string LoginId = Convert.ToString(Session["UserId"]); ;
                string SupervisorCode = "";
                string GroupItemName = ddlgroupname.SelectedValue;


                string IP = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
                string query = "";

                DataTable hdt = new DataTable();
                string HeaderQuery = "EXEC SaveDemandHeader '" + CompanyId + "','" + DepotId + "','" + RouteId + "','" + CustomerId + "','" + SupervisorCode + "','" + LoginId + "','" + GroupItemName + "','" + txt_demnaddate.Text + "','','" + LoginId + "'";
                if (HeaderQuery != null && HeaderQuery != "")
                {
                    var ssss = connect.SelQuery(HeaderQuery, connect.ConnBBIDemand);
                    if (ssss != null)
                        orderNo = Convert.ToString(ssss.Rows[0]["orderId"]);
                }

                if (orderNo != null && orderNo != "" && orderNo != "0")
                {
                    query = "INSERT INTO [SD_SalesDemand_Master]([Company_Id],[Depot_Id],[Route_Id],[Customer_Id],[Item_Id],[Cust_ItemQty],[ItemQty],[Demand_Date],[Submit_Date],[OrderNo],[CreatedBy],[CreatedDate],[IPadd],[UpdateType],[DemandInput])";
                    foreach (GridViewRow g1 in GridView1.Rows)
                    {
                        string SelectQuery1 = "";
                        string Item_ID = g1.Cells[2].Text;//item ID
                        string Item_Name = g1.Cells[3].Text;// item name
                        string cust_Txtqty = g1.Cells[4].Text;
                        orderNo = g1.Cells[0].Text == "0" ? orderNo : g1.Cells[0].Text;
                        string Txtqty = (g1.FindControl("Txtqty") as TextBox).Text;//g1.Cells[2].Text; //(g1.FindControl("Txtqty") as Label).Text;
                        if (Txtqty != "" && Convert.ToInt32(Txtqty) > 0)
                        {
                            SelectQuery1 = "SELECT '" + CompanyId + "', '" + DepotId + "', '" + RouteId + "', '" + CustomerId + "', '" + Item_ID + "', '" + cust_Txtqty + "','" + Txtqty + "', '" + txt_demnaddate.Text + "',NULL,'" + orderNo + "','" + LoginId + "',GETDATE(),'" + IP + "',NULL,NULL";

                            if (SelectQuery == "" || SelectQuery == null)
                                SelectQuery = SelectQuery1;
                            else
                                SelectQuery = "\r\n" + SelectQuery + " UNION ALL \r\n" + SelectQuery1;
                        }
                    }

                    if (SelectQuery != null && SelectQuery != "")
                    {
                        query = query + "\r\n" + SelectQuery;
                        connect.InsertValues(query, connect.ConnBBIDemand);
                    }
                }

                this.ShowPopUpMsg("Records inserted successfully");
                GridView1.DataSource = null;
                GridView1.DataBind();
                Grid1div.Visible = false;
                emptyformGrid1();
            }
            catch (Exception ex)
            {
                this.ShowPopUpMsg(ex.Message);
                return;
            }
            finally
            {
                //con.Close();
            }
        }

        protected void TABVERIFY_CMD_VIEW_Click(object sender, EventArgs e)
        {
            string slno = null;
            try
            {
                if (Vtxt_DemadDate.Text == null || Vtxt_DemadDate.Text == "")
                {
                    this.ShowPopUpMsg("Please select Demand Date...");
                    Vtxt_DemadDate.Focus();
                    return;
                }
                bindgrid("%%");
            }
            catch (Exception ex)
            {
                this.ShowPopUpMsg(ex.Message);
            }
            finally
            {
                //con.Close();
            }
        }
        protected void SendToServer_Click(object sender, EventArgs e)
        {
            string slno = null;
            try
            {
                if (RB_CUST.SelectedValue == "" && RB_CUST.SelectedValue == null)
                {
                    this.ShowPopUpMsg("Please select Customer to send data to ERP.");
                    RB_CUST.Focus();
                    return;
                }

                string SelectQuery = string.Empty;
                string CompanyId = Convert.ToString(Session["CompanyId"]);
                string DepotId = Convert.ToString(Session["DepotId"]);
                string RouteId = Convert.ToString(Session["RouteId"]);
                // string CustomerId = DDLCustomer.SelectedValue; //Convert.ToString(Session["DepotId"]);
                string LoginId = Convert.ToString(Session["UserId"]); ;


                string IP = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
                string query = "";
                query = "INSERT INTO [SD_SalesDemand_Master]([Company_Id],[Depot_Id],[Route_Id],[Customer_Id],[Item_Id],[Cust_ItemQty],[ItemQty],[Demand_Date],[Submit_Date],[OrderNo],[CreatedBy],[CreatedDate],[IPadd],[UpdateType],[DemandInput])";

                Resultjsonlist obj = new Resultjsonlist();
                List<GetJson> LstCustomer = new List<GetJson>();
                string UpdateQuery = "";

                foreach (GridViewRow g1 in GridView3.Rows)
                {
                    string SelectQuery1 = "";

                    GetJson objson = new GetJson();

                    string Item_ID = g1.Cells[3].Text;//item ID
                    string Item_Name = g1.Cells[4].Text;// item name
                    string customerid = g1.Cells[1].Text;//customerId
                    string orderNo = g1.Cells[0].Text;//OrderNo
                    string Cust_Qty = g1.Cells[5].Text;//cust_qty
                    string qty = Convert.ToString(g1.Cells[6].Text);//qty
                    objson.CustAccount = customerid;
                    objson.ItemId = Item_ID;
                    objson.Qty = qty == "0" ? Convert.ToDecimal(Cust_Qty) : Convert.ToDecimal(qty);//Convert.ToString( Math.Round( Convert.ToDouble( qty),2));
                    objson.dataAreaId = CompanyId;
                    objson.OrderId = orderNo;
                    objson.SalespersonCode = LoginId;
                    objson.SupervisorCode = g1.Cells[7].Text;

                    DateTimeOffset now = (DateTimeOffset)(Convert.ToDateTime(Vtxt_DemadDate.Text));


                    objson.OrderDate = now;
                    objson.ItemGroupId = g1.Cells[8].Text;

                    LstCustomer.Add(objson);
                    string jsonresult12 = JsonConvert.SerializeObject(objson);

                    //API.InsertEntity(endpoint, "SalesDemandStagings", jsonresult12);
                    try
                    {
                        API.InsertEntity(endpoint, "DemandStagings", jsonresult12);
                    }
                    catch (Exception ex)
                    {
                        ShowPopUpMsg("Something went wrong. please try after sometime...");
                        return;
                    }
                    if (qty != "" && Convert.ToInt32(qty) > 0)
                    {
                        SelectQuery1 = " update SD_SalesDemand_Master set Submit_Date=GETDATE() where OrderNo='" + orderNo + "' and Customer_Id='" + customerid + "' and Item_Id='" + Item_ID + "' and Company_Id='" + CompanyId + "'";
                        if (UpdateQuery == "" || UpdateQuery == null)
                            UpdateQuery = SelectQuery1;
                        else
                            UpdateQuery = "\r\n" + UpdateQuery + " UNION ALL \r\n" + SelectQuery1;
                    }
                    else if (Cust_Qty != "" && Convert.ToInt32(Cust_Qty) > 0)
                    {
                        SelectQuery1 = "SELECT '" + CompanyId + "', '" + DepotId + "', '" + RouteId + "', '" + customerid + "', '" + Item_ID + "', '" + objson.Qty + "','" + objson.Qty + "', '" + Vtxt_DemadDate.Text + "',GETDATE(),'" + orderNo + "','" + LoginId + "',GETDATE(),'" + IP + "',NULL,NULL";

                        if (SelectQuery == "" || SelectQuery == null)
                            SelectQuery = SelectQuery1;
                        else
                            SelectQuery = "\r\n" + SelectQuery + " UNION ALL \r\n" + SelectQuery1;
                    }
                }
                obj.Result = LstCustomer;
                //string jsonresult = JsonConvert.SerializeObject(LstCustomer);
                //string jsonresult1 = JsonConvert.SerializeObject(obj);
                // API.InsertEntity(endpoint, "SalesDemandStagings", jsonresult1);
                //API.InsertEntity(endpoint, "DemandStagings", jsonresult);
                if (SelectQuery != null && SelectQuery != "")
                {
                    query = query + "\r\n" + SelectQuery;
                    connect.InsertValues(query, connect.ConnBBIDemand);
                }
                if (UpdateQuery != null && UpdateQuery != "")
                    connect.InsertValues(UpdateQuery, connect.ConnBBIDemand);

                lblmgs.Visible = true;
                this.ShowPopUpMsg("Sent to ERP successfully");
                GridView1.DataSource = null;
                GridView1.DataBind();
                Grid1div.Visible = false;
            }
            catch (Exception ex)
            {
                this.ShowPopUpMsg(ex.Message);
            }
            finally
            {
                //con.Close();
            }
        }

        public void bindgrid(string customerid)
        {
            string slno = null;
            try
            {
                string query = "EXEC [dbo].[Get_DemandDetails] '" + Vtxt_DemadDate.Text + "','" + Convert.ToString(Session["CompanyId"]) + "','" + customerid + "','" + Convert.ToString(Session["UserId"]) + "'";

                DataTable dt = GetDataSrc(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    GridView3.DataSource = dt;
                    GridView3.DataBind();
                }
                else
                {
                    this.ShowPopUpMsg("No Data Found..");
                    GridView3.DataSource = null;
                    GridView3.DataBind();
                    //"errormessgae"
                }
            }
            catch (Exception ex)
            {
                this.ShowPopUpMsg(ex.Message);
            }
            finally
            {
                //con.Close();
            }
        }

        protected void RPT_CMD_VIEW_Click(object sender, EventArgs e)
        {
            string slno = null;
            try
            {
                if (rpt_strdate.Text == "" && rpt_strdate.Text == null)
                {
                    this.ShowPopUpMsg("Please select Demand Start Date");
                    rpt_strdate.Focus();
                    return;
                }
                if (rpt_Enddate.Text == "" && rpt_Enddate.Text == null)
                {
                    this.ShowPopUpMsg("Please select Demand End Date");
                    rpt_Enddate.Focus();
                    return;
                }
                string query = "EXEC [dbo].[Get_DemandReportDetails] '" + rpt_strdate.Text + "','" + rpt_Enddate.Text + "','" + Convert.ToString(Session["CompanyId"]) + "','','" + Convert.ToString(Session["UserId"]) + "','%'";

                DataTable dt = GetDataSrc(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                }
                else
                    this.ShowPopUpMsg("No Data Found.");
            }
            catch (Exception ex)
            {
                this.ShowPopUpMsg("Something went wrong. please try again ..");
            }
            finally
            {
                //con.Close();
            }
        }

        protected void RB_CUST_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RB_CUST.Text == "1")
            {
                DDlCust.Items.Clear();
                DDlCust.Items.Add(new ListItem("All", "%%"));
                bindgrid("%%");
            }
            else
            {
                string query = "EXEC [dbo].[Get_customerlist] '" + Vtxt_DemadDate.Text + "','" + Convert.ToString(Session["CompanyId"]) + "','','" + Convert.ToString(Session["UserId"]) + "'";

                DataTable dt = GetDataSrc(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DDlCust.Items.Clear();
                    // DDlCust.Items.Add(new ListItem("All", "%%"));
                    for (int i = 0; i < dt.Rows.Count; i++)
                        DDlCust.Items.Add(new ListItem(dt.Rows[i]["Cust_Name"].ToString().ToUpper(), dt.Rows[i]["Cust_ID"].ToString().ToUpper()));
                    bindgrid(DDlCust.SelectedValue);
                }
            }
        }

        protected void DDlCust_BindDataTable(object sender, EventArgs e)
        {
            bindgrid(DDlCust.SelectedValue);
        }

        private void ShowPopUpMsg(string strMessage)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + strMessage + "');", true);

        }
    }
}