using Gecko;
using System;
using System.Windows.Forms;

namespace MUIPRT
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Xpcom.Initialize("Firefox");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
            Xpcom.Initialize("Firefox");
            GeckoPreferences.User["security.warn_viewing_mixed"] = false;
            GeckoPreferences.User["plugin.state.flash"] = 0;
        }
    }
}