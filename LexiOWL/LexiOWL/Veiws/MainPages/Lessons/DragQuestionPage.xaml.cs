using Android.Views;
using LexiOWL.DAL.Interfaces;
using LexiOWL.DAL.Services;
using LexiOWL.Domain.Entities;
using LexiOWL.Domain.Enums;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.Provider.UserDictionary;

namespace LexiOWL.Veiws.MainPages.Lessons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DragQuestionPage : ContentPage
    {
        private Topic topic;
        private Test test;
        private TestService _testService;

        public DragQuestionPage(Topic topic)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            this.topic = topic;
            _testService = new TestService();

            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                test = await _testService.GetByTopicIdAndTypeAsync(topic.Id, QuestionType.Drag);

                UrlWebViewSource urlSource = new UrlWebViewSource
                {
                    Url = System.IO.Path.Combine(DependencyService.Get<IBaseUrl>().Get(), test.UrlQuestionText)
                };

                QuestionWebView.Source = urlSource;
                QuestionWebView.Navigated += QuestionWebView_Navigated;

                var words = test.CorrectAnswer.Split(' ').ToList();
                var random = new Random();
                words = words.OrderBy(x => random.Next()).ToList();

                foreach (var word in words)
                {
                    var frame = CreateDraggableFrame(word);
                    SourceContainer.Children.Add(frame);
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

        private Frame CreateDraggableFrame(string word)
        {
            var frame = new Frame
            {
                Content = new Label { Text = word, HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center},
                CornerRadius = 10,
                BackgroundColor = Color.White,
                Margin = new Thickness(10, 8, 10, 0)
            };

            var dragGesture = new DragGestureRecognizer();
            dragGesture.DragStarting += (s, e) =>
            {
                e.Data.Properties.Add("Word", word);
            };

            frame.GestureRecognizers.Clear();
            frame.GestureRecognizers.Add(dragGesture);

            return frame;
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void OnwardsButton_Clicked(object sender, EventArgs e)
        {

            if (okButton.Text == "Ок")
            {
                var correctOrder = test.CorrectAnswer.Split(' ');
                var userOrder = TargetContainer.Children.Select(c => ((Label)((Frame)c).Content).Text).ToArray();

                if (correctOrder.SequenceEqual(userOrder))
                {
                    var frame = new Frame
                    {
                        Content = new Label { Text = "ВІРНО!", TextColor = Color.FromHex("#5e5e5e"), FontSize = 14, WidthRequest = 200 },
                        CornerRadius = 2,
                        BackgroundColor = Color.FromHex("#66fa86"),
                        Margin = 10,
                        HasShadow = false
                    };

                    massegeContainer.Children.Add(frame);
                }
                else
                {
                    var frame = new Frame
                    {
                        Content = new Label { Text = $"НЕ ВІРНО!\n Парильна відповідь:\n\n {test.CorrectAnswer}", TextColor = Color.Black, HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center, FontSize = 14 },
                        CornerRadius = 2,
                        BackgroundColor = Color.FromHex("#ff8178"),
                        Margin = 10,
                        HasShadow = false,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    };

                    massegeContainer.Children.Add(frame);
                }
                okButton.Text = "Далі";
            }
            else if (okButton.Text == "Далі")
            {
                Navigation.PushAsync(new SpellingQuestionPage(topic));
            }
        }

        private void DropGestureRecognizer_Drop(object sender, DropEventArgs e)
        {
            if (e.Data.Properties.ContainsKey("Word"))
            {
                var droppedWord = e.Data.Properties["Word"].ToString();
                var sourceFrame = SourceContainer.Children.FirstOrDefault(c => ((Label)((Frame)c).Content).Text == droppedWord);
                if (sourceFrame != null)
                {
                    SourceContainer.Children.Remove(sourceFrame);
                    TargetContainer.Children.Add(sourceFrame);

                    var tapGesture = new TapGestureRecognizer();
                    tapGesture.Tapped += (s, ev) =>
                    {
                        TargetContainer.Children.Remove(sourceFrame);
                        SourceContainer.Children.Add(sourceFrame);

                        var dragGesture = new DragGestureRecognizer();
                        dragGesture.DragStarting += (s, e) =>
                        {
                            e.Data.Properties.Add("Word", ((Label)((Frame)sourceFrame).Content).Text);
                        };

                        sourceFrame.GestureRecognizers.Clear();
                        sourceFrame.GestureRecognizers.Add(dragGesture);
                    };
                    sourceFrame.GestureRecognizers.Clear();
                    sourceFrame.GestureRecognizers.Add(tapGesture);

                    if (SourceContainer.Children.Count == 0)
                    {
                        okButton.IsEnabled = true;
                        okButton.TextColor = Color.FromHex("#946710");
                    }
                }
            }
        }
    }
}
