using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Common.Logging;

namespace KsWare.Presentation.Logging
{
	public partial class LogManager //: Common.Logging.ILogManager
	{
		private static ILoggerFactoryAdapter _adapter;
		private static object _loadLock=new object();

		/// <summary>
		/// Gets the log for "KsWare.Presentation"
		/// </summary>
		public static ILog Default { get; } = InitializeLogger();

		private static ILog InitializeLogger()
		{
			var logger = LogManager.GetLogger("KsWare.Presentation");
			return logger;
		}

//		/// <summary>Gets or sets the adapter.</summary>
//		/// <value>The adapter.</value>
//		public static ILoggerFactoryAdapter Adapter {
//			get {
//				if (_adapter == null)
//				{
//					lock (_loadLock)
//					{
//						if (_adapter == null)
//							_adapter = new LoggerFactoryAdapter(Common.Logging.LogManager.Adapter);
//					}
//				}
//				return _adapter;
//			}
//			set {
//				if (value == null) throw new ArgumentNullException(nameof(Adapter));
//				lock (_loadLock) _adapter = value is LoggerFactoryAdapter ? value : new LoggerFactoryAdapter(value);
//				Common.Logging.LogManager.Adapter = value;
//			}
//		}

		/// <summary>
		/// Gets the logger by calling <see cref="M:Common.Logging.ILoggerFactoryAdapter.GetLogger(System.String)" />
		/// on the currently configured <see cref="P:Common.Logging.LogManager.Adapter" /> using the specified key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>the logger instance obtained from the current <see cref="P:Common.Logging.LogManager.Adapter" /></returns>
		public static ILog GetLogger(string key)
		{
			return new InternalLogWrapper(Common.Logging.LogManager.GetLogger(key));
		}

		/// <summary>
		/// Gets the logger by calling <see cref="M:Common.Logging.ILoggerFactoryAdapter.GetLogger(System.Type)" />
		/// on the currently configured <see cref="P:Common.Logging.LogManager.Adapter" /> using the specified type.
		/// </summary>
		/// <returns>the logger instance obtained from the current <see cref="P:Common.Logging.LogManager.Adapter" /></returns>
		public static ILog GetLogger<T>()
		{
			return new InternalLogWrapper(Common.Logging.LogManager.GetLogger<T>());
		}

		/// <summary>
		/// Gets the logger by calling <see cref="M:Common.Logging.ILoggerFactoryAdapter.GetLogger(System.Type)" />
		/// on the currently configured <see cref="P:Common.Logging.LogManager.Adapter" /> using the specified type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>the logger instance obtained from the current <see cref="P:Common.Logging.LogManager.Adapter" /></returns>
		public static ILog GetLogger(Type type)
		{
			return new InternalLogWrapper(Common.Logging.LogManager.GetLogger(type));
		}

		/// <summary>
		/// Gets the logger by calling <see cref="M:Common.Logging.ILoggerFactoryAdapter.GetLogger(System.Type)" />
		/// on the currently configured <see cref="P:Common.Logging.LogManager.Adapter" /> using the type of the calling class.
		/// </summary>
		/// <remarks>
		/// This method needs to inspect the <see cref="T:System.Diagnostics.StackTrace" /> in order to determine the calling
		/// class. This of course comes with a performance penalty, thus you shouldn't call it too
		/// often in your application.
		/// </remarks>
		/// <seealso cref="M:Common.Logging.LogManager.GetLogger(System.Type)" />
		/// <returns>the logger instance obtained from the current <see cref="P:Common.Logging.LogManager.Adapter" /></returns>
		[Obsolete("Null-Reference Exception when dealing with Dynamic Types, Prefer instead one of the LogManager.GetLogger(...) variants.")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ILog GetCurrentClassLogger()
		{
			var stackFrame = new StackFrame(1, false);
			var adapter = Common.Logging.LogManager.Adapter;
			var methodBase1 = stackFrame.GetMethod();
			var methodBase2 = methodBase1;
			var skipFrames = 2;
			while (!(methodBase2 == (MethodBase)null) && methodBase2.IsConstructor)
			{
				methodBase1 = methodBase2;
				methodBase2 = new StackFrame(skipFrames, false).GetMethod();
				++skipFrames;
			}
			Type declaringType = methodBase1.DeclaringType;
			return new InternalLogWrapper(adapter.GetLogger(declaringType));
		}

		//		Common.Logging.ILog ILogManager.GetLogger(Type type) => GetLogger(type);
		//		Common.Logging.ILog ILogManager.GetLogger(string key)=> GetLogger(key);
		//		string ILogManager.COMMON_LOGGING_SECTION => Common.Logging.LogManager.COMMON_LOGGING_SECTION;
		//		IConfigurationReader ILogManager.ConfigurationReader => Common.Logging.LogManager.ConfigurationReader;
		//		ILoggerFactoryAdapter ILogManager.Adapter { get => Common.Logging.LogManager.Adapter; set => _adapter = Common.Logging.LogManager.Adapter; }
		//		void ILogManager.Reset() => Common.Logging.LogManager.Reset();
		//		void ILogManager.Reset(IConfigurationReader reader)=> Common.Logging.LogManager.Reset(reader);
		//		[Obsolete("Null-Reference Exception when dealing with Dynamic Types, Prefer instead one of the LogManager.GetLogger(...) variants.")]
		//		[MethodImpl(MethodImplOptions.NoInlining)]
		//		Common.Logging.ILog ILogManager.GetCurrentClassLogger() => GetCurrentClassLogger();
		//		Common.Logging.ILog ILogManager.GetLogger<T>() => GetLogger<T>();

		//		private class LoggerFactoryAdapter : ILoggerFactoryAdapter
		//		{
		//			Common.Logging.ILog ILoggerFactoryAdapter.GetLogger(Type type)
		//			{
		//				return LogManager.GetLogger(type);
		//			}
		//
		//			Common.Logging.ILog ILoggerFactoryAdapter.GetLogger(string key)
		//			{
		//				return LogManager.GetLogger(key);
		//			}
		//		}

	}
}