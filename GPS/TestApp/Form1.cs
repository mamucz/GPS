using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RTP.GPS.Nmea;
using System.IO;

namespace TestApp
{
    public partial class Form1 : Form
    {
        SerialPort port;
        NmeaDevice device;
        Task task;
        List<NmeaMessage> messages = new List<NmeaMessage>();
        StreamWriter logFile;
        Gpgsv gpgsv;
        List<SatelliteVehicle> gpsSatelite = new List<SatelliteVehicle>();
        Gpgsv GPGSV
        {
            get
            {
                return gpgsv;
            }
            set
            {
                gpgsv = value;
                ProcessGSV(gpgsv);
               
            }
        }
        Gpgsv GLGSV;
        Gpgsv GNGSV;

        public delegate void AddLineDelegate(ListBox list, string line);
        public delegate void ProcessMessageDelegate(NmeaMessage message);

        void ProcessGSV(Gpgsv gpgsv)
        {
            if (gpgsv.MessageNumber == 1)
            {
                gpsSatelite.Clear();
            }
            gpsSatelite.AddRange(gpgsv);
            if (gpgsv.MessageNumber == gpgsv.TotalMessages)
            {
                foreach (var sat in gpsSatelite)
                {
                    if (listBoxSat.FindString(sat.PrnNumber.ToString()) < 0)
                        listBoxSat.Items.Add(sat.PrnNumber.ToString());
                }
                List<object> fordel = new List<object>();
                foreach (var idsat in listBoxSat.Items)
                {
                    var v = gpsSatelite.FirstOrDefault(x => x.PrnNumber.ToString() == idsat.ToString());
                    if (v == null)
                    {
                        fordel.Add(idsat);
                    }
                }
                foreach(var del in fordel)
                {
                    listBoxSat.Items.Remove(del);
                }
                fordel.Clear();

            }

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(SerialPort.GetPortNames());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            port = new SerialPort(comboBox1.Text, 57600);
            device = new SerialPortDevice(port);
            device.MessageReceived += Device_MessageReceived;
            task = device.OpenAsync();
            
        }

        private void Device_MessageReceived(object sender, NmeaMessageReceivedEventArgs e)
        {
            ProcessMessage(e.Message);
            if (logFile!=null)
            {
                logFile.WriteLine(e.Message);
            }
        }

        void AddLine(ListBox list, string line)
        {
            if (list.InvokeRequired)
                list.Invoke(new AddLineDelegate(AddLine), new object[] { list, line});
            else
                list.Items.Add(line);
        }

        void ProcessMessage(NmeaMessage message)
        {
            if (messageList.InvokeRequired)
                messageList.Invoke(new ProcessMessageDelegate(ProcessMessage), new object[] { message });
            else
            {
                Type t = message.GetType();
                string stype = message.MessageType;
                NmeaMessage m = messages.FirstOrDefault(x => x.MessageType == stype);
                if (m != null)
                {
                    messages.Remove(m);
                   
                }
                if (t != typeof(UnknownMessage))
                    messages.Add(message);
                if (messageList.FindString(message.MessageType) < 0)
                    messageList.Items.Add(message.MessageType);
                
                AddLine(listBox1, message.ToString());

                if (message.MessageType == "GPGSV")
                    GPGSV = (Gpgsv)message;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        private void messageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string messageType = messageList.SelectedItem.ToString();
            
            propertyGrid1.SelectedObject = messages.FirstOrDefault(x => x.MessageType == messageType);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            logFile = new StreamWriter(@".\log.nmea");
            button4.ForeColor = Color.Red;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            logFile.Close();
            logFile.Dispose();
            logFile = null;
            button4.ForeColor = Color.Gray;
        }

        private void listBoxSat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSat.SelectedItem == null)
                return;
            int id = Convert.ToInt16(listBoxSat.SelectedItem.ToString());
            SatelliteVehicle sat = gpsSatelite.FirstOrDefault(x => x.PrnNumber == id);
            propertyGridSat.SelectedObject = sat;
        }
    }
}
