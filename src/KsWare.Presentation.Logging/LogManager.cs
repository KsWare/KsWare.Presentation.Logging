using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Common.Logging;
using FormatMessageHandler = Common.Logging.FormatMessageHandler;
using IVariablesContext = Common.Logging.IVariablesContext;
using INestedVariablesContext = Common.Logging.INestedVariablesContext;

namespace KsWare.Presentation.Logging
{
	public class LogManager //: Common.Logging.ILogManager
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
			return new Wrapper(Common.Logging.LogManager.GetLogger(key));
		}

		/// <summary>
		/// Gets the logger by calling <see cref="M:Common.Logging.ILoggerFactoryAdapter.GetLogger(System.Type)" />
		/// on the currently configured <see cref="P:Common.Logging.LogManager.Adapter" /> using the specified type.
		/// </summary>
		/// <returns>the logger instance obtained from the current <see cref="P:Common.Logging.LogManager.Adapter" /></returns>
		public static ILog GetLogger<T>()
		{
			return new Wrapper(Common.Logging.LogManager.GetLogger<T>());
		}

		/// <summary>
		/// Gets the logger by calling <see cref="M:Common.Logging.ILoggerFactoryAdapter.GetLogger(System.Type)" />
		/// on the currently configured <see cref="P:Common.Logging.LogManager.Adapter" /> using the specified type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>the logger instance obtained from the current <see cref="P:Common.Logging.LogManager.Adapter" /></returns>
		public static ILog GetLogger(Type type)
		{
			return new Wrapper(Common.Logging.LogManager.GetLogger(type));
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
			return new Wrapper(adapter.GetLogger(declaringType));
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

		private class Wrapper : ILog
		{
			private Common.Logging.ILog _logger;

			public Wrapper(Common.Logging.ILog logger)
			{
				_logger = logger;
			}

			// Trace

			public void Trace(object message) => _logger.Trace(message);

			public void Trace(object message, Exception exception) => _logger.Trace(message, exception);

			public void TraceFormat(string format, params object[] args) => _logger.TraceFormat(format, args);

			public void TraceFormat(string format, Exception exception, params object[] args) =>
				_logger.TraceFormat(format, exception, args);

			public void TraceFormat(IFormatProvider formatProvider, string format, params object[] args) =>
				_logger.TraceFormat(formatProvider, format, args);

			public void TraceFormat(IFormatProvider formatProvider, string format, Exception exception,
				params object[] args) =>
				_logger.TraceFormat(formatProvider, format, exception, args);

			public void Trace(Action<FormatMessageHandler> formatMessageCallback) =>
				_logger.Trace(formatMessageCallback);

			public void Trace(Action<FormatMessageHandler> formatMessageCallback, Exception exception) => _logger.Trace(formatMessageCallback, exception);

			public void Trace(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback) =>
				_logger.Trace(formatProvider, formatMessageCallback);

			public void Trace(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback,
				Exception exception) => _logger.Trace(formatProvider, formatMessageCallback, exception);
			
			// Debug
			public void Debug(object message) => _logger.Debug(message);

			public void Debug(object message, Exception exception) => _logger.Debug(message, exception);

			public void DebugFormat(string format, params object[] args) => _logger.DebugFormat(format, args);

			public void DebugFormat(string format, Exception exception, params object[] args) =>
				_logger.DebugFormat(format, exception, args);

			public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args) =>
				_logger.DebugFormat(formatProvider, format, args);

			public void DebugFormat(IFormatProvider formatProvider, string format, Exception exception,
				params object[] args) =>
				_logger.DebugFormat(formatProvider, format, exception, args);

			public void Debug(Action<FormatMessageHandler> formatMessageCallback) =>
				_logger.Debug(formatMessageCallback);

			public void Debug(Action<FormatMessageHandler> formatMessageCallback, Exception exception) => _logger.Debug(formatMessageCallback, exception);

			public void Debug(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback) =>
				_logger.Debug(formatProvider, formatMessageCallback);

			public void Debug(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback,
				Exception exception) => _logger.Debug(formatProvider, formatMessageCallback, exception);

			// Info
			public void Info(object message) => _logger.Info(message);

			public void Info(object message, Exception exception) => _logger.Info(message, exception);

			public void InfoFormat(string format, params object[] args) => _logger.InfoFormat(format, args);

			public void InfoFormat(string format, Exception exception, params object[] args) =>
				_logger.InfoFormat(format, exception, args);

			public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args) =>
				_logger.InfoFormat(formatProvider, format, args);

			public void InfoFormat(IFormatProvider formatProvider, string format, Exception exception,
				params object[] args) =>
				_logger.InfoFormat(formatProvider, format, exception, args);

			public void Info(Action<FormatMessageHandler> formatMessageCallback) =>
				_logger.Info(formatMessageCallback);

			public void Info(Action<FormatMessageHandler> formatMessageCallback, Exception exception) => _logger.Info(formatMessageCallback, exception);

			public void Info(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback) =>
				_logger.Info(formatProvider, formatMessageCallback);

			public void Info(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback,
				Exception exception) => _logger.Info(formatProvider, formatMessageCallback, exception);

			// Warn

			public void Warn(object message) => _logger.Warn(message);

			public void Warn(object message, Exception exception) => _logger.Warn(message, exception);

			public void WarnFormat(string format, params object[] args) => _logger.WarnFormat(format, args);

			public void WarnFormat(string format, Exception exception, params object[] args) =>
				_logger.WarnFormat(format, exception, args);

			public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args) =>
				_logger.WarnFormat(formatProvider, format, args);

			public void WarnFormat(IFormatProvider formatProvider, string format, Exception exception,
				params object[] args) =>
				_logger.WarnFormat(formatProvider, format, exception, args);

			public void Warn(Action<FormatMessageHandler> formatMessageCallback) =>
				_logger.Warn(formatMessageCallback);

			public void Warn(Action<FormatMessageHandler> formatMessageCallback, Exception exception) => _logger.Warn(formatMessageCallback, exception);

			public void Warn(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback) =>
				_logger.Warn(formatProvider, formatMessageCallback);

			public void Warn(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback,
				Exception exception) => _logger.Warn(formatProvider, formatMessageCallback, exception);

			// Error

			public void Error(object message) => _logger.Error(message);

			public void Error(object message, Exception exception) => _logger.Error(message, exception);

			public void ErrorFormat(string format, params object[] args) => _logger.ErrorFormat(format, args);

			public void ErrorFormat(string format, Exception exception, params object[] args) =>
				_logger.ErrorFormat(format, exception, args);

			public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args) =>
				_logger.ErrorFormat(formatProvider, format, args);

			public void ErrorFormat(IFormatProvider formatProvider, string format, Exception exception,
				params object[] args) =>
				_logger.ErrorFormat(formatProvider, format, exception, args);

			public void Error(Action<FormatMessageHandler> formatMessageCallback) =>
				_logger.Error(formatMessageCallback);

			public void Error(Action<FormatMessageHandler> formatMessageCallback, Exception exception) => _logger.Error(formatMessageCallback, exception);

			public void Error(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback) =>
				_logger.Error(formatProvider, formatMessageCallback);

			public void Error(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback,
				Exception exception) => _logger.Error(formatProvider, formatMessageCallback, exception);

			// Fatal


			public void Fatal(object message) => _logger.Fatal(message);

			public void Fatal(object message, Exception exception) => _logger.Fatal(message,exception);

			public void FatalFormat(string format, params object[] args) => _logger.FatalFormat(format,args);

			public void FatalFormat(string format, Exception exception, params object[] args) =>
				_logger.FatalFormat(format, exception, args);

			public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args) =>
				_logger.FatalFormat(formatProvider, format, args);

			public void FatalFormat(IFormatProvider formatProvider, string format, Exception exception,
				params object[] args) =>
				_logger.FatalFormat(formatProvider, format, exception, args);

			public void Fatal(Action<FormatMessageHandler> formatMessageCallback) =>
				_logger.Fatal(formatMessageCallback);

			public void Fatal(Action<FormatMessageHandler> formatMessageCallback, Exception exception) => _logger.Fatal(formatMessageCallback,exception);

			public void Fatal(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback) =>
				_logger.Fatal(formatProvider, formatMessageCallback);

			public void Fatal(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback,
				Exception exception) => _logger.Fatal(formatProvider, formatMessageCallback, exception);

			public bool IsTraceEnabled => _logger.IsTraceEnabled;
			public bool IsDebugEnabled => _logger.IsDebugEnabled;
			public bool IsErrorEnabled => _logger.IsErrorEnabled;
			public bool IsFatalEnabled => _logger.IsFatalEnabled;
			public bool IsInfoEnabled => _logger.IsInfoEnabled;
			public bool IsWarnEnabled => _logger.IsWarnEnabled;
			public IVariablesContext GlobalVariablesContext => _logger.GlobalVariablesContext;
			public IVariablesContext ThreadVariablesContext => _logger.ThreadVariablesContext;
			public INestedVariablesContext NestedThreadVariablesContext => _logger.NestedThreadVariablesContext;
		}

		private class LoggerFactoryAdapter : ILoggerFactoryAdapter
		{
			Common.Logging.ILog ILoggerFactoryAdapter.GetLogger(Type type)
			{
				return LogManager.GetLogger(type);
			}

			Common.Logging.ILog ILoggerFactoryAdapter.GetLogger(string key)
			{
				return LogManager.GetLogger(key);
			}
		}
	}
}