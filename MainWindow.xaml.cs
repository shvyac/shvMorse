using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Threading;

namespace shvMORSE
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public string path_wav;
        public Modem modem;

        public MainWindow()
        {
            InitializeComponent();

            SetMorseTabInitially();
        }

        private void SetMorseTabInitially()
        {
            TextBoxSpeedStart.Text      = @"36";
            TextBoxSpeedEnd.Text        = @"16";
            TextBoxSpeedDecrement.Text  = @"2";
            TextBoxRepeatCycles.Text    = @"20";
            textBoxSystemAnswer.Text    = @"Answer shows here";
            textBoxUserAnswer.Text      = @"Your Answer";

            for (int cwPitch = 300; cwPitch <= 2600; cwPitch += 50)
            {
                ComboBoxCWPitch.Items.Add(cwPitch.ToString() + @"Hz");
            }
            ComboBoxCWPitch.SelectedIndex = 8;

            radioButtonAnswerBefore.IsChecked = true;
            radioButtonAnswerAllAfter.IsChecked = false;

            ButtonSpeedDown.Background = Brushes.LightBlue;
            ButtonStopCW.Background = Brushes.LightPink;            

            path_wav = @"shvMorse_wav";
        }

        public  void ButtonSpeedDown_Click(object sender, RoutedEventArgs e)
        {
            ButtonSpeedDown.Background = Brushes.LightPink;
            ButtonStopCW.Background = Brushes.LightBlue;
            //var modem = new Modem();
            modem = new Modem();

            int speedS = (int)Convert.ToInt16(TextBoxSpeedStart.Text);
            int speedE = (int)Convert.ToInt16(TextBoxSpeedEnd.Text);
            int speedD = (int)Convert.ToInt16(TextBoxSpeedDecrement.Text);

            int repeatCycles = (int)Convert.ToInt16(TextBoxRepeatCycles.Text);

            string freqHz = ComboBoxCWPitch.Text;
            string freq = freqHz.Remove(freqHz.Length - 2);//remove Hz
            int cwPitch = int.Parse(freq);

            string Message = string.Format(
                "Tone={0}, WPM Start={1}, End={2}, Decrement={3}, Cycles={4} ", 
                freqHz, speedS, speedE, speedD, repeatCycles);
            ListViewMorseDebug.Items.Add(Message);

            var messageABC = generateRandomCall();

            if ((bool)radioButtonAnswerBefore.IsChecked)
            {
                Debug.WriteLine("radioButtonAnswerBefore IsChecked");
                textBoxSystemAnswer.Text = messageABC.ToString();
            }

            var morseCode = modem.ConvertToMorseCode(messageABC);

            Debug.WriteLine(messageABC);
            Debug.WriteLine(morseCode);
            Debug.WriteLine("Playing from Morse Code.");

            bool answerBefore = (bool)radioButtonAnswerBefore.IsChecked;

            //-------------------------------------------------------------------------------------------------
            
            _ = modem.PlayMorseTone(morseCode, cwPitch, speedS, speedE, speedD, answerBefore, messageABC, path_wav);

            //-------------------------------------------------------------------------------------------------

            if ((bool)radioButtonAnswerAllAfter.IsChecked)
            {
                Debug.WriteLine("radioButtonAnswerAllAfter IsChecked");
                textBoxSystemAnswer.Text = messageABC.ToString();
            }          
        }

        private void ButtonStopCW_Click(object sender, RoutedEventArgs e)
        {
            ButtonSpeedDown.Background = Brushes.LightBlue;
            ButtonStopCW.Background = Brushes.LightPink;
            modem.cancelTokensourceModem.Cancel();
        }

        private void ButtonOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            string baseDir = Environment.CurrentDirectory;

            System.Diagnostics.Process.Start("explorer.exe", baseDir + @"\" + path_wav);

        }

        private string generateRandomCall()
        {
            string[] callJPN = {
                "JA", "JB", "JC", "JD", "JE", "JF", "JG", "JH", "JI", "JJ", "JK", "JL", "JM", "JN", "JO", "JP", "JQ", "JR", "JS",
                "7J", "7K", "7L", "7M", "7N",
                "8J", "8K", "8L", "8M", "8N" }; //JA - JS 日本 7J - 7N 日本 8J - 8N 日本

            string[] callABC = {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            string[] powerClass = {
                "H", "M", "L", "P" };

            /*
            (注1)空中線電力別の表記(アルファベット)については、次の区分による。
            100Ｗ超 Ｈ
            20Ｗを超え100Ｗ以下 Ｍ
            5Ｗを超え20Ｗ以下 Ｌ
            5Ｗ以下 Ｐ
            */

            string[] domainPrefNumberStr = {
                "101","102","103","104","105","106","107",
                "108","109","110","111","112","113","114",
                "02","03","04","05","06","07","08","09","10",
                "11","12","13","14","15","16","17","18","19","20",
                "21","22","23","24","25","26","27","28","29","30",
                "31","32","33","34","35","36","37","38","39","40",
                "41","42","43","44","45","46","47","48" };

            string[] domainPrefName = {
                "宗谷","留萌","上川","オホーツク","空知","石狩","根室",
                "後志","十勝","釧路","日高","胆振","桧山","渡島",
                "青森","岩手","秋田","山形","宮城","福島","新潟","長野","東京",
                "神奈川","千葉","埼玉","茨城","栃木","群馬","山梨","静岡","岐阜","愛知",
                "三重","京都","滋賀","奈良","大阪","和歌山","兵庫","富山","福井","石川",
                "岡山","島根","山口","鳥取","広島","香川","徳島","愛媛","高知","福岡",
                "佐賀","長崎","熊本","大分","宮崎","鹿児島","沖縄","小笠原" };

            System.Random r = new System.Random();

            int intPrefix = r.Next(callJPN.Count());
            int intAreaNumber = r.Next(10);
            int intSurfix1 = r.Next(callABC.Count());
            int intSurfix2 = r.Next(callABC.Count());
            int intSurfix3 = r.Next(callABC.Count());

            int intPref = r.Next(domainPrefNumberStr.Count());
            int intPower = r.Next(powerClass.Count());

            string genCall = 
                callJPN[intPrefix] + intAreaNumber.ToString() + callABC[intSurfix1] + callABC[intSurfix2] + callABC[intSurfix3]
                + @" 5NN " + domainPrefNumberStr[intPref] + powerClass[intPower] + @" K";

            Debug.WriteLine("Generated Call Sign=" + genCall);

            return genCall;
        }        
    }
}
