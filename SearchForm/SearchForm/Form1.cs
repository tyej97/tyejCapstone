using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchForm
{
    public partial class Form1 : Form
    {
        private static ManualResetEvent mre = new ManualResetEvent(false);
        public Form1()
        {
            InitializeComponent();
            

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            /*string[] array = new string[5] { "1", "2", "3", "4", "5" };
            for(int i = 0; i < array.Length; i++)
            {
                CreateNewCell( i ,array[i]);
            }
            */
            label1.Text = hScrollBar1.Value.ToString();
            Thread workerThread = new Thread(new ThreadStart(MoveCellTo));
            workerThread.Start();

        }//end of startButton_Click

        private void CreateNewCell(int cellname, string cellValue)
        {
            cell1.Visible = true;


        }//end of createNewCell

        private void MoveCellTo()
        {
            Label current = cell1;
            String speed = "Fast";
            Point newLocation = new Point(Convert.ToInt32(Math.Round(numericUpDown1.Value,0)), Convert.ToInt32(Math.Round(numericUpDown2.Value, 0)));
            int movingXFactor = 0; //the number of pixels the cell will move each frame on the X axis
            int movingYFactor = 0; //the number of pixels the cell will move each frame on the Y axis
            //Timer to cause the wait between each frame

            switch (speed)
            {
                case "Fast":
                    if (newLocation.X - current.Location.X > 0)
                    {
                        movingXFactor = (newLocation.X - current.Location.X) / 5;
                    }
                    else
                    {
                        movingXFactor = (newLocation.X - current.Location.X) / 5;
                    }
                    if (newLocation.Y - current.Location.Y > 0)
                    {
                        movingYFactor = (newLocation.Y - current.Location.Y) / 5;
                    }
                    else
                    {
                        movingYFactor = (newLocation.Y - current.Location.Y) / 5;
                    }
                    break;
            } // end of switch statement

            if(speed == "Fast")
            {
                
                int[,] moveArray = new int[5,2];// X is stored in [x,0] and Y is stored in [x,1]
                for(int i = 0; i<4; i++) //For loop to calculate the coordinates needed for each frame of the fast movement animation and load them into the moveArray
                {
                    moveArray[i,0] = current.Location.X + (movingXFactor * i);
                    moveArray[i,1] = current.Location.Y + (movingYFactor * i);
                }
                moveArray[4, 0] = newLocation.X;
                moveArray[4, 1] = newLocation.Y;


                for (int j = 0; j < 5; j++)
                {
                    Thread.Sleep(10);
                    if(current.Left < moveArray[j,0] || current.Left > moveArray[j, 0])
                    {
                        HideCell(current);
                        Thread.Sleep(10);
                        MoveCellHorizontally(current, moveArray[j, 0]);
                        MoveCellVertically(current, moveArray[j, 1]);
                        
                        Thread.Sleep(hScrollBar1.Value);
                        
                    }

                }//end of for loop
                
            }//end of if(speed == fast)

        }//end of MoveCellTo


        private void MoveCellHorizontally(Label cell, int xCoordinate)
        {
            Task.Factory.StartNew(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    cell.Left = xCoordinate;
                    cell.Show();
                    cell.Refresh();
                });
            });
        }//end of MoveCellHorizontally

        private void MoveCellVertically(Label cell, int yCoordinate)
        {
            Task.Factory.StartNew(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    cell.Top = yCoordinate;
                    cell.Show();
                    cell.Refresh();
                });
            });
        }//end of MoveCellHorizontally

        private void HideCell(Label cell)
        {
            Task.Factory.StartNew(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    cell.Hide();
                });
            });
        }

        private void ShowCell(Label cell)
        {
            Task.Factory.StartNew(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    cell.Show();
                });
            });
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            label1.Text = hScrollBar1.Value.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = hScrollBar1.Value.ToString();
            for (int x = 0; x < updownCells.Value; x++)
            {
                //CreateNewCell(55, "cell");
            }
        }
    }
}
