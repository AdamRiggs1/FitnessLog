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
    public class FoodRepository : BaseRepository, IFoodRepository
    {
        public FoodRepository(IConfiguration config) : base(config) { }
        public List<Food> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
               SELECT f.Id, f.[Name], f.Calories, f.Carbohydrates, f.Protein, f.Fat
                        
                 FROM Food f
            ";
                    var reader = cmd.ExecuteReader();

                    var food = new List<Food>();

                    while (reader.Read())
                    {
                        food.Add(NewFoodFromReader(reader));
                    }

                    reader.Close();

                    return food;
                }
            }
        }

        public Food GetFoodById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
               SELECT f.Id, f.[Name], f.Calories, f.Carbohydrates, f.Protein, f.Fat
                        
                 FROM Food f

                 WHERE f.Id = @id
            ";
                    cmd.Parameters.AddWithValue("@id", id);

                    Food food = null;
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        food = new Food()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Calories = reader.GetInt32(reader.GetOrdinal("Calories")),
                            Carbohydrates = reader.GetInt32(reader.GetOrdinal("Carbohyrdates")),
                            Protein = reader.GetInt32(reader.GetOrdinal("Protein")),
                            Fat = reader.GetInt32(reader.GetOrdinal("Fat"))
                        };
                    }

                    reader.Close();

                    return food;
                }
            }
        }

        public void Add(Food food)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Food (
                            [Name], Calories, Carbohydrates, Protein, Fat)
                        OUTPUT INSERTED.ID
                        VALUES (
                            @name, @calories, @carbohydrates, @protein, @fat)";
                    cmd.Parameters.AddWithValue("@name", food.Name);
                    cmd.Parameters.AddWithValue("@calories", food.Calories);
                    cmd.Parameters.AddWithValue("@carbohydrates", food.Carbohydrates);
                    cmd.Parameters.AddWithValue("@protein", food.Protein);
                    cmd.Parameters.AddWithValue("@fat", food.Fat);

                    food.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Food food)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                       UPDATE Food
                          SET [Name] = @name, Calories = @calories, Carbohydrates = @carbohydrates, Protein = @protein, Fat = @fat
                        WHERE Id = @id";
                cmd.Parameters.AddWithValue("@name", food.Name);
                cmd.Parameters.AddWithValue("@calories", food.Calories);
                cmd.Parameters.AddWithValue("@carbohydrates", food.Carbohydrates);
                cmd.Parameters.AddWithValue("@protein", food.Protein);
                cmd.Parameters.AddWithValue("@fat", food.Fat);
                cmd.Parameters.AddWithValue("@id", food.Id);
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
                    cmd.CommandText = "DELETE FROM Food WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Food NewFoodFromReader(SqlDataReader reader)
        {
            return new Food()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Calories = reader.GetInt32(reader.GetOrdinal("Calories")),
                Carbohydrates = reader.GetInt32(reader.GetOrdinal("Carbohyrdates")),
                Protein = reader.GetInt32(reader.GetOrdinal("Protein")),
                Fat = reader.GetInt32(reader.GetOrdinal("Fat"))
            };
        }

    }
}
