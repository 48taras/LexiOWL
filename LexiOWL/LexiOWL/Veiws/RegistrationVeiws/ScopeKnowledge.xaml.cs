using LexiOWL.DAL.Services;
using LexiOWL.Domain.Entities;
using LexiOWL.Managers;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LexiOWL.Veiws.RegistrationVeiws
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScopeKnowledge : ContentPage
    {
        private readonly ProfileServixe _profileService;
        public Profile profile;
        string SelectedKnowledgeCategory;

        public ScopeKnowledge(Profile profile)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            _profileService = new ProfileServixe();
            this.profile = profile;
        }

        private async void OnCategoryTapped(object sender, EventArgs e)
        {
            await FrameTappedAnimation.AnimateFramesWithOpacityAsync(new List<Frame> { Knowledge1, Knowledge2, Knowledge3, Knowledge4, Knowledge5 }, (Frame)sender);

            if (sender == Knowledge1)
            {
                SelectedKnowledgeCategory = LabelKnowledge1.Text;
            }
            else if (sender == Knowledge2)
            {
                SelectedKnowledgeCategory = LabelKnowledge2.Text;
            }
            else if (sender == Knowledge3)
            {
                SelectedKnowledgeCategory = LabelKnowledge3.Text;
            }
            else if (sender == Knowledge4)
            {
                SelectedKnowledgeCategory = LabelKnowledge4.Text;
            }
            else if (sender == Knowledge5)
            {
                SelectedKnowledgeCategory = LabelKnowledge5.Text;
            }
        }

        private async void OnNextButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (SelectedKnowledgeCategory != null)
                {
                    profile.AmountOfKnowledge = SelectedKnowledgeCategory.ToString();

                    await _profileService.UpdateAsync(profile);

                    Navigation.PushAsync(new StudyIntensity(profile));
                }
                else
                {
                    await DisplayAlert("Помилка", "Оберіть категорію!", "ОК");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }


    }
}