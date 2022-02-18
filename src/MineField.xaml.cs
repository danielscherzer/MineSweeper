using System.Windows.Controls;

namespace MineSweeper
{
	/// <summary>
	/// Interaction logic for MineField.xaml
	/// </summary>
	public partial class MineField : UserControl
	{
		public MineField()
		{
			InitializeComponent();
		}

		public bool Mark { get; set; }

		private void PrimaryActionCell(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (Mark)
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

		private static void MarkCell(object sender)
		{
			if ((sender as Border)?.DataContext is ICell cell)
				cell.IsMarked = !cell.IsMarked;
		}

		private static void OpenCell(object sender)
		{
			if ((sender as Border)?.DataContext is ICell cell) cell.IsOpen = true;
		}
	}
}
