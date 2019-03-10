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
        private Label[] cellArray;
        #region cell defaults
        private static bool defaultAutoSizeSetting = false;
        private static int defaultCellWidth = 30;
        private static int defaultCellHeight = 20;
        private static BorderStyle defaultCellBorderStyle = BorderStyle.FixedSingle;
        private static ContentAlignment defaultCellTextAlign = ContentAlignment.MiddleCenter;
        private static Point defaultCellPosition = new Point(20, 100);
        private static Point[] ArrayPositions = new Point[] { new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point()};
         // Point numbering guide                            
        #endregion




        public Form1()
        {
            InitializeComponent();
            cellArray = new Label[] { label1, label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16, label17, label18, label19, label20, label21, label22, label23, label24, label25, label26, label27, label28, label29, label30, label31, label32, label33, label34, label35, label36, label37, label38, label39, label40, label41, label42, label43, label44, label45, label46, label47, label48, label49, label50, label51, label52, label53, label54, label55, label56, label57, label58, label59, label60, label61, label62, label63, label64, label65, label66, label67, label68, label69, label70, label71, label72, label73, label74, label75, label76, label77, label78, label79, label80, label81, label82, label83, label84, label85, label86, label87, label88, label89, label90, label91, label92, label93, label94, label95, label96, label97, label98, label99, label100, label101, label102, label103, label104, label105, label106, label107, label108, label109, label110, label111, label112, label113, label114 };
            LoadArrayPositions();
            Thread workerThread = new Thread(new ThreadStart(SetCellArrayDefaults));
            workerThread.Start();

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            /*string[] array = new string[5] { "1", "2", "3", "4", "5" };
            for(int i = 0; i < array.Length; i++)
            {
                CreateNewCell( i ,array[i]);
            }
            */
            ScrollLabel.Text = hScrollBar1.Value.ToString();
            Thread workerThread = new Thread(new ThreadStart(MoveCellTo));
            workerThread.Start();

        }//end of startButton_Click

        private void CreateNewCell(int cellname, string cellValue)
        {
            label1.Visible = true;


        }//end of createNewCell

        private void MoveCellTo()
        {
            Label current = label1;
            Point newLocation = new Point(Convert.ToInt32(Math.Round(numericUpDown1.Value,0)), Convert.ToInt32(Math.Round(numericUpDown2.Value, 0)));
            int movingXFactor = 0; //the number of pixels the cell will move each frame on the X axis
            int movingYFactor = 0; //the number of pixels the cell will move each frame on the Y axis
            //Timer to cause the wait between each frame

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
                
            int[,] moveArray = new int[5,2];// X is stored in [x,0] and Y is stored in [x,1]
            for(int i = 0; i<4; i++) //For loop to calculate the coordinates needed for each frame of the fast movement animation and load them into the moveArray
            {
                moveArray[i,0] = current.Location.X + (movingXFactor * (i+1));
                moveArray[i,1] = current.Location.Y + (movingYFactor * (i+1));
            }
            moveArray[4, 0] = newLocation.X;
            moveArray[4, 1] = newLocation.Y;


            for (int j = 0; j < 5; j++)
            {
                Thread.Sleep(10);
                if(current.Left != moveArray[j,0] || current.Top != moveArray[j, 1])
                {
                    HideCell(current);
                    Thread.Sleep(10);
                    MoveCellHorizontally(current, moveArray[j, 0]);
                    MoveCellVertically(current, moveArray[j, 1]);
                        
                    Thread.Sleep(hScrollBar1.Value);
                        
                }

            }//end of for loop

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
            ScrollLabel.Text = hScrollBar1.Value.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ScrollLabel.Text = hScrollBar1.Value.ToString();
        }

        private void SetCellArrayDefaults()
        {
            for(int i = 0; i < cellArray.Length; i++)
            {
                cellArray[i].Invoke(new Action(() => cellArray[i].AutoSize = defaultAutoSizeSetting) );
                cellArray[i].Invoke(new Action(() => cellArray[i].Width = defaultCellWidth) );
                cellArray[i].Invoke(new Action(() => cellArray[i].Height = defaultCellHeight) );
                cellArray[i].Invoke(new Action(() => cellArray[i].BorderStyle = defaultCellBorderStyle) );
                cellArray[i].Invoke(new Action(() => cellArray[i].TextAlign = defaultCellTextAlign) );
                cellArray[i].Invoke(new Action(() => cellArray[i].Location = ArrayPositions[i]) );
                string temp = (i + 1).ToString();
                if (temp.Length == 1)
                    temp = "00" + temp;
                if (temp.Length == 2)
                    temp = "0" + temp;
                cellArray[i].Invoke(new Action(() => cellArray[i].Text = temp));
                cellArray[1].Invoke(new Action(() => cellArray[i].Hide()));
            }//end for loop

            systemActionLabel.Invoke(new Action(() => systemActionLabel.Text = "Program Ready"));
        }//end of SetCellArrayDefaults

        private void LoadArrayPositions()
        {
            int tempX;
            int tempY;
            int count = 0;
            for (tempY = 150; tempY < 410; tempY += 50)
            {
                tempX = 20;
                for(tempX = 20; tempX < 750; tempX += 40)
                {
                    ArrayPositions[count] = new Point(tempX, tempY);
                    count++;
                }
            }//end of for loop
        }// end of LoadArrayPositions
    }
}
