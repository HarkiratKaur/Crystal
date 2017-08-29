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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices ch = new Choices();
            ch.Add("Hello");
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
                using (SpeechSynthesizer speech = new SpeechSynthesizer())
                {

                    foreach (InstalledVoice voice in speech.GetInstalledVoices())
                    {
                        VoiceInfo info = voice.VoiceInfo;
                        list.Add(info.Name);
                    }

                    speech.SetOutputToDefaultAudioDevice();
                    speech.Speak("I am Sorry Rattu. Please Dont be angry on me. I was simply fighting with you. And you know what, my rattu is very very good girl. please dont say that she is bad.");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            button1.Enabled = false;
            recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }
    }
}
