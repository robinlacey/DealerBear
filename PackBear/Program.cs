using System;
using Messages;

namespace PackBear
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello PackBear!" + new SimpleMessage() {Text = " Message Library Built"}.Text);
        }
    }
}