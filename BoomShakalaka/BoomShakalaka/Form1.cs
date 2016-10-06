using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Reflection;


namespace BoomShakalaka
{
    public delegate void mydelegate(String text,int level);
    public partial class Form1 : Form
    {
    

        #region  定义变量
        //private int boom = 9;
        /// <summary>
        /// 实例化一个算法
        /// </summary>
        Algorithm booma = new Algorithm(0,0,0);

        /// <summary>
        /// 定义一个队列
        /// </summary>
        MyStack[] mystack = new MyStack[16*40];
        private int row = 9;
        private int col = 9;
        public int front = -1;//头指针
        public int rear = 0;//尾指针
        MyStack[] mystack1 = new MyStack[16*40];
        public int front1 = -1;//头指针
        public int rear1 = 0;//尾指针
        Label[,] label = new Label[40,16];

        private int WinFlag = 0;


        private int firstX;
        private int firstY;
        private bool first = true;
        private int lostFlag = 0;

        

        CountClass Hourtime = new CountClass(0, 0, 0, 0);
        public int[,] visited = new int[40,16];
        public int[,] sign = new int[40,16];
        /// <summary>
        /// 初始化雷区
        /// </summary>
        private int[,] board=new int[40,16];
        /// <summary>
        /// 定义一个变量，用于表示目前已翻开的牌数
        /// </summary>
        private int FlagCount = 0;

        /// <summary>
        /// 窗体本身的大小，用于记忆
        /// </summary>
        private int Ox;
        private int Oy;
        private int level=0;
        #endregion


        #region  窗体各种实例化
        public Form1()
        {
            //panel1.BackgroundImage = Image.FromFile(@"Image\Menu.jpg");
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    sign[i, j] = 0;
                }
            }
            ////初始化雷区，全部设为0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    visited[i, j] = 0;
                }
            }
                InitializeComponent();
                Ox = 443;
                Oy = 388;
            
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
            this.panel2.Visible = false;
            this.timer1.Enabled = false;
            this.label1.Visible = false;
            this.label2.Visible = false;
            this.label6.Visible = false;
            Set();
            //setBg();
            this.pictureBox1.Image = imageList1.Images[0];
            this.pictureBox2.Image = imageList1.Images[0];
            this.pictureBox3.Image = imageList1.Images[0];
            this.pictureBox4.Image = imageList1.Images[0];
            this.pictureBox5.Image = imageList1.Images[10];
            this.pictureBox6.Image = imageList1.Images[0];
            this.pictureBox7.Image = imageList1.Images[0];
            this.pictureBox8.Image = imageList1.Images[0];
            this.pictureBox9.Image = imageList1.Images[0];
            //this.toolTip1.SetToolTip(button1, "重新开始游戏");
            this.重新开始游戏ToolStripMenuItem.Enabled = false;
            this.下一位玩家开始ToolStripMenuItem.Visible= false;
            /////
            this.BackColor = Color.White;
           // this.panel1.BackColor = Color.Transparent;
            //this.panel1.BackgroundImage = Image.FromFile(@"Image\Menu.jpg");
        }

        private void 新游戏ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion


        #region 游戏模式的选择以及初始化
        private void 中级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.下一位玩家开始ToolStripMenuItem.Visible = false;
            Start(1);
           // this.下一位玩家开始ToolStripMenuItem.Visible = false;
        }

        private void 中级ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.下一位玩家开始ToolStripMenuItem.Visible = false;
            Start(2);
           // this.下一位玩家开始ToolStripMenuItem.Visible = false;
        }

        private void 高级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.下一位玩家开始ToolStripMenuItem.Visible = false;
            Start(3);
            
        }

        private void Start(int level1)
        {
            switch (level1)
            {
                case 1:
                    //***************************************/
                    //                                       /
                    //                                       /
                    //                                       /
                    //                                       /
                    //***************************************/
                    this.label1.Visible = true;
                    this.label2.Visible = true;
                    this.label6.Visible = true;
                    this.重新开始游戏ToolStripMenuItem.Enabled = true;
                    //////////
                    this.panel1.Visible = true;
                    level = 0;
                    booma.boomcount = 10;
                    Get();

                    //this.label4.Text = booma.boomcount.ToString();
                    pictureBox9.Image = imageList1.Images[booma.boomcount % 10];
                    pictureBox8.Image = imageList1.Images[booma.boomcount / 10];
                    //this.label3.Text = (booma.boomcount - booma.check).ToString();
                    ////选择高级后，将Form窗体扩大，panel控件也扩大
                    row = 9;
                    col = 9;
                    this.Size = new Size(Ox, Oy);
                    this.panel1.Size = new Size(9 * 25, 9 * 25);
                    //////窗体的修改

                    ////
                    lostFlag = 0;
                    timer1.Enabled = false;
                    Hourtime = new CountClass(0, 0, 0, 0);
                    this.pictureBox1.Image = imageList1.Images[0];
                    this.pictureBox2.Image = imageList1.Images[0];
                    this.pictureBox3.Image = imageList1.Images[0];
                    this.pictureBox4.Image = imageList1.Images[0];
                    this.pictureBox5.Image = imageList1.Images[10];
                    // label3.Text = time.ToString();
                    if (this.panel1.Visible == true)
                    {
                        this.panel1.Controls.Clear();
                        setBg();
                    }
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                            visited[i, j] = 0;
                    }
                    WinFlag = 0;
                    FlagCount = 0;
                    pictureBox7.Image = imageList1.Images[0];
                    pictureBox6.Image = imageList1.Images[0];
                    first = true;
                    break;
                case 2:
                        this.label1.Visible = true;
                        this.label2.Visible = true;
                        this.label6.Visible = true;
                        this.panel1.Visible = true;
                        this.重新开始游戏ToolStripMenuItem.Enabled = true; 
                        booma.boomcount = 35;
                        level = 1;
                        //this.label4.Text = booma.boomcount.ToString();
                        pictureBox9.Image = imageList1.Images[booma.boomcount % 10];
                        pictureBox8.Image = imageList1.Images[booma.boomcount / 10];
                        ////选择高级后，将Form窗体扩大，panel控件也扩大
                        row = 16;
                        col = 16;
                        this.Size = new Size(Ox + 6 * 25, Oy + 6 * 25);
                        this.panel1.Size = new Size(16 * 25, 16 * 25);
                        //////窗体的修改

                        Get();
                        ////重新开始游戏
                        lostFlag = 0;
                        timer1.Enabled = false;
                        Hourtime = new CountClass(0, 0, 0, 0);
                        this.pictureBox1.Image = imageList1.Images[0];
                        this.pictureBox2.Image = imageList1.Images[0];
                        this.pictureBox3.Image = imageList1.Images[0];
                        this.pictureBox4.Image = imageList1.Images[0];
                        this.pictureBox5.Image = imageList1.Images[10];
                        //label3.Text = time.ToString();
                        if (this.panel1.Visible == true)
                        {
                            this.panel1.Controls.Clear();
                            setBg();
                        }
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < col; j++)
                                visited[i, j] = 0;
                        }
                        WinFlag = 0;
                        FlagCount = 0;
                        pictureBox7.Image = imageList1.Images[0];
                        pictureBox6.Image = imageList1.Images[0];
                        first = true;
                        break;

                case 3:
                        /////高级游戏尝试：

                        ////选择高级后，将Form窗体扩大，panel控件也扩大
                        row = 25;
                        col = 16;
                        this.Size = new Size(Ox + 15 * 25 - 5 * 25, Oy + 6 * 25);
                        this.panel1.Size = new Size(25 * 25, 16 * 25);
                        level = 2;
                        this.label1.Visible = true;
                        this.label2.Visible = true;
                        this.label6.Visible = true;
                        this.panel1.Visible = true;
                        this.重新开始游戏ToolStripMenuItem.Enabled = true;
                        booma.boomcount = 99;
                        //this.label4.Text = booma.boomcount.ToString();
                        pictureBox9.Image = imageList1.Images[booma.boomcount % 10];
                        pictureBox8.Image = imageList1.Images[booma.boomcount / 10];
                        ////重新开始游戏
                        Get();
                        lostFlag = 0;
                        timer1.Enabled = false;
                        Hourtime = new CountClass(0, 0, 0, 0);
                        this.pictureBox1.Image = imageList1.Images[0];
                        this.pictureBox2.Image = imageList1.Images[0];
                        this.pictureBox3.Image = imageList1.Images[0];
                        this.pictureBox4.Image = imageList1.Images[0];
                        this.pictureBox5.Image = imageList1.Images[10];
                        //label3.Text = time.ToString();
                        if (this.panel1.Visible == true)
                        {
                            this.panel1.Controls.Clear();
                            setBg();
                        }
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < col; j++)
                                visited[i, j] = 0;
                        }
                        WinFlag = 0;
                        FlagCount = 0;
                        pictureBox7.Image = imageList1.Images[0];
                        pictureBox6.Image = imageList1.Images[0];
                        first = true;
                        break;
                default :
                    MessageBox.Show("程序出错！请联系开发者！","错误！",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    break;
                    
            }

        }
        #endregion


        #region 安排雷区
        public void set()
        {
            int boomcount = 0;
            int locationX,locationY;
            board = new int[40, 16];
            ///简单的难度,默认安置10个雷
            while (boomcount < booma.boomcount)
            {
                ///random函数随机生成雷的位置
                Random random = new Random();
                locationX = random.Next(0, row);
                Thread.Sleep(100);
                locationY = random.Next(0, col);
                if ((locationX >= firstX - 1 && locationX <= firstX + 1) && (locationY >= firstY - 1 && locationY <= firstY + 1) ) 
                    continue;
                if (board[locationX, locationY] == -1) continue;
                    board[locationX, locationY] = -1;
                    boomcount++;
            }
           // this.label4.Text = boomcount.ToString();

            ////////遍历数组的各个点，如果该点不是雷点，将该点的周边的雷数量显示
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                {
                    if (board[i, j] == 0)
                    {
                        for (int k = -1; k < 2; k++)
                        {
                            for (int m = -1; m < 2; m++)
                            {
                                if (i + k > -1 && j + m > -1 && i + k < row && j + m < col && board[i + k, j + m] == -1)
                                {
                                    board[i, j]++;
                                }
                            }
                        }

                    }
                }

        } 
           #endregion
        /// <summary>
        /// 尝试更加快速的布雷算法
        /// </summary>
        public void alth()
        {
             /////尝试一：事先将随机数储存入数组中保存
                                 
        }

        #region 安排困难的雷区
        public void setD()
        {
            int boomcount = 0;
            int locationX, locationY;
            board = new int[40, 16];
            ///简单的难度,默认安置10个雷
            while (boomcount < booma.boomcount)
            {
                ///random函数随机生成雷的位置
                Random random = new Random();
                locationX = random.Next(0, row);
                //Thread.Sleep();
                locationY = random.Next(0, col);
                if ((locationX >= firstX - 1 && locationX <= firstX + 1) && (locationY >= firstY - 1 && locationY <= firstY + 1))
                    continue;
                if (board[locationX, locationY] == -1) continue;
                board[locationX, locationY] = -1;
                boomcount++;
            }
            //this.label4.Text = boomcount.ToString();

            ////////遍历数组的各个点，如果该点不是雷点，将该点的周边的雷数量显示
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                {
                    if (board[i, j] == 0)
                    {
                        for (int k = -1; k < 2; k++)
                        {
                            for (int m = -1; m < 2; m++)
                            {
                                if (i + k > -1 && j + m > -1 && i + k < row && j + m < col && board[i + k, j + m] == -1)
                                {
                                    board[i, j]++;
                                }
                            }
                        }

                    }
                }






        }
        #endregion


        #region 以label为雷区
        public void setBg()
        {
            ////建立背景布局,布局以label来模拟,生成八行八列的二维数组形式
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    label[i, j] = new Label();
                    label[i,j].Location = new Point(i * 25, j * 25);
                    label[i,j].Visible = true;
                    label[i,j].Size = new Size(25, 25);
                    label[i, j].BorderStyle = BorderStyle.Fixed3D;
                    sign[i, j] = 3;
                    label[i, j].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    label[i, j].Name = i.ToString() + "哈" + j.ToString();
                        label[i, j].MouseDown += new MouseEventHandler(label_MouseDown);
                        label[i, j].MouseMove += new MouseEventHandler(label_MouseMove);
                        label[i, j].MouseLeave += new System.EventHandler(label_MouseLeave);
                        label[i, j].MouseUp += new MouseEventHandler(label_MouseUp);
                    //label.Image = Image.FromFile("stone.png");
                    this.panel1.Controls.Add(label[i, j]);
                    label[i, j].Image = imageList2.Images[9];
                }
            }
            
        }
            #endregion


        #region 排雷事件
        private void lbl_Click(object sender, EventArgs e)
        {
            int front2 = 0;
            Label lbl = sender as Label;
            
            String[] str = new String[2];
            
            //翻牌后计数加1；
            
            str = lbl.Name.Split('哈');
            firstX = int.Parse(str[0]);
            firstY = int.Parse(str[1]);


            if (label[firstX, firstY].Image != imageList2.Images[0])
            { lbl.Image = imageList2.Images[0]; sign[firstX, firstY] = 4; }

            if (first == true)
            {
                board[firstX, firstY] = 0;
                if (level == 1 || level == 0)
                    set();
                else setD();
                first = false;
                /////自动翻开空白格

                Auto(firstX, firstY);
                while (front2 < rear)
                {
                    label[mystack[front2].Locationx, mystack[front2].Locationy].Image = imageList2.Images[0];
                    sign[mystack[front2].Locationx, mystack[front2].Locationy] = 4;
                    if (board[mystack[front2].Locationx, mystack[front2].Locationy] != 0)
                    {
                        label[mystack[front2].Locationx, mystack[front2].Locationy].Image = imageList2.Images[board[mystack[front2].Locationx, mystack[front2].Locationy]];
                        sign[mystack[front2].Locationx, mystack[front2].Locationy] = 4;
                    }
                    front2++;
                }
                timer1.Enabled = true;
                timer1.Interval = 1000;
                front = -1;
                front1 = -1;
                rear = 0; rear1 = 0;
            }
            else
            {
                if (board[firstX, firstY] != -1 && board[firstX, firstY] != 0)
                    {
                        lbl.Image = imageList2.Images[board[firstX, firstY]];
                        sign[firstX, firstY] = 4;
                    }
                    if (board[firstX, firstY] == -1)
                    {
                        lostFlag = -1;
                        timer1.Enabled = false;
                        Hourtime = new CountClass(0, 0, 0, 0);
                        // label3.Text = "";
                        lbl.Image = Image.FromFile(@"Image\boom1.png");
                        MessageBox.Show("哇～你好可爱，这都玩不出～");

                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < col; j++)
                            {
                                if (board[i, j] == -1)
                                {
                                    if (sign[i, j] == 1)
                                    {
                                        label[i, j].Image = Image.FromFile(@"Image\Boom.png");
                                    }
                                    else
                                        label[i, j].Image = Image.FromFile(@"Image\boom1.png");

                                }
                                //lbl.Click += new EventHandler(this.lbl1_Click);
                            }
                        }
                    }
                    ///如果点击到的是0
                    ///将其存入队列中，然后遍历他的四周，将所有带0的位置存入队列
                    ///直到队列中没有元素为止
                    //////无雷区自动翻开
                    if (board[firstX, firstY] == 0)
                    {

                        Auto(firstX, firstY);
                        while (front2 < rear)
                        {
                            label[mystack[front2].Locationx, mystack[front2].Locationy].Image = imageList2.Images[0];
                            sign[mystack[front2].Locationx, mystack[front2].Locationy] = 4;
                            if (board[mystack[front2].Locationx, mystack[front2].Locationy] != 0)
                            {
                                label[mystack[front2].Locationx, mystack[front2].Locationy].Image = imageList2.Images[board[mystack[front2].Locationx, mystack[front2].Locationy]];
                                sign[mystack[front2].Locationx, mystack[front2].Locationy] = 4;
                            }
                            front2++;
                        }
                        front = -1;
                        front1 = -1;
                        rear = 0; rear1 = 0;
                    }
                }
                //////
                // label5.Text = FlagCount.ToString();
                pictureBox7.Image = imageList1.Images[FlagCount % 10];
                pictureBox6.Image = imageList1.Images[FlagCount / 10];
        }
        private void label_MouseUp(object sender, MouseEventArgs e)
        {
            Label lbl = sender as Label;
            int x = 0, y = 0, z = 0;
            String[] str = new String[2];
            str = lbl.Name.Split('哈');
            x = int.Parse(str[0]);
            z = int.Parse(str[1]);
            y = sign[x, z];
            if(lostFlag==0)
                for (int i = -1; i < 2; i++)
                    for (int j = -1; j < 2; j++)
                    {
                        if (x + i > -1 && x + i < row && z + j > -1 && z + j < col && sign[x + i, z + j] == 3)
                        {
                            label[x + i, z + j].Image = imageList2.Images[9];
                        }
                    }
        }
        private void label_MouseDown(object sender, MouseEventArgs e)
        {
            if (lostFlag == 0)
            {
                int front2 = 0;
                Label lbl = sender as Label;

                    ///右击，当该点的label的name为1，表示旗子，2表示不确定，3表示没标记,4表示已经被点开
                    ///int location = int.Parse(lbl.Name);
                    int x = 0, y = 0, z = 0;
                    String[] str = new String[2];
                    str = lbl.Name.Split('哈');
                    x = int.Parse(str[0]);
                    z = int.Parse(str[1]);
                    y = sign[x, z];
                    if (e.Button == MouseButtons.Right)
                    {
                        if (sign[x, z] != 4)
                            if (y == 3 && FlagCount < booma.boomcount)
                            {
                                sign[x, z] = 1;
                                lbl.Image = Image.FromFile(@"Image\flag.gif");
                                FlagCount++;
                                if (board[x, z] == -1) WinFlag++;
                                //label5.Text = FlagCount.ToString();
                                pictureBox7.Image = imageList1.Images[FlagCount % 10];
                                pictureBox6.Image = imageList1.Images[FlagCount / 10];
                                if (WinFlag == booma.boomcount)
                                {
                                    timer1.Enabled = false;
                                    for (int i = 0; i < row; i++)
                                        for (int j = 0; j < col; j++)
                                        {
                                            if (sign[i, j] == 1) label[i, j].Image = Image.FromFile(@"Image\Boom.png");
                                        }
                                    Result re = new Result();
                                    re.Get(Hourtime.minutey.ToString() + Hourtime.minutex.ToString() + "分" + Hourtime.secondy.ToString() + Hourtime.secondx.ToString(), level);
                                    re.ShowDialog();
                                    lostFlag = 1;

                                }
                            }
                            else if (y == 1)
                            {
                                sign[x, z] = 2;
                                FlagCount--;
                                if (board[x, z] == -1)
                                    WinFlag--;
                                pictureBox7.Image = imageList1.Images[FlagCount % 10];
                                pictureBox6.Image = imageList1.Images[FlagCount / 10];
                                lbl.Image = Image.FromFile(@"Image\ask.gif");
                            }
                            else if (y == 2)
                            {
                                sign[x, z] = 3;
                                lbl.Image = imageList2.Images[9];
                            }
                    }
                    else if (e.Button == MouseButtons.Left)
                    {
                        if (y == 1) { FlagCount--; }
                        if (y == 1)
                        {
                            lbl.Image = imageList2.Images[9];
                            sign[x, z] = 3;
                            pictureBox7.Image = imageList1.Images[FlagCount % 10];
                            pictureBox6.Image = imageList1.Images[FlagCount / 10];
                        }
                        else 
                        lbl.Click += new System.EventHandler(this.lbl_Click);
                        if (y == 4)
                        {
                            int count = 0;
                            for (int i = -1; i < 2; i++)
                                for (int j = -1; j < 2; j++)
                                {
                                    if (x + i > -1 && x + i < row && z + j > -1 && z + j < col && board[x + i, z + j] == -1 && sign[x + i, z + j] == 1)
                                        count++;
                                }
                            if (count == board[x, z])
                            {
                                for (int i = -1; i < 2; i++)
                                    for (int j = -1; j < 2; j++)
                                    {
                                        if (x + i > -1 && x + i < row && z + j > -1 && z + j < col && board[x + i, z + j] != -1)
                                        {
                                            label[x + i, z + j].Image = imageList2.Images[board[x + i, z + j]];
                                            sign[x + i, z + j] = 4;
                                            if (board[x + i, z + j] == 0)
                                            {
                                                Auto(x + i, z + j);
                                                while (front2 < rear)
                                                {
                                                    label[mystack[front2].Locationx, mystack[front2].Locationy].Image = imageList2.Images[0];
                                                    sign[mystack[front2].Locationx, mystack[front2].Locationy] = 4;
                                                    if (board[mystack[front2].Locationx, mystack[front2].Locationy] != 0)
                                                    {
                                                        label[mystack[front2].Locationx, mystack[front2].Locationy].Image = imageList2.Images[board[mystack[front2].Locationx, mystack[front2].Locationy]];
                                                        sign[mystack[front2].Locationx, mystack[front2].Locationy] = 4;
                                                    }
                                                    front2++;
                                                }
                                                front = -1;
                                                front1 = -1;
                                                rear = 0; rear1 = 0;
                                            }
                                        }
                                    }
                            }
                            ///如果雷没有找对，则将其九宫格闪动一下
                            else
                            {
                                for (int i = -1; i < 2; i++)
                                    for (int j = -1; j < 2; j++)
                                    {
                                        if (x + i > -1 && x + i < row && z + j > -1 && z + j < col && sign[x + i, z + j] == 3)
                                        {
                                            label[x + i, z + j].Image = imageList2.Images[0];
                                        }
                                    }
                                lbl.MouseUp += new MouseEventHandler(label_MouseUp);
                            }
                        }
                    }
            }
        }
        #endregion


        #region 自动翻开空白格算法。依据：广度遍历搜索
        public void Auto(int i,int j)
        {
            
                mystack[rear] = new MyStack();
                ///广度遍历的算法
                mystack[rear].Locationx = i;
                mystack[rear].Locationy = j;
                rear++;
                visited[i,j]=1;

            front++;
            front1++;
            while (front < rear)
            {
                if (board[mystack[front].Locationx, mystack[front].Locationy] == 0)
                {
                    for (int k = -1; k < 2; k++)
                    {
                        for (int m = -1; m < 2; m++)
                        {
                            if (mystack[front].Locationx + k > -1 && mystack[front].Locationy + m > -1 && mystack[front].Locationx + k < row && mystack[front].Locationy + m < col && visited[mystack[front].Locationx + k, mystack[front].Locationy + m] != 1)
                            {
                                mystack1[rear1] = new MyStack();
                                mystack1[rear1].Locationx = mystack[front].Locationx + k;
                                mystack1[rear1].Locationy = mystack[front].Locationy + m;
                                visited[mystack1[rear1].Locationx, mystack1[rear1].Locationy] = 1;
                                rear1++;
                            }
                        }
                    }
                }
                front++;
                ////当第一个队列的首尾指针指向一个位置时，更新mystack1的数据到mystack中
                if (front == rear)
                {
                    while (front1 < rear1)
                    {
                        mystack[rear] = new MyStack();
                        mystack[rear].Locationx = mystack1[front1].Locationx;
                        mystack[rear].Locationy = mystack1[front1].Locationy;
                        rear++;
                        front1++;
                    }
                }
            }
        }
        #endregion


        #region 各种琐碎的小优化
        public void Set()
        {
            this.pictureBox1.Visible = false;
            this.pictureBox2.Visible = false;
            this.pictureBox3.Visible = false;
            this.pictureBox4.Visible = false;
            this.pictureBox5.Visible = false;
            this.pictureBox6.Visible = false;
            this.pictureBox7.Visible = false;
            this.pictureBox8.Visible = false;
            this.pictureBox9.Visible = false;
        }
        public void Get()
        {
            this.pictureBox1.Visible = true;
            this.pictureBox2.Visible = true;
            this.pictureBox3.Visible = true;
            this.pictureBox4.Visible = true;
            this.pictureBox5.Visible = true;
            this.pictureBox6.Visible = true;
            this.pictureBox7.Visible = true;
            this.pictureBox8.Visible = true;
            this.pictureBox9.Visible = true;
        }
        /// <summary>
        /// 计时器的更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            Hourtime.secondx++;
            pictureBox4.Image = imageList1.Images[Hourtime.secondx % 10];
            if (Hourtime.secondx>9)
            {
                Hourtime.secondy++;
                Hourtime.secondx = 0;
                if (Hourtime.secondx == 0 && Hourtime.secondy == 6)
                pictureBox3.Image = imageList1.Images[0];
                else pictureBox3.Image = imageList1.Images[Hourtime.secondy];
            }
            if (Hourtime.secondy ==6)
            {
                Hourtime.secondy = 0;
                Hourtime.minutex++;
                pictureBox2.Image = imageList1.Images[Hourtime.minutex];

            }
            if (Hourtime.minutex > 9)
            {
                Hourtime.minutex = 0;
                Hourtime.minutey++;
                pictureBox1.Image = imageList1.Images[Hourtime.minutey];
            }
            //else
            //label3.Text = time.ToString();
        }
        private void label_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
        private void label_MouseLeave(object sender,EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void 显示主页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //判断是否已经最小化于托盘 
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示 
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点 
                this.Activate();
                //任务栏区显示图标 
                this.ShowInTaskbar = true;
                //托盘区图标隐藏 
               // notifyicon1.Visible = false;
            }
        }

        private void 退出游戏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Dispose();
        }
        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Battle battle = new Battle();
            battle.Mydele = new mydelegate(my_event);
            battle.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void 重新开始游戏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lostFlag = 0;
            timer1.Enabled = false;
            Hourtime = new CountClass(0, 0, 0, 0);
            this.pictureBox1.Image = imageList1.Images[0];
            this.pictureBox2.Image = imageList1.Images[0];
            this.pictureBox3.Image = imageList1.Images[0];
            this.pictureBox4.Image = imageList1.Images[0];
            this.pictureBox5.Image = imageList1.Images[10];
            //label3.Text = time.ToString();
            if (this.panel1.Visible == true)
            {
                this.panel1.Controls.Clear();
                setBg();
            }
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                    visited[i, j] = 0;
            }

            WinFlag = 0;
            FlagCount = 0;
            first = true;
            pictureBox7.Image = imageList1.Images[FlagCount % 10];
            pictureBox6.Image = imageList1.Images[FlagCount / 10];
        }

        private void my_event(String str, int level)
        {
            this.下一位玩家开始ToolStripMenuItem.Visible = true;

        }
        #endregion 



    }
}
