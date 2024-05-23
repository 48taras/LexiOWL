using Foundation;
using LexiOWL.DAL.Interfaces;
using System.IO;

namespace LexiOWL.iOS.DbManager
{
    public class BaseContentTextUrl_iOS : IBaseUrl
    {
        public string Get()
        {
            return Path.Combine(NSBundle.MainBundle.BundlePath, "Lessons");
        }
    }
}