using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterDetailPageNavigation.XAML
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SimpleModalPage : ContentPage
	{
		public SimpleModalPage()
		{
			InitializeComponent();
		}

		private void Button_OnClicked(object sender, EventArgs e)
		{
			Navigation.PopModalAsync();
		}
	}
}