using LexiOWL.DAL.Services;
using LexiOWL.Domain.Entities;
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
	public partial class Notification : ContentPage
	{
        private readonly ProfileServixe _profileService;
        public Profile profile;
		public bool IsNotification;

		public Notification (Profile profile)
		{
			InitializeComponent ();

            this.profile = profile;
            _profileService = new ProfileServixe ();
		}

        private async void FurtherButton_Clicked(object sender, EventArgs e)
        {
			try
			{
                bool result = await DisplayAlert("Сповіщення", "Увімкнути сповіщення?", "Так", "Ні");
                profile.Notification = result;

                await _profileService.UpdateAsync(profile);

                App.Current.MainPage = new DonePage(profile);
            }
			catch (Exception ex)
			{
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                profile.Notification = false;

                await _profileService.UpdateAsync(profile);

                App.Current.MainPage = new DonePage(profile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }


        protected override bool OnBackButtonPressed() { return true; }
    }
}