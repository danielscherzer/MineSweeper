using System.ComponentModel;

namespace MineSweeper
{
	public class Field : INotifyPropertyChanged
	{
		private bool open;

		public event PropertyChangedEventHandler PropertyChanged;

		public bool Open
		{
			get => open;
			set
			{
				open = value;
				InvokePropertyChanged(nameof(Open));
			}
		}

		private void InvokePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
