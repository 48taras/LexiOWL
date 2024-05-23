 using LexiOWL.DAL.Services;
using LexiOWL.DAL.Repository;
using LexiOWL.Domain.Entities;
using LexiOWL.Managers;
using Xamarin.Essentials;
using LexiOWL.Veiws.LoginVeiws;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LexiOWL.Veiws.MainPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private Repository<Customer> _customerRepository;
        private ProfileServixe _profileServixe;

        public Customer customer;
        public Profile profile;

        public ProfilePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            _customerRepository = new Repository<Customer>();
            _profileServixe = new ProfileServixe();

            var UserId = (int)App.Current.Properties["CustomerID"];

            SearchCustomer(UserId);

            FillinPersonalData();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            FillinPersonalData();
        }


        private async Task SearchCustomer(int userId)
        {
            try
            {
                if (_customerRepository != null)
                {
                    customer = await _customerRepository.Get(user => user.Id == userId);
                    if (customer != null)
                    {
                        profile = await _profileServixe.GetByCustomerIdAsync(customer.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private async void FillinPersonalData()
        {
            try
            {
                if (customer != null && profile != null)
                {
                    if (!string.IsNullOrEmpty(profile.ProfilePhotoPath))
                    {
                        userImage.Source = ImageSource.FromFile(profile.ProfilePhotoPath);
                    }
                    else
                    {
                        userImage.Source = "gray_circle.png";
                    }

                    labelUsername.Text = customer.Login;
                    labelUserEmail.Text = customer.Email;
                    lableAgeCategory.Text = profile.Age;
                    lableIntensity.Text = profile.TrainingIntensity;
                    lableKnowledge.Text = profile.AmountOfKnowledge;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private async void TapSettings_Tapped(object sender, EventArgs e)
        {
            await FrameTappedAnimation.AnimateFrameTapOnly((Frame)sender);

            Navigation.PushAsync(new SettingsPage(customer, profile));
            
        }

    }
}