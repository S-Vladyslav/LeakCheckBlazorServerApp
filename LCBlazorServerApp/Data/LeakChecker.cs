using System.Net;
using Newtonsoft.Json;
using DataAccess.Interfaces;

namespace LCBlazorServerApp.Data
{
    public class LeakChecker
    {
        public static List<T> WeLeakInfoGetResults<T>(string type, string target) where T : IDBModel, new()
        {
            try
            {
                return GetResults<T>(type, target);
            }
            catch
            {
                return new List<T>();
            }
        }

        private static List<T> GetResults<T>(string type, string target) where T : IDBModel, new()
        {
            string url = "https://api.weleakinfo.to/api";
            var responseDict = new Dictionary<string, dynamic>();

            using (var client = new WebClient())
            {
                client.QueryString.Add("value", target);
                client.QueryString.Add("type", type);
                client.QueryString.Add("key", ""); // weleakinfo API key here

                var resp = client.DownloadString(url);
                responseDict = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(resp);
            }

            if (responseDict["success"] == false)
            {
                return new List<T>();
            }

            var resultList = responseDict["result"];

            var leaksList = new List<T>();

            foreach (var result in resultList)
            {
                T leak = new T();

                if (type == "domain")
                {
                    leak.Domain = target;
                }

                var line = result["line"].ToString().Split(':');

                leak.EmailAddress = line[0];
                if (result["email_only"] == 0) leak.Password = line[1];
                if (result["last_breach"] == 0) leak.LastBreach = result["last_breach"];

                if (result["sources"] == 0) leak.Source = "";
                foreach (var source in result["sources"])
                {
                    leak.Source += $"{source} ";
                }

                leaksList.Add(leak);
            }

            return leaksList;
        }
    }
}
