using MorseCodeServer.Util;
using NAudio.Lame;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorseCodeServer.Services
{
    public class MorseDecoder : IMorseDecoder
    {
      
        public string Decode(string input)
        {
            StringBuilder decodedString = new StringBuilder();
            foreach (var letter in input)
            {
                var decoded = MorseCodeLib.Morse[letter];
                decodedString.Append(decoded);
            }
            return decodedString.ToString();
        }

        public byte[] MorseBuilder(string msg)
        {
            MemoryStream st = new MemoryStream();
           // var decoded = Decode(msg);

            foreach (var letter in msg)
            {
                switch (letter)
                {
                    case '.':
                        
                        Combine(ConfigurationManager.AppSettings["dotSound"], st);
                        break;

                    case '-':

                        Combine(ConfigurationManager.AppSettings["dashSound"], st);
                        break;

                    default:
                        break;
                }
            }
            return st.ToArray();  
        }

      
        public void Combine(string file, Stream output)
        {

            Mp3FileReader reader = new Mp3FileReader(file);
            
            if ((output.Position == 0) && (reader.Id3v2Tag != null))
            {
                output.Write(reader.Id3v2Tag.RawData, 0, reader.Id3v2Tag.RawData.Length);
            }
            Mp3Frame frame;
            while ((frame = reader.ReadNextFrame()) != null)
            {
                output.Write(frame.RawData, 0, frame.RawData.Length);
            }
        }
    }
}
