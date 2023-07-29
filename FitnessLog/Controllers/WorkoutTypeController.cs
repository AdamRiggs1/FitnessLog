using FitnessLog.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutTypeController : ControllerBase
    {
        private readonly IWorkoutTypeRepository _workoutTypeRepository;
        public WorkoutTypeController(IWorkoutTypeRepository workoutTypeRepository)
        {
            _workoutTypeRepository = workoutTypeRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_workoutTypeRepository.GetAll());
        }

    }
}
