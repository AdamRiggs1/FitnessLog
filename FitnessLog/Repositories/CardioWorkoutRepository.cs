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

namespace FitnessLog.Repositories
{
    public class CardioWorkoutRepository : BaseRepository, ICardioWorkoutRepository
    {
        public CardioWorkoutRepository(IConfiguration config) : base(config) { }
        public List<CardioWorkout> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
               SELECT cw.Id, cw.[Name], cw.Minutes, cw.Speed, cw.TypeId,

                      wt.Type
                        
                 FROM CardioWorkout cw
                      JOIN WorkoutType wt ON cw.TypeId = wt.Id
            ";
                    var reader = cmd.ExecuteReader();

                    var cardioWorkout = new List<CardioWorkout>();

                    while (reader.Read())
                    {
                        cardioWorkout.Add(NewCardioWorkoutFromReader(reader));
                    }

                    reader.Close();

                    return cardioWorkout;
                }
            }
        }

        public CardioWorkout GetCardioWorkoutById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
               SELECT cw.Id, cw.[Name], cw.Minutes, cw.Speed, cw.TypeId,

                      wt.Type
                        
                 FROM CardioWorkout cw
                      JOIN WorkoutType wt ON cw.TypeId = wt.Id

                 WHERE cw.Id = @id
            ";
                    cmd.Parameters.AddWithValue("@id", id);

                    CardioWorkout cardioWorkout = null;
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        cardioWorkout = new CardioWorkout()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Minutes = reader.GetInt32(reader.GetOrdinal("Minutes")),
                            Speed = reader.GetInt32(reader.GetOrdinal("Speed")),
                            TypeId = reader.GetInt32(reader.GetOrdinal("TypeId")),
                            WorkoutType = new WorkoutType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Type = reader.GetString(reader.GetOrdinal("Type"))
                            }
                        };
                    }

                    reader.Close();

                    return cardioWorkout;
                }
            }
        }

        public void Add(CardioWorkout cardioWorkout)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO CardioWorkout (
                            [Name], Minutes, Speed, TypeId)
                        OUTPUT INSERTED.ID
                        VALUES (
                            @name, @minutes, @speed, @typeId)";
                    cmd.Parameters.AddWithValue("@name", cardioWorkout.Name);
                    cmd.Parameters.AddWithValue("@minutes", cardioWorkout.Minutes);
                    cmd.Parameters.AddWithValue("@speed", cardioWorkout.Speed);
                    cmd.Parameters.AddWithValue("@typeId", cardioWorkout.TypeId);

                    cardioWorkout.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(CardioWorkout cardioWorkout)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       UPDATE CardioWorkout
                          SET [Name] = @name, Minutes = @minutes, Speed = @speed, TypeId = @typeId
                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@name", cardioWorkout.Name);
                    cmd.Parameters.AddWithValue("@minutes", cardioWorkout.Minutes);
                    cmd.Parameters.AddWithValue("@speed", cardioWorkout.Speed);
                    cmd.Parameters.AddWithValue("@TypeId", cardioWorkout.TypeId);
                    cmd.Parameters.AddWithValue("@id", cardioWorkout.Id);
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
                    cmd.CommandText = "DELETE FROM CardioWorkout WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private CardioWorkout NewCardioWorkoutFromReader(SqlDataReader reader)
    {
        return new CardioWorkout()
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Minutes = reader.GetInt32(reader.GetOrdinal("Minutes")),
            Speed = reader.GetInt32(reader.GetOrdinal("Speed")),
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
