using System;
using Messages;

namespace GameBear
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello GameBear!"+ new SimpleMessage(){Text = " Message Library Built"}.Text);
        }
    }
}