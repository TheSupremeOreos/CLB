using System.Diagnostics;

namespace System
{
	public static class Logger
	{
		public static ConsoleColor dateColor;

		public static ConsoleColor msgColor;

		static Logger()
		{
			Logger.dateColor = ConsoleColor.Gray;
			Logger.msgColor = ConsoleColor.White;
		}

		public static void Write(Logger.LogTypes pType, string pMessage, params object[] pObjects)
		{
			string str = string.Format(pMessage, pObjects);
			string str1 = string.Format("[{0}]", pType.ToString());
			string empty = string.Empty;
			Trace.WriteLine(string.Format("{0}{1} {2}", empty, str1, str));
			ConsoleColor foregroundColor = Console.ForegroundColor;
			Logger.LogTypes logType = pType;
			switch (logType)
			{
				case Logger.LogTypes.Error:
				{
					foregroundColor = ConsoleColor.Red;
					break;
				}
				case Logger.LogTypes.Warning:
				{
					foregroundColor = ConsoleColor.Red;
					break;
				}
				case Logger.LogTypes.Info:
				{
					foregroundColor = ConsoleColor.Blue;
					break;
				}
				case Logger.LogTypes.Connection:
				{
					foregroundColor = ConsoleColor.Green;
					break;
				}
				case Logger.LogTypes.DataLoad:
				{
					foregroundColor = ConsoleColor.Cyan;
					break;
                }
                case Logger.LogTypes.SEND:
                {
                    foregroundColor = ConsoleColor.Yellow;
                    break;
                }
                case Logger.LogTypes.RECV:
                {
                    foregroundColor = ConsoleColor.Yellow;
                    break;
                }
			}
			ConsoleColor consoleColor = Console.ForegroundColor;
			Console.ForegroundColor = Logger.dateColor;
			Console.Write(empty);
			Console.ForegroundColor = foregroundColor;
			Console.Write(str1);
			Console.ForegroundColor = Logger.msgColor;
			Console.Write(string.Concat(" ", str));
			Console.ForegroundColor = consoleColor;
			Console.WriteLine();
		}

		public enum LogTypes
		{
			Error,
			Warning,
			Info,
			Connection,
			DataLoad,
            SEND,
            RECV,
			Exception
		}
	}
}