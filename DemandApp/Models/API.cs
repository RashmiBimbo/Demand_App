using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DemandApp.Models;
using System.Net;
using System.Runtime.InteropServices;
using Antlr.Runtime;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace DemandApp.Models
{
    class API
    {
        
        private Connect connect;

        public API()
        {
            connect = new Connect();
            connect.AuthenticatCon();
        }
        private static string token = string.Empty;
        private static int RetrySeconds = 10;
        private static int MaxRetries = 10;
        private static string endpoint = "https://modernuat.sandbox.operations.dynamics.com";
       

        public void callingapi()
        {         //Get Data 
                  //var cdata = GetEntity(endpoint, "CustomersV3", "(dataAreaId='BBI',CustomerAccount='C000001')");

            //var json = AddSquareBrackets(cdata);

            //var dt = ConvertJsonToDataTable(json);
            //DataTable table = JsonConvert.DeserializeObject<DataTable>(json);
            //int tcount = table.Rows.Count;
            //string name1 = table.Rows[0][3].ToString();
            //for (int i = 0; i < dt.Rows.Count; i++) //looping through all rows including the column. change `i=1` if need to exclude the columns display
            //{
            //    for (int j = 0; j < table.Columns.Count; j++) //looping through all columns
            //    {
            //        Console.WriteLine(table.Rows[i][j]); //display of the data
            //    }
            //}
            //Console.WriteLine("Press enter to exit.");
            //Console.Read();

            //Insert 

            //string Inscontent = @"{""dataAreaId"":""BBI"",""CustomerAccont"":""JAT001"", ""CustomerGroupId"":""NAC"" }";
            string Inscontent = @"{ ""CustAccount"":""CK00002"", ""ItemId"":""FG030250"" ,""Qty"":10.00 ,""dataAreaId"":""KIT""}";
            //InsertEntity(endpoint, "CustomersV3", Inscontent);
            InsertEntity(endpoint, "SalesDemandStagings", Inscontent);


            //Update Data
            //string content = @"{""CustomerGroupId"":""PVT HSP"" }";

            //    UpdateEntity(endpoint, "CustomersV3", "(dataAreaId='BBI',CustomerAccount='C000001')", content);
        }
        public static string GetToken()
        {
            try
            {
               
                MasterData objdata= new MasterData();
        
                //var domain = objdata.GetConfigSetting("domain");// "cc53a9a2-48fc-49e7-8f12-ef5a565cbcbe";
                //var clientId = objdata.GetConfigSetting("clientId"); //"f59652f6-fd73-411b-ba47-5002f217eff9";
                //var clientSecret = objdata.GetConfigSetting("clientSecret"); //"R1Z8Q~-nETEjRazu1X4qxi6C1~PTJsCkz-y~ccid";
                //var resource = objdata.GetConfigSetting("endpoint"); //"https://mfprod.operations.dynamics.com/";

                var domain =  "cc53a9a2-48fc-49e7-8f12-ef5a565cbcbe";
                var clientId = "f59652f6-fd73-411b-ba47-5002f217eff9";
                var clientSecret = "R1Z8Q~-nETEjRazu1X4qxi6C1~PTJsCkz-y~ccid";
                var resource = "https://mfprod.operations.dynamics.com/";


                HttpClient client = new HttpClient();

                string requestUrl = $"https://login.microsoftonline.com/{domain}/oauth2/token";

                string request_content = $"grant_type=client_credentials&resource={resource}&client_id={clientId}&client_secret={clientSecret}";

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
                try
                {
                    request.Content = new StringContent(request_content, Encoding.UTF8, "application/x-www-form-urlencoded");
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpResponseMessage response = client.SendAsync(request).Result;

                string responseString = response.Content.ReadAsStringAsync().Result;
                var token = JsonConvert.DeserializeObject<TokenDC>(responseString);
                //var token = JSONSerializer<TokenDC>.DeSerialize(responseString);
                var accessToken = token.access_token;
                return accessToken;
            }
            catch (Exception ex)
            {
                var message = $"There was an error retrieving the token:\r\n{ex.Message}";
                throw new Exception(message);
            }
        }

        public static string GetEntity(string endpointUrl, string dataEntityName, string dataEntityKey)
        {
            HttpClient client = new HttpClient();

            //Get authorization token
            var erpToken = GetToken();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", erpToken);
            // var endpointUrl = "url_d365fo_without_bar";
            endpointUrl = endpointUrl + "/data/" + dataEntityName + dataEntityKey + "?cross-company=true";

            string responseString = string.Empty;
            int retries = 0;
            int seconds = RetrySeconds;

            for (; ; )
            {
                try
                {
                    retries++;

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, endpointUrl);
                    HttpResponseMessage response = client.SendAsync(request).Result;

                    responseString = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        if ((int)response.StatusCode == 429 && retries < MaxRetries)
                        {
                            //Try to use the Retry-After header value if it is returned. 
                            if (response.Headers.Contains("Retry-After"))
                            {
                                seconds = int.Parse(response.Headers.GetValues("Retry-After").FirstOrDefault());
                            }

                            Thread.Sleep(TimeSpan.FromSeconds(seconds));
                            continue;
                        }
                        else if ((int)response.StatusCode == 404)
                        {
                            // Entity not found, don't retry
                            return responseString;
                        }
                        else
                        {
                            throw new Exception(responseString);
                        }
                    }

                    return responseString;
                }
                catch (Exception ex)
                {
                    string message = $"There was an error when trying to get the {dataEntityName} with the key {dataEntityKey}:\r\n{ex.Message}";
                    throw new Exception(message);
                }
            }
        }

        public static string UpdateEntity(string endpointUrl, string dataEntityName, string dataEntityKey, string requestContent)
        {
            HttpClient client = new HttpClient();

            //Get authorization token
            var erpToken = GetToken();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", erpToken);
            //  var endpointUrl = "url_d365fo_without_bar";
            endpointUrl = endpointUrl + "/data/" + dataEntityName + dataEntityKey + "?cross-company=true";

            string responseString = string.Empty;
            int retries = 0;
            int seconds = RetrySeconds;

            for (; ; )
            {
                try
                {
                    retries++;

                    HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), endpointUrl);

                    request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.SendAsync(request).Result;

                    responseString = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        if ((int)response.StatusCode == 429 && retries < MaxRetries)
                        {
                            //Try to use the Retry-After header value if it is returned. 
                            if (response.Headers.Contains("Retry-After"))
                            {
                                seconds = int.Parse(response.Headers.GetValues("Retry-After").FirstOrDefault());
                            }

                            Thread.Sleep(TimeSpan.FromSeconds(seconds));
                            continue;
                        }
                        else
                        {
                            throw new Exception(responseString);
                        }
                    }

                    return responseString;
                }
                catch (Exception ex)
                {
                    string message = $"There was an error when trying to update into {dataEntityName}:\r\n{ex.Message}";
                    throw new Exception(message);
                }
            }
        }
        private static DataTable ConvertJsonToDataTable(string jsonData)
        {
            try
            {
                return JsonConvert.DeserializeObject<DataTable>(jsonData);

            }
            catch
            {
                return null;
            }
        }
        private static string AddSquareBrackets(string json)
        {
            return $"[{json}]";
        }

        //Insert Data
        public static string InsertEntity(string endpointUrl, string dataEntityName, string requestContent)
        {
            HttpClient client = new HttpClient();

            //Get authorization token
            var erpToken = GetToken();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", erpToken);
            //var endpointUrl = "url_d365fo_without_bar";
            endpointUrl = endpointUrl + "/data/" + dataEntityName;

            string responseString = string.Empty;
            int retries = 0;
            int seconds = RetrySeconds;

            for (; ; )
            {
                try
                {
                    retries++;

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, endpointUrl);

                    request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.SendAsync(request).Result;

                    responseString = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        if ((int)response.StatusCode == 429 && retries < MaxRetries)
                        {
                            //Try to use the Retry-After header value if it is returned. 
                            if (response.Headers.Contains("Retry-After"))
                            {
                                seconds = int.Parse(response.Headers.GetValues("Retry-After").FirstOrDefault());
                            }

                            Thread.Sleep(TimeSpan.FromSeconds(seconds));
                            continue;
                        }
                        else
                        {
                            throw new Exception(responseString);
                        }
                    }

                    return responseString;
                }
                catch (Exception ex)
                {
                    string message = $"There was an error when trying to insert into {dataEntityName}:\r\n{ex.Message}";
                    throw new Exception(message);
                }
            }
        }

        private DataTable GetDataSrc(string query)
        {
            DataTable dt;
            try
            {
                //connect.AuthenticatCon();
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
        public class TokenDC
        {

            public string token_type { get; set; }

            public string expires_in { get; set; }

            public string ext_expires_in { get; set; }

            public string expires_on { get; set; }

            public string not_before { get; set; }

            public string resource { get; set; }

            public string access_token { get; set; }
        }

    }
}


