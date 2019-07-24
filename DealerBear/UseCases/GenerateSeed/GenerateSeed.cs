using System;
using DealerBear.UseCases.GenerateSeed.Interface;

namespace DealerBear.UseCases.GenerateSeed
{
    public class GenerateSeed : IGenerateSeed
    {
        public int Execute()
        {
            return new Random().Next(int.MinValue,int.MaxValue);
        }
    }
}