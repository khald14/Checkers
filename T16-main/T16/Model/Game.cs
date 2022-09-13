using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T16.Model
{
    class Game
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public string GameStartTime { get; set; }
        public string GameDurationTime { get; set; }
    }
}
