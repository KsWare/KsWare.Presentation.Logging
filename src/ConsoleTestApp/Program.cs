using System;
using KsWare.Presentation.Logging;


namespace ConsoleTestApp
{
	class Program
	{
		private static readonly ILog Log = LogManager.GetLogger<Program>();
		static void Main(string[] args)
		{
			Log.Trace("This is a trace...");
			Log.Debug("This is a debug message...");
			Log.Info("This is an info message...");
			Log.Warn("This is a warning...");
			Log.Error("This is an error...");
			Log.Fatal("This is a fatal error...");

			KsWare.Presentation.Logging.LogManager.Default.Debug("KsWare Debug log....");

			try
			{
				throw new InvalidOperationException("Test");
			}
			catch (Exception ex)
			{
				Log.Fatal("Terminating program!", ex);
			}


			Console.ReadLine();
		}
	}
}
