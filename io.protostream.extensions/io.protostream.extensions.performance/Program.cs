﻿using System;

namespace io.protostream.extensions.performance
{
    class Program
    {
        static void Main(string[] args)
        {
            var start = DateTime.UtcNow;
            for (int i = 0; i < 10_000; i++)
            {
                string test = Guid.NewGuid().ToString();

                string result = test.RemoveSpecialCharacters('.', '_');
            }

            Console.WriteLine((DateTime.UtcNow - start).TotalMilliseconds + "ms");
            Console.ReadKey();
        }
    }
}