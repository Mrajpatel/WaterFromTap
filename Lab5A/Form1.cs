using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 *  Name: Raj Patel 
 *  Date: 2021-08-21
 *  
 *  The main perpose of this class is to create an windows form application
 *  This class is a good representation how to use windows drawing library
 *  The graphics shown in this class are just lines and rectangle
 *  It uses rectangle to create liquid falling effect from the tap and lines to draw and fill the bucket
 *  
 */

/// <Statement_of_authorship>
///     I, Raj Patel, certify that this material is my original work.
///     No other person's work has been used without due acknowledgement.
/// </Statement_of_authorship>
namespace Lab5A
{

    public partial class Form : System.Windows.Forms.Form
    {
        
        private Pen pen;                                //Pen to draw line over each other -> filling effect
        private SolidBrush brush;                       //brush to fill up the rectangle from the tap
        SolidBrush erase = new SolidBrush(Color.Black); //brush to remove the vertical rectangle
        private Graphics objectA;                       //creating a new graphics object
        private Color c = Color.LightBlue;              //setting the default color to light-blue
        private int x = 440;                            //coordinate to locate the top line of filling bucket
        private int y = 0;                              //cordinate to locate the points of the new line each time
        private bool flag = false;                      //creating a boolean variable to set the defalult values
        public Form()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(frmMain_Paint);         //Registers the Paint event handler

        }
        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            objectA = e.Graphics;
            pen = new Pen(Color.White);                     //setting the color of line to draw bucket to white
            objectA = this.CreateGraphics();                //Create a graphics object
            objectA.DrawLine(pen, 75, 300, 75, 440);        //drawing the left line of bucket
            objectA.DrawLine(pen, 280, 300, 280, 440);      //drawing the right line of bucket
            objectA.DrawLine(pen, 75, 440, 280, 440);       //drawing the base of bucket
            //objectA.DrawLine(pen, 75, 430, 280, 430);
            //objectA.FillRectangle(brush, 75, 320, 206, 120); //drawing a rectangle
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //opening the color selection box
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                c = colorDialog1.Color;                     //if the color is selected set it to variable
            }
            else
            {
                c = Color.LightCoral;                       //set the default color
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //create a condition that if the flag is hit re-paint the color
            if (flag)
            {
                erase = new SolidBrush(Color.Black);                //if the flag is hit set the color to black
                objectA.FillRectangle(erase, 76, 320, 204, 260);    //paint the bucket with black color
                flag = false;                                       //set the flag to false so it don't hit again
            }
            //set the color selected to brush
            brush = new SolidBrush(c);
            //set the graphics
            objectA = this.CreateGraphics();
            //if the trackBar is set to 0 erase the vertical rectangle
            if (trackBar1.Value == 0)
            {
                objectA.FillRectangle(erase, 90, 180, 20, 260 - y);
                //stop the timer
                timer.Stop();
            }
            //if the tracker is running
            else
            {
                //set the interval to 100 miliseconds
                timer.Interval = (100);
                //set the variable to increase the speed of the water filling effect
                int a = (trackBar1.Value * 9);
                //decrease the timer interval to increase the speed of the drawing
                timer.Interval = timer.Interval - a;
                //start the timer
                timer.Start();
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //decrease the x coordinate of the vertical rectangle
            x -= 1;
            //increase the height of new line each time timer function is called
            y += 1;
            //set the new pen to draw the new color line
            pen = new Pen(c);
            //if the height of line has not reached the top of bucket 
            if (y <= 120)
            {
                //set the new color selected and draw the vertical rectangle
                brush = new SolidBrush(c);
                objectA.FillRectangle(brush, 90, 180, 20, 260 - y);         //draw the rectangle
                objectA.DrawLine(pen, 76, x, 279, x);                       //draw the new line each time with different height
            }
            else
            {
                timer.Stop();                                           //stop the timer if the height is reached
                trackBar1.Value = 0;                                    //set the tracker back to 0
                flag = true;                                            //set the flag to true so it hits when called again
                objectA.FillRectangle(erase, 90, 180, 20, 140);         //erase the vertical rectangle from the tap to the last line in bucket
                y = 0;                                                  //re-setting the default coordinates 
                x = 440;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);                            //close the console when button is hit
        }
    }
}
