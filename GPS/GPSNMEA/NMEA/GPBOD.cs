﻿
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTP.GPS.Nmea
{
	/// <summary>
	/// Bearing Origin to Destination
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gpbod")]
	[NmeaMessageType("GPBOD")]
	public class Gpbod : NmeaMessage
	{
		/// <summary>
		/// Called when the message is being loaded.
		/// </summary>
		/// <param name="message">The NMEA message values.</param>
		protected override void OnLoadMessage(string[] message)
		{
			if (message == null || message.Length < 3)
				throw new ArgumentException("Invalid GPBOD", "message");
			if (message[0].Length > 0)
				TrueBearing = double.Parse(message[0], CultureInfo.InvariantCulture);
			else
				TrueBearing = double.NaN;
			if (message[2].Length > 0)
				MagneticBearing = double.Parse(message[2], CultureInfo.InvariantCulture);
			else
				MagneticBearing = double.NaN;
			if (message.Length > 4 && !string.IsNullOrEmpty(message[4]))
				DestinationId = message[4];
			if (message.Length > 5 && !string.IsNullOrEmpty(message[5]))
				OriginId = message[5];
		}
		/// <summary>
		/// True Bearing from start to destination
		/// </summary>
		public double TrueBearing { get; private set; }

		/// <summary>
		/// Magnetic Bearing from start to destination
		/// </summary>
		public double MagneticBearing { get; private set; }

		/// <summary>
		/// Name of origin
		/// </summary>
		public string OriginId { get; private set; }

		/// <summary>
		/// Name of destination
		/// </summary>
		public string DestinationId { get; private set; }
	}
}
