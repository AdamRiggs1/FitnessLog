using FitnessLog.Models;
using FitnessLog.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStrengthWorkoutController : ControllerBase
    {
        private readonly IUserStrengthWorkoutRepository _userStrengthWorkoutRepository;
        public UserStrengthWorkoutController(IUserStrengthWorkoutRepository userStrengthWorkoutRepository)
        {
            _userStrengthWorkoutRepository = userStrengthWorkoutRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userStrengthWorkoutRepository.GetAll());
        }

        [HttpGet("{firebaseUserId}")]
        public IActionResult GetUserProfile(string firebaseUserId)
        {
            return Ok(_userStrengthWorkoutRepository.GetByFirebaseUserId(firebaseUserId));
        }

        [HttpPost]

        public IActionResult AddUsertrengthWorkout(UserStrengthWorkout userStrengthWorkout)
        {
            _userStrengthWorkoutRepository.Add(userStrengthWorkout);
            return Ok(_userStrengthWorkoutRepository.GetAll());
        }

        [HttpPut]
        public IActionResult EditUserStrengthWorkout(UserStrengthWorkout userStrengthWorkout)
        {
            _userStrengthWorkoutRepository.Update(userStrengthWorkout);
            return Ok(_userStrengthWorkoutRepository.GetAll());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userStrengthWorkoutRepository.Delete(id);
            return Ok(_userStrengthWorkoutRepository.GetAll());
        }
    }
}
