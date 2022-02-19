using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace MineSweeper
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		public MainWindowViewModel() : this(10, 10, 10) { }
		public MainWindowViewModel(int mines, int columns, int rows)
		{
			MineField = new MineFieldViewModel(mines, columns, rows);
			MineField.PropertyChanged += Board_PropertyChanged;

			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += (s, e) => TimePlayed += TimeSpan.FromSeconds(1);
			timer.Start();
		}

		private void Board_PropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			if(MineField.IsLost || MineField.IsWon) timer.IsEnabled = false;
		}

		public MineFieldViewModel MineField { get; }

		public void OpenEmptyCell()
		{
			foreach (var column in MineField.MineField)
			{
				foreach (var cell in column)
				{
					if (0 == cell.NeighborMines)
					{
						if (!cell.IsOpen)
						{
							cell.IsOpen = true;
							return;
						}
					}
				}
			}
		}

		public TimeSpan TimePlayed
		{
			get => _timePlayed;
			private set
			{
				_timePlayed = value;
				InvokePropertyChanged(nameof(TimePlayed));
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;
		
		private TimeSpan _timePlayed = TimeSpan.FromSeconds(0);
		private readonly DispatcherTimer timer = new();

		private void InvokePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
