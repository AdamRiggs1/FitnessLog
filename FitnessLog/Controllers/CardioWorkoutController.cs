using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FitnessLog.Repositories;
using FitnessLog.Models;

namespace FitnessLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardioWorkoutController : ControllerBase
    {
        private readonly ICardioWorkoutRepository _cardioWorkoutRepository;
        public CardioWorkoutController(ICardioWorkoutRepository cardioWorkoutRepository)
        {
            _cardioWorkoutRepository = cardioWorkoutRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_cardioWorkoutRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var getCardioWorkoutsId = _cardioWorkoutRepository.GetCardioWorkoutById(id);
            if (getCardioWorkoutsId == null)
            {
                return NotFound();
            }
            return Ok(getCardioWorkoutsId);
        }



        [HttpPost]
        public IActionResult AddCardioWorkout(CardioWorkout cardioWorkout)
        {
            _cardioWorkoutRepository.Add(cardioWorkout);
            return Ok(cardioWorkout);
        }

        [HttpPut("{id}")]
        public IActionResult EditCardioWorkout(int id, CardioWorkout cardioWorkout)
        {
            _cardioWorkoutRepository.Update(cardioWorkout);
            return Ok(_cardioWorkoutRepository.GetCardioWorkoutById(id));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCardioWorkout(int id)
        {
            _cardioWorkoutRepository.Delete(id);
            return Ok(_cardioWorkoutRepository.GetAll());
        }
    }
}
