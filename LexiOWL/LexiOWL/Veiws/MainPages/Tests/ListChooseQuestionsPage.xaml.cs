using LexiOWL.DAL.Repository;
using LexiOWL.Domain.Entities;
using LexiOWL.Domain.Enums;
using LexiOWL.Veiws.MainPages.Lessons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LexiOWL.Veiws.MainPages.Tests
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListChooseQuestionsPage : ContentPage
    {
        private readonly Repository<Test> _repositoryTest;
        private Topic _topic;

        public ListChooseQuestionsPage(Topic topic)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            _topic = topic;

            _repositoryTest = new Repository<Test>();
            AddButtonsToStackLayout();
        }

        private async void AddButtonsToStackLayout()
        {
            var tests = await _repositoryTest.Get(item => item.TopicId == _topic.Id 
                && item.QuestionType == QuestionType.Choice 
                && item.IsTestCompleted == false);

            if (tests != null)
            {
                foreach (var test in tests)
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
                        Text = test.Name,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Center
                    };

                    button.Clicked += (s, e) =>
                    {
                        Navigation.PushAsync(new ChooseQuestionPage(_topic));
                    };

                    testsStackLayout.Children.Add(button);
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TabbedMainPage());
        }
    }
}