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
using System.Net;
using System.Xml.Linq;

namespace FinalApp
{
    public partial class Form2 : Form
    {
        public SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-GB"));
        public Grammar grammar;
        public Thread RecThread;
        public bool RecognizerState = true;
        public String s;
        bool flag = true;
        public Form RefToForm1 { get; set; }
        public Form2()
        {
            InitializeComponent();
            CreateHandle();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
            speechSynthesizer.Speak("hello user welcome to maps. Please tell the location to search. And end it with .search");
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
            recognizer.RequestRecognizerUpdate(); // request for recognizer update
            recognizer.SpeechRecognized += recognizer_SpeechRecognized; // if speech is recognized, call the specified method
            recognizer.SetInputToDefaultAudioDevice(); // set the input to the default audio device
            recognizer.RecognizeAsync(RecognizeMode.Multiple); // recognize speech asynchronous
            RecognizerState = true;
            RecThread = new Thread(new ThreadStart(RecThreadFunction));
            RecThread.IsBackground = false;
            RecThread.Start();
        }
        public void recognizer_SpeechRecognized(Object sender, SpeechRecognizedEventArgs e)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    if (e.Result.Text.ToLower().Equals("search", StringComparison.Ordinal))
                    {
                        flag = false;
                        List<String> list = new List<String>();
                        list = GetLatLng(location.Text);
                        maps.Url = new System.Uri("http://maps.google.com/maps?q=" + list[0] + "," + list[1]);
                        //Form3 f = new Form3(list[0], list[1]);
                        //RecognizerState = !RecognizerState;
                        //f.CreateControl();
                        //f.Show();
                    }
                    else if (!e.Result.Text.ToLower().Equals("search", StringComparison.Ordinal) && flag)
                    {
                        s = e.Result.Text.ToLower();
                        location.Text += " " + e.Result.Text.ToLower();
                    }
                    else if (e.Result.Text.ToLower().Equals("closemapapp", StringComparison.Ordinal))
                    {
                        RecognizerState = !RecognizerState;
                        this.RefToForm1.Show();
                        this.Close();
                    }
                });
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
        public static List<string> GetLatLng(string address)
        {
            var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=true", Uri.EscapeDataString(address));
            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
            var xdoc = XDocument.Load(response.GetResponseStream());
            var latLngArray = new List<string>();
            var xElement = xdoc.Element("GeocodeResponse");
            if (xElement != null)
            {
                var result = xElement.Element("result");
                if (result != null)
                {
                    var element = result.Element("geometry");
                    if (element != null)
                    {
                        var locationElement = element.Element("location");
                        if (locationElement != null)
                        {
                            var xElement1 = locationElement.Element("lat");
                            if (xElement1 != null)
                            {
                                var lat = xElement1.Value;
                                latLngArray.Add(lat);

                            }
                            var element1 = locationElement.Element("lng");
                            if (element1 != null)
                            {
                                var lng = element1.Value;
                                latLngArray.Add(lng);
                            }
                        }
                    }
                }
            }
            return latLngArray;
        }
        private void location_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            RecognizerState = !RecognizerState;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
        }
    }
}
