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
        private List<Cell> cellList = new List<Cell>();
        private string filePath = "C:\\Users\\Craig Tye\\Desktop\\Homework\\Capstone\\Capstone Project\\tyejCapstone\\SearchForm\\SearchForm\\ArrayValues.txt";
        private string selection;
        private bool isPaused = false;
        #region Cell Defaults
        private static bool defaultAutoSizeSetting = false;
        private static int defaultCellWidth = 30;
        private static int defaultCellHeight = 20;
        private static BorderStyle defaultCellBorderStyle = BorderStyle.FixedSingle;
        private static ContentAlignment defaultCellTextAlign = ContentAlignment.MiddleCenter;
        private static Color defaultColor = SystemColors.Control;
        private static Point defaultCellPosition = new Point(20, 100);
        private static Point[] ArrayPositions = new Point[] { new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point(), new Point() };
        // Point numbering guide
        private static Point SwapPoint = new Point(360, 130);
        #endregion
        #region Color Set
        private static Color correct = Color.LightGreen;
        private static Color wrong = Color.Red;
        private static Color ignore = Color.LightGray;
        private static Color selected = Color.LightBlue;
        private static Color highlight = Color.LightYellow;

        #endregion


        public Form1()
        {
            InitializeComponent();
        }

        private void RunProgram()
        {
            MultipleCellColorChange(0, cellList.Count - 1, defaultColor);

            switch (selection)
            {
                case "Linear":
                    LinearSearch();
                    break;
                case "Binary":
                    BinarySearch();
                    break;
                case "Interpolation":
                    InterpolationSearch();
                    break;
                case "Insertion":
                    InsertionSort();
                    break;
                case "Selection":
                    SelectionSort();
                    break;
                case "Quick":
                    QuickSort();
                    break;
                case "Bubble":
                    BubbleSort();
                    break;
                default: MessageBox.Show("No search or sort type selected.");
                    break;
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ScrollLabel.Text = hScrollBar1.Value.ToString();
            Thread workerThread = new Thread(new ThreadStart(SetCellArrayDefaults));
            workerThread.Start();
        }


        #region Base Methods
        private void ChangeCellColor(Label cell, Color color)
        {
            cell.Invoke(new Action(() => cell.BackColor = color));
        }

        private void ChangeTitle(string title)
        {
            systemActionLabel.Invoke(new Action(() => systemActionLabel.Text = title));
        }

        private void ShowCell(Label cell)
        {
            cell.Invoke(new Action(() => cell.Show()));
        }

        private void HideCell(Label cell)
        {
            cell.Invoke(new Action(() => cell.Hide()));
        }

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

        private Label[] GetLabelArrayValues()
        {
            return new Label[] { label1, label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16, label17, label18, label19, label20, label21, label22, label23, label24, label25, label26, label27, label28, label29, label30, label31, label32, label33, label34, label35, label36, label37, label38, label39, label40, label41, label42, label43, label44, label45, label46, label47, label48, label49, label50, label51, label52, label53, label54, label55, label56, label57, label58, label59, label60, label61, label62, label63, label64, label65, label66, label67, label68, label69, label70, label71, label72, label73, label74, label75, label76, label77, label78, label79, label80, label81, label82, label83, label84, label85, label86, label87, label88, label89, label90, label91, label92, label93, label94, label95, label96, label97, label98, label99, label100, label101, label102, label103, label104, label105, label106, label107, label108, label109, label110, label111, label112, label113, label114 };
        }

        private void PauseMethod()
        {
            while(isPaused == true)
            {
                Thread.Sleep(100);
            }
        }

        #endregion
        
        #region Advanced Methods
        private void MultipleCellColorChange(int lowerPosition, int higherPosition, Color color)
        {
            for (int i = lowerPosition; i <= higherPosition; i++)
            {
                ChangeCellColor(cellList[i].GetLabel(), color);
            }

        }//

        private void HideEntireArray()
        {
            Label[] array = GetLabelArrayValues();
            for (int i = array.Length - 1; i >= 0; i--)
            {
                HideCell(array[i]);
            }
        }//

        private void MoveCellTo(Label current, Point newLocation)
        {
            double movingXFactor = 0; //the number of pixels the cell will move each frame on the X axis
            double movingYFactor = 0; //the number of pixels the cell will move each frame on the Y axis
            double numberOfFrames = 5;

            if(hScrollBar1.Value > 90)
            {
                numberOfFrames += (hScrollBar1.Value / 25);
            }

            if (newLocation.X - current.Location.X > 0)
            {
                movingXFactor = (((double)(newLocation.X - current.Location.X)) / numberOfFrames);
            }
            else
            {
                movingXFactor = (((double)(newLocation.X - current.Location.X)) / numberOfFrames);
            }
            if (newLocation.Y - current.Location.Y > 0)
            {
                movingYFactor = (((double)(newLocation.Y - current.Location.Y)) / numberOfFrames);
            }
            else
            {
                movingYFactor = (((double)(newLocation.Y - current.Location.Y)) / numberOfFrames);
            }

            int[,] moveArray = new int[(int)numberOfFrames, 2];// X is stored in [x,0] and Y is stored in [x,1]
            for (int i = 0; i < numberOfFrames - 1; i++) //For loop to calculate the coordinates needed for each frame of the fast movement animation and load them into the moveArray
            {
                moveArray[i, 0] = (int)(current.Location.X + (movingXFactor * (i + 1)));
                moveArray[i, 1] = (int)(current.Location.Y + (movingYFactor * (i + 1)));
            }
            moveArray[(int)numberOfFrames - 1, 0] = newLocation.X;
            moveArray[(int)numberOfFrames - 1, 1] = newLocation.Y;


            for (int j = 0; j < numberOfFrames; j++)
            {
                Thread.Sleep(10);
                if (current.Left != moveArray[j, 0] || current.Top != moveArray[j, 1])
                {
                    HideCell(current);
                    Thread.Sleep(10);
                    MoveCellHorizontally(current, moveArray[j, 0]);
                    MoveCellVertically(current, moveArray[j, 1]);
                    PauseMethod();
                    Thread.Sleep(hScrollBar1.Value);

                }

            }//end of for loop

        }//

        private void CheckForVisibleArray()
        {
            if (label1.Visible == true)
            {
                HideEntireArray();
                cellList.Clear();
                SetCellArrayDefaults();

                if (cellList.Count == 0)
                {

                    ChangeTitle("Flag");
                    Thread.Sleep(hScrollBar1.Value);
                }

            }
        }//

        private void LoadValueArray() 
        {
            int failedAttempts = 0;
            CheckForVisibleArray();

            ChangeTitle("Loading Array");
            Label[] labelArray = GetLabelArrayValues();
            if (System.IO.File.Exists(filePath) == true)
            {
                string[] temp;
                temp = System.IO.File.ReadAllLines(filePath);
                for (int i = 0; i < temp.Length; i++)
                {
                    try
                    {
                        int tempInt;
                        tempInt = Int32.Parse(temp[i]);
                        if (tempInt <= 999 && tempInt >= 0)
                        {
                            cellList.Add(new Cell(labelArray[i - failedAttempts], tempInt));
                            ChangeCellColor(cellList[i - failedAttempts].GetLabel(), DefaultBackColor);
                            ShowCell(cellList[i - failedAttempts].GetLabel());
                            PauseMethod();
                            Thread.Sleep(hScrollBar1.Value);
                        }
                        else if (tempInt < 0)
                        {
                            MessageBox.Show("Value in " + filePath + " found less than zero.");
                            failedAttempts += 1;
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Value in " + filePath + " found greater than 999.");
                            failedAttempts += 1;
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Nonnumeric value found in " + filePath);
                        failedAttempts += 1;
                        return;
                    }

                }
                ChangeTitle("Array Loaded");
                return;
            }
            else
            {
                MessageBox.Show(filePath + " does not exist");
                return;
            }
        }//This function loads the value array with the values stored in the ArrayValue.txt file

        private void LoadArrayPositions()  
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
        }//This function loads the array ArrayPositions with all 114 positions

        private void SetCellArrayDefaults()
        {
            Label[] labelArray = new Label[] { label1, label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16, label17, label18, label19, label20, label21, label22, label23, label24, label25, label26, label27, label28, label29, label30, label31, label32, label33, label34, label35, label36, label37, label38, label39, label40, label41, label42, label43, label44, label45, label46, label47, label48, label49, label50, label51, label52, label53, label54, label55, label56, label57, label58, label59, label60, label61, label62, label63, label64, label65, label66, label67, label68, label69, label70, label71, label72, label73, label74, label75, label76, label77, label78, label79, label80, label81, label82, label83, label84, label85, label86, label87, label88, label89, label90, label91, label92, label93, label94, label95, label96, label97, label98, label99, label100, label101, label102, label103, label104, label105, label106, label107, label108, label109, label110, label111, label112, label113, label114 };
            LoadArrayPositions();

            for (int i = 0; i < labelArray.Length; i++)
            {
                labelArray[i].Invoke(new Action(() => labelArray[i].AutoSize = defaultAutoSizeSetting));
                labelArray[i].Invoke(new Action(() => labelArray[i].Width = defaultCellWidth));
                labelArray[i].Invoke(new Action(() => labelArray[i].Height = defaultCellHeight));
                labelArray[i].Invoke(new Action(() => labelArray[i].BorderStyle = defaultCellBorderStyle));
                labelArray[i].Invoke(new Action(() => labelArray[i].TextAlign = defaultCellTextAlign));
                labelArray[i].Invoke(new Action(() => labelArray[i].BackColor = defaultColor));
                labelArray[i].Invoke(new Action(() => labelArray[i].Location = ArrayPositions[i]));
                string temp = (i + 1).ToString();
                if (temp.Length == 1)
                    temp = "00" + temp;
                if (temp.Length == 2)
                    temp = "0" + temp;
                labelArray[i].Invoke(new Action(() => labelArray[i].Text = temp));
                labelArray[1].Invoke(new Action(() => labelArray[i].Hide()));
                labelArray[i].Invoke(new Action(() => labelArray[i].Refresh()));
            }//end for loop

            ChangeTitle("Program Ready");
        }//sets all cells to their default hidden state

        private void CreateNewRandomArraySet()
        {
            SwapCells(2, 9);
            SwapCells(30, 16);
        }

        private void MixUpArray()
        {
            MultipleCellColorChange(0, cellList.Count - 1, defaultColor);
            Random random = new Random();
            int temp;
            for(int i = 0; i < cellList.Count; i++)
            {
                temp = random.Next(0, cellList.Count - 1);
                if (temp == i)
                    temp = random.Next(0, cellList.Count - 1);
                if (temp == i)
                    temp = random.Next(0, cellList.Count - 1);
                SwapCells(i, temp); 
            }

            ChangeTitle("Array Mixed Up");
        }

        private void SwapCells(int cell1, int cell2)
        {
            Point tempCell1Point = new Point(cellList[cell1].GetLabel().Location.X, cellList[cell1].GetLabel().Location.Y);
            Point tempCell2Point = new Point(cellList[cell2].GetLabel().Location.X, cellList[cell2].GetLabel().Location.Y);

            ChangeTitle("Switching " + cell1 + " and " + cell2);

            MoveCellTo(cellList[cell1].GetLabel(), SwapPoint);
            MoveCellTo(cellList[cell2].GetLabel(), tempCell1Point);
            MoveCellTo(cellList[cell1].GetLabel(), tempCell2Point);

            Cell temp = new Cell(cellList[cell1].GetLabel(), cellList[cell1].GetValue());
            cellList[cell1] = cellList[cell2];
            cellList[cell2] = temp;

            cellList[cell1].UpdateText();
            cellList[cell2].UpdateText();
        }

        private void SwapCells(Cell cell1, Cell cell2)
        {
            Point tempCell1Point = new Point(cell1.GetLabel().Location.X, cell1.GetLabel().Location.Y);
            Point tempCell2Point = new Point(cell2.GetLabel().Location.X, cell2.GetLabel().Location.Y);

            MoveCellTo(cell1.GetLabel(), SwapPoint);
            MoveCellTo(cell2.GetLabel(), tempCell1Point);
            MoveCellTo(cell1.GetLabel(), tempCell2Point);

            cell1.UpdateText();
            cell2.UpdateText();
        }

        #endregion       

        #region Search Methods

        private void LinearSearch() //Main Linear Search
        {
            ChangeTitle("Starting Linear Search");
            Thread.Sleep(hScrollBar1.Value);
            for (int i = 0; i < cellList.Count; i++)
            {
                ChangeCellColor(cellList[i].GetLabel(), selected);
                ChangeTitle("Comparing Cell Value to Target...");
                Thread.Sleep(hScrollBar1.Value * 2);
                if (cellList[i].GetValue() == numericUpDown1.Value)
                {
                    ChangeCellColor(cellList[i].GetLabel(), correct);
                    ChangeTitle("Target Found!");
                    return;
                }
                else
                {
                    ChangeCellColor(cellList[i].GetLabel(), wrong);
                    if (hScrollBar1.Value > 200)
                    {
                        ChangeTitle("Not Target, Moving On...");
                    }
                    Thread.Sleep(hScrollBar1.Value * 2);
                }
            }

            ChangeTitle("Target Not Found");

        }

        private void BinarySearch() //Main Binary Search 
        {

            int min = 0;
            ChangeTitle("Setting min to 0");
            ChangeCellColor(cellList[0].GetLabel(), selected);
            Thread.Sleep(hScrollBar1.Value);

            int max = cellList.Count - 1;
            ChangeTitle("Setting max to " + max.ToString());
            ChangeCellColor(cellList[max].GetLabel(), selected);
            Thread.Sleep(hScrollBar1.Value);

            while (min <= max)
            {

                int mid = (max + min) / 2;
                ChangeTitle("Setting mid to " + mid.ToString());
                ChangeCellColor(cellList[mid].GetLabel(), highlight);
                Thread.Sleep(hScrollBar1.Value);

                int diff = numericUpDown1.Value.CompareTo(cellList[mid].GetValue());

                ChangeTitle("Comparing value in position #" + mid + " to target value");
                Thread.Sleep(hScrollBar1.Value);

                if (diff == 0)
                {
                    ChangeCellColor(cellList[mid].GetLabel(), correct);
                    ChangeTitle("Target found at position " + mid);
                    return;
                }
                if (min == max)
                {
                    ChangeCellColor(cellList[mid].GetLabel(), wrong);
                    ChangeTitle("Target not found in list");
                    return;
                }
                if (diff < 0)
                {
                    ChangeCellColor(cellList[mid].GetLabel(), wrong);
                    ChangeTitle("Target not found at position " + mid);
                    Thread.Sleep(hScrollBar1.Value);

                    ChangeTitle("Changing max from " + max + " to " + (mid - 1));
                    MultipleCellColorChange(mid, max, ignore);
                    max = mid - 1;
                    ChangeCellColor(cellList[max].GetLabel(), selected);
                    Thread.Sleep(hScrollBar1.Value);
                }
                if (diff > 0)
                {
                    ChangeCellColor(cellList[mid].GetLabel(), wrong);
                    ChangeTitle("Target not found at position " + mid);
                    Thread.Sleep(hScrollBar1.Value);

                    ChangeTitle("Changing min from " + min + " to " + (mid + 1));
                    MultipleCellColorChange(min, mid, ignore);
                    min = mid + 1;
                    ChangeCellColor(cellList[min].GetLabel(), selected);
                    Thread.Sleep(hScrollBar1.Value);
                }
            }
            return;

        }

        private void BinarySearch(int min, int mid, int max) // Binary Search 
        {
            bool isItTheFirstLoop = true;

            while (min <= max)
            {
                if (isItTheFirstLoop == false)
                {
                    mid = (max + min) / 2;
                }
                ChangeTitle("Setting mid to " + mid.ToString());
                ChangeCellColor(cellList[mid].GetLabel(), highlight);
                Thread.Sleep(hScrollBar1.Value);

                int diff = numericUpDown1.Value.CompareTo(cellList[mid].GetValue());

                ChangeTitle("Comparing value in position " + mid + " to target value");
                Thread.Sleep(hScrollBar1.Value);

                if (diff == 0)
                {
                    ChangeCellColor(cellList[mid].GetLabel(), correct);
                    ChangeTitle("Target found at position " + mid);
                    return;
                }
                if (min == max)
                {
                    ChangeCellColor(cellList[mid].GetLabel(), wrong);
                    ChangeTitle("Target not found in list");
                    return;
                }
                if (diff < 0)
                {
                    ChangeCellColor(cellList[mid].GetLabel(), wrong);
                    ChangeTitle("Target not found at position " + mid);
                    Thread.Sleep(hScrollBar1.Value);

                    ChangeTitle("Changing max from " + max + " to " + (mid - 1));
                    MultipleCellColorChange(mid, max, ignore);
                    max = mid - 1;
                    ChangeCellColor(cellList[max].GetLabel(), selected);
                    Thread.Sleep(hScrollBar1.Value);
                }
                if (diff > 0)
                {
                    ChangeCellColor(cellList[mid].GetLabel(), wrong);
                    ChangeTitle("Target not found at position " + mid);
                    Thread.Sleep(hScrollBar1.Value);

                    ChangeTitle("Changing min from " + min + " to " + (mid + 1));
                    MultipleCellColorChange(min, mid, ignore);
                    min = mid + 1;
                    ChangeCellColor(cellList[min].GetLabel(), selected);
                    Thread.Sleep(hScrollBar1.Value);
                }

                isItTheFirstLoop = false;
            }
            return;

        }

        private void InterpolationSearch() //Main Interpolation Search
        {
            bool isNotFirstLoop = false;

            int min = 0;
            ChangeTitle("Setting Min to 0");
            ChangeCellColor(cellList[0].GetLabel(), selected);
            Thread.Sleep(hScrollBar1.Value);

            int max = cellList.Count() - 1;
            ChangeTitle("Setting Max to " + max.ToString());
            ChangeCellColor(cellList[max].GetLabel(), selected);
            Thread.Sleep(hScrollBar1.Value);

            int target = (int)numericUpDown1.Value;
            ChangeTitle("Setting Target to " + target.ToString());
            Thread.Sleep(hScrollBar1.Value);

            double fraction = (target - cellList[0].GetValue()) / (double)(cellList[max].GetValue() - cellList[0].GetValue());
            ChangeTitle("Setting Fraction to " + fraction.ToString());
            Thread.Sleep(hScrollBar1.Value);

            int guess = (int)((max - min) * fraction);
            ChangeTitle("Setting Guess to " + guess.ToString());
            Thread.Sleep(hScrollBar1.Value);

            if (guess < 0)
            {
                guess = 0;
                ChangeTitle("Whoops! Guess less than 0");
                Thread.Sleep(hScrollBar1.Value);

                ChangeTitle("Setting guess to 0");
                Thread.Sleep(hScrollBar1.Value);
            }

            else if (guess > max)
            {
                guess = max;
                ChangeTitle("Whoops! Guess higher than " + max.ToString());
                Thread.Sleep(hScrollBar1.Value);

                ChangeTitle("Setting guess to " + max.ToString());
                Thread.Sleep(hScrollBar1.Value);
            }

            // Use an expanding binary search to bound the target.
            ChangeTitle("Comparing Guess to Target");
            ChangeCellColor(cellList[guess].GetLabel(), highlight);
            Thread.Sleep(hScrollBar1.Value);
            if (target == cellList[guess].GetValue())
            {
                ChangeTitle("Target found");
                ChangeCellColor(cellList[guess].GetLabel(), correct);
                return;
            }
            if (target < cellList[guess].GetValue())
            {
                // Search down.
                ChangeTitle("Target is lower than Guess");
                ChangeCellColor(cellList[guess].GetLabel(), wrong);
                MultipleCellColorChange(guess + 1, max, ignore);
                Thread.Sleep(hScrollBar1.Value);

                max = guess;
                ChangeTitle("Lowering Max");
                Thread.Sleep(hScrollBar1.Value);

                int offset = 1;
                ChangeTitle("Setting Offset to 1");
                Thread.Sleep(hScrollBar1.Value);

                while (target < cellList[guess].GetValue())
                {

                    if (isNotFirstLoop == true)
                    {
                        ChangeTitle("Guess is still too high");
                        Thread.Sleep(hScrollBar1.Value);

                        offset *= 2;
                        ChangeTitle("Doubling offset");
                        Thread.Sleep(hScrollBar1.Value);
                    }

                    guess -= offset;
                    ChangeTitle("Applying offset " + offset + " to Guess");
                    Thread.Sleep(hScrollBar1.Value);

                    if (guess < 0)
                    {
                        guess = 0;
                        ChangeTitle("Setting Guess to Min: " + min);
                        Thread.Sleep(hScrollBar1.Value);
                        break;
                    }

                    isNotFirstLoop = true;
                }
                min = guess;
            }
            else
            {
                // Search up.
                ChangeTitle("Target is higher than Guess");
                ChangeCellColor(cellList[guess].GetLabel(), wrong);
                MultipleCellColorChange(min, guess - 1, ignore);
                Thread.Sleep(hScrollBar1.Value);

                min = guess;
                ChangeTitle("Raising Min");
                Thread.Sleep(hScrollBar1.Value);

                int offset = 1;
                ChangeTitle("Setting Offset to 1");
                Thread.Sleep(hScrollBar1.Value);

                while (target > cellList[guess].GetValue())
                {

                    if (isNotFirstLoop == true)
                    {
                        ChangeTitle("Guess is still too low");
                        Thread.Sleep(hScrollBar1.Value);

                        offset *= 2;
                        ChangeTitle("Doubling offset");
                        Thread.Sleep(hScrollBar1.Value);
                    }

                    guess += offset;
                    ChangeTitle("Applying Offset " + offset + " to Guess");
                    Thread.Sleep(hScrollBar1.Value);

                    if (guess > max)
                    {
                        guess = max;
                        ChangeTitle("Setting Guess to Max: " + max);
                        Thread.Sleep(hScrollBar1.Value);
                        break;
                    }

                    isNotFirstLoop = true;
                }
                max = guess;

            }

            // Binary search from this point.
            BinarySearch(min, guess, max);
            return;
        }

        #endregion

        #region Sort Methods

        private void InsertionSort()
        {
            ChangeTitle("Starting Insertion Sort");
            ChangeCellColor(cellList[0].GetLabel(), correct);
            Thread.Sleep(hScrollBar1.Value);

            for (int i = 1; i < cellList.Count; i++)
            {
                // Find the spot where item i belongs.
                Cell item = cellList[i];
                ChangeCellColor(cellList[i].GetLabel(), highlight);
                if (hScrollBar1.Value > 20)
                {
                    ChangeTitle("Cell Selected");
                    Thread.Sleep(hScrollBar1.Value);
                }

                int index = 0;
                while ((index <= i) && (item.GetValue().CompareTo(cellList[index].GetValue()) > 0))
                {
                    index++;
                }


                // The item belongs in position index.
                // Move the items between values[i - 1] and values[index]
                // one spot to the right to make room.
                for (int j = i - 1; j >= index; j--)
                {
                    SwapCells(j, j + 1);
                }

                // Deposit the item in position index.
                //SwapCells(item, cellList[index]);
                cellList[index] = item;
                ChangeCellColor(cellList[index].GetLabel(), correct);
                if (hScrollBar1.Value > 20)
                {
                    ChangeTitle("Cell in Order");
                    Thread.Sleep(hScrollBar1.Value);
                }

            }

            ChangeTitle("Insertion Sort Complete");
        }

        private void SelectionSort()
        {
            ChangeTitle("Starting Selection Sort");
            Thread.Sleep(hScrollBar1.Value);

            for (int i = 0; i < cellList.Count - 1; i++)
            {
                // Find the smallest item with index >= i.
                int bestIndex = i;
                ChangeCellColor(cellList[i].GetLabel(), highlight);
                Thread.Sleep(hScrollBar1.Value);

                ChangeTitle("Searching for lower value");
                Thread.Sleep(hScrollBar1.Value);

                for (int j = i + 1; j < cellList.Count; j++)
                {
                    // See if values[j] is smaller.
                    if (cellList[j] < cellList[bestIndex])
                    {
                        // Update the best value.
                        ChangeTitle("Found Lower");
                        ChangeCellColor(cellList[bestIndex].GetLabel(), defaultColor);
                        ChangeCellColor(cellList[j].GetLabel(), highlight);
                        bestIndex = j;
                        Thread.Sleep(hScrollBar1.Value);
                    }
                }

                // Swap values[i] and values[bestIndex].
                SwapCells(bestIndex, i);
                ChangeCellColor(cellList[i].GetLabel(), correct);
            }

            ChangeCellColor(cellList[cellList.Count - 1].GetLabel(), correct);
            ChangeTitle("Selection Sort Complete");
        }

        private void QuickSort()
        {
            ChangeTitle("Starting Quick Start");
            MultipleCellColorChange(0, cellList.Count - 1, defaultColor);
            Thread.Sleep(hScrollBar1.Value);
            DoQuicksort(0, cellList.Count - 1);
            ChangeTitle("Quick Sort Complete");
            MultipleCellColorChange(0, cellList.Count - 1, defaultColor);
        }

        // Perform the quicksort algorithm for the indicated part of the array.
        private void DoQuicksort(int min, int max)
        {
            int initialMin = min;
            int initialMax = max;


            // If min >= max, there's nothing to sort.
            if (hScrollBar1.Value > 30)
            {
                ChangeTitle("Checking if min >= max");
                Thread.Sleep(hScrollBar1.Value);
            }
            if (min >= max) return;

            // Pick the dividing item.
            Cell divider = cellList[min];
            ChangeTitle("Setting divider at " + min);
            ChangeCellColor(divider.GetLabel(), highlight);
            Thread.Sleep(hScrollBar1.Value);

            // Set left and right.
            int left = min;
            int right = max;
            ChangeCellColor(cellList[left].GetLabel(), selected);
            ChangeCellColor(cellList[right].GetLabel(), selected);
            Thread.Sleep(hScrollBar1.Value);

            // Divide the array into two halves.
            while (left < right)
            {
                // The empty spot is at values[left].
                // Look down from position right.
                while ((right > left) && (cellList[right] > divider))
                {
                    ChangeCellColor(cellList[right].GetLabel(), ignore);
                    right--;
                    ChangeCellColor(cellList[right].GetLabel(), selected);
                    Thread.Sleep(hScrollBar1.Value);
                }

                // See if we're done.
                if (right <= left)
                {
                    MultipleCellColorChange(initialMin, initialMax, defaultColor);
                    break;
                }

                // Swap this item into the left side.
                SwapCells(left, right);
                ChangeCellColor(cellList[left].GetLabel(), ignore);
                left++;
                ChangeCellColor(cellList[left].GetLabel(), selected);
                Thread.Sleep(hScrollBar1.Value);

                // The empty spot is at values[right].
                // Look up from position left.
                while ((right > left) && (cellList[left] < divider))
                {
                    ChangeCellColor(cellList[left].GetLabel(), ignore);
                    left++;
                    ChangeCellColor(cellList[left].GetLabel(), selected);
                    Thread.Sleep(hScrollBar1.Value);
                }

                // See if we're done.
                if (right <= left)
                {
                    MultipleCellColorChange(initialMin, initialMax, defaultColor);
                    break;
                }

                // Swap this item into the right side.
                SwapCells(right, left);
                ChangeCellColor(cellList[right].GetLabel(), ignore);
                right--;
                ChangeCellColor(cellList[right].GetLabel(), selected);
                Thread.Sleep(hScrollBar1.Value);
            }

            // Desposit the dividing item.
            cellList[left] = divider;

            // Recurse.
            DoQuicksort(min, left - 1);
            DoQuicksort(left + 1, max);
        }

        private void BubbleSort()
        {
            bool madeSwap = false;

            ChangeTitle("Starting Bubble Sort");
            Thread.Sleep(hScrollBar1.Value);

            do
            {
                madeSwap = false;
                ChangeTitle("Starting loop");
                Thread.Sleep(hScrollBar1.Value);
                int lastCellSwitched = 1;
                for (int i = 1; i < cellList.Count; i++)
                {
                    // See if values[i] < values[i - 1].
                    ChangeTitle("Comparing " + i + " to " + (i - 1));
                    Thread.Sleep(hScrollBar1.Value);

                    if (cellList[i].GetValue() < cellList[i - 1].GetValue())
                    {
                        // Swap values[i] and values[i - 1].
                        SwapCells(i, i - 1);
                        madeSwap = true;
                        lastCellSwitched = i;
                    }

                }

                MultipleCellColorChange(lastCellSwitched, cellList.Count-1, correct);

            } while (madeSwap);

            MultipleCellColorChange(0, cellList.Count - 1, correct);
            ChangeTitle("Sort Complete");
        }


















































        #endregion

        #region UI Events Methods

        private void StartButton_Click(object sender, EventArgs e)
        {
            Thread workerThread = new Thread(new ThreadStart(RunProgram));
            workerThread.Start();
        }//end of startButton_Click

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            ScrollLabel.Text = hScrollBar1.Value.ToString();
        }

        private void loadArrayFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread workThread = new Thread(new ThreadStart(LoadValueArray));
            workThread.Start();
        }

        private void mixUpFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread workThread = new Thread(new ThreadStart(MixUpArray));
            workThread.Start();
        }

        private void InsertionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selection = "Insertion";
            selectionLabel.Text = "Insertion Sort";
        }

        private void selectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selection = "Selection";
            selectionLabel.Text = "Selection Sort";
        }

        private void quickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selection = "Quick";
            selectionLabel.Text = "Quick Sort";
        }

        private void heapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //selection = "Insertion";
        }

        private void mergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //selection = "Insertion";
        }

        private void bubbleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selection = "Bubble";
            selectionLabel.Text = "Bubble Sort";
        }

        private void linearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selection = "Linear";
            selectionLabel.Text = "Linear Search";
        }

        private void binaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selection = "Binary";
            selectionLabel.Text = "Binary Search";
        }

        private void interpolationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selection = "Interpolation";
            selectionLabel.Text = "Interpolation Search";
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if(isPaused == true)
            {
                isPaused = false;
            }
            else
            {
                isPaused = true;
            }
        }



        #endregion


    }
}
