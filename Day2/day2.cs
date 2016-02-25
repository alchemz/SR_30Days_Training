using System;
using System.Globalization;
using System.Speech.Recognition;
using System.Windows;
using System.Windows.Media;

namespace Day2.Audio_and_Video
{
	public partial class SpeechRecognitionTextSample : Window
	{
		private SpeechRecognitionEngine speechRecognizer= new SpeechRecognitionEngine();

		public SpeechRecognitionTextSample()
		{
			InitializeComponent();
			speechRecognizer.SpeechRecognized += speechRecognizer_SpeecgRecognized;

			GrammerBuilder grammerBuilder =new GrammerBuilder();
			Choices commandChoices =new Choices("weight", "color", "size");
			grammerBuilder.Append(commandChoices);

			Choices valueChoices =new Choices();
			valueChoices.Add("normal", "bold");
			valueChoices.Add("red", "green", "blue");
			valueChoices.Add("small", "medium", "large");
			grammerBuilder.Append(valueChoices);

			speechRecognizer.LoadGrammar(new Grammer(grammerBuilder));
			speechRecognizer.SetInputToDefaultAudioDevice();
			
		}

		private void btnToggleListening_Click(object sender, RoutedEventArgs e)
		{
			if(btnToggleListening_IsChecked ==true)
			speechRecognizer.RecognizedAsync(RecognizeMode.Multiple);
			else
			speechRecognizer.RecognizedAsyncStop();
			txtSpeech.Text="";
		}

		private void speechRecognizer_SpeechRecognized(object sender, RoutedEventArgs e)
		{
			lblDemo.Content = e.Result.Text;
			if(e.Result.Words.Count ==2)
			{
				string command= e.Result.Words[0].Text.ToLower();
				string value =e.Result.Words[1].Text.ToLower();
				switch(command)
				{
					case "weight":
					FontWeightConverter weightConverter = new FontWeightConverter();
					lblDemo.FontWeight =(FontWeight)weightConverter.ConverterFromString(value);
					break;

					case "color":
					lblDemo.Foreground =new SolidColorBrush((Color)ColorConverter.ConverterFromString(value));
					break;

					case "size"
					switch(value)
					{
						case "small":
						lblDemo.FontSize =12;
						break;

						case "medium":
						lblDemo.FontSize =24;
						break;

						case "large":
						lblDemo.FontSize=48;
						break;
					}
					break;
				}
				
			}
			
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			speechRecognizer.Dispose();
		}
	}
}