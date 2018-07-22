using NosyBot.Services.Dtos;
using NPoco;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace NosyBot.Services.Repositories
{
    public class NosyRepository
    {
        public void InsertStory(StoryRecord story)
        {
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(System.Environment.GetEnvironmentVariable("NewsConnectionString", EnvironmentVariableTarget.Process));
                con.Open();
                using (var db = new Database(con))
                {
                    db.Insert(story);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("NosyRepo error in InsertStory. " + ex.ToString());
            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        public bool CheckIfStoryExists(StoryRecord story)
        {
            bool result = false;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(System.Environment.GetEnvironmentVariable("NewsConnectionString", EnvironmentVariableTarget.Process));
                con.Open();
                using (var db = new Database(con))
                {
                    List<StoryRecord> results = db.Fetch<StoryRecord>($"where Url=N'{story.Url}'");
                    if (results != null && results.Count > 0)
                        result = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("NosyRepo error in GetProviders. " + ex.ToString());
            }
            finally
            {
                if (con != null) con.Close();
            }
            return result;
        }

        public List<ProviderRecord> GetProviders()
        {
            SqlConnection con = null;
            List<ProviderRecord> results = null; ;
            try
            {
                con = new SqlConnection(System.Environment.GetEnvironmentVariable("NewsConnectionString", EnvironmentVariableTarget.Process));
                con.Open();
                using (var db = new Database(con))
                {
                    results = db.Fetch<ProviderRecord>();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("NosyRepo error in GetProviders. " + ex.ToString());
            }
            finally
            {
                if (con != null) con.Close();
            }

            return results;
        }

        public void UpdateProvider(ProviderRecord provider)
        {
            SqlConnection con = null;
            List<ProviderRecord> results = null; ;
            try
            {
                con = new SqlConnection(System.Environment.GetEnvironmentVariable("NewsConnectionString", EnvironmentVariableTarget.Process));
                con.Open();
                using (var db = new Database(con))
                {
                    db.Update(provider);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("NosyRepo error in GetProviders. " + ex.ToString());
            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        public List<StoryRecord> GetLatestStoriesByProvider(int ProviderId)
        {
            SqlConnection con = null;
            List<StoryRecord> results = null; ;
            try
            {
                con = new SqlConnection(System.Environment.GetEnvironmentVariable("NewsConnectionString", EnvironmentVariableTarget.Process));
                con.Open();
                using (var db = new Database(con))
                {
                    results = db.Fetch<StoryRecord>($"SELECT TOP(5) * FROM Stories WHERE Stories.ProviderId = {ProviderId} ORDER BY PublishDate DESC");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("NosyRepo error in GetProviders. " + ex.ToString());
            }
            finally
            {
                if (con != null) con.Close();
            }

            return results;
        }

        public List<StoryRecord> GetLatestStoriesByRegion(string Region)
        {
            SqlConnection con = null;
            List<StoryRecord> results = null; ;
            try
            {
                con = new SqlConnection(System.Environment.GetEnvironmentVariable("NewsConnectionString", EnvironmentVariableTarget.Process));
                con.Open();
                using (var db = new Database(con))
                {
                    results = db.Fetch<StoryRecord>($"SELECT TOP(5) * FROM Stories WHERE Stories.Region = N'{Region}' ORDER BY PublishDate DESC");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("NosyRepo error in GetProviders. " + ex.ToString());
            }
            finally
            {
                if (con != null) con.Close();
            }

            return results;
        }

        public List<StoryRecord> GetLatestStoriesForCountry(string Country, int Number)
        {
            SqlConnection con = null;
            List<StoryRecord> results = new List<StoryRecord>();
            try
            {
                con = new SqlConnection(System.Environment.GetEnvironmentVariable("NewsConnectionString", EnvironmentVariableTarget.Process));
                con.Open();
                using (var db = new Database(con))
                {
                    results = db.Fetch<StoryRecord>($"SELECT TOP({Number}) * FROM Stories INNER JOIN Providers ON Stories.ProviderId = Providers.Id WHERE Providers.Country = N'{Country}' ORDER BY Stories.PublishDate DESC");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("NosyRepo error in GetLatestStoriesForCountry. " + ex.ToString());
            }
            finally
            {
                if (con != null) con.Close();
            }

            return results;
        }

        public WorldStoryRecords GetRegionStories(int NumberPerRegion)
        {
            SqlConnection con = null;
            WorldStoryRecords results = new WorldStoryRecords();
            try
            {
                con = new SqlConnection(System.Environment.GetEnvironmentVariable("NewsConnectionString", EnvironmentVariableTarget.Process));
                con.Open();
                using (var db = new Database(con))
                {
                    results.Americas = db.Fetch<StoryRecord>($"SELECT * FROM Providers CROSS APPLY	(SELECT TOP ({NumberPerRegion}) * FROM Stories WHERE Providers.Region = N'americas' AND Stories.ProviderId = Providers.Id ORDER BY Stories.PublishDate DESC) AS st ORDER BY st.PublishDate DESC");
                    results.EMEA = db.Fetch<StoryRecord>($"SELECT * FROM Providers CROSS APPLY	(SELECT TOP ({NumberPerRegion}) * FROM Stories WHERE Providers.Region = N'emea' AND Stories.ProviderId = Providers.Id ORDER BY Stories.PublishDate DESC) AS st ORDER BY st.PublishDate DESC");
                    results.APAC = db.Fetch<StoryRecord>($"SELECT * FROM Providers CROSS APPLY	(SELECT TOP ({NumberPerRegion}) * FROM Stories WHERE Providers.Region = N'apac' AND Stories.ProviderId = Providers.Id ORDER BY Stories.PublishDate DESC) AS st ORDER BY st.PublishDate DESC");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("NosyRepo error in GetLatestStories. " + ex.ToString());
            }
            finally
            {
                if (con != null) con.Close();
            }

            return results;
        }

        public ContinentStoryRecords GetContinentStories(int NumberPerContinent)
        {
            SqlConnection con = null;
            ContinentStoryRecords results = new ContinentStoryRecords();
            try
            {
                con = new SqlConnection(System.Environment.GetEnvironmentVariable("NewsConnectionString", EnvironmentVariableTarget.Process));
                con.Open();
                using (var db = new Database(con))
                {
                    results.NorthAmerica = db.Fetch<StoryRecord>($"SELECT * FROM Providers CROSS APPLY	(SELECT TOP ({NumberPerContinent}) * FROM Stories WHERE Providers.Continent = N'North America' AND Stories.ProviderId = Providers.Id ORDER BY Stories.PublishDate DESC) AS st ORDER BY st.PublishDate DESC");
                    results.SouthAmerica = db.Fetch<StoryRecord>($"SELECT * FROM Providers CROSS APPLY	(SELECT TOP ({NumberPerContinent}) * FROM Stories WHERE Providers.Continent = N'South America' AND Stories.ProviderId = Providers.Id ORDER BY Stories.PublishDate DESC) AS st ORDER BY st.PublishDate DESC");
                    results.Europe = db.Fetch<StoryRecord>($"SELECT * FROM Providers CROSS APPLY	(SELECT TOP ({NumberPerContinent}) * FROM Stories WHERE Providers.Continent = N'Europe' AND Stories.ProviderId = Providers.Id ORDER BY Stories.PublishDate DESC) AS st ORDER BY st.PublishDate DESC");
                    results.Asia = db.Fetch<StoryRecord>($"SELECT * FROM Providers CROSS APPLY	(SELECT TOP ({NumberPerContinent}) * FROM Stories WHERE Providers.Continent = N'Asia' AND Stories.ProviderId = Providers.Id ORDER BY Stories.PublishDate DESC) AS st ORDER BY st.PublishDate DESC");
                    results.Africa = db.Fetch<StoryRecord>($"SELECT * FROM Providers CROSS APPLY	(SELECT TOP ({NumberPerContinent}) * FROM Stories WHERE Providers.Continent = N'Africa' AND Stories.ProviderId = Providers.Id ORDER BY Stories.PublishDate DESC) AS st ORDER BY st.PublishDate DESC");
                    results.Australia = db.Fetch<StoryRecord>($"SELECT * FROM Providers CROSS APPLY	(SELECT TOP ({NumberPerContinent}) * FROM Stories WHERE Providers.Continent = N'Australia' AND Stories.ProviderId = Providers.Id ORDER BY Stories.PublishDate DESC) AS st ORDER BY st.PublishDate DESC");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("NosyRepo error in GetContinentStories. " + ex.ToString());
            }
            finally
            {
                if (con != null) con.Close();
            }

            return results;
        }
    }
}
