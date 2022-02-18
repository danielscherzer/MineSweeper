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

		public event MouseButtonEventHandler CellMouseDown; 
		
		private void OnCellMouseDown(object sender, MouseButtonEventArgs e)
		{
			if ((sender as Border)?.DataContext is ICell cell)
			{
				CellMouseDown?.Invoke(this, e);
				if (e.LeftButton == MouseButtonState.Pressed)
				{
					cell.IsOpen = true;
				}
				else
				{
					cell.IsMarked = !cell.IsMarked;
				}
			}
		}

		private void Border_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
		{
			e.ManipulationContainer = this;
			e.Handled = true;
		}

		private void Border_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
		{
			if ((sender as Border)?.DataContext is ICell cell)
			{
				var len = e.CumulativeManipulation.Translation.Length;
				if (len > 10)
				{
					cell.IsMarked = true;
				}
				e.Handled = true;
			}
		}

		private void Border_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
		{
			if ((sender as Border)?.DataContext is ICell cell)
			{
				var len = e.TotalManipulation.Translation.Length;
				if (len < 10)
				{
					cell.IsOpen = true;
				}
				e.Handled = true;
			}
		}
	}
}
