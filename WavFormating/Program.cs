using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Utils;
using NAudio.Wave;

namespace WavFormating
{
    class Program
    {
        public static int _sampleRate = 16000;
        public static int _channels = 1;

        public static byte[] ConvertWavTo16BitMonoWav(byte[] waveFileBytes)
        {
            byte[] monoWavStream = null;

            using (WaveFileReader myWav = new WaveFileReader(new MemoryStream(waveFileBytes)))
            {
                var outFormat = new WaveFormat(_sampleRate, _channels);

                using (MediaFoundationResampler mediaSampler = new MediaFoundationResampler(myWav, outFormat))
                {
                    using (MemoryStream opWav = new MemoryStream())
                    {
                        WaveFileWriter.WriteWavFileToStream(opWav, mediaSampler);
                        monoWavStream = opWav.ToArray();
                    }
                }
            }

            return monoWavStream;
        }
        static void Main(string[] args)
        {
            byte[] bytes = File.ReadAllBytes(@"<source>");
            byte[] formatted = ConvertWavTo16BitMonoWav(bytes);
            File.WriteAllBytes(@"<destination>", formatted);
        }
    }
}
