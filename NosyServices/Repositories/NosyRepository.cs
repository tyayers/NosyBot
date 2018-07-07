using NosyCommon.Dtos;
using NosyServices.Dtos;
using NPoco;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace NosyServices.Repositories
{
    public class NosyRepository
    {
        public void InsertStory(Story story)
        {
            StoryRecord storyRec = (StoryRecord)story;
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
            con.Open();
            using (var db = new Database(con))
            {
                StoryRecord rec = (StoryRecord)story;

                //Story u = new Story()
                //{
                //    ProviderId = 1,
                //    Id = 1,
                //    TimeStamp = DateTime.Now,
                //    PublishDate = DateTime.Now,
                //    Title = "First news story ever!"
                //};

                db.Insert(rec);
            }

            con.Close();
        }
    }
}
