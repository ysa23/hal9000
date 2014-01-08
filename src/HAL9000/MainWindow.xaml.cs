using System;
using System.Speech.Synthesis;
using System.Threading.Tasks;
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
		private readonly DispatcherTimer _timer = new DispatcherTimer();
		private string _speech;

		public MainWindow()
		{
			InitializeComponent();

			_voice.SetOutputToDefaultAudioDevice();
		}

		private async void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
				Close();

			if (e.Key != Key.Space && e.Key != Key.Enter)
				return;

			_speech = e.Key == Key.Space ? Speeches.HappyHour : Speeches.Dsm;

			await Task.Delay(2000);

			var timeline = new MediaTimeline(new Uri("TVStatic.mp4", UriKind.Relative)) { Duration = new Duration(TimeSpan.FromSeconds(2)) };
			timeline.Completed += TimelineOnCompleted; 
			var player = new MediaPlayer { Clock = timeline.CreateClock() };
			Background = new DrawingBrush(new VideoDrawing { Rect = new Rect(0, 0, 300, 200), Player = player });
		}

		private async void TimelineOnCompleted(object sender, EventArgs eventArgs)
		{
			Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
			await Task.Delay(2000);
			HalImage.Visibility = Visibility.Visible;
			ellipse.Visibility = Visibility.Visible;
			await Task.Delay(2000);
			_timer.Tick += Timer_Tick;
			_timer.Interval = TimeSpan.FromMilliseconds(100);
			_timer.Start();
			Speak();
		}

		private readonly SpeechSynthesizer _voice = new SpeechSynthesizer();
		private void Speak()
		{
			_voice.SpeakAsync(_speech);
			_voice.SpeakCompleted += Voice_SpeakCompleted;
		}

		private async void Voice_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
		{
			_timer.Stop();
			await Task.Delay(1000);
			HalImage.Visibility = Visibility.Hidden;
			ellipse.Visibility = Visibility.Hidden;
			await Task.Delay(500);
			SearsHarborImage.Visibility = Visibility.Visible;
			await Task.Delay(2000);
			Close();
		}

		private readonly Random _rand = new Random();

		private double _speed = 0.1;
		private bool _up;
		private void Timer_Tick(object sender, EventArgs e)
		{
			if (_up)
			{
				ss.Offset += _speed;
			}
			else
			{
				ss.Offset -= _speed;
			}
			if (ss.Offset < 0.4)
			{
				ss.Offset = 0.4;
				ChangeSpeed();
				_up = true;
			}
			if (ss.Offset > 0.8)
			{
				ss.Offset = 0.8;
				_up = false;
			}
		}

		private void ChangeSpeed()
		{
			_speed = _rand.Next(3, 10)*0.05;
		}
	}
}
