using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexiOWL.Common
{
    public class FrameTappedAnimation
    {
        public async Task AnimateFramesAsync(IEnumerable<Frame> frames, Frame selectedFrame)
        {
            await AnimateFrameTap(selectedFrame);

            ResetFrameStyles(frames);
            SetSelectedFrameStyle(selectedFrame);
        }

        private async Task AnimateFrameTap(Frame frame)
        {
            await frame.ScaleTo(0.9, 100, Easing.Linear);
            await frame.ScaleTo(1, 100, Easing.Linear);
        }

        private void ResetFrameStyles(IEnumerable<Frame> frames)
        {
            foreach (var frame in frames)
            {
                frame.Opacity = 1;
            }
        }

        private void SetSelectedFrameStyle(Frame selectedFrame)
        {
            selectedFrame.Opacity = 0.7;
        }
    }
}
