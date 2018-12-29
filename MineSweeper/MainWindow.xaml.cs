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
			Window_SizeChanged(this, null);
			mineGrid = new MineGrid(10, 10, 10);
			cells = new Cell[mineGrid.Columns, mineGrid.Rows];
			for (int x = 0; x < mineGrid.Columns; ++x)
			{
				grid.ColumnDefinitions.Add(new ColumnDefinition());
			}
			for (int y = 0; y < mineGrid.Rows; ++y)
			{
				grid.RowDefinitions.Add(new RowDefinition());
			}
			for (int x = 0; x < grid.ColumnDefinitions.Count; ++x)
			{
				for (int y = 0; y < grid.RowDefinitions.Count; ++y)
				{
					var mines = mineGrid.HowManyMines(x, y);
					var cell = new Cell(mines, OpenCell);
					grid.Children.Add(cell);
					Grid.SetColumn(cell, x);
					Grid.SetRow(cell, y);
					cells[x, y] = cell;
				}
			}
		}

		private MineGrid mineGrid;
		private Cell[,] cells;

		private void OpenCell(Cell cell)
		{
			var x = Grid.GetColumn(cell);
			var y = Grid.GetRow(cell);
			if (0 == mineGrid.HowManyMines(x, y))
			{
				mineGrid.ForEachNeighbor(x, y, (x_, y_) => cells[x_, y_].OpenCell());
			}
		}

		private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			var captionHeight = SystemParameters.CaptionHeight;
			if (Width > Height)
			{
				Height = Width + captionHeight;
			}
			else
			{
				Width = Height - captionHeight;
			}
		}
	}
}
