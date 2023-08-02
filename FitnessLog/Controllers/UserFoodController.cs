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
    public class UserFoodController : ControllerBase
    {
        private readonly IUserFoodRepository _userFoodRepository;
        public UserFoodController(IUserFoodRepository userFoodRepository)
        {
            _userFoodRepository = userFoodRepository;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userFoodRepository.GetAll());
        }

        [HttpGet("{firebaseUserId}")]
        public IActionResult GetFoodByUserProfile(string firebaseUserId)
        {
            return Ok(_userFoodRepository.GetByFirebaseUserId(firebaseUserId));
        }


        [HttpGet("GetMyFood")]
        [Authorize]

        public IActionResult GetUserFoods()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return Ok(_userFoodRepository.CurrentUsersFood(firebaseUserId));
        }

        [HttpPost]

        public IActionResult AddUserFood(UserFood userFood)
        {
            _userFoodRepository.Add(userFood);
            return Ok(_userFoodRepository.GetAll());
        }

        [HttpPut]
        public IActionResult EditUserFood(UserFood userFood)
        {
            _userFoodRepository.Update(userFood);
            return Ok(_userFoodRepository.GetAll());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userFoodRepository.Delete(id);
            return Ok(_userFoodRepository.GetAll());
        }

        private UserFood GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userFoodRepository.GetByFirebaseUserId(firebaseUserId);
        }
    }
}
