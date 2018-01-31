using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BoomShakalaka
{
    /// <summary>
    /// 布雷算法
    /// </summary>
    public class SetMine
    {
        //利用进出栈的想法预先存放雷区。
        //初始化压入栈中20个位置元素
        //初始化一个栈
        private int frontfirst = -1;
        private int rearfirst = 0;
        public MyStack[] myStackFist = new MyStack[99];
        private int[] content = new int[99];
        private int[,] flag=new int[25,16];
        private int reMine;
        private int locationx;
        private int locationy;
        private int firstX;
        private int firstY;
        /// <summary>
        /// 构造函数
        /// </summary>
        public SetMine(int boom,int x,int y)
        {
            firstX = x;
            firstY = y;
            //设置一个标记，用于判断random函数是否产生了一个相同的元素
            for (int i = 0; i < firstX; i++)
            {
                for (int j = 0; j < firstY; j++)
                {
                    //在i，j位置如果为0,则表示没有被赋值过
                    flag[i, j] = -1;
                }
            }
            //利用随机数在栈中压入元素
            //假设边框为25，16
            //排除掉一样的元素
            int boomcount = 0;
                Random random = new Random();
                locationx = random.Next(0, firstX);
                locationy = random.Next(0, firstY);
                //实例化
                myStackFist[rearfirst] = new MyStack();
                //设定标记
                myStackFist[rearfirst].Locationx = locationx;
                myStackFist[rearfirst].Locationy = locationy;
                flag[myStackFist[rearfirst].Locationx, myStackFist[rearfirst].Locationy] = rearfirst;
                //尾指针累加
                rearfirst++;
                //头指针累加
                frontfirst++;
                boomcount++;
            ///假设接下来随机点在第一个随机点的半径为2的圆中递归生成
                int rx, ry;//分别为x，y方向上的增量
                //排除点相同的元素
           while (boomcount < boom)
           {
               Random rand = new Random();
               rx=rand.Next(0,10000)%10;
               Thread.Sleep(5);
               Random randy = new Random();
               
               ry = randy.Next(0, 10000)%10;
               locationx = locationx + rx;
               locationy = locationy + ry;
               if (locationx >= firstX)
                   locationx = locationx - firstX;
               if (locationy >= firstY)
                   locationy = locationy - firstY;
                if (flag[locationx, locationy] == -1)
                {
                    //实例化
                    myStackFist[rearfirst] = new MyStack();
                    //设定标记
                    myStackFist[rearfirst].Locationx = locationx;
                    myStackFist[rearfirst].Locationy = locationy;
                    flag[myStackFist[rearfirst].Locationx, myStackFist[rearfirst].Locationy] = rearfirst;
                    //尾指针累加
                    rearfirst++;
                    //头指针累加
                    frontfirst++;
                    boomcount++;
                }
            }
        }
        /// <summary>
        /// 捕获第一次点击时的位置，如果该位置在栈中存在，则将该位置为中心的九宫格清零
        /// 如果不存在，清空九宫格
        /// 如果九宫格为零状态，则不执行该函数
        /// </summary>
        /// <param name="locationx"></param>
        /// <param name="locationy"></param>
        public MyStack[] setBoom(int locationX,int locationY)
        {
             //该标记用于标记点击后点击中心的九宫,格中存在多少雷
            reMine = 0;
            int count = 0;
            //当前位置栈中存在
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (flag[locationX + i, locationY + j] != -1)
                        {
                            //重复,记录下标
                            content[count] = flag[locationX + i, locationY + j];
                            count++;
                            flag[locationX + i, locationY+ j] = -1;
                            //标记累加
                            reMine++;
                        }
                    }
                }
            if (reMine != 0)
            {
                //将reMine相同数量的雷重新排列
                int i = 0;
                while(i < reMine)
                {
                    Random random = new Random();
                    //将被清空的栈元素重新赋值
                    locationx = random.Next(0, firstX);
                    locationy = random.Next(0, firstY);
                    //当且仅当flag标记中没有赋值，以及新生成的坐标不在点击点的九宫格中才能正确储存
                    if ((flag[locationx , locationy ] == -1)
                        &&(locationx < locationX-1 || locationx> locationX+1)
                        &&(locationy < locationY-1 || locationy> locationY+1))
                      {
                        //将新元素更新到栈的相应位置
                        myStackFist[content[i]].Locationx = locationx;
                        myStackFist[content[i]].Locationy = locationy;
                        //更新flag标记并赋值
                        flag[myStackFist[content[i]].Locationx, myStackFist[content[i]].Locationy] = content[i];
                        i++;
                      }
                }
            }
            return myStackFist;
        }
    }
}
