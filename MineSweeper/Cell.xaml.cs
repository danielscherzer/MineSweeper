using System;
using System.Windows.Controls;

namespace MineSweeper
{
	/// <summary>
	/// Interaction logic for Cell.xaml
	/// </summary>
	public partial class Cell : UserControl
	{
		private readonly Action<Cell> action;
		private bool open = false;

		public Cell(int mines, Action<Cell> action)
		{
			InitializeComponent();
			textBlockMines.Text = 0 == mines ? "" : mines.ToString();
			this.action = action ?? throw new ArgumentNullException(nameof(action));
		}

		public void OpenCell()
		{
			if (open) return;
			open = true;
			animationOpen.Begin();
			action(this);
		}

		private void MarkCell(object sender, System.Windows.Input.MouseButtonEventArgs e) => mark.Opacity = 1 - mark.Opacity;

		private void OpenCell(object sender, System.Windows.Input.MouseButtonEventArgs e) => OpenCell();
	}
}
