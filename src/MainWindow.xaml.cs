using System.Windows;

namespace MineSweeper
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

		private void RestartEasy(object sender, RoutedEventArgs e)
		{
			Resources["mineSweeperModel"] = new MainWindowViewModel(10, 10, 10);
		}

		private void RestartMedium(object sender, RoutedEventArgs e)
		{
			Resources["mineSweeperModel"] = new MainWindowViewModel(30, 15, 10);
		}

		private void RestartHard(object sender, RoutedEventArgs e)
		{
			Resources["mineSweeperModel"] = new MainWindowViewModel(100, 30, 20);
		}

		private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
#if DEBUG
			if (e.Key == System.Windows.Input.Key.Escape) Close();
#endif
		}
	}
}
