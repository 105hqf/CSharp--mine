using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomShakalaka
{
    /// <summary>
    /// 雷区的形成，以及所有的算法
    /// </summary>
    class Algorithm
    {
        /// <summary>
        /// 根据难易度确定地雷的数量
        /// </summary>
        private int BoomCount;

        public int boomcount
        {
            get { return BoomCount; }
            set { BoomCount = value; }
        }

        /// <summary>
        /// 第一一个雷的剩余数量
        /// </summary>
        private int BoomLeft;

        public int boomleft
        {
            get { return BoomLeft; }
            set { BoomLeft = value; }
        }
        
        /// <summary>
        /// 游戏难度
        /// </summary>
        private int Level;
        /// <summary>
        /// 0：简单；1：中级；2：难
        /// </summary>
        public int level
        {
            get { return Level; }
            set { Level = value; }
        }
        private int Check;

        public int check
        {
            get { return Check; }
            set { Check = value; }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="count"></param>
        /// <param name="level"></param>
        public Algorithm(int count, int level, int checkboom)
        {
            this.boomcount = count;
            this.level = level;
            this.check = checkboom;
        }
    }
}
