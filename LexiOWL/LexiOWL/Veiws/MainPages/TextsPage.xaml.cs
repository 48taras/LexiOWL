﻿using LexiOWL.DAL.Repository;
using LexiOWL.Domain.Entities;
using LexiOWL.Veiws.MainPages.Lessons;
using LexiOWL.Veiws.MainPages.Tests;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LexiOWL.Veiws.MainPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextsPage : ContentPage
    {
        private readonly Repository<Topic> _repositoryTopic;

        public TextsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            _repositoryTopic = new Repository<Topic>();
            AddButtonsToStackLayout();
        }

        private async void AddButtonsToStackLayout()
        {
            var topics = await _repositoryTopic.GetAllAsync();
            if (topics != null)
            {
                foreach (var topic in topics)
                {
                    Button button = new Button
                    {
                        CornerRadius = 12,
                        BackgroundColor = Color.White,
                        BorderWidth = 1,
                        Padding = new Thickness(10),
                        Margin = new Thickness(10),
                        BorderColor = Color.Black,
                        HeightRequest = 70,
                        Text = topic.Name,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Center
                    };

                    button.Clicked += (s, e) =>
                    {                        
                        Navigation.PushAsync(new TestsTypePage(topic));
                    };

                    topicsStackLayout.Children.Add(button);
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
