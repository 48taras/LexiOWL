using LexiOWL.DAL.Repository;
using LexiOWL.Domain.Entities;
using LexiOWL.Managers;
using LexiOWL.Veiws.MainPages;
using LexiOWL.Veiws.RegistrationVeiws;
using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LexiOWL.Veiws.LoginVeiws
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly Repository<Customer> _customerRepository;

        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            _customerRepository = new Repository<Customer>();

        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            string email = emailEntry.Text?.Trim();
            string password = passwordEntry.Text?.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Помилка", "Будь ласка, заповніть всі поля!", "OK");
                return;
            }

            if (!ValidationUtils.IsValidEmail(email))
            {
                await DisplayAlert("Помилка", "Будь ласка, введіть коректну адресу електронної пошти!", "OK");
                return;
            }

            if (email.Length < 5)
            {
                await DisplayAlert("Помилка", "Логін повинен містити не менше 5 символів!", "OK");
                return;
            }

            if (password.Length < 5 || password.Length > 25)
            {
                await DisplayAlert("Помилка", "Пароль повинен містити від 5 до 25 символів!", "OK");
                return;
            }

            if (!ValidationUtils.IsValidPassword(password))
            {
                await DisplayAlert("Помилка", "Пароль може містити тільки літери та цифри!", "OK");
                return;
            }

            if (await LoginLogic(email,password))
            {
                App.Current.Properties["IsUserRegistered"] = true;

                await App.Current.SavePropertiesAsync();

                App.Current.MainPage = new NavigationPage(new TabbedMainPage());
            }
        }

        private void TapForgotPass_Tapped(object sender, EventArgs e)
        {

        }

        private async void TapGoogle_Tapped(object sender, EventArgs e)
        {
            await FrameTappedAnimation.AnimateFramesWithOpacityAsync(new[] { FaceBookButton, GoogleButton }, (Frame)sender);

        }

        private async void TapFaceBook_Tapped(object sender, EventArgs e)
        {
           await FrameTappedAnimation.AnimateFramesWithOpacityAsync(new[] { FaceBookButton, GoogleButton }, (Frame)sender);

        }


        private async Task<bool> LoginLogic(string email, string password)
        {
            try
            {                
                var customers =  await _customerRepository.GetAllAsync();

                foreach (var customer in customers)
                {
                    if (customer.Email == email || customer.Login == email && customer.Password == password)
                    {
                        App.Current.Properties["CustomerID"] = customer.Id;
                        await App.Current.SavePropertiesAsync();
                        return true;
                    }
                    else if (customer.Email == email || customer.Login == email && customer.Password != password)
                    {
                        await DisplayAlert("Помилка", "Невірний пароль!", "OK");
                        return false;
                    }
                    else
                    {
                        await DisplayAlert("Помилка", "Користувач не існує!", "OK");
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}