using LexiOWL.DAL.Interfaces;
using LexiOWL.DAL.Repository;
using LexiOWL.DAL.Services;
using LexiOWL.Domain.Entities;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LexiOWL.Veiws.MainPages.Lessons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Lesson : ContentPage
    {
        private Repository<EducationalContent> _reposytopryEducational;
        private Repository<Statistics> _reposytopryStatistics;
        private Repository<Customer> _reposytopryCustomer;
        private Topic topic;
        private Customer customer;
        private EducationalContent educationalContent;
        int UserId;


        public Lesson(Topic _topic)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            topic = _topic;
            _reposytopryEducational = new Repository<EducationalContent>();
            _reposytopryStatistics = new Repository<Statistics>();
            _reposytopryCustomer = new Repository<Customer>();

            UserId = (int)App.Current.Properties["CustomerID"];

            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                customer = (await _reposytopryCustomer.Get(item => item.Id == UserId)).FirstOrDefault();

                educationalContent = (await _reposytopryEducational.Get(item => item.TopicId == topic.Id)).FirstOrDefault();
                
                var topicTitle = topic.Name;
                int dotIndex = topicTitle.IndexOf('.');
                
                topicFirstPart.Text = dotIndex >= 0 ? topicTitle.Substring(0, dotIndex + 1) : topicTitle;
                
                topicSecondPart.Text = dotIndex >= 0 ? topicTitle.Substring(dotIndex + 1) : string.Empty;
               
                VideoWebView.Source = new UrlWebViewSource { Url = educationalContent.UrlEducationalVideoContent };
                
                var urlSource = new UrlWebViewSource { Url = DependencyService.Get<IBaseUrl>().Get() + educationalContent.UrlEducationalTextContent };
                
                ContentWebView.Source = urlSource;
                ContentWebView.Navigated += ContentWebView_Navigated;

                if (educationalContent.IsTimerRunning)
                {
                    StartTimer();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            StartTimer();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            StopTimer();
        }

        private void ContentWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            ContentWebView.HeightRequest = -1;
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            SaveProgress();
            Navigation.PopAsync();
        }

        private void OnwardsButton_Clicked(object sender, EventArgs e)
        {
            SaveProgress();
            Navigation.PushAsync(new ChooseQuestionPage(topic));
        }

        private async void SaveProgress()
        {
            StopTimer();
            
            educationalContent.ContentViewedAt = DateTime.Now;
                        
            if (educationalContent.TimeSpent >= TimeSpan.FromMinutes(5))
            {
                educationalContent.IsContentCompleted = true;
                UpdateStatistics();
            }

            await _reposytopryEducational.UpdateAsync(educationalContent);
        }

        private void StartTimer()
        {
            educationalContent.IsTimerRunning = true;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (!educationalContent.IsTimerRunning)
                    return false;

                educationalContent.TimeSpent += TimeSpan.FromSeconds(1);
                return true;
            });
        }

        private void StopTimer()
        {
            educationalContent.IsTimerRunning = false;
        }
        
        private async void UpdateStatistics()
        {
            
            var statistics = (await _reposytopryStatistics.Get(item => item.CustomerId == customer.Id)).FirstOrDefault();
            statistics.Contents.Add(educationalContent);
            await _reposytopryStatistics.UpdateAsync(statistics);
        }

        protected override bool OnBackButtonPressed() { return true; }
    }
}
