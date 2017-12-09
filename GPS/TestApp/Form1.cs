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
using System.Windows.Forms.DataVisualization.Charting;
using System.Net.Sockets;
using System.Net;

namespace TestApp
{
    public partial class Form1 : Form
    {
        
        public delegate void AddLineDelegate(ListBox list, string line);
        public delegate void ProcessMessageDelegate(NmeaMessage message);

        private struct MessageItem
        {
            public NmeaMessage message;
            public override string ToString()
            {
                return message.MessageType;
            }
        }

        GnsDevice gns;

        public Form1()
        {
            InitializeComponent();
            chartSatelist.Series[0].Points.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(SerialPort.GetPortNames());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gns = new GnsDevice(this);
            gns.OpenPort(comboBox1.Text, 57600);
            gns.ReceiveRawNmeaMessage += Gns_ReceiveRawNmeaMessage;
            gns.NewSatelitsInView += Gns_NewSatelitsInView;
            gns.LostSatelitsInView += Gns_LostSatelitsInView;
            gns.SatelitsInViewUpdated += Gns_SatelitsInViewUpdated;
            gns.NewMessageTypeReceived += Gns_NewMessageTypeReceived;
            gns.NewUnknownMessageTypeReceived += Gns_NewUnknownMessageTypeReceived;
            gns.ActiveSatelitsUpdate += Gns_ActiveSatelitsUpdate;
            gns.NavigationDataUpdated += Gns_NavigationDataUpdated;
            
        }

        private void Gns_NavigationDataUpdated(Gpgga message)
        {
            propertyGridGGA.SelectedObject = gns;
        }

        private void Gns_ActiveSatelitsUpdate(List<int> satelits)
        {
            foreach(var sat in chartSatelist.Series[0].Points)
            {
                int id = satelits.FirstOrDefault(x => x == ((SatelliteVehicle)sat.Tag).PrnNumber);
                if (id == 0)
                    sat.Color = Color.Blue;
                else
                    sat.Color = Color.LightGreen;
            }

            foreach (var sat in chartSky.Series[0].Points)
            {
                int id = satelits.FirstOrDefault(x => x == ((SatelliteVehicle)sat.Tag).PrnNumber);
                if (id == 0)
                {
                    if (((SatelliteVehicle)sat.Tag).SignalToNoiseRatio > 0)
                        sat.MarkerColor = Color.Blue;
                    else
                        sat.MarkerColor = Color.LightGray;
                }
                else
                    sat.MarkerColor = Color.LightGreen;
            }
        }

        private void Gns_NewUnknownMessageTypeReceived(NmeaMessage newMessage)
        {
            listBoxUnknownMessages.Items.Add(new MessageItem { message = newMessage });
        }

        private void Gns_NewMessageTypeReceived(NmeaMessage newMessage)
        {
            int i = listBoxMessages.FindString(newMessage.MessageType);
            if (i < 0)
                listBoxMessages.Items.Add(new MessageItem { message = newMessage });
            else
            {
                listBoxMessages.Items.RemoveAt(i);
                listBoxMessages.Items.Insert(i,new MessageItem { message = newMessage });
            }               
        }

        private void Gns_SatelitsInViewUpdated(List<SatelliteVehicle> satelits)
        {
            foreach (var sat in satelits)
            {
               
                DataPoint dp = chartSatelist.Series[0].Points.FirstOrDefault(x => ((SatelliteVehicle)x.Tag).PrnNumber == sat.PrnNumber);
                if (dp != null)
                { 
                    dp.YValues = new double[] { sat.SignalToNoiseRatio };
                    dp.Tag = sat;
                }

                DataPoint dps = chartSky.Series[0].Points.FirstOrDefault(x => ((SatelliteVehicle)x.Tag).PrnNumber == sat.PrnNumber);
                if (dps != null)
                {
                    dps.XValue = sat.Azimuth;
                    dps.YValues = new double[] { sat.Elevation };
                    dps.Tag = sat;
                }
            }            
        }

        private void Gns_LostSatelitsInView(List<SatelliteVehicle> satelits)
        {
            List<DataPoint> remove = new List<DataPoint>();

            foreach (var sat in satelits)
            { 
                
                DataPoint dp = chartSatelist.Series[0].Points.FirstOrDefault(x => ((SatelliteVehicle)x.Tag).PrnNumber == sat.PrnNumber);
                if (dp != null)
                    remove.Add(dp);
            }
            foreach (var dp in remove)
                chartSatelist.Series[0].Points.Remove(dp);

        }

        private void Gns_NewSatelitsInView(List<SatelliteVehicle> satelits)
        {
           
          //  chart1.Series[0].Points.Clear();
            foreach (var sat in satelits)
            {
               
                
                string strType = "";
                switch(sat.System)
                {
                    case SatelliteSystem.Glonass:
                        strType = "L";
                        break;
                    case SatelliteSystem.Gps:
                        strType = "G";
                        break;
                    case SatelliteSystem.Waas:
                        strType = "W";
                        break;
                    case SatelliteSystem.Unknown:
                        strType = "X";
                        break;
                }
                DataPoint dp = new DataPoint();
                dp.Label = sat.PrnNumber.ToString()+strType;
                dp.IsValueShownAsLabel = false;
                dp.YValues = new double[] { sat.SignalToNoiseRatio };
                dp.Tag = sat;

                DataPoint dps = new DataPoint();
                dps.Label = sat.PrnNumber.ToString() + strType;
                dps.XValue =  sat.Azimuth ;
                dps.YValues = new double[] { sat.Elevation };
                dps.MarkerSize = 15;
                dps.Tag = sat;
                switch (sat.System)
                {
                    case SatelliteSystem.Unknown:
                        dps.MarkerStyle = MarkerStyle.Cross;
                        break;
                    case SatelliteSystem.Gps:
                        dps.MarkerStyle = MarkerStyle.Star10;
                        break;
                    case SatelliteSystem.Waas:
                        dps.MarkerStyle = MarkerStyle.Triangle;
                        break;
                    case SatelliteSystem.Glonass:
                        dps.MarkerStyle = MarkerStyle.Diamond;
                        break;
                    default:
                        break;
                }
                

                chartSky.Series[0].Points.Add(dps);


                chartSatelist.Series[0].Points.Add(dp);
            }
            
        }

        private void Gns_ReceiveRawNmeaMessage(NmeaMessage RawNmeaMessage)
        {
         
            listBox1.Items.Add(RawNmeaMessage);
            UdpClient client;
            client = new UdpClient(1200);
            byte[] buff = Encoding.ASCII.GetBytes(RawNmeaMessage.ToString());
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1222);
            client.Send(buff, buff.Length, ep);
            client.Close();
            client = null;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        private void messageList_SelectedIndexChanged(object sender, EventArgs e)
        {
           // string messageType = messageList.SelectedItem.ToString();
            
            //propertyGrid1.SelectedObject = messages.FirstOrDefault(x => x.MessageType == messageType);
        }

      
       
        private void listBoxSat_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void chartSatelist_Click(object sender, EventArgs e)
        {
            
            
        }

        private void chartSatelist_MouseClick(object sender, MouseEventArgs e)
        {
            var results = chartSatelist.HitTest(e.X, e.Y, false, ChartElementType.DataPoint);
         
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    propertyGridSat.SelectedObject = result.Series.Points[result.PointIndex].Tag;                    
                }
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBoxMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxMessages.SelectedItem !=null)
                propertyGridSat.SelectedObject = ((MessageItem)listBoxMessages.SelectedItem).message;
        }
    }
}
