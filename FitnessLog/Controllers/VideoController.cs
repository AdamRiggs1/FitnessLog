using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FitnessLog.Repositories;
using FitnessLog.Models;
using Microsoft.Extensions.Hosting;
using Azure;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;

namespace FitnessLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoRepository _videoRepository;
        public VideoController(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_videoRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var video = _videoRepository.GetById(id);
            if (video == null)
            {
                return NotFound();
            }
            return Ok(video);
        }

        [HttpGet("GetVideoById")]
        public IActionResult GetVideoById(int id)
        {
            var video = _videoRepository.GetById(id);
            if (video == null)
            {
                return NotFound();
            }
            return Ok(video);
        }

        [HttpGet("GetMyVideo")]
        [Authorize]

        public IActionResult GetUserFoods()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return Ok(_videoRepository.CurrentUsersVideo(firebaseUserId));
        }


        [HttpPost]
        public IActionResult Post(Video video)
        {
            video.UserProfileId = GetCurrentUserProfileId();
            // NOTE: This is only temporary to set the UserProfileId until we implement login
            // TODO: After we implement login, use the id of the current user
            try
            {
                // Handle the video URL

                // A valid video link might look like this:
                //  https://www.youtube.com/watch?v=sstOXCQ-EG0&list=PLdo4fOcmZ0oVGRpRwbMhUA0KAvMA2mLyN
                // 
                // Our job is to pull out the "v=XXXXX" part to get the get the "code/id" of the video
                //  So we can construct an URL that's appropriate for embedding a video

                // An embeddable Video URL looks something like this:
                //  https://www.youtube.com/embed/sstOXCQ-EG0

                // If this isn't a YouTube video, we should just give up
                if (!video.VideoUrl.Contains("youtube"))
                {
                    return BadRequest();
                }

                // If it's not already an embeddable URL, we have some work to do
                if (!video.VideoUrl.Contains("embed"))
                {
                    var videoCode = video.VideoUrl.Split("v=")[1].Split("&")[0];
                    video.VideoUrl = $"https://www.youtube.com/embed/{videoCode}";
                }
            }
            catch // Something went wrong while creating the embeddable url
            {
                return BadRequest();
            }

            _videoRepository.Add(video);

            return CreatedAtAction("Get", new { id = video.Id }, video);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Video video)
        {
            _videoRepository.Update(video);

            return Ok(_videoRepository.GetAll());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _videoRepository.Delete(id);
            return Ok(_videoRepository.GetAll());
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
