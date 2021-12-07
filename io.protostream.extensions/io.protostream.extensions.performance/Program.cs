﻿using System;
using System.Linq;

namespace io.protostream.extensions.performance
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int j = 0; j < 5; j++)
            {
                var start = DateTime.UtcNow;
                for (int i = 0; i < 10_000; i++)
                {
                    string test = Guid.NewGuid().ToString();

                    byte[] result = test.Replace("-", "").HexStringToByteArray();
                }
                Console.WriteLine((DateTime.UtcNow - start).TotalMilliseconds + "ms");
            }

            Console.ReadKey();
        }
    }
}
