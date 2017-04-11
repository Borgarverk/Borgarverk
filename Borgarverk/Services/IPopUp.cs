using System;
namespace Borgarverk
{
	public interface IPopUp
	{
		void ShowToast(string message);
		void ShowSnackbar(string message);
	}
}
