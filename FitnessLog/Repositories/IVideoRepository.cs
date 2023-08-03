using FitnessLog.Models;
using System.Collections.Generic;

namespace FitnessLog.Repositories
{
    public interface IVideoRepository
    {
        void Add(Video video);
        void Delete(int id);
        List<Video> GetAll();
        Video GetById(int id);
        Video GetByFirebaseUserId(string firebaseUserId);
        List<Video> CurrentUsersVideo(string firebaseUserId);
        void Update(Video video);
    }
}