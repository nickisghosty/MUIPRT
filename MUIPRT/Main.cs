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
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

#region Main Form GUI

namespace MUIPRT
{
    public partial class Main : Form
    {   //variables
        public string referrer;
        public string agent;
        public string sUserAgent;
        public string url;
        public int onepage;
        char[] delimiters;
        string value;
        string[] proxy;
        
        private Point clickLocation = new Point(0, 0);
        private Point clickLocation2 = new Point(0, 0);
        Form Form2 = new Form();

        [DllImport("user32.dll")]
        private static extern bool EnumThreadWindows(uint dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("User32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr windowHandle, StringBuilder stringBuilder, int nMaxCount);

        [DllImport("user32.dll", EntryPoint = "GetWindowTextLength", SetLastError = true)]
        internal static extern int GetWindowTextLength(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_CLOSE = 0xF060;
        private static List<IntPtr> windowList;
        private static string _className;
        private static StringBuilder apiResult = new StringBuilder(256); //256 Is max class name length.
        private delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);
        private static List<IntPtr> WindowsFinder(string className, string process)
        {
            _className = className;
            windowList = new List<IntPtr>();

            Process[] chromeList = Process.GetProcessesByName(process);

            if (chromeList.Length > 0)
            {
                foreach (Process chrome in chromeList)
                {
                    if (chrome.MainWindowHandle != IntPtr.Zero)
                    {
                        foreach (ProcessThread thread in chrome.Threads)
                        {
                            EnumThreadWindows((uint)thread.Id, new EnumThreadDelegate(EnumThreadCallback), IntPtr.Zero);
                        }
                    }
                }
            }

            return windowList;
        }

        static bool EnumThreadCallback(IntPtr hWnd, IntPtr lParam)
        {
            if (GetClassName(hWnd, apiResult, apiResult.Capacity) != 0)
            {
                if (string.CompareOrdinal(apiResult.ToString(), _className) == 0)
                {
                    windowList.Add(hWnd);
                }
            }
            return true;
        }
    


public Main()
        {
            InitializeComponent();
            geckoWebBrowser1.CreateWindow += new EventHandler<GeckoCreateWindowEventArgs>(geckoWebBrowser1_CreateWindow);        //popup event
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //   this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            sUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:52.0) Gecko/20100101 Firefox/52.0.1 Waterfox/52.0.1";
            referrer = "http://google.com";
            // default browser settings
            GeckoPreferences.Default["media.peerconnection.enabled"] = false;
            GeckoPreferences.Default["network.proxy.http_port"] = 80;
            GeckoPreferences.Default["network.proxy.http"] = "localhost";
            GeckoPreferences.Default["network.proxy.ssl_port"] = 80;
            GeckoPreferences.Default["network.proxy.ssl"] = "localhost";
            GeckoPreferences.Default["general.useragent.override"] = sUserAgent;
            GeckoPreferences.User["browser.cache.disk.enable"] = false;
            Gecko.GeckoPreferences.User["network.http.pipelining"] = true;
            GeckoPreferences.User["browser.xul.error_pages.enabled"] = true;
            Gecko.GeckoPreferences.Default["browser.cache.disk.enable"] = false;
            Gecko.GeckoPreferences.User["browser.cache.disk.enable"] = false;
            GeckoPreferences.Default["browser.xul.error_pages.enabled"] = true;
            textbox_navigate.Items.Equals(geckoWebBrowser1.History);
            var field = typeof(GeckoWebBrowser).GetField("WebBrowser", BindingFlags.Instance | BindingFlags.NonPublic);
            nsIWebBrowser nsIWebBrowser = (nsIWebBrowser)field.GetValue(geckoWebBrowser1); //this might be null if called right before initialization of browser
            Xpcom.QueryInterface<nsILoadContext>(nsIWebBrowser).SetPrivateBrowsing(true);
            geckoWebBrowser1.Navigate("http://google.com", (Gecko.GeckoLoadFlags.ReplaceHistory | Gecko.GeckoLoadFlags.BypassCache | Gecko.GeckoLoadFlags.BypassProxy), referrer, null, null); //home page


            // disable buttons
            button_skip.Enabled = false;
            button_clearurl.Enabled = false;
            button_clearproxies.Enabled = false;
            button_clearuseragents.Enabled = false;
            button_addref.Enabled = false;
            button_addurl.Enabled = false;
            button_adduseragent.Enabled = false;
            button_setref.Enabled = false;
            button_stop.Enabled = false;
            onepage = 0;
        }

        #region Main function

        private void button_start_Click(object sender, EventArgs e)
        {   // progress bar for application
            progressbar_appprogress.Maximum = (int)numupdown_views.Value;
            progressbar_appprogress.Minimum = 0;
            button_stop.Enabled = true;
            timer_load.Interval = 100;
            button_start.Enabled = false;
            button_clearproxies.Enabled = false;
            button_clearurl.Enabled = false;
            button_skip.Enabled = true;
            button_clearuseragents.Enabled = false;
            // list_proxies.SelectedIndex = (list_proxies.SelectedIndex + 1);

            if (list_urls.Items.Count == 0) //if urls list box empty and start is clicked
            {
                string MessageBoxTitle = ("You need URLs");
                string MessageBoxContent = ("Enter URLs and try again!");

                DialogResult dialogResult = MessageBox.Show(MessageBoxContent, MessageBoxTitle, MessageBoxButtons.OK);
            }
            else if (list_urls.SelectedIndex == list_urls.Items.Count - 1)  //if program finishes all proxies and urls
            {
                string MessageBoxTitle = ("You've completed the URL list");
                string MessageBoxContent = ("Clear the list and add a new URL.");
                DialogResult dialogResult = MessageBox.Show(MessageBoxContent, MessageBoxTitle, MessageBoxButtons.OK);
            }
            else if (list_urls.Items.Count >= 1)   // if urls list is not empty
            {
                url = list_urls.GetItemText(list_urls.SelectedItem);
                agent = list_agents.GetItemText(list_agents.SelectedItem);

                GeckoPreferences.User["general.useragent.override"] = agent;
                list_urls.SelectedIndex = list_urls.SelectedIndex + 1;
                timer_load.Start();

                label_status.Text = "Running";
                if (list_agents.Items.Count == 0)
                {
                    sUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:52.0) Gecko/20100101 Firefox/52.0.1 Waterfox/52.0.1";
                    GeckoPreferences.User["general.useragent.override"] = sUserAgent;
                }
                else
                {
                    //    list_agents.SelectedIndex = (list_agents.SelectedIndex + 1);
                    agent = list_agents.GetItemText(list_agents.SelectedItem);
                }
            }
            else if (list_urls.SelectedIndex == list_urls.SelectedIndex - 1)   // if selected url is the last url in list
            {
                url = list_urls.GetItemText(list_urls.SelectedItem);
                agent = list_agents.GetItemText(list_agents.SelectedItem);

                GeckoPreferences.User["general.useragent.override"] = agent;
                timer_load.Start();
                // list_proxies.SelectedIndex = (list_proxies.SelectedIndex + 1);
                label_status.Text = "Running";
                if (list_agents.Items.Count == 0) // if user agents list is empty set a default user agent
                {
                    sUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:52.0) Gecko/20100101 Firefox/52.0.1 Waterfox/52.0.1";
                    GeckoPreferences.User["general.useragent.override"] = sUserAgent;
                }
                else  // if user agents list is full move down the list and set the useragent
                {
                    // list_agents.SelectedIndex = (list_agents.SelectedIndex + 1);
                    GeckoPreferences.User["general.useragent.override"] = agent;
                }
                geckoWebBrowser1.Navigate(list_urls.GetItemText(list_urls.SelectedIndex));

            }
            else //proxies list is empty
            {
                string MessageBoxTitle = ("You need Proxies");
                string MessageBoxContent = ("Enter proxies and try again!");

                DialogResult dialogResult = MessageBox.Show(MessageBoxContent, MessageBoxTitle, MessageBoxButtons.OK);
            }
        }

        private void timer_load_Tick(object sender, EventArgs e)
        {
            timer_cleardata.Start();

            progressbar_appprogress.Value++;
            var x = Convert.ToInt32(label_currentproxnum.Text);
            x++;
            label_currentproxnum.Text = Convert.ToString(x);
            if (progressbar_appprogress.Value >= progressbar_appprogress.Maximum)      //if app finished job
            {
                timer_refreshproxylist.Stop();
                timer_load.Stop();
                timer_cleardata.Stop();
                button_skip.Enabled = false;
                label_status.Text = "Finished!";
                button_start.Enabled = true;
                button_stop.Enabled = false;
                button_clearproxies.Enabled = true;
                button_clearurl.Enabled = true;
                button_clearuseragents.Enabled = false;
                progressbar_appprogress.Value = 0;
            }
            list_proxies.TopIndex = 0;
            list_agents.TopIndex = 0;
            if (list_proxies.SelectedIndex == list_proxies.Items.Count - 1)       //if reach last proxy in list go to top and change url
            {
                list_proxies.SelectedIndex = list_proxies.TopIndex;
                if (list_urls.Items.Count == 1)
                {
                    string MessageBoxTitle = "Finished";
                    string MessageBoxContent = "Program has finished every URL in the list using the selected proxies and useragents.";
                    DialogResult dialogResult = MessageBox.Show(MessageBoxContent, MessageBoxTitle, MessageBoxButtons.OK);
                    label_status.Text = "Finished!";
                    timer_load.Stop();
                    timer_refreshproxylist.Stop();
                    timer_cleardata.Stop();
                }
                else if (list_urls.SelectedIndex == list_urls.Items.Count - 1)    //if url is last in the list, stop program is finished
                {
                    string MessageBoxTitle = "Finished";
                    string MessageBoxContent = "Program has finished every URL in the list using the selected proxies and useragents.";
                    DialogResult dialogResult = MessageBox.Show(MessageBoxContent, MessageBoxTitle, MessageBoxButtons.OK);
                    label_status.Text = "Finished!";
                    timer_load.Stop();
                    timer_refreshproxylist.Stop();
                    timer_cleardata.Stop();
                }
                else if (list_agents.Items.Count == 0)   //if agents are empty set default
                {
                    sUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:52.0) Gecko/20100101 Firefox/52.0.1 Waterfox/52.0.1";
                    GeckoPreferences.User["general.useragent.override"] = sUserAgent;
                    list_urls.SelectedIndex = (list_urls.SelectedIndex + 1);
                }
                else if (list_agents.SelectedIndex == list_agents.Items.Count - 1)
                {
                    list_agents.SelectedIndex = list_agents.TopIndex;
                    list_urls.SelectedIndex = (list_urls.SelectedIndex + 1);
                    agent = list_agents.GetItemText(list_agents.SelectedItem);
                    GeckoPreferences.User["general.useragent.override"] = agent;
                }
                else
                {
                    list_urls.SelectedIndex = (list_urls.SelectedIndex + 1);

                    agent = list_agents.GetItemText(list_agents.SelectedItem);
                    GeckoPreferences.User["general.useragent.override"] = agent;
                }
            }
            else
            {
                list_proxies.SelectedIndex = (list_proxies.SelectedIndex + 1);
                if (list_agents.Items.Count == 0)
                {
                    sUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:52.0) Gecko/20100101 Firefox/52.0.1 Waterfox/52.0.1";
                    GeckoPreferences.User["general.useragent.override"] = sUserAgent;
                }
                else
                {
                    list_agents.SelectedIndex = (list_agents.SelectedIndex + 1);

                    agent = list_agents.GetItemText(list_agents.SelectedItem);
                    GeckoPreferences.User["general.useragent.override"] = agent;
                }
            }

            if (referral_txtdrop.Text != "Referrer")
            {
                referrer = referral_txtdrop.Text;
            }
            url = list_urls.GetItemText(list_urls.SelectedItem);
            agent = list_agents.GetItemText(list_agents.SelectedItem);
            delimiters = new char[] { ':', '@' };
            value = list_proxies.SelectedItem.ToString();
            proxy = value.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);//split proxies at  :
          
            // proxy settings
            Gecko.GeckoPreferences.Default["network.proxy.type"] = 1;
            Gecko.GeckoPreferences.Default["network.proxy.http"] = proxy[0];
            Gecko.GeckoPreferences.Default["network.proxy.http_port"] = Convert.ToInt32(proxy[1]);
            Gecko.GeckoPreferences.Default["network.proxy.ssl"] = proxy[0];
            GeckoPreferences.Default["network.proxy.ssl_port"] = Convert.ToInt32(proxy[1]);
            GeckoPreferences.Default["network.proxy.remote_dns"] = true;
            GeckoPreferences.Default["network.proxy.http_remote_dns"] = true;
            GeckoPreferences.Default["network.proxy.ssl_remote_dns"] = true;
            Gecko.GeckoPreferences.User["network.proxy.type"] = 1;
            Gecko.GeckoPreferences.User["network.proxy.http"] = proxy[0];
            Gecko.GeckoPreferences.User["network.proxy.http_port"] = Convert.ToInt32(proxy[1]);
            Gecko.GeckoPreferences.User["network.proxy.ssl"] = proxy[0];
            GeckoPreferences.User["network.proxy.ssl_port"] = Convert.ToInt32(proxy[1]);
            GeckoPreferences.User["network.proxy.remote_dns"] = true;
            GeckoPreferences.User["network.proxy.http_remote_dns"] = true;
            GeckoPreferences.User["network.proxy.ssl_remote_dns"] = true;
            GeckoPreferences.User["general.useragent.override"] = agent;
            geckoWebBrowser1.Refresh();
            geckoWebBrowser1.Reload();
            geckoWebBrowser1.Navigate(url, (Gecko.GeckoLoadFlags.ReplaceHistory | Gecko.GeckoLoadFlags.BypassCache | Gecko.GeckoLoadFlags.BypassProxy), referrer, null, null); //home page
            onepage = 0;

            timer_load.Interval = (int)numupdown_interval.Value;
        }

        private void timer_cleardata_Tick(object sender, EventArgs e) // clear cache cookies history etc
        {
            nsIBrowserHistory historyMan = Xpcom.GetService<nsIBrowserHistory>(Gecko.Contracts.NavHistoryService);
            historyMan = Xpcom.QueryInterface<nsIBrowserHistory>(historyMan);
            historyMan.RemoveAllPages();


            nsICookieManager CookieMan;
            CookieMan = Xpcom.GetService<nsICookieManager>("@mozilla.org/cookiemanager;1");
            CookieMan = Xpcom.QueryInterface<nsICookieManager>(CookieMan);
            CookieMan.RemoveAll();

            // https://developer.mozilla.org/en-US/docs/Mozilla/Tech/XPCOM/Reference/Interface/imgICache
            Gecko.Cache.ImageCache.ClearCache(true);
            Gecko.Cache.ImageCache.ClearCache(false);
//            Gecko.Cache.CacheService.Clear(new CacheStoragePolicy());

            nsICacheStorageService cache;
            cache = Xpcom.GetService<nsICacheStorageService>("@mozilla.org/netwerk/cache-storage-service;1");
             try { cache.Clear(); }
            catch(Exception ex) { throw new ApplicationException("Could not clear cache:", ex); }

            imgICache icache;
            icache = Xpcom.GetService<imgITools>("@mozilla.org/image/tools;1").GetImgCacheForDocument(null);
            try { icache.ClearCache(false); }
            catch (Exception ex) { throw new ApplicationException("Could not clear image cache:", ex); }
        
            timer_cleardata.Stop();
        }

        private void button_stop_Click(object sender, EventArgs e) //stop running
        {
            list_urls.SelectedIndex = -1;
            button_stop.Enabled = false;
            button_skip.Enabled = false;
            button_start.Enabled = true;
            button_clearproxies.Enabled = true;
            button_clearurl.Enabled = true;
            if (list_agents.Items.Count > 0)
            {
                button_clearuseragents.Enabled = true;
            }
            label_status.Text = "Stopped.";
            timer_refreshproxylist.Stop();
            timer_load.Stop();
            timer_cleardata.Stop();
            label_status.Text = "Stopped";
            progressbar_appprogress.Value = 0;
        }

        #endregion Main function

        #region URL functions

        private void button_addurl_Click(object sender, EventArgs e)  //add link to list
        {
            list_urls.Items.Add(textbox_url.Text);
            if (list_urls.Items.Count >= 1)
            {
                button_clearurl.Enabled = true;
            }
            textbox_url.Text = "";
        }

        private void button_loadurllist_Click(object sender, EventArgs e)   //load link list
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "URLs",
                InitialDirectory = "C:\\",
                Filter = "Text files (*.txt*)|*.txt*",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                StreamReader streamReader = new StreamReader(openFileDialog.FileName);
                while (streamReader.Peek() > -1)
                {
                    this.list_urls.Items.Add(streamReader.ReadLine());
                }

                streamReader.Close();
            }
            else     // no file was selected in the open file dialog
            {
                string MessageBoxTitle = ("You need a URL");
                string MessageBoxContent = ("Enter at lease one URL and try again!");

                DialogResult dialogResult = MessageBox.Show(MessageBoxContent, MessageBoxTitle, MessageBoxButtons.OK);
            }
            if (list_urls.Items.Count >= 1)   // enable clear urls if list has items
            {
                button_clearurl.Enabled = true;
            }
        }

        private void button_clearurl_Click(object sender, EventArgs e)
        {
            list_urls.Items.Clear();
            if (list_urls.Items.Count == 0)
            {
                button_clearurl.Enabled = false;
            }
        }

        private void textbox_url_Enter(object sender, EventArgs e)   // url textbox focus entered
        {
            if (textbox_url.Text == "URLs to be viewed")
            {
                textbox_url.Text = "";
            }
        }

        private void textbox_url_KeyDown(object sender, KeyEventArgs e)   // enter adds url to list
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_addurl_Click(sender, e);
            }
        }

        private void textbox_url_TextChanged(object sender, EventArgs e)   // enable add url button if textbox text changes
        {
            button_addurl.Enabled = true;
        }

        private void textbox_url_Leave(object sender, EventArgs e)     //url textbox loses focus
        {
            if (textbox_url.Text == "")
            {
                textbox_url.Text = "URLs to be viewed";
                button_addurl.Enabled = false;
            }
        }

        #endregion URL functions

        #region Useragent functions

        private void button_adduseragent_Click(object sender, EventArgs e)
        {
            list_agents.Items.Add(textbox_agent.Text);
            if (list_agents.Items.Count >= 1)
            {
                button_clearuseragents.Enabled = true;
            }
            textbox_agent.Text = "";
        }

        private void button_loaduseragentslist_Click(object sender, EventArgs e)
        {
            List<string> agents = new List<string>();

            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "User Agents",
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                Filter = "Text files (*.txt*)|*.txt*",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                StreamReader streamReader = new StreamReader(openFileDialog.FileName);
                while (streamReader.Peek() > -1)
                {
                    agents.Add(streamReader.ReadLine());
                }


                streamReader.Close();
                agents.Shuffle();
                this.list_agents.Items.AddRange(agents.ToArray());

            }

            if (list_agents.Items.Count >= 1)
            {
                button_clearuseragents.Enabled = true;
            }
        }

        private void button_clearuseragents_Click(object sender, EventArgs e)
        {
            list_agents.Items.Clear();
            if (list_agents.Items.Count == 0)
            {
                button_clearuseragents.Enabled = false;
            }
        }

        private void textbox_agent_Enter(object sender, EventArgs e)
        {
            if (textbox_agent.Text == "Useragents to use")
            {
                textbox_agent.Text = "";
            }
        }

        private void textbox_agent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_adduseragent_Click(sender, e);
            }
        }

        private void textbox_agent_TextChanged(object sender, EventArgs e)
        {
            button_adduseragent.Enabled = true;
        }

        private void textbox_agent_Leave(object sender, EventArgs e)
        {
            if (textbox_agent.Text == "")
            {
                textbox_agent.Text = "Useragents to use";
                button_adduseragent.Enabled = false;
            }
        }

        #endregion Useragent functions

        #region Proxy functions

        public string proxylistfile;

        private void button_loadproxies_Click(object sender, EventArgs e)
        {
            List<string> proxies = new List<string>();
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Proxies",
                InitialDirectory = Environment.ExpandEnvironmentVariables("C:\\Users\\% USERPROFILE %\\"),
                Filter = "Text files (*.txt*)|*.txt*",
                FilterIndex = 2,
                RestoreDirectory = false
            };
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                proxylistfile = openFileDialog.FileName;

                StreamReader streamReader = new StreamReader(proxylistfile);
                while (streamReader.Peek() > -1)
                {
                    proxies.Add(streamReader.ReadLine());

                    this.label_proxiesnum.Text = Convert.ToString(proxies.Count);
                }

                streamReader.Close();
                proxies.Shuffle();
                this.list_proxies.Items.AddRange(proxies.ToArray());
                timer_refreshproxylist.Interval = (int)numupdown_interval.Value / 4;
                timer_refreshproxylist.Start();
            }
            else
            {
                string MessageBoxTitle = ("You need Proxies");
                string MessageBoxContent = ("Enter proxies and try again!");

                DialogResult dialogResult = MessageBox.Show(MessageBoxContent, MessageBoxTitle, MessageBoxButtons.OK);
            }
            label_proxiesnum.Text = Convert.ToString(list_proxies.Items.Count);
            if (list_proxies.Items.Count >= 1)
            {
                button_clearproxies.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            using (var fs = new FileStream(proxylistfile, System.IO.FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
            using (var sr = new StreamReader(fs, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (!list_proxies.Items.Contains(line))
                        list_proxies.Items.Add(line);
                }
            }
            /*
            List<string> lines = new List<string>();
            using (StreamReader r = new StreamReader(proxylistfile))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }*/

            /*    string[] lines = System.IO.File.ReadAllLines(proxylistfile);

            foreach (string str in lines)
            {
                if (!list_proxies.Items.Contains(str))
                    list_proxies.Items.Add(str);
            }            */

            label_proxiesnum.Text = list_proxies.Items.Count.ToString();
        }

        private void button_clearproxies_Click(object sender, EventArgs e)
        {
            list_proxies.Items.Clear();
            label_currentproxnum.Text = "0";
            label_proxiesnum.Text = "0";
            if (list_proxies.Items.Count == 0)
            {
                button_clearproxies.Enabled = false;
            }
        }

        #endregion Proxy functions

        #region Coords / Referral functions

        private void button_setcoords_Click(object sender, EventArgs e)
        {
            timer_setcoords.Interval = 5000;
            timer_setcoords.Start();
        }

        private void button_addref_Click(object sender, EventArgs e)
        {
            referral_txtdrop.Items.Add(referral_txtdrop.Text);
        }

        private void button_setref_Click(object sender, EventArgs e)
        {
            referrer = referral_txtdrop.Text;
        }

        private void referral_txtdrop_Enter(object sender, EventArgs e)
        {
            if (referral_txtdrop.Text == "Referrer")
            {
                referral_txtdrop.Text = "";
            }
        }

        private void referral_txtdrop_TextChanged(object sender, EventArgs e)
        {
            button_addref.Enabled = true;
            button_setref.Enabled = true;
        }

        private void referral_txtdrop_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_addref_Click(sender, e);
            }
        }

        private void referral_txtdrop_Leave(object sender, EventArgs e)
        {
            if (referral_txtdrop.Text == "")
            {
                referral_txtdrop.Text = "Referral";
            }
        }

        #endregion Coords / Referral functions

        #endregion Main Form GUI

        #region Gecko browser

        #region Browser controls

        private void button_navigate_Click(object sender, EventArgs e)
        {
            geckoWebBrowser1.Navigate(textbox_navigate.Text, (Gecko.GeckoLoadFlags.ReplaceHistory | Gecko.GeckoLoadFlags.BypassCache | Gecko.GeckoLoadFlags.BypassProxy), referrer, null, null); //home page

            //geckoWebBrowser1.Navigate(textbox_navigate.Text, Gecko.GeckoLoadFlags.BypassHistory, referrer, null, null);
            textbox_navigate.Items.Add(textbox_navigate.Text);
        }

        private void textbox_navigate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_navigate_Click(sender, e);
            }
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            geckoWebBrowser1.Refresh();
        }

        private void button_stopnav_Click(object sender, EventArgs e)
        {
            geckoWebBrowser1.Stop();
        }

        private void button_goforward_Click(object sender, EventArgs e)
        {
            geckoWebBrowser1.GoForward();
        }

        private void button_goback_Click(object sender, EventArgs e)
        {
            geckoWebBrowser1.GoBack();
        }

        #endregion Browser controls

        #region Browser automation

        private void clickad()
        {
            var iframe = geckoWebBrowser1.Document.GetHtmlElementById("aads") as Gecko.GeckoHtmlElement;
            onepage = 1;
            if (iframe != null)
            {
                iframe.ScrollIntoView(true);
                iframe.Click();

                timer_clickcoords.Interval = 3000;

                if (textbox_y.Text != "" || textbox_x.Text != "")
                {
                    timer_clickcoords.Start();
                }
            }
            else
            {
                timer_clickcoords.Interval = 3000;
                if (textbox_y.Text != "" || textbox_x.Text != "")
                {
                    timer_clickcoords.Start();
                }

            }
            /*if (textBox2.Text != "" || textBox1.Text != "")
            {
                //set cursor position to memorized location
                Cursor.Position = clickLocation;
                //set up the INPUT struct and fill it for the mouse down
                INPUT i = new INPUT();
                i.type = INPUT_MOUSE;
                i.mi.dx = 0;
                i.mi.dy = 0;
                i.mi.dwFlags = MOUSEEVENTF_LEFTDOWN;
                i.mi.dwExtraInfo = IntPtr.Zero;
                i.mi.mouseData = 0;
                i.mi.time = 0;
                //send the input
                SendInput(1, ref i, Marshal.SizeOf(i));
                //set the INPUT for mouse up and send it
                i.mi.dwFlags = MOUSEEVENTF_LEFTUP;
                SendInput(1, ref i, Marshal.SizeOf(i));
            */
        }

        [DllImport("User32.dll", SetLastError = true)]
        public static extern int SendInput(int nInputs, ref INPUT pInputs,
                                   int cbSize);

        //mouse event constants
        private const int MOUSEEVENTF_LEFTDOWN = 2;

        private const int MOUSEEVENTF_LEFTUP = 4;

        //input type constant
        private const int INPUT_MOUSE = 0;

        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        public struct INPUT
        {
            public uint type;
            public MOUSEINPUT mi;
        };

        private void timer_setcoords_Tick(object sender, EventArgs e)
        {
            clickLocation = Cursor.Position;
            //show the location on window title
            textbox_x.Text = clickLocation.X.ToString();
            textbox_y.Text = clickLocation.Y.ToString();
            timer_setcoords.Stop();
        }

        private void timer_clickcoords_Tick(object sender, EventArgs e)
        {
            //set cursor position to memorized location
            Cursor.Position = clickLocation;
            //set up the INPUT struct and fill it for the mouse down
            INPUT i = new INPUT();
            i.type = INPUT_MOUSE;
            i.mi.dx = 0;
            i.mi.dy = 0;
            i.mi.dwFlags = MOUSEEVENTF_LEFTDOWN;
            i.mi.dwExtraInfo = IntPtr.Zero;
            i.mi.mouseData = 0;
            i.mi.time = 0;
            //send the input
            SendInput(1, ref i, Marshal.SizeOf(i));
            //set the INPUT for mouse up and send it
            i.mi.dwFlags = MOUSEEVENTF_LEFTUP;
            SendInput(1, ref i, Marshal.SizeOf(i));

            timer_clickcoords.Stop();
            timer_clickcoords2.Interval = 5000;
            timer_clickcoords2.Start();

        }

        private void timer_setcoords2_Tick(object sender, EventArgs e)
        {
            clickLocation2 = Cursor.Position;
            //show the location on window title
            textbox_x2.Text = clickLocation2.X.ToString();
            textbox_y2.Text = clickLocation2.Y.ToString();
            timer_setcoords2.Stop();
        }

        private void timer_clickcoords2_Tick(object sender, EventArgs e)
        {
            //set cursor position to memorized location
            Cursor.Position = clickLocation2;
            //set up the INPUT struct and fill it for the mouse down
            INPUT i = new INPUT();
            i.type = INPUT_MOUSE;
            i.mi.dx = 0;
            i.mi.dy = 0;
            i.mi.dwFlags = MOUSEEVENTF_LEFTDOWN;
            i.mi.dwExtraInfo = IntPtr.Zero;
            i.mi.mouseData = 0;
            i.mi.time = 0;
            //send the input
            SendInput(1, ref i, Marshal.SizeOf(i));
            //set the INPUT for mouse up and send it
            i.mi.dwFlags = MOUSEEVENTF_LEFTUP;
            SendInput(1, ref i, Marshal.SizeOf(i));

            timer_clickcoords2.Stop();
        }

        private void Button_getcoords2_Click(object sender, EventArgs e)
        {
            timer_setcoords2.Interval = 5000;
            timer_setcoords2.Start();
        }
        /* private void autocaptcha()
         {
             GeckoWebBrowser geckoWebBrowser2 = new GeckoWebBrowser();

             var audiobutton = geckoWebBrowser2.Document.GetElementsByClassName("rc-button goog-inline-block rc-button-audio");
             Gecko.DOM.GeckoButtonElement next = new Gecko.DOM.GeckoButtonElement(audiobutton.ElementAt(1).DomObject);
             next.Click();
         }*/

        #endregion Browser automation

        #region Browser events
        private void geckoWebBrowser1_CreateWindow(object sender, GeckoCreateWindowEventArgs e)
        {
            // Full Screen
            var iframe = geckoWebBrowser1.Document.GetHtmlElementById("aads") as Gecko.GeckoHtmlElement;
            var close = "data:text/html,<script>self.close()</script>";
           
            GeckoWebBrowser geckoWebBrowser2 = new GeckoWebBrowser();


            Rectangle rect = System.Windows.Forms.Screen.GetWorkingArea(this);


            geckoWebBrowser2.Navigating += new EventHandler<GeckoNavigatingEventArgs>(geckoWebBrowser1_Navigating);
            geckoWebBrowser2.DocumentCompleted += new EventHandler<GeckoDocumentCompletedEventArgs>(geckoWebBrowser2_DocumentCompleted);
            geckoWebBrowser2.Dock = DockStyle.Fill;
            geckoWebBrowser2.CreateControl();

            //TabPage tab1 = new TabPage("New WebBrowser");
            //tabBrowser.TabPages.Add(tab1);
            Form2.Controls.Add(geckoWebBrowser2);
            Form2.CreateControl();

            if (list_proxies.Items.Count == 0)
            {
                geckoWebBrowser2.Navigate(e.Uri);
                e.InitialWidth = this.Width;
                e.InitialHeight = this.Height;
            }
            else
            {
                Gecko.GeckoPreferences.Default["network.proxy.type"] = 1;
                Gecko.GeckoPreferences.Default["network.proxy.http"] = proxy[0];
                Gecko.GeckoPreferences.Default["network.proxy.http_port"] = Convert.ToInt32(proxy[1]);
                Gecko.GeckoPreferences.Default["network.proxy.ssl"] = proxy[0];
                GeckoPreferences.Default["network.proxy.ssl_port"] = Convert.ToInt32(proxy[1]);
                GeckoPreferences.Default["network.proxy.remote_dns"] = true;
                GeckoPreferences.Default["network.proxy.http_remote_dns"] = true;
                GeckoPreferences.Default["network.proxy.ssl_remote_dns"] = true;
                Gecko.GeckoPreferences.User["network.proxy.type"] = 1;
                Gecko.GeckoPreferences.User["network.proxy.http"] = proxy[0];
                Gecko.GeckoPreferences.User["network.proxy.http_port"] = Convert.ToInt32(proxy[1]);
                Gecko.GeckoPreferences.User["network.proxy.ssl"] = proxy[0];
                GeckoPreferences.User["network.proxy.ssl_port"] = Convert.ToInt32(proxy[1]);
                GeckoPreferences.User["network.proxy.remote_dns"] = true;
                GeckoPreferences.User["network.proxy.http_remote_dns"] = true;
                GeckoPreferences.User["network.proxy.ssl_remote_dns"] = true;
                Gecko.GeckoPreferences.Default["browser.cache.disk.enable"] = false;
                Gecko.GeckoPreferences.User["browser.cache.disk.enable"] = false;
                GeckoPreferences.User["general.useragent.override"] = agent;
                geckoWebBrowser2.Navigate(e.Uri);
                if (iframe != null)
                {
                    e.InitialWidth = this.Width;
                    e.InitialHeight = this.Height;
                }
                else
                {
                    e.InitialWidth = this.Width;
                    e.InitialHeight = this.Height; ;
                }
            }

        }
        private void closeWindow()
        {

            // retrieve the handler of the window  
            int iHandle = FindWindow("MozillaWindowClass", "MUIPRT");
            if (iHandle > 0)
            {
                // close the window using API        
                SendMessage(iHandle, WM_SYSCOMMAND, SC_CLOSE, 0);
            }

        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        private const UInt32 WM_CLOSE = 0x0010;

        void CloseWindow(IntPtr hwnd)
        {
            SendMessage(hwnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }
        private void geckoWebBrowser2_DocumentCompleted(object sender, Gecko.Events.GeckoDocumentCompletedEventArgs e)
        {
            var iframe = geckoWebBrowser1.Document.GetHtmlElementById("aads") as Gecko.GeckoHtmlElement;

            if (iframe == null)
            {
                List<IntPtr> ChromeWindows = WindowsFinder("MozillaWindowClass", "MUIPRT");
                foreach (IntPtr windowHandle in ChromeWindows)
                {
                    int iHandle = FindWindow("MozillaWindowClass", "MUIPRT");
                    int length = GetWindowTextLength(windowHandle);
                    StringBuilder sb = new StringBuilder(length + 1);
                    GetWindowText(windowHandle, sb, sb.Capacity);
                    SendMessage(iHandle, WM_SYSCOMMAND, SC_CLOSE, 0);
                    CloseWindow(windowHandle);
                }
            }

            //else{
        //    autocaptcha()}
        }

        private void geckoWebBrowser1_DocumentCompleted(object sender, Gecko.Events.GeckoDocumentCompletedEventArgs e)
        {
            label_statusbrowser.Text = "Done.. " + geckoWebBrowser1.StatusText;
            textbox_navigate.Text = geckoWebBrowser1.Url.AbsoluteUri;
            progressbar_browser.Value = 0;
            if (geckoWebBrowser1.Document.Title == "403 Forbidden"||geckoWebBrowser1.Document.TextContent == "429 Too Many Requests")
            {
                button_skip.PerformClick();
            }
            else
            {
                if (!geckoWebBrowser1.IsBusy && onepage == 0)
                { clickad(); }
            }
        }

        private void geckoWebBrowser1_DOMContentLoaded(object sender, DomEventArgs e)
        {
            progressbar_browser.Value = 0;
        }

        private void geckoWebBrowser1_LocationChanged(object sender, EventArgs e)
        {
            textbox_navigate.Text = geckoWebBrowser1.Url.AbsoluteUri;
            label_statusbrowser.Text = "Moved... " + geckoWebBrowser1.StatusText;
        }

        private void geckoWebBrowser1_Load(object sender, DomEventArgs e)
        {
            textbox_navigate.Text = geckoWebBrowser1.Url.AbsoluteUri;
            label_statusbrowser.Text = geckoWebBrowser1.StatusText;
            //  clickad();
        }

        private void geckoWebBrowser1_MouseHover(object sender, EventArgs e)
        {
            label_statusbrowser.Text = geckoWebBrowser1.StatusText;
        }

        private void geckoWebBrowser1_Navigating(object sender, Gecko.Events.GeckoNavigatingEventArgs e)
        {
            GeckoWebBrowser geckoWebBrowser2 = new GeckoWebBrowser();

            if (this.ActiveControl == geckoWebBrowser1)
            {
                textbox_navigate.Text = geckoWebBrowser1.Url.AbsoluteUri;
                label_statusbrowser.Text = geckoWebBrowser1.StatusText;
                if (progressbar_browser.Value < 0)
                {
                    progressbar_browser.Value = 0;
                }
                else if (progressbar_browser.Value > 100)
                {
                    progressbar_browser.Value = 100;
                }
            }
            else if (this.ActiveControl == geckoWebBrowser2)
            {
                textbox_navigate.Text = geckoWebBrowser2.Url.AbsoluteUri;
                label_statusbrowser.Text = geckoWebBrowser2.StatusText;
                if (progressbar_browser.Value < 0)
                {
                    progressbar_browser.Value = 0;
                }
                else if (progressbar_browser.Value > 100)
                {
                    progressbar_browser.Value = 100;
                }
            }
        }

        private void geckoWebBrowser1_NavigationError(object sender, GeckoNavigationErrorEventArgs e)
        {
            label_statusbrowser.Text = "Error loading page. Check proxy settings or URL.";
        }

        private void geckoWebBrowser1_Navigated(object sender, GeckoNavigatedEventArgs e)
        {

            //clickad();
            label_statusbrowser.Text = "Done.";
            progressbar_browser.Value = 0;
        }

        private void geckoWebBrowser2_Navigating(object sender, GeckoNavigatingEventArgs e)
        {
            GeckoWebBrowser geckoWebBrowser2 = new GeckoWebBrowser();

            //throw new NotImplementedException();
            geckoWebBrowser1_Navigating(geckoWebBrowser2, e);
        }

        private void geckoWebBrowser1_ProgressChanged(object sender, GeckoProgressEventArgs e)
        {
            label_statusbrowser.Text = geckoWebBrowser1.StatusText;
            textbox_navigate.Text = geckoWebBrowser1.Url.AbsoluteUri;
            progressbar_browser.Maximum = 100;
            progressbar_browser.Minimum = 0;

            try
            {
                if (e.CurrentProgress == 0)
                {
                    progressbar_browser.Value = (int)(((double)e.CurrentProgress * 100) / e.MaximumProgress);
                }
                if (e.CurrentProgress < 0)
                {
                    progressbar_browser.Value = 0;
                }
                if (e.CurrentProgress > 0)
                {
                    progressbar_browser.Value = (int)(((double)e.CurrentProgress * 100) / e.MaximumProgress);
                }
                if (e.CurrentProgress > 100)
                {
                    progressbar_browser.Value = 100;
                }
                if (e.CurrentProgress < 100)
                {
                    progressbar_browser.Value = (int)(((double)e.CurrentProgress * 100) / e.MaximumProgress);
                }
                if (e.CurrentProgress == 100)
                {
                    label_statusbrowser.Text = "Done Loading...";
                    progressbar_browser.Value = 0;
                }
                else if (progressbar_browser.Value == 0)
                {
                    progressbar_browser.Value = (int)(((double)e.CurrentProgress * 100) / e.MaximumProgress);
                }
                else if (progressbar_browser.Value < 0)
                {
                    progressbar_browser.Value = 0;
                }
                else if (progressbar_browser.Value > 100)
                {
                    progressbar_browser.Value = 100;
                }
                else if (progressbar_browser.Value > 0)
                {
                    progressbar_browser.Value = (int)(((double)e.CurrentProgress * 100) / e.MaximumProgress);
                }
                else if (progressbar_browser.Value < 100)
                {
                    progressbar_browser.Value = (int)(((double)e.CurrentProgress * 100) / e.MaximumProgress);
                }
                else if (progressbar_browser.Value == 100)
                {
                    progressbar_browser.Value = 0;
                }
                else
                {
                    progressbar_browser.Value = (int)(((double)e.CurrentProgress * 100) / e.MaximumProgress);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        private void geckoWebBrowser1_Redirecting(object sender, GeckoRedirectingEventArgs e)
        {
            label_statusbrowser.Text = "Redirecting..." + geckoWebBrowser1.StatusText;
            textbox_navigate.Text = geckoWebBrowser1.Url.AbsoluteUri;
        }

        #endregion Browser events

        #endregion Gecko browser

       

        private void button_skip_Click(object sender, EventArgs e)
        {
            timer_load.Interval = 100;
        }
    }
}

