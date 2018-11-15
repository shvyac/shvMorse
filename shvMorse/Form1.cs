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
using System.Windows.Input;

namespace shvMorse
{
    public partial class Form1 : Form
    {
        Codes codeStore;

        public Form1()
        {
            InitializeComponent();
            
            codeStore = new Codes();
            Dictionary<char, string> cc = codeStore.getchars();
            
            DataSet ds = new DataSet();
            DataTable dt = ds.Tables.Add("ResopnseTime");
            DataRow dr;

            dataGridViewResponseTime.DataSource = ds;
            dataGridViewResponseTime.DataMember = "ResopnseTime";

            dt.Columns.Add("Char");
            dataGridViewResponseTime.Columns["Char"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dr = dt.NewRow();
            dr["Char"] = "mSec/elem";
            int[] speeddata = { 120, 100, 80, 67, 57, 44, 40, 36, 33 };
            foreach (int speed in speeddata)
            {
                int wpm = (int)60 * 1000 / 50 / speed;
                string txt = wpm.ToString() + "WPM"   ;
                dt.Columns.Add(txt);
                dr[txt] = speed.ToString() ;
                dataGridViewResponseTime.Columns[txt].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dt.Rows.Add(dr);            
            
            char[] az = Enumerable.Range('a', 'z' - 'a' + 1).Select(i => (Char)i).ToArray();
            char[] nm = Enumerable.Range('0', 10).Select(i => (Char)i).ToArray();
            char[] all = az.Union(nm).ToArray();

            foreach (var c in cc.Keys)
            {
                Console.WriteLine(c);
                dr = dt.NewRow();
                dr["Char"] = c;
                dt.Rows.Add(dr);
            }
            dataGridViewResponseTime.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewResponseTime.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            //Application.Idle += new EventHandler(Application_Idle);

            //this.KeyDown += new KeyEventHandler(Application_Idle);

            this.KeyPreview = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(Form1_KeyUp);

        }

        private void button1_Click(object sender, EventArgs e)
        {
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

        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

        private void button5_Click(object sender, EventArgs e)
        {
            var modem = new Modem();

            int base_wpm = Convert.ToInt16(comboBoxWPM.Text);
            int base_sound_freq = Convert.ToInt16(comboBoxFREQ.Text);
            string base_char_set = comboBoxCharSet.Text;

            Console.WriteLine(@"\n button5_Click");

            textBox1.Text += comboBoxWPM.Text + " " + comboBoxFREQ.Text + "   ";

            Console.WriteLine("\t Playing from Morse Code.");

            sw.Reset();
            sw.Start();
            string str_return = modem.PlayMorseToneWPM(base_wpm, base_sound_freq, base_char_set);
            //sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString(@"s\.fff"));

            textBox1.Text += str_return + " " + sw.Elapsed.ToString(@"s\.fff") + "\r\n";

            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.Focus();
            textBox1.ScrollToCaret();
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            //MessageBox.Show(Key.H.ToString());            
       
            if (Keyboard.IsKeyDown(Key.H) == true)
            {
                textBox1.Text += "H が押されています。" + "\r\n";
            }
            else
            {
                //textBox1.Text = "なし。";
            }

            if (Keyboard.IsKeyDown(Key.G) == true)
            {
                textBox1.Text += "G キーが押されています。" + "\r\n";
            }
            else
            {
                //textBox1.Text = "なし。";
            }
        }

        int counter = 0;
        private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //MessageBox.Show(e.KeyCode.ToString());
            if (counter == 0)
            {
                Console.WriteLine(e.KeyCode.ToString() + "  Form1_KeyDown  " + counter.ToString());
                counter++;
                sw.Stop();
                Console.WriteLine(sw.Elapsed.ToString(@"s\.fff"));
            }
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //MessageBox.Show(e.KeyCode.ToString());
            Console.WriteLine(e.KeyCode.ToString()+ "  Form1_PreviewKeyDown ");
        }

        private void Form1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (counter > 0)
            {
                counter--;
                //Console.WriteLine(e.KeyCode.ToString() + "  Form1_KeyUp  " + counter.ToString());
                button5.PerformClick();
            }
        }
    }
}
