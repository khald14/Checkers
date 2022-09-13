using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Q.Model
{
    public class Game
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public string GameStartTime { get; set; }
        public string GameDurationTime { get; set; }

    }
}
