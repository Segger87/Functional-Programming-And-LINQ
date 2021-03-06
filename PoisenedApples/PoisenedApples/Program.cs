﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace PoisenedApples
{
	class Program
	{
		static void Main(string[] args)
		{
			var p = new Program();

			var poisoned = p.PickApples()
				.Take(10000)
				.Count(apple => apple.Poisoned);

			var mostCommonPoisenedColor = p.PickApples()
				.Take(10000)
				.Where(apple => apple.Poisoned)
				.GroupBy(apple => apple.Colour)
				.Select(apple => new { colour = apple.Key, count = apple.Count() })
				.OrderByDescending(a => a.count)
				.Skip(1)
				.First();

			var nonPoisonedRedInARow = p.PickApples()
				.Take(10000)
				.TakeWhile(a=> !a.Poisoned)
				.Where(apple => apple.Colour == "Red")
				.Count();

			var sequentialGreenApples = p.PickApples()
				.Take(10000)
				.Skip(1)
				.Zip(p.PickApples()
				.Take(10000), (a, b) => a.Colour == "Green" && b.Colour == "Green")
				.Where(a => a)
				.Count();


			Console.WriteLine($"Poisoned apples in 10,000 = {poisoned}");
			Console.WriteLine($"Most Common Color for Poisoned apples = {mostCommonPoisenedColor.colour} with a count of {mostCommonPoisenedColor.count}");
			Console.WriteLine($"Max non Poisoned Red Apples in a row = {nonPoisonedRedInARow}");
			Console.WriteLine($"Total number of sequential green apples = {sequentialGreenApples}");

			Console.ReadLine();
		}
		private IEnumerable<Apple> PickApples()
		{
			int colourIndex = 1;
			int poisonIndex = 7;

			while (true)
			{
				yield return new Apple
				{
					Colour = GetColour(colourIndex),
					Poisoned = poisonIndex % 41 == 0
				};

				colourIndex += 5;
				poisonIndex += 37;
			}
		}

		private string GetColour(int colourIndex)
		{
			if (colourIndex % 13 == 0 || colourIndex % 29 == 0)
			{
				return "Green";
			}

			if (colourIndex % 11 == 0 || colourIndex % 19 == 0)
			{
				return "Yellow";
			}

			return "Red";
		}

		private class Apple
		{
			public string Colour { get; set; }
			public bool Poisoned { get; set; }

			public override string ToString()
			{
				return $"{Colour} apple{(Poisoned ? " (poisoned!)" : "")}";
			}
		}
	}
}
