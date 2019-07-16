
using System;
using Messages;

namespace DealerBear_API
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello DealerBear!" + new SimpleMessage(){Text = " Message Library Built"}.Text);
        }
    }
}