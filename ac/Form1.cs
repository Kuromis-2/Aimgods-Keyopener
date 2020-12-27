using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Input;
using System.Net;
using System.IO;
using aimgods;
using Newtonsoft.Json.Linq;

namespace ac
{
    
    public partial class Form1 : Form
    {
        private static bool activepress = false;
        public static int requests;
        public static int delayms;
        public static string usernae;
        public static string passwod;
        public Form1()
        {
            InitializeComponent();
            
        }
        bool isrunning = true;
        private Point offset;
        private Point start = new Point(0, 0);
        bool mousedown = false;

        private void gradientPanel1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mousedown = true;
            start = new Point(e.X, e.Y);
        }
        private void gradientPanel1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (mousedown)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.start.X, p.Y - this.start.Y);
            }
        }
        private void gradientPanel1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mousedown = false;
        }
        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            usernae = username.Text;
            passwod = passwordtext.Text;
            try
            {
                FinalmouseAPI API = new FinalmouseAPI(null,usernae,passwod);
                Form2 formm = new Form2();
                formm.API = API;
                formm.Refresh();
                API.meAPI();
                this.Hide();
                formm.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show("non poggies");
                if (Keyboard.IsKeyDown(Key.LeftShift)&&Keyboard.IsKeyDown(Key.LeftAlt))
                    throw;
            }
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n')
            {
                button1_Click_1(null,null);
            }
        }
        private void passwordtext_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n')
            {
                button1_Click_1(this,new EventArgs());
                e.Handled = true;
            }
        }
    }
}
