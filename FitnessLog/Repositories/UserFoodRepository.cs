using FitnessLog.Models;
using FitnessLog.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace FitnessLog.Repositories
{
    public class UserFoodRepository : BaseRepository, IUserFoodRepository
    {
        public UserFoodRepository(IConfiguration config) : base(config) { }
        public List<UserFood> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
               SELECT uf.Id, uf.UserProfileId, uf.FoodId,
                      up.[Name], up.Age, up.Email, up.FirebaseUserId,

                    f.[Name] AS FoodName, f.Calories, f.Carbohydrates, f.Calories, f.Fat
                        
                 FROM UserFood uf
                      LEFT JOIN Food f ON uf.FoodId = f.id
                      LEFT JOIN UserProfile up ON uf.UserProfileId = up.id
            ";
                    var reader = cmd.ExecuteReader();

                    var userFood = new List<UserFood>();

                    while (reader.Read())
                    {
                        userFood.Add(NewUserFoodFromReader(reader));
                    }


                    return userFood;
                }
            }
        }

        public UserFood GetByFirebaseUserId(string firebaseUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT uf.Id, uf.UserProfileId, uf.FoodId,

                      up.[Name], up.Age, up.Email, up.FirebaseUserId,

                    f.[Name] AS FoodName, f.Calories, f.Carbohydrates, f.Protein, f.Fat
                        
                 FROM UserFood uf
                      JOIN UserProfile up ON uf.UserProfileId = up.Id
                      JOIN Food f ON uf.FoodId = f.Id";

                    DbUtils.AddParameter(cmd, "@FirebaseUserId", firebaseUserId);

                    UserFood userFood = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userFood = new UserFood()
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
                            FoodId = reader.GetInt32(reader.GetOrdinal("FoodId")),
                            Food = new Food()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("StrengthWorkoutId")),
                                Name= reader.GetString(reader.GetOrdinal("StrengthWorkoutName")),
                                Calories = reader.GetInt32(reader.GetOrdinal("Calories")),
                                Carbohydrates = reader.GetInt32(reader.GetOrdinal("Carbohydrates")),
                                Protein = reader.GetInt32(reader.GetOrdinal("Protein")),
                                Fat = reader.GetInt32(reader.GetOrdinal("Fat"))
                            }
                        };
                    };
                    reader.Close();

                    return userFood;
                }
            }
        }

        public List<UserFood> CurrentUsersFood(string firebaseUserId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                      SELECT uf.Id, uf.UserProfileId, uf.FoodId,

                      up.[Name], up.Age, up.Email, up.FirebaseUserId,

                    f.[Name] AS FoodName, f.Calories, f.Carbohydrates, f.Protein, f.Fat
                        
                 FROM UserFood uf
                      JOIN UserProfile up ON uf.UserProfileId = up.Id
                      JOIN Food f ON uf.FoodId = f.Id"";

                    WHERE up.FirebaseUserId = @firebaseUserId
                    ";

                    cmd.Parameters.AddWithValue("@firebaseUserId", firebaseUserId);

                    var reader = cmd.ExecuteReader();

                    var userFood = new List<UserFood>();

                    while (reader.Read())
                    {
                        userFood.Add(NewUserFoodFromReader(reader));
                    }

                    reader.Close();

                    return userFood;
                }
            }
        }

        public void Add(UserFood userFood)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO UserFood (
                            uf.UserProfileId, uf.FoodId,

                        OUTPUT INSERTED.ID
                        VALUES (
                            @userProfileId, @userStrengthWorkoutId)";
                    cmd.Parameters.AddWithValue("@userProfileId", userFood.UserProfileId);
                    cmd.Parameters.AddWithValue("@foodId", userFood.FoodId);

                    userFood.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(UserFood userFood)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                       UPDATE UserFood
                          SET UserProfileId = @userProfileId, FoodId = @foodId
                        WHERE Id = @id";
                cmd.Parameters.AddWithValue("@userProfileId", userFood.UserProfileId);
                cmd.Parameters.AddWithValue("@foodId", userFood.FoodId);
                cmd.Parameters.AddWithValue("@id", userFood.Id);
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
                    cmd.CommandText = "DELETE FROM UserFood WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private UserFood NewUserFoodFromReader(SqlDataReader reader)
        {
            return new UserFood()
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
                FoodId = reader.GetInt32(reader.GetOrdinal("FoodId")),
                Food = new Food()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("StrengthWorkoutId")),
                    Name= reader.GetString(reader.GetOrdinal("StrengthWorkoutName")),
                    Calories = reader.GetInt32(reader.GetOrdinal("Calories")),
                    Carbohydrates = reader.GetInt32(reader.GetOrdinal("Carbohydrates")),
                    Protein = reader.GetInt32(reader.GetOrdinal("Protein")),
                    Fat = reader.GetInt32(reader.GetOrdinal("Fat"))
                }
            };
        }
    }
}

