using FitnessLog.Models;
using FitnessLog.Utils;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace FitnessLog.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration config) : base(config) { }
        public UserProfile GetByFirebaseUserId(string firebaseUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT up.Id, Up.FirebaseUserId, up.[Name], 
                               up.Email, up.Age 
                          FROM UserProfile up
                         WHERE FirebaseUserId = @FirebaseUserId";

                    DbUtils.AddParameter(cmd, "@FirebaseUserId", firebaseUserId);

                    UserProfile userProfile = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userProfile = new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Age = DbUtils.GetInt(reader, "Age"),
                            Email = DbUtils.GetString(reader, "Email"),
                        };
                    }
                    reader.Close();

                    return userProfile;
                }
            }
        }

        public List<UserProfile> GetAllUsers()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT up.Id, Up.FirebaseUserId, up.[Name], 
                               up.Email, up.Age
                          FROM UserProfile up";

                    var userProfiles = new List<UserProfile>();


                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        userProfiles.Add(new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Age = DbUtils.GetInt(reader, "Age"),
                            Email = DbUtils.GetString(reader, "Email"),
                        });
                    }

                    return userProfiles;
                }
            }
        }


        public void Add(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO UserProfile (
                            [Name], Email, Age, FirebaseUserId)
                        OUTPUT INSERTED.ID
                        VALUES (
                            @name, @email, @age, @firebaseUserId)";
                    cmd.Parameters.AddWithValue("@name", userProfile.Name);
                    cmd.Parameters.AddWithValue("@email", userProfile.Email);
                    cmd.Parameters.AddWithValue("@age", userProfile.Age);
                    cmd.Parameters.AddWithValue("@firebaseUserId", userProfile.FirebaseUserId);

                    userProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                       UPDATE UserProfile
                          SET [Name] = @name, Email = @email, Age = @age, FirebaseUserId = @firebaseUserId
                        WHERE Id = @id";
                cmd.Parameters.AddWithValue("@name", userProfile.Name);
                cmd.Parameters.AddWithValue("@email", userProfile.Email);
                cmd.Parameters.AddWithValue("@age", userProfile.Age);
                cmd.Parameters.AddWithValue("@firebaseUserId", userProfile.FirebaseUserId);
                cmd.Parameters.AddWithValue("@id", userProfile.Id);
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
                    cmd.CommandText = "DELETE FROM CardioWorkout WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
