using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoomShakalaka
{

    /// <summary>
    /// 玩法：对战模式
    /// </summary>
    public partial class Battle : Form
    {
        
        public mydelegate Mydele;
        private int level = 1;
        public Battle()
        {
            InitializeComponent();
        }

        private void Battle_Load(object sender, EventArgs e)
        {
            ///button的修饰
            button1.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.BorderSize = 0;
            ////comboBox用于显示选择的对战人数
            this.comboBox1.Text = this.comboBox1.Items[0].ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked == true) level = 1;
            else if (this.radioButton2.Checked == true) level = 2;
            else if (this.radioButton3.Checked == true) level = 3;
            switch (level)
            {
                case 1:
                    if (MessageBox.Show("您选择的游戏模式是:简单  您选择的游戏人数为：" + this.comboBox1.Text.ToString()+"\n"
                        +"确定开始游戏吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) 
                    {
                        ///委托传值
                        Form1 form = new Form1();
                        Mydele(comboBox1.Text.ToString(), level);
                        this.Close();
                        this.Dispose();
                    }
                    break;
                case 2:
                    if (MessageBox.Show("您选择的游戏模式是:普通  您选择的游戏人数为：" + this.comboBox1.Text.ToString()+"\n"+
                        "确定开始游戏吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) 
                    {
                        ///委托传值
                        Form1 form = new Form1();
                        Mydele(comboBox1.Text.ToString(), level);
                        this.Close();
                        this.Dispose();
                    }
                    break;
                case 3:
                    if (MessageBox.Show("您选择的游戏模式是:困难  您选择的游戏人数为：" + this.comboBox1.Text.ToString() + "\n" + 
                        "确定开始游戏吗？", "提示",MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) 
                    {
                        ///委托传值
                        Form1 form = new Form1();
                        Mydele(comboBox1.Text.ToString(), level);
                        this.Close();
                        this.Dispose();
                    }
                    break;
            }
        }
    }
}
