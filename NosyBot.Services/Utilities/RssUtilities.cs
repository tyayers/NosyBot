using NosyBot.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;

namespace NosyBot.Services.Utilities
{
    public static class RssUtilities
    {
        public static List<StoryRecord> GetRSSUpdates(ProviderRecord provider)
        {
            List<StoryRecord> results = new List<StoryRecord>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string rssContent = client.GetStringAsync(provider.RssUrl).Result;

                    if (!String.IsNullOrEmpty(rssContent))
                    {

                        XDocument doc = XDocument.Parse(rssContent);

                        string newUpdate = GetPublishDate(doc.Element("rss").Element("channel"));
                        DateTime newUpdateTime = DateTime.Parse(newUpdate);
                        //DateTime lastUpdateTime = DateTime.Parse(provider.LastUpdate);
                        if (newUpdateTime > provider.LastUpdated)
                        {
                            //provider.LastUpdated = newUpdateTime;
                            IEnumerable<XElement> items = doc.Descendants("item");

                            foreach (XElement item in items)
                            {
                                StoryRecord newsItem = new StoryRecord
                                {
                                    Title = item.Element("title").Value,
                                    Description = item.Element("description").Value,
                                    Language = provider.Language,
                                    Url = item.Element("link").Value,
                                    ImageUrl = NosyBot.Services.Utilities.RssUtilities.GetImageUrl(item),
                                    ProviderId = provider.Id,
                                    ProviderName = provider.Name,
                                    LastUpdated = DateTime.Now,
                                    PublishDate = DateTime.Parse(item.Element("pubDate").Value)
                                };

                                if (newsItem.PublishDate > provider.LastPublished)
                                {
                                    // This came in after the last update, so add
                                    results.Add(newsItem);
                                }
                            }

                            provider.LastPublished = newUpdateTime;
                            provider.LastUpdated = DateTime.Now;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing provider {provider.Name}, error: {ex.Message}");
            }

            return results;
        }

        public static string GetImageUrl(XElement element)
        {
            XNamespace media = "http://search.yahoo.com/mrss/";

            string result = "";

            XElement mediaElem = element.Element(media + "content");
            if (mediaElem != null)
            {
                result = mediaElem.Attribute("url").Value;
            }
            else
            {
                // Get it like spiegel
                mediaElem = element.Element("enclosure");
                if (mediaElem != null)
                {
                    result = mediaElem.Attribute("url").Value;
                }
            }

            return result;
        }

        public static string GetPublishDate(XElement channel)
        {
            string result = "";

            XElement dateElem = channel.Element("lastBuildDate");
            if (dateElem != null)
            {
                result = dateElem.Value;
            }
            else
            {
                dateElem = channel.Element("date");
                if (dateElem != null)
                    result = dateElem.Value;
            }

            return result;
        }
    }
}
