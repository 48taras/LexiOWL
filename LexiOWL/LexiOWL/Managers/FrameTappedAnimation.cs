using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LexiOWL.Managers
{
    public static class FrameTappedAnimation
    {
        public async static Task AnimateFramesWithOpacityAsync(IEnumerable<Frame> frames, Frame selectedFrame)
        {
            await AnimateFrameTap(selectedFrame);

            ResetFrameStyles(frames);
            SetSelectedFrameStyle(selectedFrame);
        }

        private async static Task AnimateFrameTap(Frame frame)
        {
            await frame.ScaleTo(0.9, 100, Easing.Linear);
            await frame.ScaleTo(1, 100, Easing.Linear);
        }

        public async static Task AnimateFrameTapOnly(Frame frame)
        {
            await frame.ScaleTo(0.9, 100, Easing.Linear);
            await frame.ScaleTo(1, 100, Easing.Linear);
        }

        private static void  ResetFrameStyles(IEnumerable<Frame> frames)
        {
            foreach (var frame in frames)
            {
                frame.Opacity = 1;
            }
        }

        private static void SetSelectedFrameStyle(Frame selectedFrame)
        {
            selectedFrame.Opacity = 0.7;
        }
    }
}
