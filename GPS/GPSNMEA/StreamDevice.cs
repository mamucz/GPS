﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RTP.GPS.Nmea
{
	/// <summary>
	/// Generic stream device
	/// </summary>
    public class StreamDevice : NmeaDevice
    {
		System.IO.Stream m_stream;
		/// <summary>
		/// Initializes a new instance of the <see cref="StreamDevice"/> class.
		/// </summary>
		/// <param name="stream">The stream.</param>
		public StreamDevice(Stream stream) : base()
		{
			m_stream = stream;
		}

		/// <summary>
		/// Opens the stream asynchronous.
		/// </summary>
		/// <returns></returns>
		protected override Task<Stream> OpenStreamAsync()
		{
			return Task.FromResult(m_stream);
		}

		/// <summary>
		/// Closes the stream asynchronous.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <returns></returns>
		protected override Task CloseStreamAsync(System.IO.Stream stream)
		{
			return Task.FromResult(true); //do nothing
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (m_stream != null)
				m_stream.Dispose();
			m_stream = null;
		}
    }
}
