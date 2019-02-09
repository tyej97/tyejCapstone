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
            string[] array = new string[5] { "1", "2", "3", "4", "5" };
            for(int i = 0; i < array.Length; i++)
            {
                CreateNewCell( i ,array[i]);
            }


            Point temp = new Point(100, 100);
            String tempString = "Fast";
            if (cell1.Location == temp)
                return;
            MoveCellTo(cell1,temp,tempString);

        }//end of startButton_Click

        private void CreateNewCell(int cellname, string cellValue)
        {
            cell1.Visible = true;


        }//end of createNewCell

        private void MoveCellTo(Label current, Point newLocation, String speed)
        {
            int movingXFactor = 0; //the number of pixels the cell will move each frame on the X axis
            int movingYFactor = 0; //the number of pixels the cell will move each frame on the Y axis
            //Timer to cause the wait between each frame

            switch (speed)
            {
                case "Fast": movingXFactor = (newLocation.X - current.Location.X) / 5;
                    movingYFactor = (newLocation.Y - current.Location.Y) / 5;
                    break;
            } // end of switch statement

            if(speed == "Fast")
            {
                
                Point[] moveArray = new Point[5];
                for(int i = 0; i<5; i++) //For loop to calculate the coordinates needed for each frame of the fast movement animation and load them into the moveArray
                {
                    int tempX = current.Location.X + (movingXFactor * i);
                    int tempY = current.Location.Y + (movingYFactor * i);
                    moveArray[i] = new Point(tempX, tempY);
                }

                current.Location = moveArray[0];
                current.Update();
                for (int j = 1; j < 5; j++)
                {
                    SleepFor(1000);
                    mre.WaitOne();
        
                    current.Location = moveArray[j];
                    current.Refresh();
                }
                
            }//end of if(speed == fast)

            current.Location = new Point(100, 100);
        }//end of MoveCellTo


        private void SleepFor(int milliseconds)
        {
            Thread.Sleep(milliseconds);
            mre.Set();
        }
    }
}
