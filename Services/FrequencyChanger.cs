using Microsoft.Extensions.Caching.Memory;
using MorseCodeServer.Models;
using NAudio.MediaFoundation;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace MorseCodeServer.Services
{
    public class FrequencyChanger
    {
        byte[] cacheEntry;
        private IMemoryCache _cache;

        public FrequencyChanger(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
            CacheSounds();


        }
        public void CacheSounds()
        {
            string[] names = new string[] { "dotSound", "dashSound" };
            foreach (var name in names)
            {
                if (!_cache.TryGetValue(name, out cacheEntry))
                {
                    // Key not in cache, so get data.
                    cacheEntry = System.IO.File.ReadAllBytes(ConfigurationManager.AppSettings[name]);

                    _cache.Set(name, cacheEntry);
                }
            }
        }
        public void ChangeFrequency(byte[] audio, int sound)
        {
            string[] sounds = new string[] { ConfigurationManager.AppSettings["dotSound"].Replace(".mp3", ""), ConfigurationManager.AppSettings["dashSound"].Replace(".mp3", "") };

            foreach (var item in sounds)
            {

                using (var reader = new MediaFoundationReader(item + ".mp3"))
                {
                    var pitch = new SmbPitchShiftingSampleProvider(reader.ToSampleProvider());

                    var semitone = Math.Pow(2, 1.0 / sound);
                    var upOneTone = semitone * semitone;
                    var downOneTone = 1.0 / upOneTone;

                    pitch.PitchFactor = (float)downOneTone;

                    MediaFoundationEncoder.EncodeToMp3(pitch.ToWaveProvider(),
                            item + "2.mp3", 48000);

                    reader.Close();
                }
                System.IO.File.Delete(item + ".mp3");
                System.IO.File.Move(item + "2.mp3", item + ".mp3");
            }
        }
    }
}
