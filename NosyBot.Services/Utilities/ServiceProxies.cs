using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NosyBot.Services.Utilities
{
    public static class ServiceProxies
    {
        public static async Task<string> GetTranslation(string Text, string sourceLanguage, string destinationLanguage)
        {
            string text = Text;
            string from = sourceLanguage;
            string to = destinationLanguage;
            string uri = "https://api.microsofttranslator.com/v2/Http.svc/Translate?text=" + HttpUtility.UrlEncode(text) + "&from=" + from + "&to=" + to;
            string translation = "";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            string subKey = System.Environment.GetEnvironmentVariable("TranslatorKey", EnvironmentVariableTarget.Process); //System.Configuration.ConfigurationManager.AppSettings["TranslatorKey"];

            var authTokenSource = new AzureAuthToken(subKey);
            string authToken;
            authToken = await authTokenSource.GetAccessTokenAsync();

            httpWebRequest.Headers.Add("Authorization", authToken);
            using (WebResponse response = await httpWebRequest.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            {
                DataContractSerializer dcs = new DataContractSerializer(Type.GetType("System.String"));
                translation = (string)dcs.ReadObject(stream);
            }

            return translation;
        }
    }
}
