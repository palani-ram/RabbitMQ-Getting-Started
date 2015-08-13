using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GettingStartedWithRabbitMQ
{
    public partial class Form1 : Form
    {
        public string HOST_NAME = "localhost";
        public string QUEUE_NAME = "helloWorld";

        private Consumer consumer;
        private Producer producer;

        public Form1()
        {
            InitializeComponent();

            //create the producer
            producer = new Producer(HOST_NAME, QUEUE_NAME);

            //create the consumer
            consumer = new Consumer(HOST_NAME, QUEUE_NAME);

            //listen for message events
            consumer.onMessageReceived += handleMessage;

            //start consuming
            consumer.StartConsuming();
        }

        //delegate to post to UI thread
        private delegate void showMessageDelegate(string message);

        //Callback for message receive
        public void handleMessage(byte[] message)
        {
            showMessageDelegate s = new showMessageDelegate(richTextBox1.AppendText);

            this.Invoke(s, System.Text.Encoding.UTF8.GetString(message) + Environment.NewLine);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            producer.SendMessage(System.Text.Encoding.UTF8.GetBytes(textBox1.Text));
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                producer.SendMessage(System.Text.Encoding.UTF8.GetBytes(textBox1.Text));
            }
        }
    }
}
