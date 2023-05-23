using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snakeGame
{
    class setting
    {
        public static int width { get; set; }
        public static int height { get; set; }

        public static String directions;

        public setting()
        {
            width = 25;
            height = 25;
            directions = "left";
        }
    }
}
