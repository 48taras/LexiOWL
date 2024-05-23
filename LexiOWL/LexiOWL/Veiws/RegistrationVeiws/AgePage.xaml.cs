using Android.Bluetooth;
using Android.SE.Omapi;
using LexiOWL.DAL.Repository;
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
    public partial class AgePage : ContentPage
    {
        private readonly ProfileServixe _profileService;
        string SelectedAgeCategory;
        public Customer customer;
        public Profile profile;

        public AgePage(Customer customer, Profile profile)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            _profileService = new ProfileServixe();
            this.customer = customer;
            this.profile = profile;
        }

        private async void OnNextButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (SelectedAgeCategory != null)
                {
                    if (profile != null)
                    {
                        profile.Age = SelectedAgeCategory.ToString();

                        await _profileService.UpdateAsync(profile);

                        Navigation.PushAsync(new ScopeKnowledge(profile));
                    }
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

        private async void OnCategoryTapped(object sender, EventArgs e)
        {
            await FrameTappedAnimation.AnimateFramesWithOpacityAsync(new List<Frame> { AgeCategory1, AgeCategory2, AgeCategory3, AgeCategory4, AgeCategory5 }, (Frame)sender);

            if (sender == AgeCategory1)
            {
                SelectedAgeCategory = LabelAgeCategory1.Text;
            }
            else if (sender == AgeCategory2)
            {
                SelectedAgeCategory = LabelAgeCategory2.Text;
            }
            else if (sender == AgeCategory3)
            {
                SelectedAgeCategory = LabelAgeCategory3.Text;
            }
            else if (sender == AgeCategory4)
            {
                SelectedAgeCategory = LabelAgeCategory4.Text;
            }
            else if (sender == AgeCategory5)
            {
                SelectedAgeCategory = LabelAgeCategory5.Text;
            }
        }
    }
}