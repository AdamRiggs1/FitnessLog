using FitnessLog.Models;
using System.Collections.Generic;

namespace FitnessLog.Repositories
{
    public interface IFoodRepository
    {
        void Add(Food food);
        void Delete(int id);
        List<Food> GetAll();
        Food GetFoodById(int id);
        void Update(Food food);
    }
}