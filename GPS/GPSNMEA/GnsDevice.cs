using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [CategoryAttribute("Navigation"), DescriptionAttribute("Latitude [degree]")]
		public double Latitude { get; private set; }

        /// <summary>
        /// Longitude
        /// </summary>
        [CategoryAttribute("Navigation"), DescriptionAttribute("Longitude [degree]")]   
        public double Longitude { get; private set; }

        /// <summary>
        /// Fix Quality
        /// </summary>
        [CategoryAttribute("Fix"), DescriptionAttribute("Navidation fix quality [degree]")]
        public FixQuality FixQuality { get; private set; }

        /// <summary>
        /// Number of satellites being tracked
        /// </summary>
        [CategoryAttribute("Fix"), DescriptionAttribute("Satelits used for navigation data")]
        public int NumberOfSatellites { get; private set; }

        /// <summary>
		/// Altitude
		/// </summary>
        [CategoryAttribute("Navigation"), DescriptionAttribute("Altitude [meters]")]
        public double Altitude { get; private set; }

        /// <summary>
        /// Course over ground true
        /// </summary>
        [CategoryAttribute("Motion"), DescriptionAttribute("Course over ground [true]")]
        public double COGT { get; private set; }

        /// <summary>
        /// Course over ground magnetic
        /// </summary>
        [CategoryAttribute("Motion"), DescriptionAttribute("Course over ground [magnwtic]")]
        public double COGM { get; private set; }

        /// <summary>
        /// Speed over ground [m/s]
        /// </summary>
        [CategoryAttribute("Motion"), DescriptionAttribute("Speed over ground [meter per seconds]")]
        public double SOG { get; private set; }

        /// <summary>
        /// Mode indicator
        /// </summary>
        [CategoryAttribute("Fix"), DescriptionAttribute("Motion fix type")]
        public Gnvtg.MovingModeMeasureType ModeIndicator { get; private set; }

        /// <summary>
		/// Time of day fix was taken
		/// </summary>
        [CategoryAttribute("Fix"), DescriptionAttribute("Last time of fix")]
        public TimeSpan FixTime { get; private set; }


        /// <summary>
        /// Dilution of precision
        /// </summary>
        [CategoryAttribute("Fix"), DescriptionAttribute("Dilution of precision")]
        public double Pdop { get; private set; }

        /// <summary>
        /// Horizontal dilution of precision
        /// </summary>
        [CategoryAttribute("Fix"), DescriptionAttribute("Horizontal of precision"),]
        public double Hdop { get; private set; }

        /// <summary>
        /// Vertical dilution of precision
        /// </summary>
        [CategoryAttribute("Fix"), DescriptionAttribute("Vertical of precision")]
        public double Vdop { get; private set; }

        /// <summary>
        /// Mode
        /// </summary>
        [CategoryAttribute("Fix"), DescriptionAttribute("Fix mode")]
        public Gpgsa.Mode FixMode { get; private set; }

        public enum Commands
        {
            HotStart,
            WarmStart,
            ColdStart,
        }

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

        public delegate void MovingDataUpdatedEventHandler(Gnvtg message);
        public event MovingDataUpdatedEventHandler MovingDataUpdated;

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
            if (message.GetType() == typeof(Gnvtg))
                OnMotionDataUpdated((Gnvtg)message);
            if (message.MessageType == "GNGSA")
            {
                Hdop = ((Gpgsa)message).Hdop;
                Pdop = ((Gpgsa)message).Pdop;
                Vdop = ((Gpgsa)message).Vdop;
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
            FixMode = gpgsa.FixMode;
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
            FixQuality = message.Quality;
            NumberOfSatellites = message.NumberOfSatellites;
            Altitude = message.Altitude;
            FixTime = message.FixTime;
            
            if (NavigationDataUpdated != null)
                NavigationDataUpdated(message);
        }

        private void OnMotionDataUpdated(Gnvtg message)
        {
            COGT = message.COGT;
            COGM = message.COGM;
            SOG = message.SOG;
            ModeIndicator = message.ModeIndicator;
   
            if (MovingDataUpdated != null)
                MovingDataUpdated(message);
        }

        private void GetCommandCheckSums(byte[] buff, ref byte CK_A, ref byte CK_B)
        {
            CK_A = 0;
            CK_B = 0;
            for (int i = 0; i < buff.Count(); i++)
            {
                CK_A = (byte)(CK_A + buff[i]);
                CK_B = (byte)(CK_B + CK_A);
            }
        }
            
        private byte[] BuildCommand(byte Class, byte ID, byte[] payload)
        {
            byte[] body = new byte[payload.Length + 4];
            body[0] = Class;
            body[1] = ID;
            body[2] = (byte)payload.Length;
            body[3] = 0;

            payload.CopyTo(body, 4); 
            byte CK_A= 0;
            byte CK_B=0;
            GetCommandCheckSums(body, ref CK_A, ref CK_B);
            byte[] cmd = new byte[body.Length + 4];
            cmd[0] = 181;
            cmd[1] = 98;
            body.CopyTo(cmd, 2);
            cmd[cmd.Length - 2] = CK_A;
            cmd[cmd.Length - 1] = CK_B;
            return cmd;
        }

        public void SendCommand(Commands command)
        {
            byte[] scmd = null;
            if (command == Commands.HotStart)
                scmd = BuildCommand(6, 4, new byte[] {0,0,2,0 });
            if (command == Commands.WarmStart)
                scmd = BuildCommand(6, 4, new byte[] { 0, 1, 2, 0 });
            if (command == Commands.ColdStart)
                scmd = BuildCommand(6, 4, new byte[] { 0xFF, 0xFF, 2, 0 });
            ((SerialPortDevice)device).Write(scmd, 0, scmd.Length);
            //    B5 62 06 04 04 00 00 00 02 00 10 68
        }

        public void Dispose()
        {
            
        }
    }
}
