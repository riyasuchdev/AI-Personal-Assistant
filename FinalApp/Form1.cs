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
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Diagnostics;

namespace FinalApp
{
    public partial class Form1 : Form
    {
        
        public SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-GB"));
        public Grammar grammar;
        public Thread RecThread;
        public bool RecognizerState = false;
        public String s;
        bool app = false;
        bool flag = true;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
            speechSynthesizer.Speak("welcome to our application");
            GrammarBuilder builder = new GrammarBuilder();
            builder.AppendDictation();
            grammar = new Grammar(builder);
            recognizer = new SpeechRecognitionEngine();
            //recognizer.LoadGrammar(grammar);
            string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\WordLibrary.txt");
            for (int i = 0; i < lines.Count(); i++)
            {
                recognizer.LoadGrammar(new Grammar(new GrammarBuilder(lines[i])));
            }
            //recognizer.LoadGrammar(new Grammar(new GrammarBuilder("yash")));
            RecognizerState = this.Visible;
            recognizer.RequestRecognizerUpdate(); // request for recognizer update
            recognizer.SpeechRecognized += recognizer_SpeechRecognized; // if speech is recognized, call the specified method
            recognizer.SetInputToDefaultAudioDevice(); // set the input to the default audio device
            recognizer.RecognizeAsync(RecognizeMode.Multiple); // recognize speech asynchronous
            RecThread = new Thread(new ThreadStart(RecThreadFunction));
            RecThread.IsBackground = false;
            RecThread.Start();
        }
        public void recognizer_SpeechRecognized(Object sender, SpeechRecognizedEventArgs e)
        {
            try
            {
                if (speakbt.Checked)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        s = e.Result.Text.ToLower();
                        wordstxt.Text = (e.Result.Text.ToLower());
                    });
                }                
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }
        public void RecThreadFunction()
        {
            while (RecognizerState)
            {
                try
                {
                    recognizer.Recognize();
                }
                catch
                {
                }
            }
        }

        private void stopbt_Click(object sender, EventArgs e)
        {
            SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
            speechSynthesizer.Speak("Thank you for using the application");
            this.Close();
        }

        private void wordstxt_TextChanged(object sender, EventArgs e)
        {
            flag = this.Visible;
            if (s.Equals("startapplication", StringComparison.Ordinal))
            {
                app = true;
                //MessageBox.Show("application mode started");
            }
            if(app)
            {
                try
                {
                    Process firstProc = new Process();
                    firstProc.StartInfo.FileName = s;
                    firstProc.EnableRaisingEvents = true;
                    firstProc.Start();
                    app = false;
                }
                catch
                {
                }
            }
            if (s.Equals("openmaps", StringComparison.Ordinal) && flag)
            {
                this.Visible = false;
                flag = this.Visible;
                RecognizerState = this.Visible;
                Form2 f2 = new Form2();
                f2.RefToForm1 = this;
                f2.CreateControl();
                f2.Show();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void speakbt_CheckedChanged(object sender, EventArgs e)
        {
            if (speakbt.Checked == false)
            {
                RecognizerState = false;
            }
        }
    }
}
