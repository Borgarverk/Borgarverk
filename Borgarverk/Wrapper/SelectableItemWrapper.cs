using System;
namespace Borgarverk
{
	public class SelectableItemWrapper<T>
	{
		public bool IsSelected { get; set; }
		public T Item { get; set; }

		public SelectableItemWrapper(T model)
		{
			this.IsSelected = false;
			this.Item = model;
		}
	}
}
