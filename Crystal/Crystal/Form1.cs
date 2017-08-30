using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Collections;

namespace Crystal
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
        SpeechSynthesizer speech = new SpeechSynthesizer();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            speech.SelectVoice("Microsoft Zira Desktop");
            Choices ch = new Choices();
            ch.Add("Hello");
            ch.Add("youtube");
            GrammarBuilder gbuilder = new GrammarBuilder();
            gbuilder.Append(ch);
            Grammar grammer = new Grammar(gbuilder);

            recognizer.LoadGrammarAsync(grammer);

            recognizer.SetInputToDefaultAudioDevice();

            recognizer.SpeechRecognized += Recognized;
        }

        private void Recognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "Hello")
            {
                ArrayList list = new ArrayList();
                
                    
                    foreach (InstalledVoice voice in speech.GetInstalledVoices())
                    {
                        VoiceInfo info = voice.VoiceInfo;
                        list.Add(info.Name);
                    }

                    speech.SetOutputToDefaultAudioDevice();
                    speech.Speak("hello");
                
            }

            else if (e.Result.Text=="youtube")
            {
                speech.Speak("Opening youtube!!!");
                System.Diagnostics.Process.Start("http://youtube.com");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
                                 
           if(button1.Text.ToUpper()=="START")
            {
                speech.Speak("Starting Speech Recognizer");
                button1.Text = "STOP";
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
                
            }
           else
            {
                speech.Speak("Stoping Speech Recognizer");
                button1.Text = "START";
                recognizer.RecognizeAsyncStop();
                
            }
        }

        

        private void button2_Click_1(object sender, EventArgs e)
        {
            recognizer.Dispose();
            speech.Dispose();
            Application.Exit();
        }
    }
}
