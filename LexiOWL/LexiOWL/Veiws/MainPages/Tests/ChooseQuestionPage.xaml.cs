using LexiOWL.DAL.Interfaces;
using LexiOWL.DAL.Repository;
using LexiOWL.Domain.Entities;
using LexiOWL.Domain.Enums;
using LexiOWL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LexiOWL.Veiws.MainPages.Lessons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseQuestionPage : ContentPage
    {
        private Topic topic;
        private Test test;
        private Customer customer;
        private Statistics statistics;


        private Repository<Test> _repositorytest;
        private Repository<Customer> _repositoryCustomer;
        private Repository<Statistics> _repositoryStatistics;

        public ChooseQuestionPage(Topic _topic)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            _repositorytest = new Repository<Test>();
            _repositoryCustomer = new Repository<Customer>();
            _repositoryStatistics = new Repository<Statistics>();

            topic = _topic;

            var UserId = (int)App.Current.Properties["CustomerID"];

            SearchCustomer(UserId);

            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                test = (await _repositorytest.Get(item => item.TopicId == topic.Id 
                    && item.IsTestCompleted == false 
                    && item.QuestionType == QuestionType.Choice)).FirstOrDefault();

                UrlWebViewSource urlSource = new UrlWebViewSource
                {
                    Url = System.IO.Path.Combine(DependencyService.Get<IBaseUrl>().Get(), test.UrlQuestionText)
                };

                QuestionWebView.Source = urlSource;
                QuestionWebView.Navigated += QuestionWebView_Navigated;

                FillAnswers();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void FillAnswers()
        {
            var random = new Random();
            var shuffledAnswers = test.Answers.OrderBy(x => random.Next()).ToList();

            LabelAnswer1.Text = shuffledAnswers[0].AnswerText;
            LabelAnswer2.Text = shuffledAnswers[1].AnswerText;
            LabelAnswer3.Text = shuffledAnswers[2].AnswerText;
            LabelAnswer4.Text = shuffledAnswers[3].AnswerText;
        }

        private async void OnAnswerTapped(object sender, EventArgs e)
        {
            try
            {
                var selectedFrame = (Frame)sender;

                await FrameTappedAnimation.AnimateFrameTapOnly((selectedFrame));

                var selectedLabel = (Label)selectedFrame.Content;
                string selectedAnswer = selectedLabel.Text;

                Frame correctFrame = null;
                if (LabelAnswer1.Text == test.CorrectAnswer) correctFrame = Answer1;
                if (LabelAnswer2.Text == test.CorrectAnswer) correctFrame = Answer2;
                if (LabelAnswer3.Text == test.CorrectAnswer) correctFrame = Answer3;
                if (LabelAnswer4.Text == test.CorrectAnswer) correctFrame = Answer4;

                if (selectedAnswer == test.CorrectAnswer)
                {
                    selectedFrame.BackgroundColor = Color.LightGreen;
                    test.IsTestCorrect = true;
                    test.IsTestCompleted = true;
                }
                else
                {
                    selectedFrame.BackgroundColor = Color.FromHex("ff5232");
                    correctFrame.BackgroundColor = Color.LightGreen;
                    test.IsTestCompleted = true;
                    test.IsTestCorrect = false;
                }

                Answer1.IsEnabled = false;
                Answer2.IsEnabled = false;
                Answer3.IsEnabled = false;
                Answer4.IsEnabled = false;

                nextButton.TextColor = Color.FromHex("#946710");
                nextButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task SearchCustomer(int userId)
        {
            try
            {
                if (_repositoryCustomer != null)
                {
                    customer = (await _repositoryCustomer.Get(user => user.Id == userId)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
            Navigation.PushAsync(new DragQuestionPage(topic));
        }

        protected override bool OnBackButtonPressed() { return true; }
    }
}