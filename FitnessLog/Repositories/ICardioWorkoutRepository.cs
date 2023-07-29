﻿using FitnessLog.Models;
using System.Collections.Generic;

namespace FitnessLog.Repositories
{
    public interface ICardioWorkoutRepository
    {
        List<CardioWorkout> GetAll();
        void Add(CardioWorkout cardioWorkout);

        void Update(CardioWorkout cardioWorkout);

        void Delete(int id);
    }
}