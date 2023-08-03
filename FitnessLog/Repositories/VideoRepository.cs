using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using FitnessLog.Models;
using FitnessLog.Repositories;
using FitnessLog.Utils;
using Microsoft.Extensions.Hosting;
using Azure;

namespace FitnessLog.Repositories
{

    public class VideoRepository : BaseRepository, IVideoRepository
    {
        public VideoRepository(IConfiguration configuration) : base(configuration) { }

        public List<Video> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
               SELECT v.Id, v.VideoUrl, v.UserProfileId,

                      up.[Name], up.Email, up.Age, up.FirebaseUserId
                        
                 FROM Video v 
                      JOIN UserProfile up ON v.UserProfileId = up.Id
            ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        var videos = new List<Video>();
                        while (reader.Read())
                        {
                            videos.Add(new Video()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                VideoUrl = DbUtils.GetString(reader, "VideoUrl"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                UserProfile = new UserProfile()
                                {
                                    Id = DbUtils.GetInt(reader, "UserProfileId"),
                                    Name = DbUtils.GetString(reader, "Name"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    Age = DbUtils.GetInt(reader, "Age"),
                                    FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId")
                                },
                            });
                        }

                        return videos;
                    }
                }
            }
        }

        public Video GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                     SELECT v.Id, v.VideoUrl, v.UserProfileId,

                      up.[Name], up.Email, up.Age, up.FirebaseUserId
                        
                 FROM Video v 
                      JOIN UserProfile up ON v.UserProfileId = up.Id
                  WHERE Id = @Id
            ";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        Video video = null;
                        if (reader.Read())
                        {
                            video = new Video()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                VideoUrl = DbUtils.GetString(reader, "VideoUrl"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId")
                            };
                        }

                        return video;
                    }
                }
            }
        }

        public List<Video> CurrentUsersVideo(string firebaseUserId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT v.Id, v.VideoUrl, v.UserProfileId,

                      up.[Name], up.Email, up.Age, up.FirebaseUserId
                        
                 FROM Video v 
                      JOIN UserProfile up ON v.UserProfileId = up.Id
                  WHERE Id = @Id
                    ";
                    cmd.Parameters.AddWithValue("@firebaseUserId", firebaseUserId);

                    var reader = cmd.ExecuteReader();

                    var userFood = new List<Video>();

                    {

                        var videos = new List<Video>();
                        while (reader.Read())
                        {
                            videos.Add(new Video()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                VideoUrl = DbUtils.GetString(reader, "VideoUrl"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                UserProfile = new UserProfile()
                                {
                                    Id = DbUtils.GetInt(reader, "UserProfileId"),
                                    Name = DbUtils.GetString(reader, "Name"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    Age = DbUtils.GetInt(reader, "Age"),
                                    FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId")
                                },
                            });
                        }

                        return videos;
                    }
                }
            }
        }

        public Video GetByFirebaseUserId(string firebaseUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT v.Id, v.UserProfileId, v.VideoUrl,

                      up.[Name], up.Age, up.Email, up.FirebaseUserId
                        
                 FROM Video v
                      JOIN UserProfile up ON v.UserProfileId = up.Id";

                    DbUtils.AddParameter(cmd, "@FirebaseUserId", firebaseUserId);

                    Video video = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        video = new Video()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            VideoUrl = DbUtils.GetString(reader, "VideoUrl"),
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                            UserProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, ("UserProfileId")),
                                FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Age = DbUtils.GetInt(reader, "Age"),
                                Email = DbUtils.GetString(reader, "Email"),
                            }
                        };
                    };
                    reader.Close();

                    return video;
                }
            }
        }


        public void Add(Video video)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Video (VideoUrl, UserProfileId)
                        OUTPUT INSERTED.ID
                        VALUES (@VideoUrl, @UserProfileId)";

                    DbUtils.AddParameter(cmd, "@VideoUrl", video.VideoUrl);
                    DbUtils.AddParameter(cmd, "@UserProfileId", video.UserProfileId);

                    video.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Video video)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Video
                           SET VideoUrl = @VideoUrl,
                               UserProfileId = @UserProfileId
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@VideoUrl", video.VideoUrl);
                    DbUtils.AddParameter(cmd, "@UserProfileId", video.UserProfileId);
                    DbUtils.AddParameter(cmd, "@Id", video.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Video WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
