using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RTP.GPS.Nmea.Gpgga;

namespace RTP.GPS.Nmea
{
    public class GnsDevice : object, IDisposable
    {
        /// <summary>
		/// Latitude
		/// </summary>
		public double Latitude { get; private set; }

        /// <summary>
        /// Longitude
        /// </summary>
        public double Longitude { get; private set; }

        /// <summary>
        /// Fix Quality
        /// </summary>
        public FixQuality Quality { get; private set; }

        /// <summary>
        /// Number of satellites being tracked
        /// </summary>
        public int NumberOfSatellites { get; private set; }

        /// <summary>
		/// Altitude
		/// </summary>
		public double Altitude { get; private set; }


        private SerialPort port;
        private StreamReader filestream;
        private NmeaDevice device;
        private Task task;
        private List<NmeaMessage> messages = new List<NmeaMessage>();
        private List<NmeaMessage> unknownmessages = new List<NmeaMessage>();
        private List<int> ActiveStelits = new List<int>();

        private StreamWriter logFile;

        private Gpgsv gpgsv;
        private Gpgsv glgsv;
        private Gpgsa gpgsa;

        private List<SatelliteVehicle> gpsSateliteInMessage = new List<SatelliteVehicle>();
        private List<SatelliteVehicle> gpsSatelite = new List<SatelliteVehicle>();
        private List<SatelliteVehicle> glonasSateliteInMessage = new List<SatelliteVehicle>();
        private List<SatelliteVehicle> glonasSatelite = new List<SatelliteVehicle>();
        private List<SatelliteVehicle> actualSatelite = new List<SatelliteVehicle>();
        private Control control;
        private int GSACounter;

        public delegate void ReceiveRawNmeaMessageEventHandler(NmeaMessage RawNmeaMessage);
        public event ReceiveRawNmeaMessageEventHandler ReceiveRawNmeaMessage;

        public delegate void NewSatelitsInViewEventHandler(List<SatelliteVehicle> satelits);
        public event NewSatelitsInViewEventHandler NewSatelitsInView;

        public delegate void LostSatelitsInViewEventHandler(List<SatelliteVehicle> satelits);
        public event LostSatelitsInViewEventHandler LostSatelitsInView;

        public delegate void SatelitsInViewUpdatedEventHandler(List<SatelliteVehicle> satelits);
        public event SatelitsInViewUpdatedEventHandler SatelitsInViewUpdated;

        public delegate void NewMessageTypeReceivedEventHandler(NmeaMessage newMessage);
        public event NewMessageTypeReceivedEventHandler NewMessageTypeReceived;

        public delegate void NewUnknownMessageTypeReceivedEventHandler(NmeaMessage newMessage);
        public event NewUnknownMessageTypeReceivedEventHandler NewUnknownMessageTypeReceived;

        public delegate void ActiveSatelitsUpdateEventHandler(List<int> satelits);
        public event ActiveSatelitsUpdateEventHandler ActiveSatelitsUpdate;

        public delegate void NavigationDataUpdatedEventHandler(Gpgga message);
        public event NavigationDataUpdatedEventHandler NavigationDataUpdated;

        public delegate void DeviceMessageReceivedDelegate(object sender, NmeaMessageReceivedEventArgs e);

        private Gpgsv GPGSV
        {
            get
            {
                return gpgsv;
            }
            set
            {
                gpgsv = value;
                ProcessGSV(gpgsv, gpsSateliteInMessage, gpsSatelite);

            }
        }

        private Gpgsv GLGSV
        {
            get
            {
                return glgsv;
            }
            set
            {
                glgsv = value;
                ProcessGSV(glgsv, glonasSateliteInMessage, glonasSatelite);

            }
        }

        private Gpgsa GNGSA
        {
            get
            {
                return gpgsa;
            }
            set
            {
                gpgsa = value;
                ProcessGSA(gpgsa);
            }
        }

        public GnsDevice(Control control = null)
        {
            this.control = control;
        }

        public void OpenPort(string ComPort, int BaudRate)
        {
            port = new SerialPort(ComPort, BaudRate);
            device = new SerialPortDevice(port);
            InitDevice();
           
        }

        public void OpenFile(string FileName, int Frequency)
        {
            try
            {
                filestream = new StreamReader(FileName);
            }
            catch (FileNotFoundException e)
            {

            }
            InitDevice();
        }

        private void InitDevice()
        {
            device.MessageReceived += DeviceMessageReceived;
            task = device.OpenAsync();
        }

        private void DeviceMessageReceived(object sender, NmeaMessageReceivedEventArgs e)
        {
            if (control != null && control.InvokeRequired)
            {
                control.BeginInvoke(new DeviceMessageReceivedDelegate(DeviceMessageReceived), new object[] { sender, e });
            }
            else
                ProcessMessage(e.Message);
        }

        void ProcessMessage(NmeaMessage message)
        {
            OnReceiveRawNmeaMessage(message);
            if (message.GetType() == typeof(Gpgga))
                OnNavigationDataUpdated((Gpgga)message);
            if (message.MessageType == "GNGSA")
            {
                if (GSACounter == 0)
                    ActiveStelits.Clear(); 
                GSACounter++;
            }
            else
            {
                if (GSACounter > 0)
                    OnActiveSatelitsUpdate(ActiveStelits);
                GSACounter = 0;
            }
            Type t = message.GetType();
            string stype = message.MessageType;
            NmeaMessage m = messages.FirstOrDefault(x => x.MessageType == stype);
            if (m != null)
            {
                messages.Add(m);
            }
            if (t != typeof(UnknownMessage))
            {
                messages.Add(message);
                OnNewMessageTypeReceived(message);
            }
            else
            { 
                unknownmessages.Add(message);
                OnNewUnknownMessageTypeReceived(message);
            }
            if (message.MessageType == "GPGSV")
                GPGSV = (Gpgsv)message;
            if (message.MessageType == "GLGSV")
                GLGSV = (Gpgsv)message;
            if (message.MessageType == "GNGSA")
                GNGSA = (Gpgsa)message;
        }

        public void OnReceiveRawNmeaMessage(NmeaMessage RawNmeaMessage)
        {
            if (ReceiveRawNmeaMessage!=null)
                ReceiveRawNmeaMessage(RawNmeaMessage);
        }

        void ProcessGSA(Gpgsa gpgsa)
        {
            ActiveStelits.AddRange(gpgsa.SVs);
        }

        void ProcessGSV(Gpgsv gpgsv, List<SatelliteVehicle> inMessageSatelits, List<SatelliteVehicle> deviceSatelits)
        {
            if (gpgsv.MessageNumber == 1)
            {
                inMessageSatelits.Clear();
            }
            inMessageSatelits.AddRange(gpgsv);


            if (gpgsv.MessageNumber == gpgsv.TotalMessages)
            {
                List<SatelliteVehicle> newSatelits = new List<SatelliteVehicle>();
                List<SatelliteVehicle> lostSatelits = new List<SatelliteVehicle>();

                foreach (var sat in inMessageSatelits)
                {
                    if (deviceSatelits.FirstOrDefault(x => x.PrnNumber == sat.PrnNumber) == null)
                        newSatelits.Add(sat);
                }

                foreach (var sat in deviceSatelits)
                {
                    if (inMessageSatelits.FirstOrDefault(x => x.PrnNumber == sat.PrnNumber) == null)
                        lostSatelits.Add(sat);
                }

                deviceSatelits.Clear();
                deviceSatelits.AddRange(inMessageSatelits);
                if (newSatelits.Count>0)
                    OnNewSatelitsInView(newSatelits);
                if (lostSatelits.Count>0)
                    OnLostSatelitsInView(lostSatelits);
                SatelitsInViewUpdated(inMessageSatelits);
            }
        }

        private void OnNewSatelitsInView(List<SatelliteVehicle> satelits)
        {
            if (NewSatelitsInView != null)
                NewSatelitsInView(satelits);
        }

        private void OnLostSatelitsInView(List<SatelliteVehicle> satelits)
        {
            if (LostSatelitsInView != null)
                LostSatelitsInView(satelits);
        }

        private void OnSatelitsInViewUpdated(List<SatelliteVehicle> satelits)
        {
            if (SatelitsInViewUpdated != null)
                SatelitsInViewUpdated(satelits);
        }

        private void OnNewMessageTypeReceived(NmeaMessage newMessage)
        {
            if (NewMessageTypeReceived != null)
                NewMessageTypeReceived(newMessage);
        }

        private void OnNewUnknownMessageTypeReceived(NmeaMessage newMessage)
        {
            if (NewUnknownMessageTypeReceived != null)
                NewUnknownMessageTypeReceived(newMessage);
        }

        private void OnActiveSatelitsUpdate(List<int> satelits)
        {
            if (ActiveSatelitsUpdate != null)
                ActiveSatelitsUpdate(satelits);
        }

        private void OnNavigationDataUpdated(Gpgga message)
        {
            Latitude = message.Latitude;
            Longitude = message.Longitude;
            Quality = message.Quality;
            NumberOfSatellites = message.NumberOfSatellites;
            Altitude = message.Altitude;
            
            if (NavigationDataUpdated != null)
                NavigationDataUpdated(message);
        }

        public void Dispose()
        {
            
        }
    }
}
