using Android.Preferences;
using LexiOWL.DAL;
using LexiOWL.DAL.Interfaces;
using LexiOWL.DAL.Repository;
using LexiOWL.Domain.Entities;
using LexiOWL.Domain.Enums;
using LexiOWL.Veiws.MainPages;
using LexiOWL.Veiws.RegistrationVeiws;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using static Android.Resource;

namespace LexiOWL
{
    public partial class App : Application
    {
        public const string DBFILENAME = "lexiowl.db";
        private const string IsUserRegisteredKey = "IsUserRegistered";

        private bool IsUserRegistered
        {
            get => Application.Current.Properties.ContainsKey(IsUserRegisteredKey) && (bool)Application.Current.Properties[IsUserRegisteredKey];
            set => Application.Current.Properties[IsUserRegisteredKey] = value;
        }

        public App()
        {
            InitializeComponent();

            string dbPath = DependencyService.Get<IDbPath>().GetDatabasePath(DBFILENAME);

            using (var db = new LexiOwlDbContext(dbPath))
            {
                db.Database.EnsureCreated();
                if (db.Customers.Count() == 0)
                {
                    try
                    {
                        var customer = new Customer { Login = "Tарас", Email = "48207taras48207@gmail.com", Password = "taras" };
                        db.Customers.Add(customer);
                        db.SaveChanges();

                        var profile = new Profile { Age = "18 - 24", AmountOfKnowledge = "😁 Знаю дуже добре", TrainingIntensity = "Ефективно 15 хвилин/день", ProfilePhotoPath = "", CustomerId = customer.Id };
                        db.Profiles.Add(profile);
                        db.SaveChanges();

                        Topic topic = new Topic
                        {
                            Name = "Алфавіт. Відмінності звуків",
                        };

                        db.Topics.Add(topic);
                        db.SaveChanges();

                        EducationalContent educationalContent = new EducationalContent
                        {
                            UrlEducationalVideoContent = "https://www.youtube.com/embed/QyD8e94n0SA?si=b_SC873ngqx4WqsR",
                            UrlEducationalTextContent = "Alphabet/Alphabet.html",
                            TopicId = topic.Id
                        };

                        db.EducationalContents.Add(educationalContent);

                        customer.Profile = profile;
                        topic.EducationalContents.Add(educationalContent);
                        db.SaveChanges();

                        var testChoice = new Test
                        {
                            UrlQuestionText = "Alphabet/AlphabetQuestion1.html",
                            TopicId = topic.Id,
                            QuestionType = QuestionType.Choice,
                            CorrectAnswer = "10"
                        };

                        testChoice.Answers.Add(new TestAnswer { AnswerText = "10", TestId = testChoice.Id });
                        testChoice.Answers.Add(new TestAnswer { AnswerText = "12", TestId = testChoice.Id });
                        testChoice.Answers.Add(new TestAnswer { AnswerText = "8", TestId = testChoice.Id });
                        testChoice.Answers.Add(new TestAnswer { AnswerText = "4", TestId = testChoice.Id });

                        db.TestAnswers.AddRange(testChoice.Answers);
                        db.Tests.Add(testChoice);
                        db.SaveChanges();

                        var testDrag = new Test
                        {
                            UrlQuestionText = "Alphabet/AlphabetQuestion2.html",
                            TopicId = topic.Id,
                            QuestionType = QuestionType.Drag,
                            CorrectAnswer = "А Б В Г Ґ Д Е Є Ж З"
                        };

                        db.Tests.Add(testDrag);
                        db.SaveChanges();

                        var testSpelling = new Test
                        {
                            UrlQuestionText = "Alphabet/AlphabetQuestion3.html",
                            TopicId = topic.Id,
                            QuestionType = QuestionType.Spelling,
                            CorrectAnswer = "А Б В Г Ґ Д Е Є Ж З И І Ї Й К Л М Н О П Р С Т У Ф Х Ц Ч Ш Щ Ь Ю Я"
                        };

                        db.Tests.Add(testSpelling);
                        db.SaveChanges();
                    }
                    catch (DbUpdateException ex)
                    {
                        var innerException = ex.InnerException;
                        while (innerException != null)
                        {
                            Console.WriteLine(innerException.Message);
                            innerException = innerException.InnerException;
                        }
                    }

                }
            }
        }

        protected override void OnStart()
        {
            if (CheckUserAuthentication())
            {
                MainPage = new NavigationPage(new TabbedMainPage());
            }
            else
            {
                MainPage = new NavigationPage(new WelcomePage());
            }
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }

        private bool CheckUserAuthentication()
        {
            return IsUserRegistered;
        }

    }
}
