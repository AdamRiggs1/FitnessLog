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
    public class StrengthWorkoutController : ControllerBase
    {
        private readonly IStrengthWorkoutRepository _strengthWorkoutRepository;
        public StrengthWorkoutController(IStrengthWorkoutRepository strengthWorkoutRepository)
        {
            _strengthWorkoutRepository = strengthWorkoutRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_strengthWorkoutRepository.GetAll());
        }

        [HttpPost]
        public IActionResult AddStrengthWorkout(StrengthWorkout strengthWorkout)
        {
            _strengthWorkoutRepository.Add(strengthWorkout);
            return Ok(_strengthWorkoutRepository.GetAll());
        }

        [HttpPut]
        public IActionResult EditStrengthWorkout(StrengthWorkout strengthWorkout)
        {
            _strengthWorkoutRepository.Update(strengthWorkout);
            return Ok(_strengthWorkoutRepository.GetAll());
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStrengthWorkout(int id)
        {
            _strengthWorkoutRepository.Delete(id);
            return Ok(_strengthWorkoutRepository.GetAll());
        }
    }
}
