using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Media;

namespace mezamashitokei
{
    //目覚まし時計
    //Two ComboBoxes for hour and minute values, and a ticking timer of x secs (0.5, 1?) counts down, 
    //and every time it finishes its count the time is check to see if it passes the value set by the combination in the two comboboxes
    //A tone/mp3/website is played/launched for X secs (1 minute) when time passes the time set by the user. Grace period of 2 minutes before alarm restarts/alarm is switched off. 
    //Alarm does not ring if current time passes the set time by 10 minutes.

    public partial class 目覚まし時計 : Form
    {

        //Drop down values
        //string[] Hour_items = new string[] { "00", "01", "02", "03", "04", "05", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23" };
        //string[] Minute_items = new string[] { "00", "01", "02", "03", "04", "05", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59" };

        System.Timers.Timer timer;
        
        public void 目覚まし時計_FormClosing(object sender, FormClosedEventArgs e)
        {
            timer.Stop();
        }

        public 目覚まし時計()
        {
            InitializeComponent();
            DateTime UserTimeSet = dateTimePicker1.Value;

            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += CheckTime;
            //comboBox1.DataSource = Hour_items;
            //comboBox2.DataSource = Minute_items;

        }

        private void CheckTime(object source, System.Timers.ElapsedEventArgs e)
        {
            DateTime CurrentTime = DateTime.Now;
            DateTime UserTimeSet = dateTimePicker1.Value;

            //Subtract UserTimeSet from CurrentTime, compare with 10 minutes to see if longer
            //TimeSpan.Compare(a,b)
            // if less than 0 then a < b
            if (    (UserTimeSet.Subtract(CurrentTime) > TimeSpan.FromMinutes(0))   && TimeSpan.Compare(UserTimeSet.Subtract(CurrentTime),TimeSpan.FromMinutes(10)) <= 10)
            {
                timer.Stop();
                try
                {
                    SoundPlayer player = new SoundPlayer();
                    player.SoundLocation = @"C:\Windows\Media\Alarm01.wav";
                    player.PlayLooping();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "error lul", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(string.Format("Time now is {0:HH:mm:ss}", DateTime.Now));
            //Console.WriteLine(string.Format("Time input as {0:HH:mm:ss}", ));
            //DateTime remindat = ConvertToDateTime(comboBox1.Text + ":" + comboBox2.Text + ":00"); 
            //Console.WriteLine("When compared value is " + ())
            timer.Start();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer.Stop();
            SoundPlayer player = new SoundPlayer();
            player.Stop();

        }
    }
}
