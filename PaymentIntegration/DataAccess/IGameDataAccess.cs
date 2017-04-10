
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IGameDataAccess
    {
        int SaveUserPoint(string userId, int points);
        UserGameInfo GetGameInfoByUser(string userId);
        int EntollIntoGame(string userId);
    }
}
