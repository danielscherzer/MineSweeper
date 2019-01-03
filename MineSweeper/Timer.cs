using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace MineSweeper
{
	class Timer : INotifyPropertyChanged
	{
		public Timer()
		{
			timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += (s, e) => ++Seconds;
			timer.Start();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public int Seconds
		{
			get => _seconds; private set
			{
				_seconds = value;
				InvokePropertyChanged(nameof(Seconds));
			}
		}

		private DispatcherTimer timer;
		private int _seconds;

		private void InvokePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
