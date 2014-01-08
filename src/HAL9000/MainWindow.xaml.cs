using System;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using HAL9000.Speach;

namespace HAL9000
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		DispatcherTimer timer = new DispatcherTimer();
		public MainWindow()
		{
			InitializeComponent();

            voice.SetOutputToDefaultAudioDevice();

		}

		private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
				Close();

			if (e.Key != Key.Space)
				return;

			Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
			HalImage.Visibility = Visibility.Visible;
			ellipse.Visibility = Visibility.Visible;
			timer.Tick += timer_Tick;
			timer.Interval = TimeSpan.FromMilliseconds(100);
			timer.Start();
			Speak();
		}

		private SpeechSynthesizer voice = new SpeechSynthesizer();
		private void Speak()
		{
			 
            

			voice.SpeakAsync(HappyHourSpeach.Speach);
		}

		private Random rand = new Random();

		private double speed = 0.1;
		private bool up = false;
		void timer_Tick(object sender, EventArgs e)
		{
			
			if (up)
			{
				ss.Offset += speed;
			}
			else
			{
				ss.Offset -= speed;
			}
			if (ss.Offset < 0.4)
			{
				ss.Offset = 0.4;
				ChangeSpeed();
				up = true;
			}
			if (ss.Offset > 0.8)
			{
				ss.Offset = 0.8;
				up = false;
			}

		}

		private void ChangeSpeed()
		{
			speed = rand.Next(3, 10)*0.05;
		}
	}
}
