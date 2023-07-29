using FitnessLog.Models;
using FitnessLog.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCardioWorkoutController : ControllerBase
    {
        private readonly IUserCardioWorkoutRepository _userCardioWorkoutRepository;
        public UserCardioWorkoutController(IUserCardioWorkoutRepository userCardioWorkoutRepository)
        {
            _userCardioWorkoutRepository = userCardioWorkoutRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userCardioWorkoutRepository.GetAll());
        }

        [HttpGet("{firebaseUserId}")]
        public IActionResult GetUserProfile(string firebaseUserId)
        {
            return Ok(_userCardioWorkoutRepository.GetByFirebaseUserId(firebaseUserId));
        }


        [HttpPost]

        public IActionResult AddUserCardioWorkout(UserCardioWorkout userCardioWorkout)
        {
            _userCardioWorkoutRepository.Add(userCardioWorkout);
            return Ok(_userCardioWorkoutRepository.GetAll());
        }

        [HttpPut]
        public IActionResult EditUserStrengthWorkout(UserCardioWorkout userCardioWorkout)
        {
            _userCardioWorkoutRepository.Update(userCardioWorkout);
            return Ok(_userCardioWorkoutRepository.GetAll());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userCardioWorkoutRepository.Delete(id);
            return Ok(_userCardioWorkoutRepository.GetAll());
        }
    }
}
