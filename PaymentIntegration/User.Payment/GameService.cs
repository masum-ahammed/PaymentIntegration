using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class GameService
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
               return  dbContext.SaveChanges();
            }
        }
    }
}
