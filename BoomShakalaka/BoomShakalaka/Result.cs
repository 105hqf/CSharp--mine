using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BoomShakalaka
{


    public partial class Result : Form
    {
        private Game []game=new Game[100];
        private String s;
        private int level;
        private String[] resultArray = new String[100];
        private int result;
        public Result()
        {
            InitializeComponent();
        }

        private void Result_Load(object sender, EventArgs e)
        {
            switch(level)
            {
                case 0:
                    Read(Application.StartupPath + "\\easy.txt", 0);
                    break;
                case 1:
                    Read(Application.StartupPath + "\\medium.txt", 1);
                    break;
                case 2:
                    Read(Application.StartupPath + "\\hard.txt", 2);
                    break;
                default:
                    MessageBox.Show("游戏出错，请联系开发者！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Dispose();
                    break;
            }
            Result1(level);
            this.label3.Text = s;
            this.label4.Text = "第" + result.ToString() + " 名!";
            this.label6.Text = resultArray[0].ToString()+"秒";
        }
        /// <summary>
        /// 如果游戏成功，读取游戏所花的时间,以及游戏难度
        /// </summary>
        /// <param name="str"></param>
        public void Get(String str,int Level)
        {
            s = str;
            level = Level;
        }

        #region 文件的读写，每一次游戏成绩记录文件中，以用于排名
        /// <summary>
        /// 文件的读取
        /// </summary>
        /// <param name="path"></param>
        /// <param name="level"></param>
        private void Read(string path,int level)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            int line;
            String str = "";
            switch (level)
            {
                case 0:
                    line=0;

                    while (str != null)
                    {
                        ////文件的读取
                        if ((str = sr.ReadLine()) != null)
                        {
                            this.listBox1.Items.Add(str);
                            line++;
                        }
                    }
                     break;
                case 1:

                     line=0;
                    while (str != null)
                    {
                        ////文件的读取
                        if ((str = sr.ReadLine()) != null)
                        {
                            this.listBox2.Items.Add(str);
                            line++;
                        }
                    }
                     break;
                case 2:
                     line = 0;
                     while (str != null)
                    {
                        ////文件的读取
                        if ((str = sr.ReadLine()) != null)
                        {
                            this.listBox3.Items.Add(str);
                            line++;
                        }
                    }
                     break;
            }
            sr.Close();
            sr.Dispose();
        }
        /// <summary>
        /// 数据的重新写入,保存在文件中
        /// </summary>
        /// <param name="str"></param>
        private void Write(String []str,int level)
        {
            int length=0;
            ///根据难易度分别对成绩进行记录
            switch (level)
            {
                case 0:
                    length = 0;
                    ///文件流。打开文件准备写入,SreamWrite(path,bool append)中，后面的Append如果为false，则将打开的文件数据覆盖
                    StreamWriter sw = new StreamWriter(Application.StartupPath + "\\easy.txt",false);
                    while (str[length] != null)
                    {
                        sw.WriteLine(str[length]);
                        
                        length++;
                    }
                    sw.Close();
                    sw.Dispose();
                    break;
                case 1:
                    length = 0;
                    StreamWriter sw1 = new StreamWriter(Application.StartupPath + "\\medium.txt",false);
                    while (str[length] != null)
                    {
                        sw1.WriteLine(str[length]);
                        
                        length++;
                    }
                    sw1.Close();
                    sw1.Dispose();
                    break;
                case 2:
                    length = 0;
                    StreamWriter sw2 = new StreamWriter(Application.StartupPath + "\\hard.txt",false);
                    while (str[length] != null)
                    {
                         sw2.WriteLine(str[length]);
                        
                        length++;
                    }
                    sw2.Close();
                    sw2.Dispose();
                    break;

            }

        }
        #endregion


        private void button1_Click(object sender, EventArgs e)
        {
            Write(resultArray, level);
            this.Dispose();
        }

        #region 游戏排名
        /// <summary>
        /// 读取listbox中的数据，将其存入resultArray中，用于判断当前游戏所用时间的排名
        /// </summary>
        public void Result1(int level)
        {
            Game temp=new Game(0,0);
            Game temp1 = new Game(0, 0);
            int count = 0;
            switch (level)
            {
                case 0:
                    if (listBox1.Items.Count == 0)
                    {
                        resultArray[0]=s;
                        result = 0;
                    } 
                    else
                    {
                        for (int i = 0; i < listBox1.Items.Count; i++)
                        {
                            ///字符串分割，得到分钟和秒钟
                            String[] str = new String[2];
                            str = listBox1.Items[i].ToString().Trim().ToString().Split('分');
                            game[i] = new Game(0, 0);
                            game[i].Minute = int.Parse(str[0].Trim().ToString());
                            game[i].Second = int.Parse(str[1].Trim().ToString());
                        }
                        ///将目前的时间排序存入
                        String[] split = new String[2];
                        split= s.ToString().Trim().Split('分');
                        for (int j = 0; j < listBox1.Items.Count; j++)
                        {
                            if ((int.Parse(split[1]) < game[j].Second && int.Parse(split[0]) < game[j].Minute) ||
                                (int.Parse(split[1]) >= game[j].Second && int.Parse(split[0]) < game[j].Minute) ||
                                (int.Parse(split[1]) < game[j].Second && int.Parse(split[0]) == game[j].Minute))
                            {
                                temp.Second = game[j].Second;
                                temp.Minute = game[j].Minute;
                                game[j].Second = int.Parse(split[1]);
                                game[j].Minute = int.Parse(split[0]);
                                for (int k = j + 1; k < listBox1.Items.Count + 1; k++)
                                {
                                    if (k == listBox1.Items.Count) game[k] = new Game(0, 0);
                                    temp1.Second = game[k].Second;
                                    temp1.Minute = game[k].Minute;
                                    game[k].Second = temp.Second;
                                    game[k].Minute = temp.Minute;
                                    temp = temp1;
                                }
                                ///当找到适合的位置后，返回该位置的下标，排序就是该位置的下标+1；
                                result = j + 1;
                                break;
                            }
                            else if (int.Parse(split[1]) == game[j].Second && int.Parse(split[0]) == game[j].Minute)
                            { result = j + 1; break; }

                            count++;
                      
                        }
                        ////当所有的内容都小于目前的时间的时候，将其存入尾端
                        if (count == listBox1.Items.Count)
                        {
                            game[listBox1.Items.Count] = new Game(0, 0);
                            game[listBox1.Items.Count].Minute = int.Parse(split[0]);
                            game[listBox1.Items.Count].Second = int.Parse(split[1]);
                            result = listBox1.Items.Count + 1;
                        }
                        for (int num = 0; num <listBox1.Items.Count; num++)
                        {
                            resultArray[num] = "";
                            resultArray[num] = game[num].Minute+"分"+game[num].Second;
                        }

                    }
                    break;
                case 1:
                    if (listBox2.Items.Count == 0)
                    {
                        resultArray[0]=s;
                        result = 0;
                    } 
                    else
                    {
                        for (int i = 0; i < listBox2.Items.Count; i++)
                        {
                            ///字符串分割，得到分钟和秒钟
                            String[] str = new String[2];
                            str = listBox2.Items[i].ToString().Trim().ToString().Split('分');
                            game[i] = new Game(0, 0);
                            game[i].Minute = int.Parse(str[0].Trim().ToString());
                            game[i].Second = int.Parse(str[1].Trim().ToString());
                        }
                        ///将目前的时间排序存入
                        String[] split = new String[2];
                        split= s.ToString().Trim().Split('分');
                        for (int j = 0; j < listBox2.Items.Count; j++)
                        {
                            if ((int.Parse(split[1]) < game[j].Second && int.Parse(split[0]) < game[j].Minute) ||
                                (int.Parse(split[1]) >= game[j].Second && int.Parse(split[0]) < game[j].Minute) ||
                                (int.Parse(split[1]) < game[j].Second && int.Parse(split[0]) == game[j].Minute))
                            {
                                temp.Second = game[j].Second;
                                temp.Minute = game[j].Minute;
                                game[j].Second = int.Parse(split[1]);
                                game[j].Minute = int.Parse(split[0]);
                                for (int k = j + 1; k < listBox2.Items.Count + 1; k++)
                                {
                                    if (k == listBox2.Items.Count) game[k] = new Game(0, 0);
                                    temp1.Second = game[k].Second;
                                    temp1.Minute = game[k].Minute;
                                    game[k].Second = temp.Second;
                                    game[k].Minute = temp.Minute;
                                    temp = temp1;
                                }
                                ///当找到适合的位置后，返回该位置的下标，排序就是该位置的下标+1；
                                result = j + 1;
                                break;
                            }
                            else if (int.Parse(split[1]) == game[j].Second && int.Parse(split[0]) == game[j].Minute)
                            { result = j + 1; break; }

                            count++;
                      
                        }
                        ////当所有的内容都小于目前的时间的时候，将其存入尾端
                        if (count == listBox2.Items.Count)
                        {
                            game[listBox2.Items.Count] = new Game(0, 0);
                            game[listBox2.Items.Count].Minute = int.Parse(split[0]);
                            game[listBox2.Items.Count].Second = int.Parse(split[1]);
                            result = listBox2.Items.Count + 1;
                        }
                        for (int num = 0; num <listBox2.Items.Count+1; num++)
                        {
                            resultArray[num] = game[num].Minute+"分"+game[num].Second;
                        }

                    }
                    break;
                case 2:
                    if (listBox3.Items.Count == 0)
                    {
                        resultArray[0]=s;
                        result = 0;
                    } 
                    else
                    {
                        for (int i = 0; i < listBox3.Items.Count; i++)
                        {
                            ///字符串分割，得到分钟和秒钟
                            String[] str = new String[2];
                            str = listBox3.Items[i].ToString().Trim().ToString().Split('分');
                            game[i] = new Game(0, 0);
                            game[i].Minute = int.Parse(str[0].Trim().ToString());
                            game[i].Second = int.Parse(str[1].Trim().ToString());
                        }
                        ///将目前的时间排序存入
                        String[] split = new String[2];
                        split= s.ToString().Trim().Split('分');
                        for (int j = 0; j < listBox3.Items.Count; j++)
                        {
                            if ((int.Parse(split[1]) < game[j].Second && int.Parse(split[0]) < game[j].Minute) ||
                                (int.Parse(split[1]) >= game[j].Second && int.Parse(split[0]) < game[j].Minute) ||
                                (int.Parse(split[1]) < game[j].Second && int.Parse(split[0]) == game[j].Minute))
                            {
                                temp.Second = game[j].Second;
                                temp.Minute = game[j].Minute;
                                game[j].Second = int.Parse(split[1]);
                                game[j].Minute = int.Parse(split[0]);
                                for (int k = j + 1; k < listBox3.Items.Count + 1; k++)
                                {
                                    if (k == listBox3.Items.Count) game[k] = new Game(0, 0);
                                    temp1.Second = game[k].Second;
                                    temp1.Minute = game[k].Minute;
                                    game[k].Second = temp.Second;
                                    game[k].Minute = temp.Minute;
                                    temp = temp1;
                                }
                                ///当找到适合的位置后，返回该位置的下标，排序就是该位置的下标+1；
                                result = j + 1;
                                break;
                            }
                            else if (int.Parse(split[1]) == game[j].Second && int.Parse(split[0]) == game[j].Minute)
                            { result = j + 1; break; }

                            count++;
                      
                        }
                        ////当所有的内容都小于目前的时间的时候，将其存入尾端
                        if (count == listBox3.Items.Count)
                        {
                            game[listBox3.Items.Count] = new Game(0, 0);
                            game[listBox3.Items.Count].Minute = int.Parse(split[0]);
                            game[listBox3.Items.Count].Second = int.Parse(split[1]);
                            result = listBox3.Items.Count + 1;
                        }
                        for (int num = 0; num <listBox3.Items.Count+1; num++)
                        {
                            resultArray[num] = game[num].Minute+"分"+game[num].Second;
                        }

                    }
                    break;
            }
       

        } 
        #endregion

    }
}
