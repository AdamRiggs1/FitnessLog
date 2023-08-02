using FitnessLog.Models;
using FitnessLog.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public IActionResult GetStrengthWorkoutByUserProfile(string firebaseUserId)
        {
            return Ok(_userStrengthWorkoutRepository.GetByFirebaseUserId(firebaseUserId));
        }

        
        [HttpGet("GetMyStrengthWorkout")]
        [Authorize]
       
        public IActionResult GetUserStrenthWorkouts()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return Ok(_userStrengthWorkoutRepository.CurrentUsersStrengthWorkouts(firebaseUserId));
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

        private UserStrengthWorkout GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userStrengthWorkoutRepository.GetByFirebaseUserId(firebaseUserId);
        }
    }
}
