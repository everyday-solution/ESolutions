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
		#region PerformMenuLoop
		public static void Show(List<MenuItem> menuItems)
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
					menuItem.Action();
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
