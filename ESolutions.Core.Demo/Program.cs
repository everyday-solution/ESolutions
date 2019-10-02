using ESolutions.Core.Console;
using System;
using System.Collections.Generic;
using System.Security;

namespace ESolutions.Core.Demo
{
	/// <summary>
	/// Demo Program for ESolutions.Core.Console
	/// </summary>
	class Program
	{
		#region Main
		/// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		/// <param name="args">The arguments.</param>
		static void Main(string[] args)
		{
			var menuItems = new List<MenuItem>()
			{
				new MenuItem('a', "Perform action 1", Program.MenuAction1),
				new MenuItem('b', "Perform action 2", Program.MenuAction2),
				new MenuItem('c', "Perform action that throws", Program.MenuActionThrows),
			};

			var argumentOperations = new List<ArgumentOperation>()
			{
				new ArgumentOperation("-operation1", "Performs operation 1", Program.ArgumentOperation1),
				new ArgumentOperation("-operation2", "Performs operation 2", Program.ArgumentOperation2),
				new ArgumentOperation("-operationthrows", "Performs operation that throws", Program.ArgumentOperationThrows),
			};

			Console.ConsoleMenu.Run(args, menuItems, argumentOperations);
		}
		#endregion

		#region MenuAction1
		public static void MenuAction1(IEnumerable<String> args)
		{
			System.Console.WriteLine("Menu action 1 was triggered");
		}
		#endregion

		#region MenuAction2
		public static void MenuAction2(IEnumerable<String> args)
		{
			System.Console.WriteLine("Menu action 2 was triggered");
		}
		#endregion

		#region MenuActionThrows
		public static void MenuActionThrows(IEnumerable<string> args)
		{
			throw new SecurityException();
		}
		#endregion

		#region ArgumentOperation1
		public static void ArgumentOperation1(IEnumerable<String> args)
		{
			System.Console.WriteLine("Argument operation 1 was triggered");
		}
		#endregion

		#region ArgumentOperation2
		public static void ArgumentOperation2(IEnumerable<String> args)
		{
			System.Console.WriteLine($"Argument operation 2 was triggered");
		}
		#endregion
		
		#region ArgumentOperationThrows
		public static void ArgumentOperationThrows(IEnumerable<string> args)
		{
			throw new SecurityException();
		}
		#endregion
	}
}
