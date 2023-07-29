using FitnessLog.Models;
using FitnessLog.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace FitnessLog.Repositories
{
    public class WorkoutTypeRepository : BaseRepository, IWorkoutTypeRepository
    {
        public WorkoutTypeRepository(IConfiguration config) : base(config) { }
        public List<WorkoutType> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
               SELECT wt.Id, wt.Type
                        
                 FROM WorkoutType wt
            ";
                    var reader = cmd.ExecuteReader();

                    var workoutType = new List<WorkoutType>();

                    while (reader.Read())
                    {
                        workoutType.Add(new WorkoutType()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Type = reader.GetString(reader.GetOrdinal("Type"))
                        });
                    };

                    return workoutType;
                }
            }
        }
    }
}
