using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ESolutions.Core.Console
{
	/// <summary>
	/// A menu displayed in the console offering operations assicoated to char keys.
	/// </summary>
	public static class ConsoleMenu
	{
		//Fields
		#region exitChar
		/// <summary>
		/// The char the exits the menu loop.
		/// </summary>
		private const Char exitChar = 'x';
		#endregion

		//Methods
		#region Run
		/// <summary>
		/// Runs the specified menu of command line arguments.
		/// </summary>
		/// <param name="args">The arguments from the command line.</param>
		/// <param name="menuItems">The menu items that are displayed if no arguments are provied</param>
		/// <param name="argumentOperations">The argument operations that shall be performed upon calling the console app.</param>
		/// <remarks>
		/// if the command line contains any of the following "? -? /? help -help /help" will show the help of the argumentOperations.
		/// if the command line contains any other arguments they are matched to the argumentOperations switches and the operations are executed.
		/// if to argument is provided the menu items will be displayed in a loop until the exitChar is typed in.
		/// </remarks>
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
		/// <summary>
		/// Shows all menu items with the key char and their description. Hitting a associated char key will exceute
		/// the menu items operation. The menu items will be called in a loop until the exitChar is hit.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <param name="menuItems">The menu items.</param>
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
		/// <summary>
		/// Performs a single menu items operation.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <param name="menuItems">The menu items.</param>
		/// <param name="keyChar">The key character.</param>
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
		/// <summary>
		/// Performs a single command line argument operation.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <param name="runner">The runner.</param>
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
				throw new Exception($"Argument switch {runner.ArgumentSwitch} failed.", ex);
			}
		}
		#endregion
	}
}
