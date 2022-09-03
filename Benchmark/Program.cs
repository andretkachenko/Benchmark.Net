using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Benchmark
{
    internal class Program
    {
        private static void Main()
        {
            ConsoleKey key;
            var actions = PrepareActions();

            Console.WriteLine("Press ESC to stop");
            do
            {
                Benchmark(actions, Variants.Items);
                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Escape);
        }

        private static List<Func<Item, string, DateTime?, bool>> PrepareActions()
        {
            var actions = new List<Func<Item, string, DateTime?, bool>>();
            var methods = typeof(Variants).GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static);
            for (var i = 0; i < methods.Length; i++)
            {
                actions.Add(methods[i].CreateDelegate(typeof(Func<Item, string, DateTime?, bool>)) as Func<Item, string, DateTime?, bool>);
            }
            return actions;
        }

        private static void Benchmark(List<Func<Item, string, DateTime?, bool>> actions, List<Item> items)
        {
            var rand = new Random();
            long[] results = new long[items.Count];

            for (int i = 0; i < actions.Count; i++)
            {
                Console.WriteLine($"{actions[i].Method.Name}:");

                for (int t = 0; t < 10; t++)
                {
                    var code = rand.Next(0, 1) == 1 ? "AU" : "NZ";
                    var date = DateTime.Now.AddDays(rand.Next(-50, 50));
                    Console.Write($"({ code}, { date.ToShortDateString()}) - ");

                    for (int j = 0; j < items.Count; j++)
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        actions[i](items[j], code, date);
                        sw.Stop();
                        results[j] = sw.ElapsedTicks;
                    }
                    Console.Write("{0}\n", string.Join(", ", results));
                }
                Console.WriteLine();
            }
        }
    }
}
