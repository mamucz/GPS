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

namespace TestApp
{
    public partial class Form1 : Form
    {
        
        public delegate void AddLineDelegate(ListBox list, string line);
        public delegate void ProcessMessageDelegate(NmeaMessage message);

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
            
        }

        private void Gns_SatelitsInViewUpdated(List<SatelliteVehicle> satelits)
        {
            foreach (var sat in satelits)
            {
               
                DataPoint dp = chartSatelist.Series[0].Points.FirstOrDefault(x => ((SatelliteVehicle)x.Tag).PrnNumber == sat.PrnNumber);
                if (dp != null)
                    dp.YValues = new double[] { sat.SignalToNoiseRatio };
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

                chartSky.Series[0].Points.Add(dps);


                chartSatelist.Series[0].Points.Add(dp);
            }
            
        }

        private void Gns_ReceiveRawNmeaMessage(string RawNmeaMessage)
        {
            listBox1.Items.Add(RawNmeaMessage);
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
    }
}
