using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Threading;

namespace MineSweeper
{
	public class MineSweeperModel : INotifyPropertyChanged
	{
		public MineSweeperModel() : this(10, 10, 10) { }
		public MineSweeperModel(int mines, int columns, int rows)
		{
			Columns = columns;
			Rows = rows;
			_minesToMark = mines;
			Mines = mines;
			_mineField = new List<List<Field>>();
			//initialize with empty
			for (int x = 0; x < Columns; ++x)
			{
				var col = new List<Field>();
				for (int y = 0; y < Rows; ++y)
				{
					var field = new Field();
					//local copy of variables for late lambda evaluation necessary
					var xLocal = x;
					var yLocal = y;
					field.PropertyChanged += (s, e) => FieldChanged(field, e.PropertyName ?? string.Empty, xLocal, yLocal);
					col.Add(field);
				}
				_mineField.Add(col);
			}
			var rand = new Random();
			//place mines
			for (int i = 0; i < MinesToMark;)
			{
				var x = rand.Next(0, Columns);
				var y = rand.Next(0, Rows);
				if (!_mineField[x][y].IsMine)
				{
					_mineField[x][y].IsMine = true;
					ForEachNeighbor(x, y, Increment);
					++i;
				}
			}

			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += (s, e) => ++SecondsPlayed;
			timer.Start();
		}

		public void OpenEmptyField()
		{
			foreach (var fields in MineField)
			{
				foreach (var field in fields)
				{
					if (0 == field.NeighborMines)
					{
						if (!field.IsOpen)
						{
							field.IsOpen = true;
							return;
						}
					}
				}
			}
		}

		public IEnumerable<IEnumerable<IField>> MineField => _mineField;

		public int Columns { get; }

		public int Rows { get; }

		public bool IsLost
		{
			get => _isLost; private set
			{
				_isLost = value;
				InvokePropertyChanged(nameof(IsLost));
			}
		}

		public bool IsWon
		{
			get => _isWon; private set
			{
				_isWon = value;
				InvokePropertyChanged(nameof(IsWon));
			}
		}

		public int MinesToMark
		{
			get => _minesToMark;
			set
			{
				_minesToMark = value;
				InvokePropertyChanged(nameof(MinesToMark));
			}
		}

		public int SecondsPlayed
		{
			get => _secondsPlayed;
			private set
			{
				_secondsPlayed = value;
				InvokePropertyChanged(nameof(SecondsPlayed));
			}
		}
		private readonly List<List<Field>> _mineField;
		private bool _isLost = false;
		private int _minesToMark;

		public int Mines { get; }

		private bool _isWon = false;
		private int _secondsPlayed = 0;
		private readonly DispatcherTimer timer = new();

		public event PropertyChangedEventHandler? PropertyChanged;

		private void FieldChanged(Field field, string propertyName, int x, int y)
		{
			if (IsLost) return;
			if(nameof(field.IsMarked) == propertyName)
			{
				MinesToMark += field.IsMarked ? -1 : 1;
			}
			else if (field.IsOpen)
			{
				if (field.IsMine)
				{
					//open mine field -> lost
					Lost();
					return;
				}
				else if (0 == field.NeighborMines)
				{
					void Open(int x_, int y_)
					{
						var f = _mineField[x_][y_];
						if (!f.IsOpen) f.IsOpen = true;
					}
					ForEachNeighbor(x, y, Open);
				}
			}
			CheckWin();
		}

		private void InvokePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void Lost()
		{
			IsLost = true;
			foreach (var column in _mineField)
			{
				foreach (var field in column)
				{
					field.IsOpen = true;
				}
			}
			timer.IsEnabled = false;
		}

		private void CheckWin()
		{
			int closedCount = 0;
			foreach (var column in _mineField)
			{
				foreach (var field in column)
				{
					if(!field.IsOpen)
					{
						closedCount++;
					}
				}
			}
			IsWon = closedCount == Mines;
			if(IsWon) timer.IsEnabled = false;
		}

		private void ForEachNeighbor(int x, int y, Action<int, int> action)
		{
			if (action == null) throw new ArgumentNullException(nameof(action));

			var left = 0 < x;
			if (left)
			{
				action(x - 1, y);
			}
			var right = x + 1 < Columns;
			if (right)
			{
				action(x + 1, y);
			}
			var bottom = 0 < y;
			if (bottom)
			{
				action(x, y - 1);
				if (left) action(x - 1, y - 1);
				if (right) action(x + 1, y - 1);
			}
			var top = y + 1 < Rows;
			if (top)
			{
				action(x, y + 1);
				if (left) action(x - 1, y + 1);
				if (right) action(x + 1, y + 1);
			}
		}

		private void Increment(int x, int y)
		{
			if (!_mineField[x][y].IsMine) ++_mineField[x][y].NeighborMines;
		}
	}
}
