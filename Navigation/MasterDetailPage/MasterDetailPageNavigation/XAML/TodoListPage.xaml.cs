using System;
using MasterDetailPageNavigation.XAML;
using Xamarin.Forms;

namespace MasterDetailPageNavigation
{
	public partial class TodoListPage : ContentPage
	{
		public TodoListPage ()
		{
			InitializeComponent ();
		}

		private void Button_OnClicked(object sender, EventArgs e)
		{
			Navigation.PushModalAsync(new SimpleModalPage());
		}

		private void Button2_OnClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new SimpleHierarchicalPage());
		}
	}
}

