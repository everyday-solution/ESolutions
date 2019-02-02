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
		public static void Run(String[] args, List<MenuItem> menuItems)
		{
			if (args?.Length <= 0)
			{
				ConsoleMenu.ShowMenu(menuItems);
			}
			else if (args.Contains("?"))
			{
				foreach (var runner in menuItems)
				{
					System.Console.WriteLine($"{runner.Key} => {runner.Text}");
					foreach (var argumentRunner in runner.Arguments)
					{
						System.Console.WriteLine($"\t-{argumentRunner}");
					}
				}
			}
			else
			{
				foreach (var runner in args)
				{
					menuItems.FirstOrDefault(x => x.Key == runner.First())?.Action(args);
				}
			}
		}
		#endregion

		#region ShowMenu
		private static void ShowMenu(List<MenuItem> menuItems)
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

				var menuItem = menuItems.FirstOrDefault(runner => runner.Key == keyChar);
				if (menuItem != null)
				{
					menuItem.Action(null);
					System.Console.WriteLine();
				}
				else
				{
					System.Console.WriteLine("Unknown key");
				}
				System.Console.WriteLine();
			}
			while (keyChar != exitChar);

			System.Console.WriteLine("Bye Bye!");
			Thread.Sleep(1000);
		}
		#endregion
	}
}
