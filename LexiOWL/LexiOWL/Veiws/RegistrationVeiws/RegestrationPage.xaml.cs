using LexiOWL.DAL.Repository;
using LexiOWL.DAL.Services;
using LexiOWL.Domain.Entities;
using LexiOWL.Managers;
using LexiOWL.Veiws.LoginVeiws;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LexiOWL.Veiws.RegistrationVeiws
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegestrationPage : ContentPage
    {
        private readonly Repository<Customer> _customerRepository;
        private readonly ProfileServixe _profileService;

        public RegestrationPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
                        
            _customerRepository = new Repository<Customer>();
            _profileService = new ProfileServixe();
        }

        private async void FurtherButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                string email = emailEntry.Text?.Trim();
                string login = loginEntry.Text?.Trim();
                string password = passwordEntry.Text?.Trim();

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Помилка", "Будь ласка, заповніть всі поля!", "OK");
                    return; 
                }

                if (!ValidationUtils.IsValidEmail(email))
                {
                    await DisplayAlert("Помилка", "Будь ласка, введіть коректну адресу електронної пошти!", "OK");
                    return; 
                }

                if (login.Length < 5)
                {
                    await DisplayAlert("Помилка", "Логін повинен містити не менше 5 символів!", "OK");
                    return;
                }
                
                if (!ValidationUtils.IsValidUsername(login))
                {
                    await DisplayAlert("Помилка", "Логін може містити тільки букви!", "OK");
                    return;
                }

                if (password.Length < 5 || password.Length > 50)
                {
                    await DisplayAlert("Помилка", "Пароль повинен містити від 5 до 50 символів!", "OK");
                    return;
                }
                
                if (!ValidationUtils.IsValidPassword(password))
                {
                    await DisplayAlert("Помилка", "Пароль може містити тільки літери та цифри!", "OK");
                    return;
                }

                var newUser = new Customer
                {
                    Login = login,
                    Email = email,
                    Password = password,
                };

                await _customerRepository.AddAsync(newUser);

                var newProfile = new Profile
                {
                    CustomerId = newUser.Id
                };

                await _profileService.AddAsync(newProfile);


                Navigation.PushAsync(new AgePage(newUser, newProfile));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Помилка", "Не вдалося зареєструвати користувача!", "OK");
            }
        }

        private void TapAvtorisation_Tapped(object sender, EventArgs e)
        {
            var loginPage = new LoginPage();

            Navigation.PushAsync(loginPage);
        }

        protected override bool OnBackButtonPressed() { return true; }
    }
}