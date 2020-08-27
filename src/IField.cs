using System.ComponentModel;

namespace MineSweeper
{
	public interface IField : INotifyPropertyChanged
	{
		bool IsMarked { get; set; }
		bool IsMine { get; }
		byte NeighborMines { get; }
		bool IsOpen { get; set; }
	}
}