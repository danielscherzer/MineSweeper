using System;

namespace MineSweeper
{
	public class MineGrid
	{
		public MineGrid(int columns, int rows, int mines)
		{
			Columns = columns;
			Rows = rows;
			Mines = mines;
			cells = new byte[Columns, Rows];
			var rand = new Random();
			//place mines
			for(int i = 0; i < Mines;)
			{
				var x = rand.Next(0, Columns);
				var y = rand.Next(0, Rows);
				if(0 == cells[x,y])
				{
					cells[x, y] = Mine;
					ForEachNeighbor(x, y, Increment);
					++i;
				}
			}
		}

		public int Columns { get; }
		public int Rows { get; }
		public int Mines { get; }

		public byte HowManyMines(int x, int y) => cells[x, y];

		private const byte Mine = 255;
		private readonly byte[,] cells;

		public void ForEachNeighbor(int x, int y, Action<int, int> action)
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
			if (Mine != cells[x, y]) ++cells[x, y];
		}
	}
}
