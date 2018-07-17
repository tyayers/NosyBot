using Newtonsoft.Json.Linq;
using NosyBot.Services.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NosyBot.Services.Utilities
{
    public static class ServiceProxies
    {
        public static void GetTranslations(TranslateCommand translateCommand)
        {
            foreach (string lang in translateCommand.Translations.Keys)
            {
                int counter = 0;
                Newtonsoft.Json.Linq.JArray requestTexts = new Newtonsoft.Json.Linq.JArray();

                while (counter <= 24 && counter < translateCommand.Translations[lang].Stories.Count)
                {
                    JObject text = new JObject();
                    text.Add("Text", translateCommand.Translations[lang].Stories[counter].Title);
                    requestTexts.Add(text);
                    counter++;
                }

                List<string> translations = GetTranslation(requestTexts, lang, translateCommand.DisplayLanguage).Result;

                if (translations.Count > 0)
                {
                    for (int i = 0; i < translations.Count; i++)
                    {
                        translateCommand.Translations[lang].Stories[i].TranslatedTitle = translations[i];
                    }
                }
            }
        }

        public static async Task<List<string>> GetTranslation(JArray texts, string sourceLanguage, string destinationLanguage)
        {
            List<string> results = new List<string>();
            string from = sourceLanguage;
            string to = destinationLanguage;
            string uri = "https://api.cognitive.microsofttranslator.com/translate?api-version=3.0&from=" + from + "&to=" + to;
            string subKey = System.Environment.GetEnvironmentVariable("TranslatorKey", EnvironmentVariableTarget.Process); //System.Configuration.ConfigurationManager.AppSettings["TranslatorKey"];

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subKey);
                var content = new StringContent(texts.ToString(), Encoding.UTF8, "application/json");

                HttpResponseMessage msg = client.PostAsync(uri, content).Result;
                if (msg.IsSuccessStatusCode)
                {
                    var JsonDataResponse = msg.Content.ReadAsStringAsync().Result;
                    Newtonsoft.Json.Linq.JArray obj = Newtonsoft.Json.Linq.JArray.Parse(JsonDataResponse);
                    if (obj != null)
                    {
                        foreach (JObject translationObject in obj)
                        {
                            int counter = 0;
                            JArray translations = (JArray)translationObject["translations"];
                            while (counter < translations.Count)
                            {
                                results.Add(translations[counter]["text"].ToString());
                                counter++;
                            }
                        }
                    }
                }
            }

            return results;
        }
    }
}
