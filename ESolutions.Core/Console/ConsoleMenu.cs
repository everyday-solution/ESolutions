using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ESolutions.Core.Console
{
	public static class ConsoleMenu
	{
		//Fields
		#region exitChar
		private const Char exitChar = 'x';
		#endregion

		//Methods
		#region Run
		public static void Run(String[] args, List<MenuItem> menuItems, List<ArgumentOperation> argumentOperations)
		{
			if (args?.Length <= 0)
			{
				ConsoleMenu.ShowMenu(args, menuItems);
			}
			else
			{
				var helpSwitches = new List<String>() { "?", "-?", "/?", " help", "-help", "/help" };
				if (args.Any(runner=>helpSwitches.Contains(runner)))
				{
					foreach (var runner in argumentOperations)
					{
						System.Console.WriteLine($"{runner.ArgumentSwitch} =>\t\t\t {runner.Description}");
					}
				}
				else
				{
					var operations = args.Join(argumentOperations, left => left, right => right.ArgumentSwitch, (left, right) => right);
					foreach (var runner in operations)
					{
						PerformArgumentOperation(args, runner);
					}
				}
			}
		}
		#endregion

		#region ShowMenu
		private static void ShowMenu(String[] args, List<MenuItem> menuItems)
		{
			try
			{
				var keyChar = exitChar;
				do
				{
					System.Console.WriteLine("=================");
					foreach (var runner in menuItems)
					{
						System.Console.WriteLine($"{runner.Key}) {runner.Text}");
					}
					System.Console.WriteLine($"{exitChar}) Exit");
					System.Console.Write("Your selection: ");
					keyChar = System.Console.ReadKey().KeyChar;
					System.Console.WriteLine();

					ConsoleMenu.PerformMenuItemAction(args, menuItems, keyChar);
				}
				while (keyChar != exitChar);
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex.DeepParse());
				System.Console.Write("Hit any key to exit...");
				System.Console.ReadKey();
			}

			System.Console.WriteLine("Bye Bye!");
			Thread.Sleep(1000);
		}
		#endregion

		#region PerformMenuItemAction
		private static void PerformMenuItemAction(String[] args, List<MenuItem> menuItems, Char keyChar)
		{
			var menuItem = menuItems.FirstOrDefault(runner => runner.Key == keyChar);

			if (menuItem != null)
			{
				try
				{
					menuItem.Action(args);
					System.Console.WriteLine();
				}
				catch (Exception ex)
				{
					System.Console.WriteLine(ex.DeepParse());
					System.Console.Write("Hit any key to exit...");
					System.Console.ReadKey();
				}
			}
			else
			{
				System.Console.WriteLine("Unknown key");
			}

			System.Console.WriteLine();
		}
		#endregion

		#region PerformArgumentOperation
		private static void PerformArgumentOperation(String[] args, ArgumentOperation runner)
		{
			try
			{
				runner.Operation(args);
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex.DeepParse());
				System.Console.WriteLine(ex.StackTrace);
				throw ex;
			}
		}
		#endregion
	}
}
