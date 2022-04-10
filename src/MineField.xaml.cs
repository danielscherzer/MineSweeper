using System;
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

		private const int delta = 10;

		private void CellManipulationStarting(object sender, ManipulationStartingEventArgs e)
		{
			e.ManipulationContainer = this;
			e.Handled = true;
		}

		private void CellManipulationDelta(object sender, ManipulationDeltaEventArgs e)
		{
			IfCell(sender, cell =>
			{
				var len = e.CumulativeManipulation.Translation.Length;
				if (len > delta)
				{
					cell.IsMarked = true;
				}
				e.Handled = true;
			});
		}

		private void CellManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
		{
			IfCell(sender, cell =>
			{
				var len = e.TotalManipulation.Translation.Length;
				if (len < delta)
				{
					cell.IsOpen = true;
				}
				e.Handled = true;
			});
		}

		private void CellMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			IfCell(sender, cell => cell.IsOpen = true);
		}

		private void CellMouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			IfCell(sender, cell => cell.IsMarked = !cell.IsMarked);
		}

		private static void IfCell(object sender, Action<ICell> action)
		{
			if ((sender as Border)?.DataContext is ICell cell)
			{
				action(cell);
			}
		}
	}
}
