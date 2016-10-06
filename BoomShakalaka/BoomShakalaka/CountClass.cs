using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomShakalaka
{
    /// <summary>
    /// 用于计数，计算timer过去的时间以及显示特效图片的方式
    /// </summary>
    class CountClass
    {
        /// <summary>
        /// 第四个图片的计数
        /// </summary>
        private int Secondx;

        public int secondx
        {
            get { return Secondx; }
            set { Secondx = value; }
        }
        /// <summary>
        /// 第三个图片的计数
        /// </summary>
        private int Secondy;

        public int secondy
        {
            get { return Secondy; }
            set { Secondy = value; }
        }
        /// <summary>
        /// 第二张图片的计数
        /// </summary>
        private int Minutex;

        public int minutex
        {
            get { return Minutex; }
            set { Minutex = value; }
        }
        /// <summary>
        /// 第一张
        /// </summary>
        private int Minutey;

        public int minutey
        {
            get { return Minutey; }
            set { Minutey = value; }
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        public CountClass(int a, int b, int c, int d)
        {
            this.secondx = a;
            this.secondy = b;
            this.minutex = c;
            this.minutey = d;
        }

    }
}
