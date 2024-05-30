using LexiOWL.Domain.Entities;
using LexiOWL.Veiws.MainPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LexiOWL.Veiws.RegistrationVeiws
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DonePage : ContentPage
	{
        public Profile profile;

        public DonePage (Profile _profile)
		{
			InitializeComponent();

            profile = _profile;
		}

        private async void DoneButton_Clicked(object sender, EventArgs e)
        {
            App.Current.Properties["IsUserRegistered"] = true;

            App.Current.Properties["CustomerID"] = profile.CustomerId;

            await App.Current.SavePropertiesAsync();

            App.Current.MainPage = new NavigationPage(new TabbedMainPage());
        }


        protected override bool OnBackButtonPressed() { return true; }
    }
}