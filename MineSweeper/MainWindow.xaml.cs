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

		private void Restart(object sender, RoutedEventArgs e)
		{
			//Resources["mineSweeperModel"] = new MineSweeperModel();
			//Resources["timer"] = new Timer();
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
	}
}
