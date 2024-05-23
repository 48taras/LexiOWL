using LexiOWL.DAL.Repository;
using LexiOWL.DAL.Services;
using LexiOWL.Domain.Entities;
using LexiOWL.Veiws.RegistrationVeiws;
using System;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.Provider.ContactsContract;
using Profile = LexiOWL.Domain.Entities.Profile;
using Application = Xamarin.Forms.Application;

namespace LexiOWL.Veiws.MainPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private ProfileServixe _profileServixe;
        private Repository<Customer> _customerRepository;

        private Customer _customer;
        private Profile _profile;

        public SettingsPage(Customer customer, Profile profile)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            _customer = customer;
            _profile = profile;
            _profileServixe = new ProfileServixe();
            _customerRepository = new Repository<Customer>();

            FillinPersonalData();
        }

        private async Task<bool> CheckAndRequestStoragePermissionAsync()
        {
            var storageStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (storageStatus != PermissionStatus.Granted)
            {
                
                var storageRequest = await Permissions.RequestAsync<Permissions.StorageRead>();

                if (storageRequest != PermissionStatus.Granted)
                {
                    await DisplayAlert("Ошибка", "Разрешение на чтение внешнего хранилища не было предоставлено", "OK");
                    return false;
                }
            }
            return true;
        }




        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void TapEditorPhotoButton_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (await CheckAndRequestStoragePermissionAsync())
                {
                    var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                    {
                        Title = "Оберіть фото:"
                    });

                    var stream =await photo.OpenReadAsync();

                    userImage.Source = ImageSource.FromStream(() => stream);

                    _profile.ProfilePhotoPath = userImage.Source.ToString();

                    await _profileServixe.UpdateAsync(_profile);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }

        private async void ExitButton_Clicked(object sender, EventArgs e)
        {
            App.Current.Properties["IsUserRegistered"] = false;
            await App.Current.SavePropertiesAsync();

            Application.Current.MainPage = new NavigationPage(new WelcomePage());
        }


        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                _customer.Email = emailEntry.Text.Trim();
                _customer.Login = loginEntry.Text.Trim();
                _customer.Password = passwordEntry.Text.Trim();

                _profile.Age = (string)pickerAge.SelectedItem;
                _profile.AmountOfKnowledge = (string)pickerKnowledge.SelectedItem;
                _profile.TrainingIntensity = (string)pickerIntensity.SelectedItem;

                await _customerRepository.UpdateAsync(_customer);
                await _profileServixe.UpdateAsync(_profile);

                Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private async void FillinPersonalData()
        {
            try
            {
                if (_customer != null && _profile != null)
                {
                    if (!string.IsNullOrEmpty(_profile.ProfilePhotoPath))
                    {
                        userImage.Source = ImageSource.FromFile(_profile.ProfilePhotoPath);
                    }
                    else
                    {
                        userImage.Source = "gray_circle.png";
                    }

                    loginEntry.Text = _customer.Login;
                    emailEntry.Text = _customer.Email;
                    passwordEntry.Text = _customer.Password;
                    pickerAge.SelectedItem = _profile.Age;
                    pickerIntensity.SelectedItem = _profile.TrainingIntensity;
                    pickerKnowledge.SelectedItem = _profile.AmountOfKnowledge;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}