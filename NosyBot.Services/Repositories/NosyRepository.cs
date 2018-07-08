﻿using NosyBot.Services.Dtos;
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

        public WorldStoryRecords GetLatestStories(int NumberPerRegion)
        {
            SqlConnection con = null;
            WorldStoryRecords results = new WorldStoryRecords();
            try
            {
                con = new SqlConnection(System.Environment.GetEnvironmentVariable("NewsConnectionString", EnvironmentVariableTarget.Process));
                con.Open();
                using (var db = new Database(con))
                {
                    results.Americas = db.Fetch<StoryRecord>($"SELECT TOP({NumberPerRegion}) * FROM Stories INNER JOIN Providers ON Stories.ProviderId = Providers.Id WHERE Providers.Region = N'americas' ORDER BY Stories.PublishDate DESC");
                    results.EMEA = db.Fetch<StoryRecord>($"SELECT TOP({NumberPerRegion}) * FROM Stories INNER JOIN Providers ON Stories.ProviderId = Providers.Id WHERE Providers.Region = N'emea' ORDER BY Stories.PublishDate DESC");
                    results.APAC = db.Fetch<StoryRecord>($"SELECT TOP({NumberPerRegion}) * FROM Stories INNER JOIN Providers ON Stories.ProviderId = Providers.Id WHERE Providers.Region = N'apac' ORDER BY Stories.PublishDate DESC");
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
    }
}
