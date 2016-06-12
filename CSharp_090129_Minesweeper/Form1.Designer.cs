using System;
using System.Windows.Forms;

namespace MineSweeper
{
    partial class Form1
    {
        /// <summary>
        /// 
        /// </summary>
        public int width;

        /// <summary>
        /// 
        /// </summary>
        public int height;

        /// <summary>
        /// 
        /// </summary>
        public int mineCount;

        /// <summary>
        /// 
        /// </summary>
        public bool[,] bools;

        /// <summary>
        /// 
        /// </summary>
        public Button[,] buttons;

        /// <summary>
        /// 
        /// </summary>
        public int unclickedButtonCount;

        /// <summary>
        /// 
        /// </summary>
        public TextBox textBox1;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        public System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="mct"></param>
        public Form1(int w, int h, int mc)
        {
            // Width.
            if (w < 0)
            {
                throw new Exception("TODO");
            }
            width = w;

            // Height.
            if (h < 0)
            {
                throw new Exception("TODO");
            }
            height = h;

            // Mine count.
            if ((mc < 0) || (mc > width * height))
            {
                throw new Exception("TODO");
            }
            mineCount = mc;

            // Bools.
            bools = new bool[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bools[x, y] = false;
                }
            }

            // Mines.
            for (int i = 0; i < mineCount; i++)
            {
                Random r = new Random();
                int x;
                int y;
                do
                {
                    x = r.Next(width);
                    y = r.Next(height);
                }
                while (bools[x, y]);

                // There is no mine at [x,y].

                bools[x, y] = true;
            }

            unclickedButtonCount = width * height;

            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // The size of the button.
            int buttonSize = 50;

            buttons = new Button[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    buttons[x, y] = new Button();
                }
            }
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();

            //
            // buttons
            //
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Button button = buttons[x, y];

                    int upperLeftCornerX = x * buttonSize;
                    int upperLeftCornerY = y * buttonSize;

                    button.Location = new System.Drawing.Point(upperLeftCornerX, upperLeftCornerY);
                    button.Name = String.Format("{0},{1}", x, y);
                    button.Size = new System.Drawing.Size(buttonSize, buttonSize);
                    button.UseVisualStyleBackColor = true;
                    button.Click += new System.EventHandler(this.button_Click);
                }
            }

            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, height * buttonSize);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(width * buttonSize, buttonSize);

            // 
            // Form1
            //
            this.ClientSize = new System.Drawing.Size(width * buttonSize, (height + 1) * buttonSize);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    this.Controls.Add(buttons[x, y]);
                }
            }
            this.Controls.Add(textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string[] xy = button.Name.Split(',');
            int x = Int32.Parse(xy[0]);
            int y = Int32.Parse(xy[1]);

            unclickedButtonCount--;

            if (bools[x, y])
            {
                // Clicked on mine => game over.
                button.Text = "X";

                // Disable all buttons.
                foreach (Button b in buttons)
                {
                    b.Enabled = false;
                }

                textBox1.Text = "You have lost!";
            }
            else
            {
                // Clicked not on mine => display number of neighbouring mines.
                int neighbouringMineCount = CountNeighbouringMines(x, y);
                button.Text = neighbouringMineCount.ToString();
                button.Enabled = false;

                if (neighbouringMineCount == 0)
                {
                    int i;
                    int j;

                    // Upper left.
                    i = x - 1;
                    j = y - 1;
                    if (Within(i, j) && buttons[i,j].Enabled)
                    {
                        buttons[i, j].PerformClick();
                    }

                    // Upper center.
                    i = x;
                    j = y - 1;
                    if (Within(i, j) && buttons[i, j].Enabled)
                    {
                        buttons[i, j].PerformClick();
                    }

                    // Upper left.
                    i = x + 1;
                    j = y - 1;
                    if (Within(i, j) && buttons[i, j].Enabled)
                    {
                        buttons[i, j].PerformClick();
                    }

                    // Left.
                    i = x - 1;
                    j = y;
                    if (Within(i, j) && buttons[i, j].Enabled)
                    {
                        buttons[i, j].PerformClick();
                    }

                    // Right.
                    i = x + 1;
                    j = y;
                    if (Within(i, j) && buttons[i, j].Enabled)
                    {
                        buttons[i, j].PerformClick();
                    }

                    // Lower left.
                    i = x - 1;
                    j = y + 1;
                    if (Within(i, j) && buttons[i, j].Enabled)
                    {
                        buttons[i, j].PerformClick();
                    }

                    // Lower center.
                    i = x;
                    j = y + 1;
                    if (Within(i, j) && buttons[i, j].Enabled)
                    {
                        buttons[i, j].PerformClick();
                    }

                    // Lower right.
                    i = x + 1;
                    j = y + 1;
                    if (Within(i, j) && buttons[i, j].Enabled)
                    {
                        buttons[i, j].PerformClick();
                    }
                }

                // Won!
                if (unclickedButtonCount == mineCount)
                {
                    // Disable all buttons.
                    foreach (Button b in buttons)
                    {
                        b.Enabled = false;
                    }

                    textBox1.Text = "You have won!";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int CountNeighbouringMines(int x, int y)
        {
            int i;
            int j;
            int neighbouringMineCount = 0;

            // Upper left,
            i = x - 1;
            j = y - 1;
            if (Within(i, j) && bools[i, j])
            {
                neighbouringMineCount++;
            }

            // Upper center.
            i = x;
            j = y - 1;
            if (Within(i, j) && bools[i, j])
            {
                neighbouringMineCount++;
            }

            // Upper left.
            i = x + 1;
            j = y - 1;
            if (Within(i, j) && bools[i, j])
            {
                neighbouringMineCount++;
            }

            // Left.
            i = x - 1 ;
            j = y;
            if (Within(i, j) && bools[i, j])
            {
                neighbouringMineCount++;
            }

            // Right.
            i = x + 1;
            j = y;
            if (Within(i, j) && bools[i, j])
            {
                neighbouringMineCount++;
            }

            // Lower left.
            i = x - 1;
            j = y + 1;
            if (Within(i, j) && bools[i, j])
            {
                neighbouringMineCount++;
            }

            // Lower center.
            i = x;
            j = y + 1;
            if (Within(i, j) && bools[i, j])
            {
                neighbouringMineCount++;
            }

            // Lower right.
            i = x + 1;
            j = y + 1;
            if (Within(i, j) && bools[i, j])
            {
                neighbouringMineCount++;
            }

            return neighbouringMineCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool Within(int x, int y)
        {
            if ((x < 0) || (width <= x))
            {
                return false;
            }

            if ((y < 0) || (height <= y))
            {
                return false;
            }

            return true;
        }
    }
}