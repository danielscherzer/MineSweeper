using System;
using System.Windows;
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

		public Cell(string text, Action<Cell> action)
		{
			InitializeComponent();
			textBlock.Text = text;
			this.action = action ?? throw new ArgumentNullException(nameof(action));
			this.action = action;
		}

		public void OpenCell()
		{
			if (open) return;
			cell.Children.Remove(button);
			open = true;
			action(this);
		}

		private void OpenCell(object sender, RoutedEventArgs e) => OpenCell();

		private void Button_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var button = sender as Button;
			button.Content = ("X" == (string)button.Content) ? null : "X";
		}

		private void Cell_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{

		}
	}
}
