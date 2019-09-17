using System.Windows;
using System.Windows.Controls;

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
			Resources["mineSweeperModel"] = new MineSweeperModel(10, 10, 10);
		}

		private void RestartMedium(object sender, RoutedEventArgs e)
		{
			Resources["mineSweeperModel"] = new MineSweeperModel(30, 15, 10);
		}

		private void RestartHard(object sender, RoutedEventArgs e)
		{
			Resources["mineSweeperModel"] = new MineSweeperModel(100, 30, 20);
		}

		private void OpenCell(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var element = sender as Border;
			var field = element.DataContext as IField;
			field.IsOpen = true;
		}

		private void MarkCell(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var element = sender as Border;
			var field = element.DataContext as IField;
			field.IsMarked = !field.IsMarked;
		}

		private void OpenEmptyField(object sender, RoutedEventArgs e)
		{
			var mineSweeperModel = Resources["mineSweeperModel"] as MineSweeperModel;
			mineSweeperModel?.OpenEmptyField();
		}
	}
}
