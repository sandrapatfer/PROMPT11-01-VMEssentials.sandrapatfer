using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sessao3
{
    public partial class Form1 : Form
    {
        SessionRecorder m_recorder;

        public Form1()
        {
            InitializeComponent();
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {
            m_recorder = new SessionRecorder(this);
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Start Recording.... (clicks)");
            m_recorder.StartRecorder("Click");
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Stop Recording....");
            m_recorder.StopRecorder();
        }

        private void isel_Click(object sender, EventArgs e)
        {
            textbox.Text = ((Button)sender).Text;
        }

        private void prompt_Click(object sender, EventArgs e)
        {
            textbox.Text = ((Button)sender).Text;
        }

       
    }
}
