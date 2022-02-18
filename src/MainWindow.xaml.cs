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

		private void PrimaryActionCell(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if(Mark)
			{
				MarkCell(sender);
			}
			else
			{
				OpenCell(sender);
			}
		}

		private void SecondaryActionCell(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (!Mark)
			{
				MarkCell(sender);
			}
			else
			{
				OpenCell(sender);
			}
		}

		private void OpenEmptyCell(object sender, RoutedEventArgs e)
		{
			var mineSweeperModel = Resources["mineSweeperModel"] as MainWindowViewModel;
			mineSweeperModel?.OpenEmptyCell();
		}

		private bool Mark => mark.IsChecked.HasValue && mark.IsChecked.Value;

		private static void MarkCell(object sender)
		{
			if ((sender as Border)?.DataContext is ICell cell)
				cell.IsMarked = !cell.IsMarked;
		}

		private static void OpenCell(object sender)
		{
			if ((sender as Border)?.DataContext is ICell cell) cell.IsOpen = true;
		}

		private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
#if DEBUG
			if (e.Key == System.Windows.Input.Key.Escape) Close();
#endif
		}
	}
}
