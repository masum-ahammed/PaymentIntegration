using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace User.Web.Models
{
    public class UserGameInfo
    {
        public int Id { get; set; }
        public string UserId { get; set; }
     
        public bool IsEnrolledInGame { get; set; }
        public int Point { get; set; }
    }
}