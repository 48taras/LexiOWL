using LexiOWL.DAL.Interfaces;
using LexiOWL.DAL.Repository;
using LexiOWL.DAL.Services;
using LexiOWL.Domain.Entities;
using LexiOWL.Domain.Enums;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LexiOWL.Veiws.MainPages.Lessons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpellingQuestionPage : ContentPage
    {
        private Topic topic;
        private Test test;
        private Repository<Test> _repositoryTest;

        public SpellingQuestionPage(Topic topic)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            this.topic = topic;
            _repositoryTest = new Repository<Test>();

            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                test = (await _repositoryTest.Get(item => item.TopicId == topic.Id
                    && item.IsTestCompleted == false
                    && item.QuestionType == QuestionType.Spelling)).FirstOrDefault();

                UrlWebViewSource urlSource = new UrlWebViewSource
                {
                    Url = System.IO.Path.Combine(DependencyService.Get<IBaseUrl>().Get(), test.UrlQuestionText)
                };

                QuestionWebView.Source = urlSource;
                QuestionWebView.Navigated += QuestionWebView_Navigated;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void EntrySpelling_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(entrySpelling.Text))
            {
                okButton.IsEnabled = false;
                okButton.TextColor = Color.Gray;
            }
            else
            {
                okButton.IsEnabled = true;
                okButton.TextColor = Color.FromHex("#946710");
            }
        }


        private void QuestionWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            QuestionWebView.HeightRequest = -1;
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void OnwardsButton_Clicked(object sender, EventArgs e)
        {
            if (okButton.Text == "Ок")
            {
                if (entrySpelling.Text == test.CorrectAnswer)
                {
                    var frame = new Frame
                    {
                        Content = new Label
                        {
                            Text = "ВІРНО!",
                            TextColor = Color.FromHex("#5e5e5e"),
                            FontSize = 14,
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center,
                            VerticalTextAlignment = Xamarin.Forms.TextAlignment.Center
                        },
                        CornerRadius = 2,
                        BackgroundColor = Color.FromHex("#66fa86"),
                        Margin = 10,
                        HasShadow = false,
                        Padding = 10
                    };

                    entrySpelling.IsEnabled = false;
                    massegeContainer.Children.Add(frame);
                }
                else
                {
                    var frame = new Frame
                    {
                        Content = new Label
                        {
                            Text = $"НЕ ВІРНО!\n Парильна відповідь:\n\n {test.CorrectAnswer}",
                            TextColor = Color.Black,
                            FontSize = 14,
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center,
                            VerticalTextAlignment = Xamarin.Forms.TextAlignment.Center
                        },
                        CornerRadius = 2,
                        BackgroundColor = Color.FromHex("#ff8178"),
                        Margin = 10,
                        HasShadow = false,
                        Padding = 10
                    };

                    entrySpelling.IsEnabled = false;
                    massegeContainer.Children.Add(frame);
                }
                okButton.Text = "Далі";

            }
            else if (okButton.Text == "Далі")
            {
                Navigation.PushAsync(new ResultPage());
            }
        }

        protected override bool OnBackButtonPressed() { return true; }
    }
}