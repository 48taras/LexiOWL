using Android.Text;
using LexiOWL.DAL.Interfaces;
using LexiOWL.DAL.Repository;
using LexiOWL.DAL.Services;
using LexiOWL.Domain.Entities;
using LexiOWL.Domain.Enums;
using LexiOWL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace LexiOWL.Veiws.MainPages.Lessons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseQuestionPage : ContentPage
    {
        private Topic topic;
        private Test test;
        private TestService _testService; 

        public ChooseQuestionPage(Topic _topic)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            _testService = new TestService();
            topic = _topic;
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                test = await _testService.GetByTopicIdAndTypeAsync(topic.Id, QuestionType.Choice);

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

            var poss = test.Answers.ToList();

            var shuffledAnswers = test.Answers.OrderBy(x => random.Next()).ToList();

            //LabelAnswer1.Text = shuffledAnswers[0].AnswerText;
            //LabelAnswer2.Text = shuffledAnswers[1].AnswerText;
            //LabelAnswer3.Text = shuffledAnswers[2].AnswerText;
            //LabelAnswer4.Text = shuffledAnswers[3].AnswerText;
            
            LabelAnswer1.Text = "10";
            LabelAnswer2.Text = "12";
            LabelAnswer3.Text = "6";
            LabelAnswer4.Text = "15";
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
                }
                else
                {
                    selectedFrame.BackgroundColor = Color.FromHex("ff5232");
                    correctFrame.BackgroundColor = Color.LightGreen;
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


    }
}