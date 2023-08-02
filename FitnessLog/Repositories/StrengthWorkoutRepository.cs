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
    public class StrengthWorkoutRepository : BaseRepository, IStrengthWorkoutRepository
    {
        public StrengthWorkoutRepository(IConfiguration config) : base(config) { }
        public List<StrengthWorkout> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
               SELECT sw.Id, sw.[Name], sw.Weight, sw.Reps, sw.Sets, sw.TypeId,

                      wt.Type
                        
                 FROM StrengthWorkout sw
                      JOIN WorkoutType wt ON sw.TypeId = wt.Id
            ";
                    var reader = cmd.ExecuteReader();

                    var strengthWorkout = new List<StrengthWorkout>();

                    while (reader.Read())
                    {
                        strengthWorkout.Add(NewStrengthWorkoutFromReader(reader));
                    }

                    reader.Close();

                    return strengthWorkout;
                }
            }
        }

        public StrengthWorkout GetStrengthWorkoutById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
               SELECT sw.Id, sw.[Name], sw.Weight, sw.Reps, sw.Sets, sw.TypeId,

                      wt.Type
                        
                 FROM StrengthWorkout sw
                      JOIN WorkoutType wt ON sw.TypeId = wt.Id

                 WHERE sw.Id = @id
            ";
                    cmd.Parameters.AddWithValue("@id", id);

                    StrengthWorkout strengthWorkout = null;
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        strengthWorkout = new StrengthWorkout()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Weight = reader.GetInt32(reader.GetOrdinal("Weight")),
                            Reps = reader.GetInt32(reader.GetOrdinal("Reps")),
                            Sets = reader.GetInt32(reader.GetOrdinal("Sets")),
                            TypeId = reader.GetInt32(reader.GetOrdinal("TypeId")),
                            WorkoutType = new WorkoutType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Type = reader.GetString(reader.GetOrdinal("Type"))
                            }
                        };
                    }

                    reader.Close();

                    return strengthWorkout;
                }
            }
        }

        public void Add(StrengthWorkout strengthWorkout)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO StrengthWorkout (
                            [Name], Reps, Sets, Weight, TypeId)
                        OUTPUT INSERTED.ID
                        VALUES (
                            @name, @reps, @sets, @weight, @typeId)";
                    cmd.Parameters.AddWithValue("@name", strengthWorkout.Name);
                    cmd.Parameters.AddWithValue("@reps", strengthWorkout.Reps);
                    cmd.Parameters.AddWithValue("@sets", strengthWorkout.Sets);
                    cmd.Parameters.AddWithValue("@weight", strengthWorkout.Weight);
                    cmd.Parameters.AddWithValue("@typeId", strengthWorkout.TypeId);

                    strengthWorkout.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(StrengthWorkout strengthWorkout)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                       UPDATE StrengthWorkout
                          SET [Name] = @name, Reps = @reps, Sets = @sets, Weight = @weight, TypeId = @typeId
                        WHERE Id = @id";
                cmd.Parameters.AddWithValue("@name", strengthWorkout.Name);
                cmd.Parameters.AddWithValue("@reps", strengthWorkout.Reps);
                cmd.Parameters.AddWithValue("@sets", strengthWorkout.Sets);
                cmd.Parameters.AddWithValue("@weight", strengthWorkout.Weight);
                cmd.Parameters.AddWithValue("@TypeId", strengthWorkout.TypeId);
                cmd.Parameters.AddWithValue("@id", strengthWorkout.Id);
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
                    cmd.CommandText = "DELETE FROM StrengthWorkout WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private StrengthWorkout NewStrengthWorkoutFromReader(SqlDataReader reader)
        {
            return new StrengthWorkout()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Reps = reader.GetInt32(reader.GetOrdinal("Reps")),
                Sets = reader.GetInt32(reader.GetOrdinal("Sets")),
                Weight = reader.GetInt32(reader.GetOrdinal("Weight")),
                TypeId = reader.GetInt32(reader.GetOrdinal("TypeId")),
                WorkoutType = new WorkoutType()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Type = reader.GetString(reader.GetOrdinal("Type"))
                },
            };
        }

    }
}
