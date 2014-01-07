using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace HAL9000
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
				Close();

			if (e.Key != Key.Space)
				return;

			Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
			HalImage.Visibility = Visibility.Visible;
		}
	}
}
