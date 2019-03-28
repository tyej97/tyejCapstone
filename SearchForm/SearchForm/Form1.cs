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
        private int[] valueArray;
        private string filePath = "C:\\Users\\Craig Tye\\Desktop\\Homework\\Capstone\\Capstone Project\\tyejCapstone\\SearchForm\\SearchForm\\ArrayValues.txt";
        #region cell defaults
        private static bool defaultAutoSizeSetting = false;
        private static int defaultCellWidth = 30;
        private static int defaultCellHeight = 20;
        private static BorderStyle defaultCellBorderStyle = BorderStyle.FixedSingle;
        private static ContentAlignment defaultCellTextAlign = ContentAlignment.MiddleCenter;
        private static Color defaultColor = SystemColors.Control;
        private static Point defaultCellPosition = new Point(20, 100);
        private static Point[] ArrayPositions = new Point[] { new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point() };
        // Point numbering guide                            
        #endregion
        #region Color Set
        private static Color correct = Color.LightGreen;
        private static Color ignore = Color.LightGray;
        private static Color selected = Color.LightBlue;
        private static Color wrong = Color.Red;
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
            ScrollLabel.Text = hScrollBar1.Value.ToString();
            Thread workerThread = new Thread(new ThreadStart(RunProgram));
            workerThread.Start();

        }//end of startButton_Click

        private void RunProgram()
        {
            if (LoadValueArray() == true)
            {
                int selection = -1; //int representing the selected list item for which search process to use.
                for (int i = 0; i < 3; i++)
                {
                    if (checkedListBox1.GetItemChecked(i) == true)
                    {
                        selection = i;
                    }
                }

                switch (selection)
                {
                    case 0: LinearSearch();
                        break;
                    case 1:
                        MessageBox.Show("Binary");//BinarySearch();
                        break;
                    case 2:
                        MessageBox.Show("Interpolation");//InterpolationSearch();
                        break;
                    case -1: MessageBox.Show("No Search type selected.");
                        break;
                }
            }
        }

        private void MoveCellTo(Label current, Point newLocation)
        {
            int movingXFactor = 0; //the number of pixels the cell will move each frame on the X axis
            int movingYFactor = 0; //the number of pixels the cell will move each frame on the Y axis
            int numberOfFrames = 5;

            if (newLocation.X - current.Location.X > 0)
            {
                movingXFactor = (newLocation.X - current.Location.X) / numberOfFrames;
            }
            else
            {
                movingXFactor = (newLocation.X - current.Location.X) / numberOfFrames;
            }
            if (newLocation.Y - current.Location.Y > 0)
            {
                movingYFactor = (newLocation.Y - current.Location.Y) / numberOfFrames;
            }
            else
            {
                movingYFactor = (newLocation.Y - current.Location.Y) / numberOfFrames;
            }

            int[,] moveArray = new int[numberOfFrames, 2];// X is stored in [x,0] and Y is stored in [x,1]
            for (int i = 0; i < numberOfFrames - 1; i++) //For loop to calculate the coordinates needed for each frame of the fast movement animation and load them into the moveArray
            {
                moveArray[i, 0] = current.Location.X + (movingXFactor * (i + 1));
                moveArray[i, 1] = current.Location.Y + (movingYFactor * (i + 1));
            }
            moveArray[numberOfFrames - 1, 0] = newLocation.X;
            moveArray[numberOfFrames - 1, 1] = newLocation.Y;


            for (int j = 0; j < numberOfFrames; j++)
            {
                Thread.Sleep(10);
                if (current.Left != moveArray[j, 0] || current.Top != moveArray[j, 1])
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
            cell.Invoke(new Action(() => cell.Hide()));
        }

        private void ShowCell(Label cell)
        {
            cell.Invoke(new Action(() => cell.Show()));
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
            for (int i = 0; i < cellArray.Length; i++)
            {
                cellArray[i].Invoke(new Action(() => cellArray[i].AutoSize = defaultAutoSizeSetting));
                cellArray[i].Invoke(new Action(() => cellArray[i].Width = defaultCellWidth));
                cellArray[i].Invoke(new Action(() => cellArray[i].Height = defaultCellHeight));
                cellArray[i].Invoke(new Action(() => cellArray[i].BorderStyle = defaultCellBorderStyle));
                cellArray[i].Invoke(new Action(() => cellArray[i].TextAlign = defaultCellTextAlign));
                cellArray[i].Invoke(new Action(() => cellArray[i].BackColor = defaultColor));
                cellArray[i].Invoke(new Action(() => cellArray[i].Location = ArrayPositions[i]));
                string temp = (i + 1).ToString();
                if (temp.Length == 1)
                    temp = "00" + temp;
                if (temp.Length == 2)
                    temp = "0" + temp;
                cellArray[i].Invoke(new Action(() => cellArray[i].Text = temp));
                cellArray[1].Invoke(new Action(() => cellArray[i].Hide()));
                cellArray[i].Invoke(new Action(() => cellArray[i].Refresh()));
            }//end for loop

            systemActionLabel.Invoke(new Action(() => systemActionLabel.Text = "Program Ready"));
        }//end of SetCellArrayDefaults

        private void LoadArrayPositions() //This function loads the ArrayPositions with 
        {
            int tempX;
            int tempY;
            int count = 0;
            for (tempY = 150; tempY < 410; tempY += 50)
            {
                tempX = 20;
                for (tempX = 20; tempX < 750; tempX += 40)
                {
                    ArrayPositions[count] = new Point(tempX, tempY);
                    count++;
                }
            }//end of for loop
        }// end of LoadArrayPositions

        private bool LoadValueArray() //This function loads the value array with the values stored in the ArrayValue.txt file. 
        {
            if (label1.Visible == true)
            {
                HideEntireArray();
            }
            ChangeTitle("Loading Array");
            if (System.IO.File.Exists(filePath) == true)
            {
                string[] temp;
                temp = System.IO.File.ReadAllLines(filePath);
                valueArray = new int[temp.Length];
                for (int i = 0; i < temp.Length; i++)
                {
                    try
                    {
                        int tempInt;
                        tempInt = Int32.Parse(temp[i]);
                        if (tempInt <= 999 && tempInt >= 0)
                        {
                            valueArray[i] = tempInt;
                            NewCellText(cellArray[i], temp[i]);
                            ChangeCellColor(cellArray[i], defaultColor);
                            ShowCell(cellArray[i]);
                            Thread.Sleep(hScrollBar1.Value);
                        }
                        else if (tempInt < 0)
                        {
                            MessageBox.Show("Value in " + filePath + " found less than zero.");
                            return false;
                        }
                        else
                        {
                            MessageBox.Show("Value in " + filePath + " found greater than 999.");
                            return false;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Nonnumeric value found in " + filePath);
                        return false;
                    }

                }
                return true;
            }
            else
            {
                MessageBox.Show(filePath + " does not exist");
                return false;
            }
        }//end of LoadValueArray

        private void NewCellText(Label label, string number)
        {
            if (number.Length == 1)
            {
                label.Invoke(new Action(() => label.Text = "00" + number));
            }
            else if (number.Length == 2)
            {
                label.Invoke(new Action(() => label.Text = "0" + number));
            }
            else
            {
                label.Invoke(new Action(() => label.Text = number));
            }
        } // end of NewCellText

        private void ChangeTitle(string title)
        {
            systemActionLabel.Invoke(new Action(() => systemActionLabel.Text = title));
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
                for (int ix = 0; ix < checkedListBox1.Items.Count; ++ix)
                    if (e.Index != ix) checkedListBox1.SetItemChecked(ix, false);
        }

        private void HideEntireArray()
        {
            for (int i = valueArray.Length - 1; i >= 0; i--)
            {
                HideCell(cellArray[i]);
            }
        }

        private void LinearSearch()
        {
            ChangeTitle("Starting Linear Search");
            Thread.Sleep(hScrollBar1.Value);
            for (int i = 0; i < valueArray.Length; i++)
            {
                ChangeCellColor(cellArray[i], selected);
                ChangeTitle("Comparing Cell Value to Target...");
                Thread.Sleep(hScrollBar1.Value*2);
                if(valueArray[i] == numericUpDown1.Value)
                {
                    ChangeCellColor(cellArray[i], correct);
                    ChangeTitle("Target Found!");
                    return;
                }
                else
                {
                    ChangeCellColor(cellArray[i], ignore);
                    if (hScrollBar1.Value > 200)
                    {
                        ChangeTitle("Not Target, Moving On...");
                    }                    
                    Thread.Sleep(hScrollBar1.Value*2);
                }
            }

            ChangeTitle("Target Not Found");

        }

        private void ChangeCellColor(Label cell, Color color)
        {
            cell.Invoke(new Action(() => cell.BackColor = color));
        }

    }
}
