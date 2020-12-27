using aimgods;
using System;
using System.Drawing;
using System.Net.Mime;
using System.Windows.Forms;
using System.Threading;

namespace ac
{
    public partial class Form2 : Form
    {
        public FinalmouseAPI API;
        public Form2()
        {

            InitializeComponent();
            zwei = KeyText;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private Point offset;
        private Point start = new Point(0, 0);
        bool mousedown = false;
        private void gradientPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            mousedown = true;
            start = new Point(e.X, e.Y);
        }

        private void gradientPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousedown)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.start.X, p.Y - this.start.Y);
            }
        }
        private void gradientPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            mousedown = false;
        }

        private void Refresh()
        {
            try
            {
                API.meAPI();
                if ((string)API.me["PlayerRank"]=="0")
                {
                    LabelRank.Text = "N/A";
                }
                else
                {
                    LabelRank.Text = API.me["PlayerRank"].ToString();
                }
                LabelKeys.Text = API.me["GoldenKeys"].ToString();
                activegame.Text = API.me["IsActive"].ToString();
                LabelUsername.Text = API.me["UserName"].ToString();
                if (activegame.Text=="true")
                {
                    LabelTier.Text = API.me["PlayerTier"].ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        private void refresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        public static TextBox zwei = null;

        private void button1_Click(object sender, EventArgs e)
        {

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                if (Int32.Parse(API.me["GoldenKeys"].ToString()) == 0)
                {
                    MessageBox.Show("No Keys left (non poggies)");
                }
                API.openKey(Int32.Parse(API.me["GoldenKeys"].ToString()), 200,KeyText);
            }).Start();
            //API.openKey(100);
        }
        
        private void pictureBox4_Click(object sender, EventArgs e)
        {
        
        }
    }
}
