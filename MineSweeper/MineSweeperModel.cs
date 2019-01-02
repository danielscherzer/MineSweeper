using System;
using System.Collections.Generic;

namespace MineSweeper
{
	public class MineSweeperModel
	{
		public MineSweeperModel()
		{
			const int mines = 10;
			_mineField = new List<List<Field>>();
			//initialize with empty
			for (int x = 0; x < Columns; ++x)
			{
				var col = new List<Field>();
				for(int y = 0; y < Rows; ++y)
				{
					var field = new Field();
					//local copy of variables for late lambda evaluation necessary
					var xLocal = x;
					var yLocal = y;
					field.PropertyChanged += (s, e) => FieldChanged(field, xLocal, yLocal);
					col.Add(field);
				}
				_mineField.Add(col);
			}
			var rand = new Random();
			//place mines
			for (int i = 0; i < mines;)
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
		}

		public IEnumerable<IEnumerable<IField>> MineField => _mineField;

		public int Columns => 10;
		public int Rows => 10;

		public bool IsLost { get; private set; } = false;

		private readonly List<List<Field>> _mineField;

		private void FieldChanged(Field field, int x, int y)
		{
			if (IsLost) return;
			if (field.IsOpen)
			{
				if (field.IsMine)
				{
					//open mine field -> lost
					IsLost = true;
					Lost();
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
		}

		private void Lost()
		{
			foreach(var column in _mineField)
			{
				foreach(var field in column)
				{
					field.IsOpen = true;
				}
			}
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
