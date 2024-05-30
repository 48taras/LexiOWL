
using Android.Content.Res;
using Java.Lang.Annotation;
using LexiOWL.DAL.Interfaces;
using LexiOWL.DAL.Repository;
using LexiOWL.Domain.Entities;
using LexiOWL.Veiws.MainPages.Lessons;
using System;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using Button = Xamarin.Forms.Button;

namespace LexiOWL.Veiws.MainPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private readonly Repository<Topic> _repositoryTopic;

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            _repositoryTopic = new Repository<Topic>();
            AddFramesToStackLayout();
        }

        private async void AddFramesToStackLayout()
        {
            var topics = await _repositoryTopic.GetAllAsync();
            if (topics != null)
            {
                int column = 0; 

                foreach (var topic in topics)
                {
                    Button button = new Button
                    {
                        CornerRadius = 20,
                        BackgroundColor = Color.White,
                        BorderWidth = 1,
                        Padding = 1,
                        Margin = 16,
                        BorderColor = Color.Black,
                        HeightRequest = 160,
                        WidthRequest = 160,
                        Text = topic.Name,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    };

                    button.Clicked += (s, e) =>
                    {
                        Navigation.PushAsync(new Lesson(topic));
                    };

                    topicButtons.Children.Add(button, column, topicButtons.Children.Count / 2); // Добавляем в текущий столбец

                    column = (column + 1) % 2;
                }
            }
        }

        protected override bool OnBackButtonPressed() { return true; }

    }
}