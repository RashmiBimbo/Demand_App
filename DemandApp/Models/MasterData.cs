using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DemandApp.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Web.UI.WebControls;

namespace DemandApp.Models
{
    public class MasterData
    {
     
        private Connect connect;
        public MasterData() {
            connect = new Connect();
            connect.AuthenticatCon();
        }

        public string GetConfigSetting( string key) {
            string Sqlquery = "exec GetConfigSettings '"+key+"'";
            DataTable dt = GetDataSrc(Sqlquery);

            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToString(dt.Rows[0]["Value"]);
            }
            else
            {
                return "";
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
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", ex.Message, false);
                return null;
            }
        }



    }


}