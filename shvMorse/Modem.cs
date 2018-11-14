namespace MorseCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.IO;

    public class Modem
    {
        private int TimeUnitInMilliSeconds { get; set; } = 30;
        private int Frequency { get; set; } = 650;
        public char DotUnicode { get; set; } = '●'; // U+25CF
        public char DashUnicode { get; set; } = '▬'; // U+25AC
        public char Dot { get; set; } = '.';
        public char Dash { get; set; } = '-';

        Codes codeStore;

        public Modem()
        {
            codeStore = new Codes();
        }

        public Modem(int timeUnitInMilliSeconds) : this()
        {
            TimeUnitInMilliSeconds = timeUnitInMilliSeconds;
        }

        public string ConvertToMorseCode(string sentence, bool addStartAndEndSignal = false)
        {
            var generatedCodeList = new List<string>();
            var wordsInSentence = sentence.Split(' ');

            //if (addStartAndEndSignal)
            //{
            //    generatedCodeList.Add(codeStore.GetSignalCode(SignalCodes.StartingSignal));
            //}

            foreach (var word in wordsInSentence)
            {
                foreach (var letter in word.ToUpperInvariant().ToCharArray())
                {
                    generatedCodeList.Add(codeStore[letter]);
                }
                generatedCodeList.Add("_");
            }

            if (addStartAndEndSignal)
            {
                //generatedCodeList.Add(codeStore.GetSignalCode(SignalCodes.EndOfWork));
            }
            else
            {
                generatedCodeList.RemoveAt(generatedCodeList.Count - 1);
            }

            return string.Join(" ", generatedCodeList).Replace(" _ ", "  ");
        }

        public void PlayMorseTone(string morseStringOrSentence)
        {
            if (IsValidMorse(morseStringOrSentence))
            {
                var pauseBetweenLetters = "_"; // One Time Unit
                var pauseBetweenWords = "___"; // Seven Time Unit

                morseStringOrSentence = morseStringOrSentence.Replace("  ", pauseBetweenWords);
                morseStringOrSentence = morseStringOrSentence.Replace(" ", pauseBetweenLetters);

                int[] speeddata = { 120, 100, 80, 67, 57, 44, 40, 36, 33 };
                foreach (int speed in speeddata)
                    //for (int speed = 60; speed > 30; speed--)
                {
                    int wpm = (int) 60 * 1000 / 50 / speed;
                    Console.WriteLine(wpm.ToString()+ "WPM  " + speed.ToString()+ "mSec/elem");

                    for (int i = 0; i < 2; i++)
                    {
                        PlayBeep(morseStringOrSentence, 600, speed);
                        PlayBeep("--.-", 600, speed);
                    }
                }

                foreach (var character in morseStringOrSentence.ToCharArray())
                {
                    //Console.Write(character);

                    switch (character)
                    {
                        case '.':
                            //Console.Beep(Frequency, TimeUnitInMilliSeconds);
                            //PlayBeep((UInt16)Frequency, TimeUnitInMilliSeconds);
                            //Console.Write(character);
                            break;
                        case '-':
                            //Console.Beep(Frequency, TimeUnitInMilliSeconds * 3);
                            //PlayBeep((UInt16)Frequency, TimeUnitInMilliSeconds * 3);
                            //Console.Write(character);
                            break;
                        case '_':
                            //Console.Beep(50, TimeUnitInMilliSeconds);
                            //Thread.Sleep(TimeUnitInMilliSeconds  );
                            //PlayBeep((UInt16)50, TimeUnitInMilliSeconds);
                            //Console.Write(character);
                            break;
                    }
                }
            }
            else
            {
                PlayMorseTone(ConvertToMorseCode(morseStringOrSentence));
            }
        }

        public string PlayMorseToneWPM(int base_wpm, int base_sound_freq, string base_char_set)
        {
            codeStore = new Codes();
            Dictionary<char, string> cc = codeStore.getchars();
            char[] char_keys = cc.Keys.ToArray();
            int maxnum = cc.Keys.Count ;

            switch (base_char_set)
            {
                case "Characters":
                    maxnum = 26;
                    break;
                case "C+Numbers":
                    maxnum = 36;
                    break;
                case "C+N+Special Characters":
                    maxnum = 48;
                    break;
                case "C+N+S+Brackets":
                    maxnum = 54;
                    break;
                default:
                    maxnum = 26;
                    break;
            }
            
            Random cRandom = new System.Random();
            int rnd = cRandom.Next(maxnum);
            string string_char = char_keys[rnd].ToString();
            string morse_code = ConvertToMorseCode(string_char);

            if (IsValidMorse(morse_code))
            {
                //var pauseBetweenLetters = "_"; // One Time Unit
                //var pauseBetweenWords = "___"; // Seven Time Unit

                //morseStringOrSentence = morseStringOrSentence.Replace("  ", pauseBetweenWords);
                //morseStringOrSentence = morseStringOrSentence.Replace(" ", pauseBetweenLetters);

                int[] speeddata = { 120, 100, 80, 67, 57, 44, 40, 36, 33 };
                //foreach (int speed in speeddata)
                {
                    int speed = (int)60 * 1000 / 50 / base_wpm;
                    Console.WriteLine(base_wpm.ToString() + "WPM  " + speed.ToString() + "mSec/elem");

                    for (int i = 0; i < 1; i++)
                    {
                        PlayBeep(morse_code, (ushort) base_sound_freq, speed);
                        //PlayBeep("--.-", (ushort) base_sound_freq, speed);
                    }
                }
            }
            else
            {
                //PlayMorseToneWPM(ConvertToMorseCode(string_char));
            }

            return string_char;
        }

        private bool IsValidMorse(string sentence)
        {
            var countDot = sentence.Count(x => x == '.');
            var countDash = sentence.Count(x => x == '-');
            var countSpace = sentence.Count(x => x == ' ');

            return
                sentence.Length > (countDot + countDash + countSpace)
                ? false : true;
        }

        public static void PlayBeep(string playchar, UInt16 frequency, int msDuration, UInt16 volume = 16383)
        {
            int numChar = playchar.Count();
            //Console.WriteLine(numChar);

            int totalDuration = 0;
            foreach (char c in playchar)
            {
                if (Convert.ToString(c) == ".") totalDuration += msDuration * 2;
                if (Convert.ToString(c) == "-") totalDuration += msDuration * 4;
                if (Convert.ToString(c) == "_") totalDuration += msDuration * 3;
            }
            Console.WriteLine( playchar.ToString() + " " + totalDuration.ToString() + "mSec" );

            var mStrm = new MemoryStream();
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
            int samples = (int)((decimal)samplesPerSecond * totalDuration / 1000);
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


            foreach (char c in playchar)
            {
                ushort frequency2 = 50;
                if (Convert.ToString(c) == ".")
                {
                    frequency2 = frequency;
                    samples = (int)((decimal)samplesPerSecond * msDuration / 1000);
                    //Console.WriteLine(".");

                    double theta = frequency2 * TAU / (double)samplesPerSecond;
                    // 'volume' is UInt16 with range 0 thru Uint16.MaxValue ( = 65 535)
                    // we need 'amp' to have the range of 0 thru Int16.MaxValue ( = 32 767)
                    double amp = volume >> 2; // so we simply set amp = volume / 2
                    for (int step = 0; step < samples; step++)
                    {
                        short s = (short)(amp * Math.Sign( Math.Sin(theta * (double)step)));
                        writer.Write(s);
                        //Console.Write(s);
                    }
                     theta = 50 * TAU / (double)samplesPerSecond;
                    // 'volume' is UInt16 with range 0 thru Uint16.MaxValue ( = 65 535)
                    // we need 'amp' to have the range of 0 thru Int16.MaxValue ( = 32 767)
                     amp = volume >> 2; // so we simply set amp = volume / 2
                    for (int step = 0; step < samples; step++)
                    {
                        short s = (short)(amp * Math.Sin(theta * (double)step));
                        writer.Write(s);
                        //Console.Write(s);
                    }
                }
                if (Convert.ToString(c) == "-")
                {
                    frequency2 = frequency;

                    samples = (int)((decimal)samplesPerSecond * msDuration * 3 / 1000);
                    //Console.WriteLine("-");

                    double theta = frequency2 * TAU / (double)samplesPerSecond;
                    // 'volume' is UInt16 with range 0 thru Uint16.MaxValue ( = 65 535)
                    // we need 'amp' to have the range of 0 thru Int16.MaxValue ( = 32 767)
                    double amp = volume >> 2; // so we simply set amp = volume / 2
                    for (int step = 0; step < samples; step++)
                    {
                        short s = (short)(amp * Math.Sign( Math.Sin(theta * (double)step) ) );
                        writer.Write(s);
                        //Console.WriteLine(s);
                    }

                    samples = (int)((decimal)samplesPerSecond * msDuration / 1000);
                    theta = 50 * TAU / (double)samplesPerSecond;
                    // 'volume' is UInt16 with range 0 thru Uint16.MaxValue ( = 65 535)
                    // we need 'amp' to have the range of 0 thru Int16.MaxValue ( = 32 767)
                     amp = volume >> 2; // so we simply set amp = volume / 2
                    for (int step = 0; step < samples; step++)
                    {
                        short s = (short)(amp * Math.Sin(theta * (double)step));
                        writer.Write(s);
                        //Console.Write(s);
                    }
                }
                if (Convert.ToString(c) == "_")
                {
                    frequency2 = 1;
                    samples = (int)((decimal)samplesPerSecond * msDuration * 3 / 1000);
                    //Console.WriteLine("_");

                    double theta = frequency2 * TAU / (double)samplesPerSecond;
                    // 'volume' is UInt16 with range 0 thru Uint16.MaxValue ( = 65 535)
                    // we need 'amp' to have the range of 0 thru Int16.MaxValue ( = 32 767)
                    double amp = volume >> 2; // so we simply set amp = volume / 2
                    for (int step = 0; step < samples; step++)
                    {
                        short s = (short)(amp * Math.Sin(theta * (double)step));
                        writer.Write(s);
                        //Console.Write(s);
                    }

                }


                //Console.WriteLine(samples);
            }


            mStrm.Seek(0, SeekOrigin.Begin);
            new System.Media.SoundPlayer(mStrm).PlaySync();
            writer.Close();
            mStrm.Close();
        }


    }
}