using LexiOWL.Veiws.LoginVeiws;
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
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void WelcomeButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegestrationPage());
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var loginPage = new LoginPage();

            Navigation.PushAsync(loginPage);
        }


        protected override bool OnBackButtonPressed() { return true; }
    }
}