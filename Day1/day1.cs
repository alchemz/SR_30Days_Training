using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Recognition;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            btnDisable.Enabled= true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            // Create a new SpeechRecognitionEngine instance.
            SpeechRecognizer recognizer = new SpeechRecognizer();

            // Create a simple grammar that recognizes "red", "green", or "blue".
            Choices commands = new Choices();
            commands.Add(new string[] { "red", "blue", "green" ,"print my name"});

            // Create a GrammarBuilder object and append the Choices object.
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(commands);

            // Create the Grammar instance and load it into the speech recognition engine.
            Grammar g = new Grammar(gb);
            recEngine.LoadGrammarAsync(g);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;
            // Register a handler for the SpeechRecognized event.
            
        }

        // Create a simple handler for the SpeechRecognized event.
        void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "red":
                MessageBox.Show("Red);
                break;

                case "print my name":
                richTextBox1.Text += "\nJohnny";
                break;

            }
            throw new NotImplementedException();
            MessageBox.Show("Speech recognized: " + e.Result.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rectEngine.RecognizeAsyncStop();
            btnDisable.Enabled=false;
        }
    }
}

