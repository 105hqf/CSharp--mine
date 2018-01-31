using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomShakalaka
{
    /// <summary>
    /// 定义一个栈
    /// </summary>
    public class MyStack
    {
        private int locationx;

        public int Locationx
        {
            get { return locationx; }
            set { locationx = value; }
        }
        private int locationy;

        public int Locationy
        {
            get { return locationy; }
            set { locationy = value; }
        }

        public void getLocation(int x, int y)
        {
            this.Locationx = x;
            this.Locationy = y;
        }
    }
}
