using Gecko;
using Gecko.DOM;
using Gecko.Cache;
using Gecko.Certificates;
using Gecko.Collections;
using Gecko.Cryptography;
using Gecko.CustomMarshalers;
using Gecko.Images;
using Gecko.Interop;
using Gecko.IO;
using Gecko.JQuery;
using Gecko.Net;
using Gecko.Observers;
using Gecko.Plugins;
using Gecko.Search;
using Gecko.Services;
using Gecko.Utils;
using Gecko.WebIDL;
using Gecko.Windows;
using Gecko.Events;
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
            PromptFactory.PromptServiceCreator();
            Xpcom.EnableProfileMonitoring = false;
            
            Xpcom.Initialize("Firefox");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
            //Xpcom.Initialize("Firefox");
            GeckoPreferences.User["security.warn_viewing_mixed"] = false;
            GeckoPreferences.User["plugin.state.flash"] = 0;
        }
    }
}