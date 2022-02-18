using System.Windows.Controls;
using System.Windows.Input;

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

		public bool Mark { get; set; } //TODO: set mark

		private void CellMouseDown(object sender, MouseButtonEventArgs e)
		{
			if ((sender as Border)?.DataContext is ICell cell)
			{
				if (e.LeftButton == MouseButtonState.Pressed ^ Mark)
				{
					cell.IsOpen = true;
				}
				else
				{
					cell.IsMarked = !cell.IsMarked;
				}
			}
		}
	}
}
