using LexiOWL.DAL.Interfaces;
using LexiOWL.DAL.Services;
using LexiOWL.Domain.Entities;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LexiOWL.Veiws.MainPages.Lessons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Lesson : ContentPage
    {
        private EducationalContentService _educationalContentService;
        private Topic topic;
        private EducationalContent educationalContent;

        public Lesson(Topic _topic)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            topic = _topic;
            _educationalContentService = new EducationalContentService();

            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                educationalContent = await _educationalContentService.GetByTopicIdAsync(topic.Id);
                
                var topicTitle = topic.Name;
                int dotIndex = topicTitle.IndexOf('.');
                
                topicFirstPart.Text = dotIndex >= 0 ? topicTitle.Substring(0, dotIndex + 1) : topicTitle;
                
                topicSecondPart.Text = dotIndex >= 0 ? topicTitle.Substring(dotIndex + 1) : string.Empty;
               
                VideoWebView.Source = new UrlWebViewSource { Url = educationalContent.UrlEducationalVideoContent };
                
                var urlSource = new UrlWebViewSource { Url = DependencyService.Get<IBaseUrl>().Get() + educationalContent.UrlEducationalTextContent };
                
                ContentWebView.Source = urlSource;
                ContentWebView.Navigated += ContentWebView_Navigated;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ContentWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            ContentWebView.HeightRequest = -1;
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void OnwardsButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChooseQuestionPage(topic));
        }
    }
}
