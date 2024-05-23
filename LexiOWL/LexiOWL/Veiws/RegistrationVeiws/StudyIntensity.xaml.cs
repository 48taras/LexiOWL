using Android.App;
using Android.SE.Omapi;
using LexiOWL.DAL.Services;
using LexiOWL.Domain.Entities;
using LexiOWL.Managers;
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
	public partial class StudyIntensity : ContentPage
	{
        private ProfileServixe profileServixe;
		public Profile profile;
        string SelectedIntensityCategory;

        public StudyIntensity (Profile profile)
		{
			InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            this.profile = profile;
            profileServixe = new ProfileServixe ();

        }

        private async void FurtherButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (SelectedIntensityCategory != null)
                {
                    profile.TrainingIntensity = SelectedIntensityCategory.ToString();

                    await profileServixe.UpdateAsync(profile);

                    App.Current.MainPage = new Notification(profile);
                }
                else
                {
                    await DisplayAlert("Помилка", "Оберіть категорію!", "ОК");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");

                await DisplayAlert("Помилка", "Щось пішло не так!", "ОК");
            }

        }

        private async void OnButtonTapped(object sender, EventArgs e)
        {
            await FrameTappedAnimation.AnimateFramesWithOpacityAsync(new List<Frame> { Intensity1, Intensity2, Intensity3, Intensity4 }, (Frame)sender);

            if (sender == Intensity1)
            {
                SelectedIntensityCategory = "Спокійно (5 хвилин/день)";
            }
            else if (sender == Intensity2)
            {
                SelectedIntensityCategory = "Звичайно (10 хвилин/день)";
            }
            else if (sender == Intensity3)
            {
                SelectedIntensityCategory = "Ефективно (15 хвилин/день)";
            }
            else if (sender == Intensity4)
            {
                SelectedIntensityCategory = "Серйозно (20 хвилин/день)";
            }
        }

    }
}