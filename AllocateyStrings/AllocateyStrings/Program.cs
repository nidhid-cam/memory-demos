﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AllocateyStrings
{
    class Program
    {
        private static HttpClient httpClient = new HttpClient();

        static void Main(string[] args)
        {
            string.Intern("http://blog.maartenballiauw.be");

            Console.WriteLine("PID: {0}", Process.GetCurrentProcess().Id);
            Console.WriteLine("Hit enter to allocate some strings");
            Console.ReadLine();
            AllocateSomeStrings();

            Console.WriteLine("Hit enter to allocate some string duplicates");
            Console.ReadLine();
            AllocateSomeStringDuplicates();

            Console.WriteLine("Hit enter to allocate interned string literals");
            Console.ReadLine();
            LiteralInterning();

            Console.WriteLine("Hit enter to exit");
            Console.ReadLine();
        }

        static void LiteralInterning()
        { 
            var hello = "Hello";
            var helloWorld1 = "Hello, World";
            var helloWorld2 = "Hello, World";
            var helloWorld3 = hello + ", World";

            Console.WriteLine("['{0}', '{1}'] ==? {2}, ReferenceEquals? {3}",
                helloWorld1,
                helloWorld2,
                helloWorld1 == helloWorld2,
                ReferenceEquals(helloWorld1, helloWorld2));

            Console.WriteLine("['{0}', '{1}'] ==? {2}, ReferenceEquals? {3}",
                helloWorld1,
                helloWorld3,
                helloWorld1 == helloWorld3,
                ReferenceEquals(helloWorld1, helloWorld3));

            Console.WriteLine("'{0}'.IsInterned: {1}", hello, string.IsInterned(hello) != null);
            Console.WriteLine("'{0}'.IsInterned: {1}", helloWorld1, string.IsInterned(helloWorld1) != null);
            Console.WriteLine("'{0}'.IsInterned: {1}", helloWorld2, string.IsInterned(helloWorld2) != null);
            Console.WriteLine("'{0}'.IsInterned: {1}", helloWorld3, string.IsInterned(helloWorld3) != null);
        }

        static void AllocateSomeStrings()
        {
            var a = new string('-', 25);

            var b = "Hello, World!";

            var c = httpClient.GetStringAsync("http://blog.maartenballiauw.be").Result;
        }

        static void AllocateSomeStringDuplicates()
        {
            var a = "http://blog.maartenballiauw.be";
            var stringList = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                stringList.Add(a + "/");
            }
        }

        static void AllocateSomeStringDuplicatesWithInterning()
        {
            var dummy = string.Intern("http://blog.maartenballiauw.be/");

            var url = "http://blog.maartenballiauw.be";

            var stringList = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                stringList.Add(string.Intern(url + "/"));
            }
        }
    }
}
