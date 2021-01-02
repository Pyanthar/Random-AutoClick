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
using System.Runtime.InteropServices;

namespace AutoClick_by_Pyanthar_mdr
{
    public partial class Form1 : Form
    {
        float temp;
        bool isActive;
        System.Windows.Forms.Timer T1;
        int T=0;
        int test;
        int iTempo;
        int iVar = 20;
        Random rnd = new Random();


        //Clic souris
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        //////////
        
        public Form1()
        {
            InitializeComponent();
            T1 = new System.Windows.Forms.Timer();
            button1.Text = "Désactivé";
            textBox1.Text = "1";
            T1.Interval = 1;
            hScrollBar2.Value = (int)(Properties.Settings.Default.Opacity * 100);
        }

        void Tick (Object myObject,EventArgs myEventArgs)
        {
            if (iTempo == 0)
            {
                iTempo = 1;
            }
            T1.Interval = rnd.Next(iTempo, iTempo + iVar+1);
            label4.Text = "Tempo générée " + T1.Interval.ToString();
            T++;            
            if (IsKeyLocked(Keys.CapsLock))
            {
                label5.Text = "On";
                DoMouseClick();
            } else { label5.Text = "Off"; }
            
        
        }
        
        void stopAC ()
        {
            T1.Stop();
            button1.Text = "Désactivé";
            isActive = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            T1.Tick += new EventHandler(Tick);
           

        }

        public void DoMouseClick()
        {
            //simulate a click on the current  mouse position
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            temp = hScrollBar1.Value;
            iTempo = (int)temp;
            textBox1.Text = iTempo.ToString();
            label6.Text = "Un clic tout les " + temp.ToString() + " ms";
            stopAC();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (isActive == false)
            {
                isActive = true;
                T1.Start();
                button1.Text = "Activé";
            }else if (isActive == true)
            {
                stopAC();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //T1.Interval = Int32.Parse(textBox1.Text);
                iTempo = Int32.Parse(textBox1.Text);
            }
            catch { }
            if (iTempo == 0)
            {
                iTempo = 1;
                textBox1.Text = "1";
            }
            label6.Text = "Un clic tout les " + iTempo.ToString() + " ms";
            hScrollBar1.Value = iTempo;
            stopAC();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            test++;
            button2.Text = test.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            test=0;
            button2.Text = "Clique !";
        }


        //quitter
        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        //modes
        private void avancéToolStripMenuItem_Click(object sender, EventArgs e)
        {
            avancéToolStripMenuItem.Checked = true;
            normalToolStripMenuItem.Checked = false;
            textBox2.Enabled = true;
            textBox2.BackColor = Color.White;
            stopAC();
        }
        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iVar = 20;
            avancéToolStripMenuItem.Checked = false;
            normalToolStripMenuItem.Checked = true;
            stopAC();
            textBox2.BackColor = Color.DarkGray;
            textBox2.Text = "20";
            isActive = false;
            textBox2.Enabled = false;
        }


        //plage de variation
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                iVar = Int32.Parse(textBox2.Text);
            } catch { }
            stopAC();
        }


        //opacité
        private void hScrollBar2_ValueChanged(object sender, EventArgs e)
        {
            label7.Text = "Opacité : " + hScrollBar2.Value + " %";
            this.Opacity = (double)hScrollBar2.Value / 100;
            Properties.Settings.Default.Opacity = this.Opacity;
            Properties.Settings.Default.Save();
        }



        ////////////////////////
        //fonctions pour profils

        void loadsett (int i)
        {
            stopAC();

            switch (i)
            {
                case 1:
                    textBox1.Text = Properties.Settings.Default.P1_temp.ToString();
                    textBox2.Text = Properties.Settings.Default.P1_var.ToString();
                    //textBox1.Text = iTempo.ToString();
                    //lbldebug.Text = "load " + Properties.Settings.Default.P1_temp.ToString();

                    break;

                case 2:
                    textBox1.Text = Properties.Settings.Default.P2_temp.ToString();
                    textBox2.Text = Properties.Settings.Default.P2_var.ToString();
                    break;

                case 3:
                    textBox1.Text = Properties.Settings.Default.P3_temp.ToString();
                    textBox2.Text = Properties.Settings.Default.P3_var.ToString();
                    break;

                case 4:
                    textBox1.Text = Properties.Settings.Default.P4_temp.ToString();
                    textBox2.Text = Properties.Settings.Default.P4_var.ToString();
                    break;

            }

        }

        void savesett (int i)
        {
            stopAC();

            switch (i)
            {
                case 1:
                    Properties.Settings.Default.P1_temp = iTempo;
                    Properties.Settings.Default.P1_var = iVar;
                    //lbldebug.Text = "save " + Properties.Settings.Default.P1_temp.ToString();
                    break;

                case 2:
                    Properties.Settings.Default.P2_temp = iTempo;
                    Properties.Settings.Default.P2_var = iVar;
                    break;

                case 3:
                    Properties.Settings.Default.P3_temp = iTempo;
                    Properties.Settings.Default.P3_var = iVar;
                    break;

                case 4:
                    Properties.Settings.Default.P4_temp = iTempo;
                    Properties.Settings.Default.P4_var = iVar;
                    break;

            }
            //Properties.Settings.Default.Opacity = this.Opacity;
            Properties.Settings.Default.Save();
        }

        private void savep1_Click(object sender, EventArgs e)
        {
            savesett(1);
        }

        private void savep2_Click(object sender, EventArgs e)
        {
            savesett(2);
        }

        private void savep3_Click(object sender, EventArgs e)
        {
            savesett(3);
        }

        private void savep4_Click(object sender, EventArgs e)
        {
            savesett(4);
        }

        private void loadp1_Click(object sender, EventArgs e)
        {
            loadsett(1);
            profil1ToolStripMenuItem.Checked = true;
            profil2ToolStripMenuItem.Checked = false;
            profil3ToolStripMenuItem.Checked = false;
            profil4ToolStripMenuItem.Checked = false;
            lbl_profilname.Text = "Profil 1 :";
        }

        private void loadp2_Click(object sender, EventArgs e)
        {
            loadsett(2);
            profil1ToolStripMenuItem.Checked = false;
            profil2ToolStripMenuItem.Checked = true;
            profil3ToolStripMenuItem.Checked = false;
            profil4ToolStripMenuItem.Checked = false;
            lbl_profilname.Text = "Profil 2 :";
        }

        private void loadp3_Click(object sender, EventArgs e)
        {
            loadsett(3);
            profil1ToolStripMenuItem.Checked = false;
            profil2ToolStripMenuItem.Checked = false;
            profil3ToolStripMenuItem.Checked = true;
            profil4ToolStripMenuItem.Checked = false;
            lbl_profilname.Text = "Profil 3 :";
        }

        private void loadp4_Click(object sender, EventArgs e)
        {
            loadsett(4);
            profil1ToolStripMenuItem.Checked = false;
            profil2ToolStripMenuItem.Checked = false;
            profil3ToolStripMenuItem.Checked = false;
            profil4ToolStripMenuItem.Checked = true;
            lbl_profilname.Text = "Profil 4 :";
        }

        ////////////////////////////////////
        ////////////////////////////////////
    }
}
