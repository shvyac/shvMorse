using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;
using MorseCode;

namespace shvMorse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        //[System.Runtime.InteropServices.DllImport("kernel32.dll")]
        //private static extern bool Beep(uint dwFreq, uint dwDuration);

        private void button1_Click(object sender, EventArgs e)
        {
            //Beep(700, 400);

            //TestSine();



            int freq = 600;
            do
            {
                PlayBeep((UInt16)freq, 1000);
                //string f = createWave(freq, 1.5);
                freq += 50;
            } while (freq < 1200);
        }

        public static void PlayBeep(UInt16 frequency, int msDuration, UInt16 volume = 16383)
        {
            //string wavfilename = frequency.ToString() + @"_" + msDuration.ToString() + @".wav";
            //Console.WriteLine(wavfilename);

            var mStrm = new MemoryStream();
            //FileStream filStream = new FileStream(wavfilename, FileMode.Create, FileAccess.Write) ;

            BinaryWriter writer = new BinaryWriter(mStrm);

            const double TAU = 2 * Math.PI;
            int formatChunkSize = 16;
            int headerSize = 8;
            short formatType = 1;
            short tracks = 1;
            int samplesPerSecond = 44100;
            short bitsPerSample = 16;
            short frameSize = (short)(tracks * ((bitsPerSample + 7) / 8));
            int bytesPerSecond = samplesPerSecond * frameSize;
            int waveSize = 4;
            int samples = (int)((decimal)samplesPerSecond * msDuration / 1000);
            int dataChunkSize = samples * frameSize;
            int fileSize = waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize;
            // var encoding = new System.Text.UTF8Encoding();
            writer.Write(0x46464952); // = encoding.GetBytes("RIFF")
            writer.Write(fileSize);
            writer.Write(0x45564157); // = encoding.GetBytes("WAVE")
            writer.Write(0x20746D66); // = encoding.GetBytes("fmt ")
            writer.Write(formatChunkSize);
            writer.Write(formatType);
            writer.Write(tracks);
            writer.Write(samplesPerSecond);
            writer.Write(bytesPerSecond);
            writer.Write(frameSize);
            writer.Write(bitsPerSample);
            writer.Write(0x61746164); // = encoding.GetBytes("data")
            writer.Write(dataChunkSize);
            {
                double theta = frequency * TAU / (double)samplesPerSecond;
                // 'volume' is UInt16 with range 0 thru Uint16.MaxValue ( = 65 535)
                // we need 'amp' to have the range of 0 thru Int16.MaxValue ( = 32 767)
                double amp = volume >> 2; // so we simply set amp = volume / 2
                for (int step = 0; step < samples; step++)
                {
                    short s = (short)(amp * Math.Sin(theta * (double)step));
                    writer.Write(s);
                }
            }

            mStrm.Seek(0, SeekOrigin.Begin);
            new System.Media.SoundPlayer(mStrm).PlaySync();
            writer.Close();
            mStrm.Close();
        } // public static void PlayBeep(UInt16 frequency, int msDuration, UInt16 volume = 16383)

        private void button2_Click_1(object sender, EventArgs e)
        {
            int freq = 500;
            do
            {
                string f = createWave(freq, 1.5);

                string p = @"D:\shvDell_Dev@\VS2017\shvMorse\shvMorse\bin\Debug\" + f;

                SoundPlayer simpleSound = new SoundPlayer(p);
                simpleSound.Play();


                freq += 50;
            } while (freq < 1200);
        }

        WaveHeaderArgs createHeader = new WaveHeaderArgs();

        private string createWave(int frequency, double duration)
        {

            string wavfilename = frequency.ToString() + @"_" + duration.ToString("F") + @".wav";
            Console.WriteLine(wavfilename);

            string FileName2 = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\test002.wav";
            string FileName = wavfilename;

            using (FileStream filStream = new FileStream(FileName, FileMode.Create, FileAccess.Write))
            using (BinaryWriter binWriter = new BinaryWriter(filStream))
            {

                createHeader.FormatChunkSize = 16;
                createHeader.FormatID = 1;
                createHeader.Channel = 2;
                createHeader.SampleRate = 44100;
                createHeader.BitPerSample = 16;

                int NumberOfBytePerSample = ((ushort)(Math.Ceiling((double)createHeader.BitPerSample / 8)));
                createHeader.BlockSize = (short)(NumberOfBytePerSample * createHeader.Channel);
                createHeader.BytePerSec = createHeader.SampleRate * createHeader.Channel * NumberOfBytePerSample;
                int msec = (int)duration * 1000;
                int DataLength = (createHeader.SampleRate * msec) / 1000;
                createHeader.DataChunkSize = createHeader.BlockSize * DataLength;
                createHeader.FileSize = createHeader.DataChunkSize + 44;

                binWriter.Write(headerBytes());

                for (UInt32 cnt = 0; cnt < DataLength; cnt++)
                {
                    double Radian = (double)cnt / createHeader.SampleRate;
                    Radian *= 2 * Math.PI;

                    // 10Hzの正弦波を作る。
                    double Wave = Math.Sin(Radian * frequency);

                    short Data = (short)(Wave * 30000);

                    binWriter.Write(BitConverter.GetBytes(Data));
                    //binWriter.Write(BitConverter.GetBytes(Data));
                }

            }

            return wavfilename;
        }

        public struct WaveHeaderArgs
        {
            public string RiffHeader;
            public int FileSize;
            public string WaveHeader;
            public string FormatChunk;
            public int FormatChunkSize;
            public short FormatID;
            public short Channel;
            public int SampleRate;
            public int BytePerSec;
            public short BlockSize;
            public short BitPerSample;
            public string DataChunk;
            public int DataChunkSize;
            public int PlayTimeMsec;
        }

        private byte[] headerBytes()
        {
            byte[] Datas = new byte[44];

            Array.Copy(Encoding.ASCII.GetBytes("RIFF"), 0, Datas, 0, 4);
            Array.Copy(BitConverter.GetBytes((UInt32)(createHeader.FileSize - 8)), 0, Datas, 4, 4);
            Array.Copy(Encoding.ASCII.GetBytes("WAVE"), 0, Datas, 8, 4);
            Array.Copy(Encoding.ASCII.GetBytes("fmt "), 0, Datas, 12, 4);
            Array.Copy(BitConverter.GetBytes((UInt32)(createHeader.FormatChunkSize)), 0, Datas, 16, 4);
            Array.Copy(BitConverter.GetBytes((UInt16)(createHeader.FormatID)), 0, Datas, 20, 2);
            Array.Copy(BitConverter.GetBytes((UInt16)(createHeader.Channel)), 0, Datas, 22, 2);
            Array.Copy(BitConverter.GetBytes((UInt32)(createHeader.SampleRate)), 0, Datas, 24, 4);
            Array.Copy(BitConverter.GetBytes((UInt32)(createHeader.BytePerSec)), 0, Datas, 28, 4);
            Array.Copy(BitConverter.GetBytes((UInt16)(createHeader.BlockSize)), 0, Datas, 32, 2);
            Array.Copy(BitConverter.GetBytes((UInt16)(createHeader.BitPerSample)), 0, Datas, 34, 2);
            Array.Copy(Encoding.ASCII.GetBytes("data"), 0, Datas, 36, 4);
            Array.Copy(BitConverter.GetBytes((UInt32)(createHeader.DataChunkSize)), 0, Datas, 40, 4);

            return (Datas);
        }

        private void button3_Click(object sender, EventArgs ea)
        {
            int freq = 600;
            int t = 90;
            int e = 30;

            PlayBeep((UInt16)freq, t);
            PlayBeep((UInt16)10, e);
            PlayBeep((UInt16)freq, e);
            PlayBeep((UInt16)10, e);
            PlayBeep((UInt16)freq, t);
            PlayBeep((UInt16)10, e);
            PlayBeep((UInt16)freq, e);
            PlayBeep((UInt16)10, t);

            PlayBeep((UInt16)freq, t);
            PlayBeep((UInt16)10, e);
            PlayBeep((UInt16)freq, t);
            PlayBeep((UInt16)10, e);
            PlayBeep((UInt16)freq, e);
            PlayBeep((UInt16)10, e);
            PlayBeep((UInt16)freq, t);
            PlayBeep((UInt16)10, e);

        }

      

        private void button4_Click_1(object sender, EventArgs e)
        {
            //static void Main(string[] args)
            //{
            var modem = new Modem();
            var message = "C";

            //Beep(700, 40);

            // Converting to Morse Code
            var morseCode = modem.ConvertToMorseCode(message);

            Console.WriteLine($"Morse Code for Sentence : {message}");
            Console.WriteLine(morseCode);

            textBox1.Text += $"Morse Code for Sentence : {message}";

            // Morse Code can be played with generated morse code
            Console.WriteLine("Playing from Morse Code.");
            modem.PlayMorseTone(morseCode);

            // or Can be direactly played by passing sentence
            //Console.WriteLine("Playing from Message.");
            //modem.PlayMorseTone(message);
            //}
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
