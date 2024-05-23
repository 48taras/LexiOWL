using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LexiOWL.Veiws.MainPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextsPage : ContentPage
    {
        public TextsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);


        }
    }
}