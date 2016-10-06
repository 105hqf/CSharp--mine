using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomShakalaka
{
    class alth
    {
        public struct Location
        {
            public int locationx, locationy;
        };
        public Location[,] location = new Location[40, 16];
        /// <summary>
        /// 初始化数组
        /// </summary>
        public void array()
        {
            ////尝试更新算法布雷算法

            /////想法：利用数组。随机数去下标，去后的元素将其赋值为-1；

            for (int i = 0; i < 40; i++)
                for (int j = 0; j < 16; j++)
                {
                    location[i, j].locationx = i;
                    location[i, j].locationy = j;
                }
        }
        /// <summary>
        /// 利用随机数产生下标，将产生过得下标的元素变为（-1，-1）
        /// </summary>
        public void algor(int locaX,int locaY)
        {
            ////取出的下标
            int subscriptx;
            int subscripty;
            ////如果当前取出的下标所代表的元素是（-1，-1）则取出，重新执行函数
            do
            {

                Random random = new Random();
                subscriptx = random.Next(0, 40);
                subscripty = random.Next(0, 16);

            } while (location[subscriptx, subscripty].locationx == -1 && location[subscriptx, subscripty].locationy == -1);
            /////将符合要求的位置赋值，并将该位置取消
                locaX = location[subscriptx, subscripty].locationx;
                locaY = location[subscriptx, subscriptx].locationy;

                location[subscriptx, subscripty].locationx = -1;
                location[subscriptx, subscripty].locationy = -1;
        }

    }
   
}
