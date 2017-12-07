using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace RTP.GPS.Nmea
{
	/// <summary>
	/// A Serial Port NMEA device
	/// </summary>
	public class SerialPortDevice : NmeaDevice
	{
		private System.IO.Ports.SerialPort m_port;

		/// <summary>
		/// Initializes a new instance of the <see cref="SerialPortDevice" /> class.
		/// </summary>
		/// <param name="port">The serial port.</param>
		/// <exception cref="System.ArgumentNullException">port</exception>
		public SerialPortDevice(System.IO.Ports.SerialPort port)
		{
			if (port == null)
				throw new ArgumentNullException("port");
			m_port = port;
		}

		/// <summary>
		/// Gets the active serial port.
		/// </summary>
		public System.IO.Ports.SerialPort Port
		{
			get
			{
				return m_port;
			}
		}

		/// <summary>
		/// Creates the stream the NmeaDevice is working on top off.
		/// </summary>
		/// <returns></returns>
		protected override Task<System.IO.Stream> OpenStreamAsync()
		{
			m_port.Open();
			return Task.FromResult<System.IO.Stream>(m_port.BaseStream);
		}

		/// <summary>
		/// Closes the stream the NmeaDevice is working on top off.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <returns></returns>
		protected override Task CloseStreamAsync(System.IO.Stream stream)
		{
			m_port.Close();
			return Task.FromResult(true);
		}

		/// <summary>
		/// Writes data to the serial port (useful for RTCM/dGPS scenarios)
		/// </summary>
		/// <param name="buffer">The byte array that contains the data to write to the port.</param>
		/// <param name="offset">The zero-based byte offset in the buffer parameter at which to begin copying 
		/// bytes to the port.</param>
		/// <param name="count">The number of bytes to write.</param>
		public void Write(byte[] buffer, int offset, int count)
		{
			m_port.Write(buffer, offset, count);
		}
	}
}
