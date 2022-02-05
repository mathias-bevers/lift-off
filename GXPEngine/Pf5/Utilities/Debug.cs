using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Mathias.Utilities
{
	/// <summary>
	///     A helper class to make logging information to the console easier.
	/// </summary>
	public static class Debug
	{
		/// <summary>
		///     Log a <paramref name="message" /> to the console with a red "[ERROR]" tag in front of it.
		/// </summary>
		/// <param name="message">The text that has to be displayed in the console</param>
		public static void LogError(string message)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("[ERROR] ");
			Console.ResetColor();
			Console.WriteLine(message);
		}

		/// <summary>
		///     Log a <paramref name="message" /> to the console with a gray-ish "[INFO]" tag in front of it.
		/// </summary>
		/// <param name="message">The text that has to be displayed in the console</param>
		public static void Log(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string callerPath = null)
		{
			Console.ForegroundColor = ConsoleColor.DarkGray;
			string fileName = callerPath.Split('\\').Last();
			Console.Write($"[INFO {fileName}:{lineNumber}] ");
			Console.ResetColor();
			Console.WriteLine(message);
		}

		/// <summary>
		///     Log a <paramref name="message" /> to the console with a yellow "[WARNING]" tag in front of it.
		/// </summary>
		/// <param name="message">The text that has to be displayed in the console</param>
		public static void LogWaring(string message)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("[WARNING] ");
			Console.ResetColor();
			Console.WriteLine(message);
		}

		/// <summary>
		///     Log a <paramref name="message" /> with a blue "[INITIALIZED]" tag in front of it and the
		///     <paramref name="elapsedTime" /> in milliseconds it took to initialize afterwards.
		/// </summary>
		/// <param name="message">The text that has to be displayed in the console</param>
		/// <param name="elapsedTime">The time it took to initialize the object.</param>
		public static void Initialized(string message, double elapsedTime)
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("[INITIALIZED] ");
			Console.ResetColor();
			Console.WriteLine($"{message} in {elapsedTime}ms...");
		}
	}
}