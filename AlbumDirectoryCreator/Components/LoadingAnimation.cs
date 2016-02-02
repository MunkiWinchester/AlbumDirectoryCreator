using ControlAnimation;
using Logging;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlbumDirectoryCreator.Components
{
    public static class LoadingAnimation
    {
        private static readonly Logger Logger = new Logger();

        public static void Start(Control targetControl)
        {
            try
            {
                ControlAnimator.StartAnimating(targetControl, ControlAnimator.DrawMode.Lines, Color.Red,
                    false);
            }
            catch (Exception ex)
            {
                Logger.Error("Control Animation Error", ex);
            }
        }

        public static void End(Control targetControl)
        {
            try
            {
                ControlAnimator.StopAnimating(targetControl);
            }
            catch (Exception ex)
            {
                Logger.Error("Control Animation Error", ex);
            }
        }
    }
}