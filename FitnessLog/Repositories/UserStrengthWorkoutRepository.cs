using FitnessLog.Models;
using FitnessLog.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace FitnessLog.Repositories
{
    public class UserStrengthWorkoutRepository : BaseRepository, IUserStrengthWorkoutRepository
    {
        public UserStrengthWorkoutRepository(IConfiguration config) : base(config) { }
        public List<UserStrengthWorkout> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
               SELECT us.Id, us.UserProfileId, us.StrengthWorkoutId,
                      up.[Name], up.Age, up.Email, up.FirebaseUserId,

                    sw.[Name] AS StrengthWorkoutName, sw.Reps, sw.Sets, sw.Weight, sw.TypeId,

                    wt.Type
                        
                 FROM UserStrengthWorkout us
                      LEFT JOIN StrengthWorkout sw ON us.StrengthWorkoutId = sw.id
                      JOIN WorkoutType wt ON sw.TypeId = wt.id
                      LEFT JOIN UserProfile up ON us.UserProfileId = up.id
            ";
                    var reader = cmd.ExecuteReader();

                    var userStrengthWorkout = new List<UserStrengthWorkout>();

                    while (reader.Read())
                    {
                        userStrengthWorkout.Add(NewUserStrengthWorkoutFromReader(reader));
                    }
             

                    return userStrengthWorkout;
                }
            }
        }

        public UserStrengthWorkout GetByFirebaseUserId(string firebaseUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT us.Id, us.UserProfileId, us.StrengthWorkoutId,

                      up.[Name], up.Age, up.Email, up.FirebaseUserId,

                    sw.[Name] AS StrengthWorkoutName, sw.Reps, sw.Sets, sw.Weight, sw.TypeId,

                    wt.Type
                        
                 FROM UserStrengthWorkout us
                      JOIN WorkoutType wt ON sw.TypeId = wt.Id
                      JOIN UserProfile up ON us.UserProfileId = up.Id
                      JOIN StrengthWorkout sw ON us.StrengthWorkoutId = sw.Id";

                    DbUtils.AddParameter(cmd, "@FirebaseUserId", firebaseUserId);

                    UserStrengthWorkout userStrengthWorkout = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userStrengthWorkout = new UserStrengthWorkout()
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
                            StrengthWorkoutId = DbUtils.GetInt(reader, "CardioWorkoutId"),
                            StrengthWorkout = new StrengthWorkout()
                            {
                                Id = DbUtils.GetInt(reader, "CardioWorkoutId"),
                                Name= DbUtils.GetString(reader, "StrengthWorkoutName"),
                                Reps = DbUtils.GetInt(reader, "Reps"),
                                Sets = DbUtils.GetInt(reader, "Sets"),
                                Weight = DbUtils.GetInt(reader, "Weight"),
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

                    return userStrengthWorkout;
                }
            }
        }

        public List<UserStrengthWorkout> CurrentUsersStrengthWorkouts(string firebaseUserId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                      SELECT us.Id, us.UserProfileId, us.StrengthWorkoutId,
                      up.[Name], up.Age, up.Email, up.FirebaseUserId,

                    sw.[Name] AS StrengthWorkoutName, sw.Reps, sw.Sets, sw.Weight, sw.TypeId,

                    wt.Type
                        
                 FROM UserStrengthWorkout us
                      LEFT JOIN StrengthWorkout sw ON us.StrengthWorkoutId = sw.id
                      JOIN WorkoutType wt ON sw.TypeId = wt.id
                      LEFT JOIN UserProfile up ON us.UserProfileId = up.id

                    WHERE up.FirebaseUserId = @firebaseUserId
                    ";

                    cmd.Parameters.AddWithValue("@firebaseUserId", firebaseUserId);

                    var reader = cmd.ExecuteReader();

                    var userStrengthWorkouts = new List<UserStrengthWorkout>();

                    while (reader.Read())
                    {
                        userStrengthWorkouts.Add(NewUserStrengthWorkoutFromReader(reader));
                    }

                    reader.Close();

                    return userStrengthWorkouts;
                }
            }
        }

        public void Add(UserStrengthWorkout userStrengthWorkout)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO UserStrengthWorkout (
                            UserProfileId, StrengthWorkoutId)

                        OUTPUT INSERTED.Id
                        VALUES (
                            @userProfileId, @strengthWorkoutId)";
                    cmd.Parameters.AddWithValue("@userProfileId", userStrengthWorkout.UserProfileId);
                    cmd.Parameters.AddWithValue("@strengthWorkoutId", userStrengthWorkout.StrengthWorkoutId);

                    userStrengthWorkout.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(UserStrengthWorkout userStrengthWorkout)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                       UPDATE UserStrengthWorkout
                          SET UserProfileId = @userProfileId, StrengthWorkoutId = @strengthWorkoutId
                        WHERE Id = @id";
                cmd.Parameters.AddWithValue("@userProfileId", userStrengthWorkout.UserProfileId);
                cmd.Parameters.AddWithValue("@strengthWorkout", userStrengthWorkout.StrengthWorkoutId);
                cmd.Parameters.AddWithValue("@id", userStrengthWorkout.Id);
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
                    cmd.CommandText = "DELETE FROM UserStrengthWorkout WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private UserStrengthWorkout NewUserStrengthWorkoutFromReader(SqlDataReader reader)
        {
            return new UserStrengthWorkout()
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
                StrengthWorkoutId = reader.GetInt32(reader.GetOrdinal("StrengthWorkoutId")),
                StrengthWorkout = new StrengthWorkout()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("StrengthWorkoutId")),
                    Name= reader.GetString(reader.GetOrdinal("StrengthWorkoutName")),
                    Reps = reader.GetInt32(reader.GetOrdinal("Reps")),
                    Sets = reader.GetInt32(reader.GetOrdinal("Sets")),
                    Weight = reader.GetInt32(reader.GetOrdinal("Weight")),
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
