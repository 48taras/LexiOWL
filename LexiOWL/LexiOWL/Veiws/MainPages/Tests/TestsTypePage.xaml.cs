using LexiOWL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace LexiOWL.Veiws.MainPages.Tests
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestsTypePage : Xamarin.Forms.TabbedPage
    {
        public TestsTypePage(Topic topic)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            var choosePage = new ListChooseQuestionsPage(topic)
            {
                Title = "Вибірка"
            };
            var dragPage = new ListDragQuastionsPage(topic)
            {
                Title = "Перетягування"
            };
            var spellingPage = new ListSpellingQuestionsPage(topic)
            {
                Title = "Правопис"
            };

            Children.Add(dragPage);
            Children.Add(choosePage);
            Children.Add(spellingPage);

            CurrentPage = Children[1];

            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Top);
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false);

        }
    }
}
