using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FitnessLog.Repositories;
using FitnessLog.Models;
using Microsoft.Extensions.Hosting;
using Azure;

namespace FitnessLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodRepository _foodRepository;
        public FoodController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_foodRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var getFoodId = _foodRepository.GetFoodById(id);
            if (getFoodId == null)
            {
                return NotFound();
            }
            return Ok(getFoodId);
        }

        [HttpPost]
        public IActionResult AddFood(Food food)
        {
            _foodRepository.Add(food);
            return Ok(_foodRepository.GetAll());
        }

        [HttpPut]
        public IActionResult EditStrengthWorkout(Food food)
        {
            _foodRepository.Update(food);
            return Ok(_foodRepository.GetAll());
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStrengthWorkout(int id)
        {
            _foodRepository.Delete(id);
            return Ok(_foodRepository.GetAll());
        }
    }
}
