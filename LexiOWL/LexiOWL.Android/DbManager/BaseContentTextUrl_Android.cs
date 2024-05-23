using LexiOWL.DAL.Interfaces;
using LexiOWL.Droid.DbManager;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseContentTextUrl_Android))]
namespace LexiOWL.Droid.DbManager
{
    public class BaseContentTextUrl_Android : IBaseUrl
    {
        public string Get()
        {
            return "file:///android_asset/Lessons/";
        }
    }
}