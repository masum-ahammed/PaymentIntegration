using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class GameDataAccess: IGameDataAccess
    {
        public int SaveUserPoint(string userId, int points)
        {
            using (DataDbContext dbContext = new DataDbContext())
            {
                var userGameInfo = dbContext.UserGameInfos.Where(x => x.UserId == userId).SingleOrDefault();
                if (userGameInfo == null)
                {
                    dbContext.UserGameInfos.Add(new UserGameInfo() { IsEnrolledInGame = false, UserId = userId, Point = points });
                }
                else
                {
                    userGameInfo.Point = points;
                }
                return dbContext.SaveChanges();
            }
        }

       

        public UserGameInfo GetGameInfoByUser(string userId)
        {
            using (DataDbContext dbContext = new DataDbContext())
            {
                UserGameInfo gameInfo = dbContext.UserGameInfos.SingleOrDefault(x => x.UserId == userId);
                if (gameInfo == null)
                {
                    gameInfo = new UserGameInfo();
                }
                return gameInfo;
            }
        }

        public int EntollIntoGame(string userId)
        {
            using (DataDbContext dbContext = new DataDbContext())
            {
                UserGameInfo gameInfo = dbContext.UserGameInfos.SingleOrDefault(x => x.UserId == userId);
                if (gameInfo != null)
                {
                    gameInfo.IsEnrolledInGame = true;
                }
                return dbContext.SaveChanges();
            }
        }

    }
}
