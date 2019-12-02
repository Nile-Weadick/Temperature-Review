using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Project_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // *************************************************************************
        // Properites for the entire Form - global variables for the form

        int high, low, highDay, lowDay;
        double average, sum;
        int[] array;

        OpenFileDialog txtFile = new OpenFileDialog();

        private void clearUI()
        {
            lblAverage.Text = string.Empty;
            lblHigh.Text = string.Empty;
            lblLow.Text = string.Empty;
            average = 0.0;
            sum = 0;
            low = 0;
            high = 0;
        }

        private void openFile()
        {
            clearUI();
            // Initailize File
            OpenFileDialog txtFile = new OpenFileDialog();

            //IF dialog is opened get the file path
            if (txtFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lblPath.Text = txtFile.FileName;
            }

            else
            {
                lblPath.Text = "File failed to Open";
            }
        }

        private void populateArray()
        {
            // Local variables
            string line;
            int temp;

            List<int> list = new List<int>();//Create list

            //Open the file handle
            System.IO.StreamReader file = 
            new System.IO.StreamReader(lblPath.Text);

            file.ReadLine(); // skip first line

            // read each line of text, conervting the string data on each line to an int. 
            //then adding the int to a list that can then be given to an array
            while ((line = file.ReadLine()) != null)
            {
                temp = int.Parse(line);
                list.Add(temp);
            }

            //Adding data from list to array
            array = list.ToArray();
        }

        // calculate min, max, average in the array;
        private void calcResults()
        {
            //low = array.Min();
            //high = array.Max();
            //average = array.Average();

            low = array[0];
            high = array[0];
            
            // Sort through the array to find min, max, and average.
            // along with the day of month of min and max took place
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < low)
                {
                    low = array[i];
                    lowDay = i+1;
                }

                if (array[i] > high)
                {
                    high = array[i];
                    highDay = i+1;
                }

                sum += array[i];
                average = sum / array.Length;
            }


        }

        // Display Results
        private void monthStatistics()
        {
            lblLow.Text = "Month low temperature was " + low.ToString() + " on day " + lowDay;
            lblHigh.Text = "Month high temperature was " + high.ToString() + " on day " + highDay;
            lblAverage.Text = "Average temperature was " + average.ToString("F");
        }

        // call open dialog method
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            openFile();
        }

        //Call methods to open file, read file, calc results, and display results
        private void btnResults_Click(object sender, EventArgs e)
        {
            populateArray();
            calcResults();
            monthStatistics();
            btnResults.Enabled = false;
        }
    }
}
