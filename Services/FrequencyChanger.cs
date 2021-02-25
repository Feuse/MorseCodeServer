using MorseCodeServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MorseCodeServer.Services
{
    public static class FrequencyChanger
    {
        public static int ChangeFrequency(int sound)
        {
            var freq = sound switch
            {
                1 => 100,
                2 => 200,
                3 => 300,
                4 => 400,
                5 => 500,
                6 => 600,
                7 => 700,
                8 => 800,
                9 => 900,
                10 => 1000,
                _ => 600,
            };
            return freq;
        }

    }
}
