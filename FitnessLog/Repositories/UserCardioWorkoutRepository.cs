using FitnessLog.Models;
using FitnessLog.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace FitnessLog.Repositories
{
    public class UserCardioWorkoutRepository : BaseRepository, IUserCardioWorkoutRepository
    {
        public UserCardioWorkoutRepository(IConfiguration config) : base(config) { }
        public List<UserCardioWorkout> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
               SELECT uc.Id, uc.UserProfileId, uc.CardioWorkoutId,

                      up.[Name], up.Age, up.Email, up.FirebaseUserId,

                    cw.[Name], cw.[Minutes], cw.Speed, cw.TypeId,

                    wt.Type
                        
                 FROM UserCardioWorkout uc
                      LEFT JOIN CardioWorkout cw ON uc.CardioWorkoutId = cw.id
                      JOIN WorkoutType wt ON cw.TypeId = wt.id
                      JOIN UserProfile up ON uc.UserProfileId = up.id
            ";
                    var reader = cmd.ExecuteReader();

                    var userCardioWorkout = new List<UserCardioWorkout>();

                    while (reader.Read())
                    {
                       userCardioWorkout.Add(NewUserCardioWorkoutFromReader(reader)); 
                    };

                    return userCardioWorkout;
                }
            }
        }

        public UserCardioWorkout GetByFirebaseUserId(string firebaseUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT uc.Id, uc.UserProfileId, uc.CardioWorkoutId,

                      up.[Name], up.Age, up.Email, up.Minutes, up.FirebaseUserId,

                    cw.[Name], cw.Minutes, cw.Speed, cw.TypeId,

                    wt.Type
                        
                 FROM UserCardioWorkout uc
                      JOIN WorkoutType wt ON cw.TypeId = wt.Id
                      JOIN UserProfile up ON uc.UserProfileId = up.Id
                      JOIN CardioWorkout cw ON uc.CardioWorkoutId = cw.Id";

                    DbUtils.AddParameter(cmd, "@FirebaseUserId", firebaseUserId);

                    UserCardioWorkout userCardioWorkout = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userCardioWorkout = new UserCardioWorkout()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                            UserProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, ("UserProfileId")),
                                FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Age = DbUtils.GetInt(reader, "Age"),
                                Email = DbUtils.GetString(reader, "Email"),
                            },
                            CardioWorkoutId = DbUtils.GetInt(reader, "CardioWorkoutId"),
                            CardioWorkout = new CardioWorkout()
                            {
                                Id = DbUtils.GetInt(reader, "CardioWorkoutId"),
                                Name= DbUtils.GetString(reader, "Name"),
                                Minutes = DbUtils.GetInt(reader, "Minutes"),
                                Speed = DbUtils.GetInt(reader, "Speed"),
                                TypeId = DbUtils.GetInt(reader, "TypeId"),
                                WorkoutType = new WorkoutType()
                                {
                                    Id = DbUtils.GetInt(reader, "TypeId"),
                                    Type = DbUtils.GetString(reader, "Type"),
                                }
                            }
                        };
                        };
                        reader.Close();

                        return userCardioWorkout;
                    }
                }
            }

        public void Add(UserCardioWorkout userCardioWorkout)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO UserCardioWorkout (
                            us.UserProfileId, us.CardioWorkoutId

                        OUTPUT INSERTED.ID
                        VALUES (
                            @userProfileId, @userCardioWorkoutId)";
                    cmd.Parameters.AddWithValue("@userProfileId", userCardioWorkout.UserProfileId);
                    cmd.Parameters.AddWithValue("@cardiohWorkoutId", userCardioWorkout.CardioWorkoutId);

                    userCardioWorkout.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(UserCardioWorkout userCardioWorkout)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                       UPDATE UserCardioWorkout
                          SET UserProfileId = @userProfileId, CardioWorkoutId = @cardioWorkoutId
                        WHERE Id = @id";
                cmd.Parameters.AddWithValue("@userProfileId", userCardioWorkout.UserProfileId);
                cmd.Parameters.AddWithValue("@cardioWorkoutId", userCardioWorkout.CardioWorkoutId);
                cmd.Parameters.AddWithValue("@id", userCardioWorkout.Id);
                cmd.ExecuteNonQuery();

            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM UserCardioWorkout WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private UserCardioWorkout NewUserCardioWorkoutFromReader(SqlDataReader reader)
            {
                return new UserCardioWorkout()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                    UserProfile = new UserProfile()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                        FirebaseUserId = reader.GetString(reader.GetOrdinal("FirebaseUserId")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Age = reader.GetInt32(reader.GetOrdinal("Age")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                    },
                    CardioWorkoutId = reader.GetInt32(reader.GetOrdinal("CardioWorkoutId")),
                    CardioWorkout = new CardioWorkout()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("CardioWorkoutId")),
                        Name= reader.GetString(reader.GetOrdinal("Name")),
                        Minutes = reader.GetInt32(reader.GetOrdinal("Minutes")),
                        Speed = reader.GetInt32(reader.GetOrdinal("Speed")),
                        TypeId = reader.GetInt32(reader.GetOrdinal("TypeId")),
                        WorkoutType = new WorkoutType()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("TypeId")),
                            Type = reader.GetString(reader.GetOrdinal("Type")),
                        }
                    }
                };
            }
        }
    }

