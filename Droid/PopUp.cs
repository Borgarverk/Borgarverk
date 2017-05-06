using System;
using Borgarverk.Droid;
using Xamarin.Forms;
using Android.Support.Design.Widget;
using Plugin.CurrentActivity;
using Android.App;
using Android.Widget;

[assembly: Dependency(typeof(Borgarverk.Droid.PopUp))]

namespace Borgarverk.Droid
{
	public class PopUp : IPopUp
	{
		public void ShowSnackbar(string message)
		{
			Activity activity = CrossCurrentActivity.Current.Activity;
			Android.Views.View activityRootView = activity.FindViewById(Android.Resource.Id.Content);
			Snackbar.Make(activityRootView, message, Snackbar.LengthLong).Show();
		}

		public void ShowToast(string message)
		{
			Activity activity = CrossCurrentActivity.Current.Activity;
			Toast.MakeText(Forms.Context, message, ToastLength.Long).Show();
		}
	}
}
