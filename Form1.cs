using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

 
    public partial class Form1 : Form
    {
        
        Crestic game = new Crestic();
        public Form1()
        {
           

            InitializeComponent();
            button_start.Click += Button_start_Click;
            button_choose.Click += Button_choose_Click;
            button1.Click += Button1_Click;
            button2.Click += Button1_Click;
            button3.Click += Button1_Click;
            button4.Click += Button1_Click;
            button5.Click += Button1_Click;
            button6.Click += Button1_Click;
            button7.Click += Button1_Click;
            button8.Click += Button1_Click;
            button9.Click += Button1_Click;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (game.gamestart && !game.gameend)
            {
                int n = Convert.ToInt32(((Button)sender).AccessibleName);
                int result = game.SelectPos(n, ref sender, ref button_choose);
                if (result == 3) { label2.Enabled = true; label2.Text = "Неверный ход"; }
                else if(result == 4) { label2.Enabled = true; label2.Text = "Ничья"; }
                else if (result == 2) { label2.Enabled = true; label2.Text = "Крестики победили"; }
                else if (result == 1) { label2.Enabled = true; label2.Text = "Нолики победили"; }
                else { label2.Enabled = false; label2.Text = ""; }
            }
        }

        private void Button_choose_Click(object sender, EventArgs e)
        {
            game.Change(ref button_choose);
    
        }

        private void Button_start_Click(object sender, EventArgs e)
        {
            game.Start(ref button_choose, ref button_start, ref button1, ref button2, ref button3, ref button4, ref button5, ref button6, ref button7,
                ref button8, ref button9);
            label2.Text = "";
        }

    }
    class Crestic
    {
       
        private bool IsCrestic;
        public bool gamestart;
        public bool gameend;
        private int[] area = new int[9];
        public Crestic()
        {
            IsCrestic = true;
            for (int i = 0; i < 9; i++)
            {
                area[i] = 0;
            }
        }
       
        public void Start(ref Button a, ref Button start, ref Button b1, ref Button b2, ref Button b3, ref Button b4, ref Button b5, ref Button b6,
            ref Button b7, ref Button b8, ref Button b9)
        {
            if (gamestart)
            {
                End(ref a, ref start, ref b1, ref  b2, ref  b3, ref  b4, ref  b5, ref  b6,
            ref  b7, ref  b8, ref b9);
            }
            else
            {
              
                gameend = false;
               gamestart = true;
                start.Text = "Заново";
            }
        }
        private void End(ref Button a, ref Button start, ref Button b1, ref Button b2, ref Button b3, ref Button b4, ref Button b5, ref Button b6,
            ref Button b7, ref Button b8, ref Button b9)
        {
            gamestart = false;
            IsCrestic = true;
            a.Image = WindowsFormsApp1.Properties.Resources.Crest;
            start.Text = "Начать";
            for (int i = 0; i < 9; i++)
            {
                area[i] = 0;
            }
            b1.Image = null;
            b2.Image = null;
            b3.Image = null;
            b4.Image = null;
            b5.Image = null;
            b6.Image = null;
            b7.Image = null;
            b8.Image = null;
            b9.Image = null;
        }

        public void Change(ref Button a)
        {
            if (!gamestart)
                IsCrestic = !IsCrestic;
            if (IsCrestic)
                a.Image = WindowsFormsApp1.Properties.Resources.Crest;
            else a.Image = WindowsFormsApp1.Properties.Resources.Noll;
        }
        public int SelectPos(int x,ref object a, ref Button c)
        {
          if(!gameend)
            if (area[x] != 0) return 3;
            else
            {
                area[x] = Convert.ToInt32(IsCrestic) + 1;
               if(area[x]==2)  ((Button)a).Image = WindowsFormsApp1.Properties.Resources.Crest;
                if (area[x] == 1) ((Button)a).Image = WindowsFormsApp1.Properties.Resources.Noll;
                    if (Win()) { gameend = true; return Convert.ToInt32(IsCrestic) + 1; }
                    
                IsCrestic = !IsCrestic;
                    if (DoubleNichia()) { gameend = true; return 4; }
                    if (IsCrestic) c.Image = WindowsFormsApp1.Properties.Resources.Crest;
                    else c.Image = WindowsFormsApp1.Properties.Resources.Noll ;
                }
            return 0;
        }
        private bool Win()
        {
            if (area[0] == area[1] && area[1] == area[2] && area[0] != 0
                || area[3] == area[4] && area[4] == area[5] && area[3] != 0
                || area[6] == area[7] && area[7] == area[8] && area[6] != 0)
            {
                return true;
            }
            if (area[0] == area[3] && area[3] == area[6] && area[0] != 0
                || area[1] == area[4] && area[4] == area[7] && area[1] != 0
                || area[2] == area[5] && area[5] == area[8] && area[2] != 0)
            {
                return true;
            }
            if (area[0] == area[4] && area[4] == area[8] && area[0] != 0
                || area[2] == area[4] && area[4] == area[6] && area[2] != 0)
            {
                return true;
            }
            return false;

        }
        private bool DoubleNichia()
        {

        
            int c = 0;
            for(int i=0;i<9;i++)
            {
                if (area[i] == 0)
                {
                    area[i] = Convert.ToInt32(IsCrestic) + 1;
                    if (!Nichia()) c++;
                    area[i] = 0;
                }
            }
            if (c == 0) return true;
            else return false;
        }
        private bool Nichia()
        {
            int[] a = new int[9];
            area.CopyTo(a, 0);
            int c = 0;
            for(int i=0;i<9;i++)
            {
                if (area[i] == 0) area[i] = 1;
            }
            if (Win()) c++;
            a.CopyTo(area, 0);
            for(int i=0;i<9;i++)
            {
                if (area[i] == 0) area[i] = 2;
            }
            if (Win()) c++;
            a.CopyTo(area, 0);
            if (c == 0) return true;
            else return false;
        }

    }

}
