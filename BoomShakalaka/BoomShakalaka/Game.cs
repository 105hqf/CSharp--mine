using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomShakalaka
{
    class Game
    {
        private int minute;

        public int Minute
        {
            get { return minute; }
            set { minute = value; }
        }

        private int second;

        public int Second
        {
            get { return second; }
            set { second = value; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="str"></param>
        /// <param name="level"></param>
        public Game(int a, int b)
        {
            this.Second = b;
            this.Minute = a;
        }
    }
}
