using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MineSweeper
{
	public class MineFieldViewModel : INotifyPropertyChanged
	{
		public MineFieldViewModel() : this(10, 10, 10) { }
		public MineFieldViewModel(int mines, int columns, int rows)
		{
			Columns = columns;
			Rows = rows;
			_minesToMark = mines;
			Mines = mines;
			_mineField = new List<List<Cell>>();
			//initialize with empty
			for (int x = 0; x < Columns; ++x)
			{
				List<Cell> col = new();
				for (int y = 0; y < Rows; ++y)
				{
					Cell cell = new();
					//local copy of variables for late lambda evaluation necessary
					int xLocal = x;
					int yLocal = y;
					cell.PropertyChanged += (s, e) => CellChanged(cell, e.PropertyName ?? string.Empty, xLocal, yLocal);
					col.Add(cell);
				}
				_mineField.Add(col);
			}
			Random rand = new();
			//place mines
			for (int i = 0; i < mines;)
			{
				int x = rand.Next(0, Columns);
				int y = rand.Next(0, Rows);
				if (!_mineField[x][y].IsMine)
				{
					_mineField[x][y].IsMine = true;
					ForEachNeighbor(x, y, Increment);
					++i;
				}
			}
		}

		public IEnumerable<IEnumerable<ICell>> MineField => _mineField;

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
			private set
			{
				_minesToMark = value;
				InvokePropertyChanged(nameof(MinesToMark));
			}
		}

		public int Mines { get; }

		public event PropertyChangedEventHandler? PropertyChanged;

		private readonly List<List<Cell>> _mineField;
		private bool _isLost;
		private int _minesToMark;
		private bool _isWon;

		private void CellChanged(Cell cell, string propertyName, int x, int y)
		{
			if (IsLost || IsWon) return;
			if (cell.IsOpen)
			{
				if (cell.IsMine)
				{
					//open mine cell -> lost
					IsLost = true;
					OpenAllCells();
					_mineField[x][y].IsWrongCell = true;
					return;
				}
				else if (0 == cell.NeighborMines)
				{
					void Open(int x_, int y_)
					{
						var f = _mineField[x_][y_];
						if (!f.IsOpen) f.IsOpen = true;
					}
					ForEachNeighbor(x, y, Open);
				}
			}
			UpdateState();
		}

		private void InvokePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void OpenAllCells()
		{
			foreach (var column in _mineField)
			{
				foreach (var cell in column)
				{
					cell.IsOpen = true;
				}
			}
		}

		private void UpdateState()
		{
			int closedCount = 0;
			int markedCount = 0;
			foreach (var column in _mineField)
			{
				foreach (var cell in column)
				{
					if (!cell.IsOpen)
					{
						closedCount++;
					}
					if(cell.IsMarked)
					{
						markedCount++;
					}
				}
			}
			IsWon = closedCount == Mines;
			if(IsWon) OpenAllCells();
			MinesToMark = Mines - markedCount;
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

