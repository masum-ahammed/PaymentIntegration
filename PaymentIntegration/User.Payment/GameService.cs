using Data.Model;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class GameService
    {
        IGameDataAccess _GameDataAccess;

        public GameService()
        {
            _GameDataAccess = new GameDataAccess();
        }
        public int SaveUserPoint(string userId, int points)
        {
            return _GameDataAccess.SaveUserPoint(userId, points);
        }

        public UserGameInfo GetGameInfoByUser(string userId)
        {
            return _GameDataAccess.GetGameInfoByUser(userId);
        }

        public int EntollIntoGame(string userId)
        {
            return _GameDataAccess.EntollIntoGame(userId);
        }
    }
}
