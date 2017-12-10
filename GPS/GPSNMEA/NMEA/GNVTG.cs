using RTP.GPS.Nmea;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTP.GPS.Nmea
{
    /// <summary>
    /// Course over ground and ground speed
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gpbod")]
    [NmeaMessageType("GNVTG")]
    public class Gnvtg : NmeaMessage
    {
        public enum MovingModeMeasureType
        {
            Autonomous,
            Differential,
            DeadReckoning,
            None,
            Unknown
        }

        /// <summary>
		/// Called when the message is being loaded.
		/// </summary>
		/// <param name="message">The NMEA message values.</param>
		protected override void OnLoadMessage(string[] message)
        {
            if (message == null || message.Length < 3)
                throw new ArgumentException("Invalid GPBOD", "message");

            double tmp;

            if (double.TryParse(message[0], NumberStyles.Float, CultureInfo.InvariantCulture, out tmp))
                COGT = tmp;
            else
                COGT = double.NaN;

            if (double.TryParse(message[2], NumberStyles.Float, CultureInfo.InvariantCulture, out tmp))
                COGM = tmp;
            else
                COGM = double.NaN;

            if (double.TryParse(message[6], NumberStyles.Float, CultureInfo.InvariantCulture, out tmp))
                SOG = tmp/3600.0f;
            else
                SOG = double.NaN;

            switch (message[8])
            {
                case "A":
                    ModeIndicator = MovingModeMeasureType.Autonomous; 
                    break;
                case "D":
                    ModeIndicator = MovingModeMeasureType.Differential;
                    break;
                case "E":
                    ModeIndicator = MovingModeMeasureType.DeadReckoning;
                    break;
                case "N":
                    ModeIndicator = MovingModeMeasureType.None;
                    break;
                default:
                    ModeIndicator = MovingModeMeasureType.Unknown;
                    break;

            }


        }
        /// <summary>
        /// Course over ground true
        /// </summary>
        public double COGT { get; private set; }

        /// <summary>
        /// Course over ground magnetic
        /// </summary>
        public double COGM { get; private set; }

        /// <summary>
        /// Speed over ground [m/s]
        /// </summary>
        public double SOG { get; private set; }

        /// <summary>
        /// Mode indicator
        /// </summary>
        public MovingModeMeasureType ModeIndicator { get; private set; }
    }
}
