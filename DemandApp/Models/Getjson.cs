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

namespace DemandApp.Models
{
    public class GetJson
    {
        public  string CustAccount { get;set;}
        public string ItemId { get; set; }
        public decimal Qty { get; set; }
        public string dataAreaId { get; set; }
        public string OrderId { get; set; }
        public string SupervisorCode { get; set; }
        public string SalespersonCode { get; set; }
        //  public string TransferDate { get; set; }
        public string ItemGroupId { get; set; }
        public DateTimeOffset OrderDate { get; set; }

    }
    public class Resultjsonlist
    {
        public List<GetJson> Result { get; set; }
      
    }
}