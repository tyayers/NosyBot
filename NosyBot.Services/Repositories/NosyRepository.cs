using NosyBot.Common.Dtos;
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
            SqlConnection con = new SqlConnection(System.Environment.GetEnvironmentVariable("NewsConnectionString", EnvironmentVariableTarget.Process));
            con.Open();
            using (var db = new Database(con))
            {
                //Story u = new Story()
                //{
                //    ProviderId = 1,
                //    Id = 1,
                //    TimeStamp = DateTime.Now,
                //    PublishDate = DateTime.Now,
                //    Title = "First news story ever!"
                //};

                db.Insert(story);
            }

            con.Close();
        }
    }
}
