using Gecko;
using Gecko.Cache;
using Gecko.DOM;
using Gecko.Events;
using MUIPRT.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Screen = System.Windows.Forms.Screen;

#region Main Form GUI

namespace MUIPRT
{
    public partial class Main : Form
    {
        //variables
        public string Referrer;

        public string Agent;
        public string SUserAgent;
        public string Url;
        public int Onepage;
        private char[] _delimiters;
        private string _value;
        private string[] _proxy;
        private Point _clickLocation = new Point(0, 0);
        private Point _clickLocation2 = new Point(0, 0);
        private readonly Form _form2 = new Form();
        private GeckoWebBrowser geckoWebBrowser2 = new GeckoWebBrowser();
        private string coordsX;
        private string coordsY;
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
        public static extern int SendMessage(int hWnd, uint msg, int wParam, int lParam);

        private List<string> historyList = new List<string>();

        public const int WmSyscommand = 0x0112;
        public const int ScClose = 0xF060;
        private static List<IntPtr> _windowList;
        private static string _className;
        private static readonly StringBuilder ApiResult = new StringBuilder(256); //256 Is max class name length.

        private delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

        private static List<IntPtr> WindowsFinder(string className, string process)
        {
            _className = className;
            _windowList = new List<IntPtr>();

            Process[] chromeList = Process.GetProcessesByName(process);

            if (chromeList.Length > 0)
            {
                foreach (Process chrome in chromeList)
                {
                    if (chrome.MainWindowHandle != IntPtr.Zero)
                    {
                        foreach (ProcessThread thread in chrome.Threads)
                        {
                            EnumThreadWindows((uint)thread.Id, EnumThreadCallback, IntPtr.Zero);
                        }
                    }
                }
            }

            return _windowList;
        }

        private static bool EnumThreadCallback(IntPtr hWnd, IntPtr lParam)
        {
            if (GetClassName(hWnd, ApiResult, ApiResult.Capacity) != 0)
            {
                if (string.CompareOrdinal(ApiResult.ToString(), _className) == 0)
                {
                    _windowList.Add(hWnd);
                }
            }

            return true;
        }

        public Main()
        {
            InitializeComponent();
            geckoWebBrowser1.CreateWindow += geckoWebBrowser1_CreateWindow; //popup event
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //   this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            SUserAgent =
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:52.0) Gecko/20100101 Firefox/52.0.1 Waterfox/52.0.1";
            Referrer = "http://google.com";
            // default browser settings
            GeckoPreferences.Default["media.peerconnection.enabled"] = false;
            GeckoPreferences.Default["network.proxy.http_port"] = 80;
            GeckoPreferences.Default["network.proxy.http"] = "localhost";
            GeckoPreferences.Default["network.proxy.ssl_port"] = 80;
            GeckoPreferences.Default["network.proxy.ssl"] = "localhost";
            GeckoPreferences.Default["general.useragent.override"] = SUserAgent;
            GeckoPreferences.User["browser.cache.disk.enable"] = false;
            GeckoPreferences.User["network.http.pipelining"] = true;
            GeckoPreferences.User["browser.xul.error_pages.enabled"] = true;
            GeckoPreferences.Default["browser.cache.disk.enable"] = false;
            GeckoPreferences.User["browser.cache.disk.enable"] = false;
            GeckoPreferences.Default["browser.xul.error_pages.enabled"] = true;
            GeckoPreferences.User["security.mixed_content.block_active_content"] = false;
            GeckoPreferences.Default["security.mixed_content.block_active_content"] = false;

            // Loops through all the history entries

            geckoWebBrowser1.HistoryNewEntry += GeckoWebBrowser1_HistoryNewEntry;

            var field = typeof(GeckoWebBrowser).GetField("WebBrowser", BindingFlags.Instance | BindingFlags.NonPublic);
            if (!(field is null))
            {
                nsIWebBrowser
                    nsIWebBrowser =
                        (nsIWebBrowser)field.GetValue(
                            geckoWebBrowser1); //this might be null if called right before initialization of browser
                Xpcom.QueryInterface<nsILoadContext>(nsIWebBrowser).SetPrivateBrowsing(true);
            }

            geckoWebBrowser1.Navigate("http://google.com",
                (GeckoLoadFlags.ReplaceHistory | GeckoLoadFlags.BypassCache | GeckoLoadFlags.BypassProxy), Referrer,
                null, null); //home page

            // disable buttons
            button_skip.Enabled = false;
            button_clearurl.Enabled = false;
            button_clearproxies.Enabled = false;
            button_clearuseragents.Enabled = false;
            button_addurl.Enabled = false;
            button_adduseragent.Enabled = false;
            button_setref.Enabled = false;
            button_stop.Enabled = false;
            comboBox_autoclick.SelectedItem = comboBox_autoclick.Items[0];
            textBox_autoclick.Text = "advert";
            textbox_x.Visible = false;
            button_getcoords.Visible = false;
            //set tooltips
            
Onepage = 0;   //makes auto-clicked ad only click once.
        }

        private void GeckoWebBrowser1_HistoryNewEntry(object sender, GeckoHistoryEventArgs e)
        {
            AutoCompleteStringCollection hiStringCollection = new AutoCompleteStringCollection();

            historyList.Clear();
            try
            {
                foreach (GeckoHistoryEntry entry in geckoWebBrowser1.History)
                {
                    // Gets the URL and adds it to the result

                    if (entry.Url.AbsoluteUri.ToString().StartsWith(value: "http:\\www."))
                    {
                        historyList.Add(entry.Url.AbsoluteUri.ToString().Substring(startIndex: 11));
                    }
                    else if (entry.Url.AbsoluteUri.ToString().StartsWith(value: "https:\\www."))
                    {
                        historyList.Add(entry.Url.AbsoluteUri.ToString().Substring(startIndex: 12));
                    }
                    else if (entry.Url.AbsoluteUri.ToString().StartsWith(value: "http:\\"))
                    {
                        historyList.Add(entry.Url.AbsoluteUri.ToString().Substring(startIndex: 7));
                    }
                    else if (entry.Url.AbsoluteUri.ToString().StartsWith(value: "https:\\"))
                    {
                        historyList.Add(entry.Url.AbsoluteUri.ToString().Substring(startIndex: 8));
                    }
                    else
                    {
                        historyList.Add((entry.Url.AbsoluteUri.ToString()));
                    }

                    // Sends the result to the console
                }

                textbox_navigate.Items.Clear();
                textbox_navigate.AutoCompleteCustomSource.Clear();
                foreach (string item in historyList)
                {
                    textbox_navigate.Items.Add(item.ToString());
                    hiStringCollection.Add(item.ToString());
                    textbox_navigate.AutoCompleteCustomSource.Add(item.ToString());
                }

                textbox_navigate.AutoCompleteCustomSource = hiStringCollection;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Source + "\n" + exception.InnerException + "\n" + exception.StackTrace + "\n" + exception.Message + "\n" + exception.TargetSite + "\n" + exception.HelpLink);
                throw;
            }
        }

        #region Main function

        private void button_start_Click(object sender, EventArgs e)
        {
            autoclick_textbox_clicked = false;
            // progress bar for application
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
                string messageBoxTitle = ("You need URLs");
                string messageBoxContent = ("Enter URLs and try again!");

                DialogResult dialogResult = MessageBox.Show(messageBoxContent, messageBoxTitle, MessageBoxButtons.OK);
            }
            else if (list_urls.SelectedIndex == list_urls.Items.Count - 1) //if program finishes all proxies and urls
            {
                string messageBoxTitle = ("You've completed the URL list");
                string messageBoxContent = ("Clear the list and add a new URL.");
                DialogResult dialogResult = MessageBox.Show(messageBoxContent, messageBoxTitle, MessageBoxButtons.OK);
            }
            else if (list_urls.Items.Count >= 1) // if urls list is not empty
            {
                Url = list_urls.GetItemText(list_urls.SelectedItem);
                Agent = list_agents.GetItemText(list_agents.SelectedItem);

                GeckoPreferences.User["general.useragent.override"] = Agent;
                list_urls.SelectedIndex = list_urls.SelectedIndex + 1;
                timer_load.Start();

                label_status.Text = Resources.Text_Running;
                if (list_agents.Items.Count == 0)
                {
                    SUserAgent =
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:52.0) Gecko/20100101 Firefox/52.0.1 Waterfox/52.0.1";
                    GeckoPreferences.User["general.useragent.override"] = SUserAgent;
                }
                else
                {
                    //    list_agents.SelectedIndex = (list_agents.SelectedIndex + 1);
                    Agent = list_agents.GetItemText(list_agents.SelectedItem);
                }
            }
            else if (list_urls.SelectedIndex == list_urls.SelectedIndex - 1) // if selected url is the last url in list
            {
                Url = list_urls.GetItemText(list_urls.SelectedItem);
                Agent = list_agents.GetItemText(list_agents.SelectedItem);

                GeckoPreferences.User["general.useragent.override"] = Agent;
                timer_load.Start();
                // list_proxies.SelectedIndex = (list_proxies.SelectedIndex + 1);
                label_status.Text = Resources.Text_Running;
                if (list_agents.Items.Count == 0) // if user agents list is empty set a default user agent
                {
                    SUserAgent =
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:52.0) Gecko/20100101 Firefox/52.0.1 Waterfox/52.0.1";
                    GeckoPreferences.User["general.useragent.override"] = SUserAgent;
                }
                else // if user agents list is full move down the list and set the useragent
                {
                    // list_agents.SelectedIndex = (list_agents.SelectedIndex + 1);
                    GeckoPreferences.User["general.useragent.override"] = Agent;
                }

                geckoWebBrowser1.Navigate(list_urls.GetItemText(list_urls.SelectedIndex));
            }
            else //proxies list is empty
            {
                string messageBoxTitle = ("You need Proxies");
                string messageBoxContent = ("Enter proxies and try again!");

                DialogResult dialogResult = MessageBox.Show(messageBoxContent, messageBoxTitle, MessageBoxButtons.OK);
            }
        }

        private void timer_load_Tick(object sender, EventArgs e)
        {
            timer_cleardata.Start();

            progressbar_appprogress.Value++;
            var x = Convert.ToInt32(label_currentproxnum.Text);
            x++;
            label_currentproxnum.Text = Convert.ToString(x);
            if (progressbar_appprogress.Value >= progressbar_appprogress.Maximum) //if app finished job
            {
                timer_refreshproxylist.Stop();
                timer_load.Stop();
                timer_cleardata.Stop();
                button_skip.Enabled = false;
                label_status.Text = Resources.Text_Finished;
                button_start.Enabled = true;
                button_stop.Enabled = false;
                button_clearproxies.Enabled = true;
                button_clearurl.Enabled = true;
                button_clearuseragents.Enabled = false;
                progressbar_appprogress.Value = 0;
            }

            list_proxies.TopIndex = 0;
            list_agents.TopIndex = 0;
            if (list_proxies.SelectedIndex == list_proxies.Items.Count - 1
            ) //if reach last proxy in list go to top and change url
            {
                list_proxies.SelectedIndex = list_proxies.TopIndex;
                if (list_urls.Items.Count == 1)
                {
                    string messageBoxTitle = "Finished";
                    string messageBoxContent =
                        "Program has finished every URL in the list using the selected proxies and useragents.";
                    DialogResult dialogResult =
                        MessageBox.Show(messageBoxContent, messageBoxTitle, MessageBoxButtons.OK);
                    label_status.Text = Resources.Text_Finished;
                    timer_load.Stop();
                    timer_refreshproxylist.Stop();
                    timer_cleardata.Stop();
                }
                else if (list_urls.SelectedIndex == list_urls.Items.Count - 1
                ) //if url is last in the list, stop program is finished
                {
                    string messageBoxTitle = "Finished";
                    string messageBoxContent =
                        "Program has finished every URL in the list using the selected proxies and useragents.";
                    DialogResult dialogResult =
                        MessageBox.Show(messageBoxContent, messageBoxTitle, MessageBoxButtons.OK);
                    label_status.Text = Resources.Text_Finished;
                    timer_load.Stop();
                    timer_refreshproxylist.Stop();
                    timer_cleardata.Stop();
                }
                else if (list_agents.Items.Count == 0) //if agents are empty set default
                {
                    SUserAgent =
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:52.0) Gecko/20100101 Firefox/52.0.1 Waterfox/52.0.1";
                    GeckoPreferences.User["general.useragent.override"] = SUserAgent;
                    list_urls.SelectedIndex = (list_urls.SelectedIndex + 1);
                }
                else if (list_agents.SelectedIndex == list_agents.Items.Count - 1)
                {
                    list_agents.SelectedIndex = list_agents.TopIndex;
                    list_urls.SelectedIndex = (list_urls.SelectedIndex + 1);
                    Agent = list_agents.GetItemText(list_agents.SelectedItem);
                    GeckoPreferences.User["general.useragent.override"] = Agent;
                }
                else
                {
                    list_urls.SelectedIndex = (list_urls.SelectedIndex + 1);

                    Agent = list_agents.GetItemText(list_agents.SelectedItem);
                    GeckoPreferences.User["general.useragent.override"] = Agent;
                }
            }
            else
            {
                list_proxies.SelectedIndex = (list_proxies.SelectedIndex + 1);
                if (list_agents.Items.Count == 0)
                {
                    SUserAgent =
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:52.0) Gecko/20100101 Firefox/52.0.1 Waterfox/52.0.1";
                    GeckoPreferences.User["general.useragent.override"] = SUserAgent;
                }
                else
                {
                    list_agents.SelectedIndex = (list_agents.SelectedIndex + 1);

                    Agent = list_agents.GetItemText(list_agents.SelectedItem);
                    GeckoPreferences.User["general.useragent.override"] = Agent;
                }
            }

            if (referral_txtdrop.Text != Resources.Text_Referrer)
            {
                Referrer = referral_txtdrop.Text;
            }

            Url = list_urls.GetItemText(list_urls.SelectedItem);
            Agent = list_agents.GetItemText(list_agents.SelectedItem);
            _delimiters = new[] { ':', '@' };
            _value = list_proxies.SelectedItem.ToString();
            _proxy = _value.Split(_delimiters, StringSplitOptions.RemoveEmptyEntries); //split proxies at  :

            // proxy settings
            GeckoPreferences.Default["network.proxy.type"] = 1;
            GeckoPreferences.Default["network.proxy.http"] = _proxy[0];
            GeckoPreferences.Default["network.proxy.http_port"] = Convert.ToInt32(_proxy[1]);
            GeckoPreferences.Default["network.proxy.ssl"] = _proxy[0];
            GeckoPreferences.Default["network.proxy.ssl_port"] = Convert.ToInt32(_proxy[1]);
            GeckoPreferences.Default["network.proxy.remote_dns"] = true;
            GeckoPreferences.Default["network.proxy.http_remote_dns"] = true;
            GeckoPreferences.Default["network.proxy.ssl_remote_dns"] = true;
            GeckoPreferences.User["network.proxy.type"] = 1;
            GeckoPreferences.User["network.proxy.http"] = _proxy[0];
            GeckoPreferences.User["network.proxy.http_port"] = Convert.ToInt32(_proxy[1]);
            GeckoPreferences.User["network.proxy.ssl"] = _proxy[0];
            GeckoPreferences.User["network.proxy.ssl_port"] = Convert.ToInt32(_proxy[1]);
            GeckoPreferences.User["network.proxy.remote_dns"] = true;
            GeckoPreferences.User["network.proxy.http_remote_dns"] = true;
            GeckoPreferences.User["network.proxy.ssl_remote_dns"] = true;
            GeckoPreferences.User["general.useragent.override"] = Agent;
            geckoWebBrowser1.Refresh();
            geckoWebBrowser1.Reload();
            geckoWebBrowser1.Navigate(Url,
                (GeckoLoadFlags.ReplaceHistory | GeckoLoadFlags.BypassCache | GeckoLoadFlags.BypassProxy), Referrer,
                null, null); //home page
            Onepage = 0;

            timer_load.Interval = (int)numupdown_interval.Value;
        }

        private void timer_cleardata_Tick(object sender, EventArgs e) // clear cache cookies history etc  
        {
            nsIBrowserHistory historyMan = Xpcom.GetService<nsIBrowserHistory>(Contracts.NavHistoryService);
            historyMan = Xpcom.QueryInterface<nsIBrowserHistory>(historyMan);

            // Fix for CS1061: Replace the non-existent 'RemoveAllPages' method with 'RemovePagesByTimeframe'  
            long beginTime = 0; // Start of the epoch  
            long endTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(); // Current time in milliseconds  

            historyMan.RemovePagesByTimeframe(beginTime, endTime);

            var cookieMan = Xpcom.GetService<nsICookieManager>("@mozilla.org/cookiemanager;1");
            cookieMan = Xpcom.QueryInterface<nsICookieManager>(cookieMan);
            cookieMan.RemoveAll();

            // Clear image and cache  
            ImageCache.ClearCache(true);
            ImageCache.ClearCache(false);

            var cache = Xpcom.GetService<nsICacheStorageService>("@mozilla.org/netwerk/cache-storage-service;1");
            try
            {
                cache.Clear();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not clear cache:", ex);
            }

            var icache = Xpcom.GetService<imgITools>("@mozilla.org/image/tools;1").GetImgCacheForDocument(null);
            try
            {
                icache.ClearCache(false);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not clear image cache:", ex);
            }

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

            label_status.Text = Resources.Text_Stopped;
            timer_refreshproxylist.Stop();
            timer_load.Stop();
            timer_cleardata.Stop();
            label_status.Text = Resources.Text_Stopped;
            progressbar_appprogress.Value = 0;
        }

        #endregion Main function

        #region URL functions

        private void button_addurl_Click(object sender, EventArgs e) //add link to list
        {
            list_urls.Items.Add(textbox_url.Text);
            if (list_urls.Items.Count >= 1)
            {
                button_clearurl.Enabled = true;
            }

            textbox_url.Text = "";
        }

        private void button_loadurllist_Click(object sender, EventArgs e) //load link list
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = Resources.Text_Urls,
                InitialDirectory = "C:\\",
                Filter = Resources.Text_Text_Files,
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(openFileDialog.FileName);
                while (streamReader.Peek() > -1)
                {
                    list_urls.Items.Add(streamReader.ReadLine() ?? throw new InvalidOperationException());
                }

                streamReader.Close();
            }
            else // no file was selected in the open file dialog
            {
                string messageBoxTitle = ("You need a URL");
                string messageBoxContent = ("Enter at lease one URL and try again!");

                DialogResult dialogResult = MessageBox.Show(messageBoxContent, messageBoxTitle, MessageBoxButtons.OK);
            }

            if (list_urls.Items.Count >= 1) // enable clear urls if list has items
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

        private void textbox_url_Enter(object sender, EventArgs e) // url textbox focus entered
        {
            if (textbox_url.Text == Resources.Text_Urls_to_be_viewed)
            {
                textbox_url.Text = "";
            }
        }

        private void textbox_url_KeyDown(object sender, KeyEventArgs e) // enter adds url to list
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_addurl_Click(sender, e);
            }
        }

        private void
            textbox_url_TextChanged(object sender, EventArgs e) // enable add url button if textbox text changes
        {
            button_addurl.Enabled = true;
        }

        private void textbox_url_Leave(object sender, EventArgs e) //url textbox loses focus
        {
            if (textbox_url.Text == "")
            {
                textbox_url.Text = Resources.Text_Urls_to_be_viewed;
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

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = Resources.Text_User_Agents,
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                Filter = Resources.Text_Text_Files,
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(openFileDialog.FileName);
                while (streamReader.Peek() > -1)
                {
                    agents.Add(streamReader.ReadLine());
                }

                streamReader.Close();
                agents.Shuffle();
                list_agents.Items.AddRange(agents.ToArray());
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
            if (textbox_agent.Text == Resources.Text_Useragents_to_use)
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
                textbox_agent.Text = Resources.Text_Useragents_to_use;
                button_adduseragent.Enabled = false;
            }
        }

        #endregion Useragent functions

        #region Proxy functions

        public string Proxylistfile;

        private void button_loadproxies_Click(object sender, EventArgs e)
        {
            List<string> proxies = new List<string>();
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = Resources.Text_Proxies,
                InitialDirectory = Environment.ExpandEnvironmentVariables("C:\\Users\\% USERPROFILE %\\"),
                Filter = Resources.Text_Text_Files,
                FilterIndex = 2,
                RestoreDirectory = false
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Proxylistfile = openFileDialog.FileName;

                StreamReader streamReader = new StreamReader(Proxylistfile);
                while (streamReader.Peek() > -1)
                {
                    proxies.Add(streamReader.ReadLine());

                    label_proxiesnum.Text = Convert.ToString(proxies.Count);
                }

                streamReader.Close();
                proxies.Shuffle();
                list_proxies.Items.AddRange(proxies.ToArray());
                if ((int)numupdown_interval.Value == 0)
                {
                    timer_refreshproxylist.Interval = 3000; //default interval if user sets 0
                }
                else
                {
                    timer_refreshproxylist.Interval = (int)numupdown_interval.Value / 4;
                }
                timer_refreshproxylist.Start();
            }
            else
            {
                string messageBoxTitle = ("You need Proxies");
                string messageBoxContent = ("Enter proxies and try again!");

                DialogResult dialogResult = MessageBox.Show(messageBoxContent, messageBoxTitle, MessageBoxButtons.OK);
            }

            label_proxiesnum.Text = Convert.ToString(list_proxies.Items.Count);
            if (list_proxies.Items.Count >= 1)
            {
                button_clearproxies.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            using (var fs = new FileStream(Proxylistfile, FileMode.Open, FileAccess.Read,
                FileShare.ReadWrite | FileShare.Delete))
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
            label_currentproxnum.Text = Resources.Text_0;
            label_proxiesnum.Text = Resources.Text_0;
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
        }

        private void button_setref_Click(object sender, EventArgs e)
        {
            Referrer = referral_txtdrop.Text;
        }

        private void referral_txtdrop_Enter(object sender, EventArgs e)
        {
            if (referral_txtdrop.Text == Resources.Text_Referrer)
            {
                referral_txtdrop.Text = "";
            }
        }

        private void referral_txtdrop_TextChanged(object sender, EventArgs e)
        {
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
                referral_txtdrop.Text = Resources.Text_Referral;
            }
        }

        #endregion Coords / Referral functions

        #endregion Main Form GUI

        #region Gecko browser

        #region Browser controls

        private void button_navigate_Click(object sender, EventArgs e)
        {
            button_refresh.BackgroundImage = Resources.stop;
            geckoWebBrowser1.Navigate(textbox_navigate.Text,
                (GeckoLoadFlags.ReplaceHistory | GeckoLoadFlags.BypassCache | GeckoLoadFlags.BypassProxy), Referrer,
                null, null); //home page
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
            if(geckoWebBrowser1.IsBusy)
            {
                geckoWebBrowser1.Stop();
            }
            else
            {
                geckoWebBrowser1.Reload();
            }
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

        private void Clickad()
        {
          
                var links = geckoWebBrowser1.Document.GetElementsByTagName("div");
                foreach (GeckoHtmlElement link in links)
                {
                    if (link.GetAttribute(comboBox_autoclick.SelectedItem.ToString()) == textBox_autoclick.Text)
                    {
                        Onepage = 1;
                        if ((string) comboBox_autoclick.SelectedItem != "coordinates")
                        {

                            string content = null;
                            GeckoIFrameElement _E =
                                geckoWebBrowser1.DomDocument.GetElementsByTagName("iframe")[0] as GeckoIFrameElement;

                            var innerHTML = _E.ContentWindow.Document;


                            GeckoAnchorElement a = innerHTML.GetElementsByTagName("a").Last() as GeckoAnchorElement;

                            a.Click();
                        }
                    }
                    else
                    {
                       
                        if (link.GetAttribute(comboBox_autoclick.SelectedItem.ToString()) == textBox_autoclick.Text)
                        {
                            link.ScrollIntoView(true);
                            

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
                    }
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

        [DllImport("User32.dll", SetLastError = true)]
        public static extern int SendInput(int nInputs, ref INPUT pInputs,
            int cbSize);

        //mouse event constants
        private const int MOUSEEVENTF_LEFTDOWN = 2;

        private const int MOUSEEVENTF_LEFTUP = 4;

        //input type constant
        private const int INPUT_MOUSE = 0;

        public struct Mouseinput
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
            public Mouseinput mi;
        }

         private void timer_setcoords_Tick(object sender, EventArgs e)
        {
            _clickLocation = Cursor.Position;
            //show the location on window title
            textbox_x.Text = _clickLocation.X.ToString();
            textbox_y.Text = _clickLocation.Y.ToString();
            timer_setcoords.Stop();
        }

        private void timer_clickcoords_Tick(object sender, EventArgs e)
        {
            //set cursor position to memorized location
            Cursor.Position = _clickLocation;
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
        }

          /*        private void timer_setcoords2_Tick(object sender, EventArgs e)
        {
            _clickLocation2 = Cursor.Position;
            //show the location on window title
            textbox_x2.Text = _clickLocation2.X.ToString();
            textbox_y2.Text = _clickLocation2.Y.ToString();
            timer_setcoords2.Stop();
        }

        private void timer_clickcoords2_Tick(object sender, EventArgs e)
        {
            //set cursor position to memorized location
            Cursor.Position = _clickLocation2;
            //set up the INPUT struct and fill it for the mouse down
            Input i = new Input
            {
                Type = InputMouse,
                Mi =
                {
                    Dx = 0,
                    Dy = 0,
                    DwFlags = MouseeventfLeftdown,
                    DwExtraInfo = IntPtr.Zero,
                    MouseData = 0,
                    Time = 0
                }
            };
            //send the input
            SendInput(1, ref i, Marshal.SizeOf(i));
            //set the INPUT for mouse up and send it
            i.Mi.DwFlags = MouseeventfLeftup;
            SendInput(1, ref i, Marshal.SizeOf(i));

            timer_clickcoords2.Stop();
        }

        private void Button_getcoords2_Click(object sender, EventArgs e)
        {
            timer_setcoords2.Interval = 5000;
            timer_setcoords2.Start();
        }
                  /*
         private void autocaptcha()
         {
 

             var audiobutton = geckoWebBrowser2.Document.GetElementsByClassName("rc-button goog-inline-block rc-button-audio");
             Gecko.DOM.GeckoButtonElement next = new Gecko.DOM.GeckoButtonElement(audiobutton.ElementAt(1).DomObject);
             next.Click();
         }*/

        #endregion Browser automation

        #region Browser events

        private void geckoWebBrowser1_CreateWindow(object sender, GeckoCreateWindowEventArgs e)
        {

            // Full Screen
            var iframe = geckoWebBrowser1.Document.GetHtmlElementById("aads");
            // var close = "data:text/html,<script>self.close()</script>";


            Rectangle rect = Screen.GetWorkingArea(this);

            geckoWebBrowser2.Navigating += geckoWebBrowser2_Navigating;
            geckoWebBrowser2.DocumentCompleted += geckoWebBrowser2_DocumentCompleted;
            geckoWebBrowser2.Dock = DockStyle.Fill;
            geckoWebBrowser2.CreateControl();

            //TabPage tab1 = new TabPage("New WebBrowser");
            //tabBrowser.TabPages.Add(tab1);
            _form2.Controls.Add(geckoWebBrowser2);
            _form2.CreateControl();

            if (list_proxies.Items.Count == 0)
            {
                
                geckoWebBrowser2.Navigate(e.ToString());
                e.InitialWidth = Width;
                e.InitialHeight = Height;
            }
            else
            {
                GeckoPreferences.Default["network.proxy.type"] = 1;
                GeckoPreferences.Default["network.proxy.http"] = _proxy[0];
                GeckoPreferences.Default["network.proxy.http_port"] = Convert.ToInt32(_proxy[1]);
                GeckoPreferences.Default["network.proxy.ssl"] = _proxy[0];
                GeckoPreferences.Default["network.proxy.ssl_port"] = Convert.ToInt32(_proxy[1]);
                GeckoPreferences.Default["network.proxy.remote_dns"] = true;
                GeckoPreferences.Default["network.proxy.http_remote_dns"] = true;
                GeckoPreferences.Default["network.proxy.ssl_remote_dns"] = true;
                GeckoPreferences.User["network.proxy.type"] = 1;
                GeckoPreferences.User["network.proxy.http"] = _proxy[0];
                GeckoPreferences.User["network.proxy.http_port"] = Convert.ToInt32(_proxy[1]);
                GeckoPreferences.User["network.proxy.ssl"] = _proxy[0];
                GeckoPreferences.User["network.proxy.ssl_port"] = Convert.ToInt32(_proxy[1]);
                GeckoPreferences.User["network.proxy.remote_dns"] = true;
                GeckoPreferences.User["network.proxy.http_remote_dns"] = true;
                GeckoPreferences.User["network.proxy.ssl_remote_dns"] = true;
                GeckoPreferences.Default["browser.cache.disk.enable"] = false;
                GeckoPreferences.User["browser.cache.disk.enable"] = false;
                GeckoPreferences.User["general.useragent.override"] = Agent;

                geckoWebBrowser2.Navigate(e.ToString());
                if (iframe != null)
                {
                    e.InitialWidth = Width;
                    e.InitialHeight = Height;
                }
                else
                {
                    e.InitialWidth = Width;
                    e.InitialHeight = Height;
                }
            }
        }   

        private void CloseWindow()
        {
            // retrieve the handler of the window
            int iHandle = FindWindow("MozillaWindowClass", "MUIPRT");
            if (iHandle > 0)
            {
                // close the window using API
                SendMessage(iHandle, WmSyscommand, ScClose, 0);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        private const UInt32 WmClose = 0x0010;

        private void CloseWindow(IntPtr hwnd)
        {
            SendMessage(hwnd, WmClose, IntPtr.Zero, IntPtr.Zero);
        }

        private void geckoWebBrowser2_DocumentCompleted(object sender, GeckoDocumentCompletedEventArgs e)
        {
            geckoWebBrowser1_DocumentCompleted(geckoWebBrowser2, e);
            /*
            var iframe = geckoWebBrowser1.Document.GetHtmlElementById("aads");

            if (iframe == null)
            {
                List<IntPtr> chromeWindows = WindowsFinder("MozillaWindowClass", "MUIPRT");
                foreach (IntPtr windowHandle in chromeWindows)
                {
                    int iHandle = FindWindow("MozillaWindowClass", "MUIPRT");
                    int length = GetWindowTextLength(windowHandle);
                    StringBuilder sb = new StringBuilder(length + 1);
                    GetWindowText(windowHandle, sb, sb.Capacity);
                    SendMessage(iHandle, WmSyscommand, ScClose, 0);
                    CloseWindow(windowHandle);
                }
            }
                      */
            //else{
            //    autocaptcha()}
        }

        private void geckoWebBrowser1_DocumentCompleted(object sender, GeckoDocumentCompletedEventArgs e)
        {
           
                statusstip_browser.Visible = false;
                button_refresh.BackgroundImage = Resources.re;
            
            label_statusbrowser.Text = Resources.Text_Done + geckoWebBrowser1.StatusText;
            textbox_navigate.Text = geckoWebBrowser1.Url.AbsoluteUri;
            progressbar_browser.Value = 0;
            if (geckoWebBrowser1.Document.Title == "403 Forbidden" ||
                geckoWebBrowser1.Document.TextContent == "429 Too Many Requests")
            {
                button_skip.PerformClick();
            }
            else
            {
                if (!geckoWebBrowser1.IsBusy && Onepage == 0)
                {
                    Clickad();
                }
            }
        }

        private void geckoWebBrowser1_DOMContentLoaded(object sender, DomEventArgs e)
        {
            
                statusstip_browser.Visible = false;
                button_refresh.BackgroundImage = Resources.re;
            
            button_refresh.BackgroundImage = Resources.re;
            progressbar_browser.Value = 0;
        }

        private void geckoWebBrowser1_LocationChanged(object sender, EventArgs e)
        {
            if (geckoWebBrowser1.IsBusy)
            {
                statusstip_browser.Visible = true;
                button_refresh.BackgroundImage = Resources.stop;
            }
            else
            {
                statusstip_browser.Visible = false;
                button_refresh.BackgroundImage = Resources.re;
            }
            textbox_navigate.Text = geckoWebBrowser1.Url.AbsoluteUri;
            label_statusbrowser.Text = Resources.Text_Moved + geckoWebBrowser1.StatusText;
        }

        private void geckoWebBrowser1_Load(object sender, DomEventArgs e)
        {
            if (geckoWebBrowser1.IsBusy)
            {
                statusstip_browser.Visible = true;
                button_refresh.BackgroundImage = Resources.stop;
            }
            else
            {
                statusstip_browser.Visible = false;
                button_refresh.BackgroundImage = Resources.re;
            }
            textbox_navigate.Text = geckoWebBrowser1.Url.AbsoluteUri;
            label_statusbrowser.Text = geckoWebBrowser1.StatusText;
            button_refresh.BackgroundImage = Resources.stop;
            //  clickad();
        }

        private void geckoWebBrowser1_MouseHover(object sender, EventArgs e)
        {
            label_statusbrowser.Text = geckoWebBrowser1.StatusText;
        }

        private void geckoWebBrowser1_Navigating(object sender, GeckoNavigatingEventArgs e)
        {
            if (geckoWebBrowser1.IsBusy)
            {
             statusstip_browser.Visible = true;
                button_refresh.BackgroundImage = Resources.stop;
            }
            else
            {
                statusstip_browser.Visible = false;
                button_refresh.BackgroundImage = Resources.re; 
            }

            if (ActiveControl == geckoWebBrowser1)
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
            else if (ActiveControl == geckoWebBrowser2)
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
            if (geckoWebBrowser1.IsBusy)
            {
                statusstip_browser.Visible = true;
                button_refresh.BackgroundImage = Resources.stop;
            }
            else
            {
                statusstip_browser.Visible = false;
                button_refresh.BackgroundImage = Resources.re;
            }
            label_statusbrowser.Text = Resources.Text_Error_loading;
        }

        private void geckoWebBrowser1_Navigated(object sender, GeckoNavigatedEventArgs e)
        {
            //clickad();
            label_statusbrowser.Text = Resources.Text_Done;
            progressbar_browser.Value = 0;
            button_refresh.BackgroundImage = Resources.re;
         
                statusstip_browser.Visible = false;
                button_refresh.BackgroundImage = Resources.re;
            

        }

        private void geckoWebBrowser2_Navigating(object sender, GeckoNavigatingEventArgs e)
        {

            //throw new NotImplementedException();
            geckoWebBrowser1_Navigating(geckoWebBrowser2, e);
        }

        private void geckoWebBrowser1_ProgressChanged(object sender, GeckoProgressEventArgs e)
        {
            label_statusbrowser.Text = geckoWebBrowser1.StatusText;
            textbox_navigate.Text = geckoWebBrowser1.Url.AbsoluteUri;
            progressbar_browser.Maximum = 100;
            progressbar_browser.Minimum = 0;
            if(geckoWebBrowser1.IsBusy)
            {
                button_refresh.BackgroundImage = Resources.stop;
                        statusstip_browser.Visible = true;

            }
            else
            {
                button_refresh.BackgroundImage = Resources.re;
                statusstip_browser.Visible = false;

            }

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
                        label_statusbrowser.Text = Resources.Text_Done_Loading;
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
                    Console.WriteLine(ex.Message);
                }
        }

        private void geckoWebBrowser1_Redirecting(object sender, GeckoRedirectingEventArgs e)
        {
            if (geckoWebBrowser1.IsBusy)
            {
                statusstip_browser.Visible = true;
                button_refresh.BackgroundImage = Resources.stop;
            }
            else
            {
                statusstip_browser.Visible = false;
                button_refresh.BackgroundImage = Resources.re;
            }
            label_statusbrowser.Text = Resources.Text_Redirecting + geckoWebBrowser1.StatusText;
            textbox_navigate.Text = geckoWebBrowser1.Url.AbsoluteUri;
        }

        #endregion Browser events

        #endregion Gecko browser

        private void button_skip_Click(object sender, EventArgs e)
        {
            timer_load.Interval = 100;
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            textbox_navigate.Width = (button_navigate.Bounds.Left - 2) - textbox_navigate.Bounds.Left;
        }

        private void button_goback_MouseEnter(object sender, EventArgs e)
        {
            button_goback.BackgroundImage = Resources.backhov;
        }

        private void button_goback_MouseDown(object sender, MouseEventArgs e)
        {
            button_goback.BackgroundImage = Resources.backd;
        }

        private void button_goback_MouseUp(object sender, MouseEventArgs e)
        {
            if ((Cursor.Position.X >= button_goback.ContentRectangle.X) &&
                (Cursor.Position.X <= (button_goback.ContentRectangle.X + button_goback.ContentRectangle.Width)) &&
                (Cursor.Position.Y >= button_goback.ContentRectangle.Y) && (Cursor.Position.Y <
                                                                            (button_goback.ContentRectangle.Y +
                                                                             button_goback.ContentRectangle.Height)))
                button_goback.BackgroundImage = Resources.backhov;
            else
                button_goback.BackgroundImage = Resources.back;
        }

        private void button_goback_MouseLeave(object sender, EventArgs e)
        {
            button_goback.BackgroundImage = Resources.back;
        }

        private void button_goback_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Location.X >= button_goback.ContentRectangle.X) && (e.Location.X <= (button_goback.ContentRectangle.X + button_goback.ContentRectangle.Width)) && (e.Location.Y >= button_goback.ContentRectangle.Y) && (e.Location.Y < (button_goback.ContentRectangle.Y + button_goback.ContentRectangle.Height)))
                button_goback.BackgroundImage = Resources.backhov;
            else
                button_goback.BackgroundImage = Resources.back;
        }

        private void geckoWebBrowser1_CanGoBackChanged(object sender, EventArgs e)
        {
            button_goback.Enabled = geckoWebBrowser1.CanGoBack;
        }

        private void geckoWebBrowser1_CanGoForwardChanged(object sender, EventArgs e)
        {
            button_goforward.Enabled = geckoWebBrowser1.CanGoForward;
        }

        private void button_goforward_MouseEnter(object sender, EventArgs e)
        {
            button_goforward.BackgroundImage = Resources.fwdhov;
        }

        private void button_goforward_MouseDown(object sender, MouseEventArgs e)
        {
            button_goforward.BackgroundImage = Resources.fwdd;
        }

        private void button_goforward_MouseUp(object sender, MouseEventArgs e)
        {
            if ((Cursor.Position.X >= button_goforward.ContentRectangle.X) &&
                (Cursor.Position.X <= (button_goforward.ContentRectangle.X + button_goforward.ContentRectangle.Width)) &&
                (Cursor.Position.Y >= button_goforward.ContentRectangle.Y) && (Cursor.Position.Y <
                                                                               (button_goforward.ContentRectangle.Y +
                                                                                   button_goforward.ContentRectangle.Height)))
                button_goforward.BackgroundImage = Resources.fwdhov;
            else
                button_goforward.BackgroundImage = Resources.fwd;
        }

        private void button_goforward_MouseLeave(object sender, EventArgs e)
        {
            button_goforward.BackgroundImage = Resources.fwd;
        }

        private void button_goforward_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Location.X >= button_goforward.ContentRectangle.X) && (e.Location.X <= (button_goforward.ContentRectangle.X + button_goforward.ContentRectangle.Width)) && (e.Location.Y >= button_goforward.ContentRectangle.Y) && (e.Location.Y < (button_goforward.ContentRectangle.Y + button_goforward.ContentRectangle.Height)))
                button_goforward.BackgroundImage = Resources.fwdhov;
            else
                button_goforward.BackgroundImage = Resources.fwd;
        }

        private void button_refresh_MouseEnter(object sender, EventArgs e)
        {
            if (geckoWebBrowser1.IsBusy)
                button_refresh.BackgroundImage = Resources.stophov;
            else
                button_refresh.BackgroundImage = Resources.refhov;
        }

        private void button_refresh_MouseDown(object sender, MouseEventArgs e)
        {
            if (geckoWebBrowser1.IsBusy)
                button_refresh.BackgroundImage = Resources.stopd;
            else
                button_refresh.BackgroundImage = Resources.refd;
        }

        private void button_refresh_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != 0)
            {
                if ((Cursor.Position.X >= button_refresh.ContentRectangle.X) &&
                    (Cursor.Position.X <= (button_refresh.ContentRectangle.X + button_refresh.ContentRectangle.Width)) &&
                    (Cursor.Position.Y >= button_refresh.ContentRectangle.Y) && (Cursor.Position.Y <
                        (button_refresh.ContentRectangle.Y +
                         button_refresh.ContentRectangle.Height)))
                    button_refresh.BackgroundImage = Resources.stophov;
                else
                    button_refresh.BackgroundImage = Resources.stop;
            }
            else
            {
                if ((Cursor.Position.X >= button_refresh.ContentRectangle.X) &&
                    (Cursor.Position.X <=
                     (button_refresh.ContentRectangle.X + button_refresh.ContentRectangle.Width)) &&
                    (Cursor.Position.Y >= button_refresh.ContentRectangle.Y) && (Cursor.Position.Y <
                        (button_refresh.ContentRectangle.Y +
                         button_refresh.ContentRectangle.Height)))
                    button_refresh.BackgroundImage = Resources.refhov;
                else
                    button_refresh.BackgroundImage = Resources.re;
            }
        }

        private void button_refresh_MouseLeave(object sender, EventArgs e)
        {
            if (geckoWebBrowser1.IsBusy)
                button_refresh.BackgroundImage = Resources.stop;
            else
                button_refresh.BackgroundImage = Resources.re;
        }

        private void button_refresh_MouseMove(object sender, MouseEventArgs e)
        {
            if (geckoWebBrowser1.IsBusy)
            {
                if ((e.Location.X >= button_refresh.ContentRectangle.X) &&
                    (e.Location.X <= (button_refresh.ContentRectangle.X + button_refresh.ContentRectangle.Width)) &&
                    (e.Location.Y >= button_refresh.ContentRectangle.Y) && (e.Location.Y <
                                                                            (button_refresh.ContentRectangle.Y +
                                                                             button_refresh.ContentRectangle.Height)))
                    button_refresh.BackgroundImage = Resources.stophov;
                else
                    button_refresh.BackgroundImage = Resources.stop;
            }
            else
            {
                if ((e.Location.X >= button_refresh.ContentRectangle.X) &&
                    (e.Location.X <= (button_refresh.ContentRectangle.X + button_refresh.ContentRectangle.Width)) &&
                    (e.Location.Y >= button_refresh.ContentRectangle.Y) && (e.Location.Y <
                                                                            (button_refresh.ContentRectangle.Y +
                                                                             button_refresh.ContentRectangle.Height)))
                    button_refresh.BackgroundImage = Resources.refhov;
                else
                    button_refresh.BackgroundImage = Resources.re;
            }
        }

        private void button_navigate_MouseEnter(object sender, EventArgs e)
        {
            button_navigate.BackgroundImage = Resources.gohov;
        }

        private void button_navigate_MouseDown(object sender, MouseEventArgs e)
        {
            button_navigate.BackgroundImage = Resources.god;
        }

        private void button_navigate_MouseUp(object sender, MouseEventArgs e)
        {
            if ((Cursor.Position.X >= button_navigate.ContentRectangle.X) &&
                (Cursor.Position.X <= (button_navigate.ContentRectangle.X + button_navigate.ContentRectangle.Width)) &&
                (Cursor.Position.Y >= button_navigate.ContentRectangle.Y) && (Cursor.Position.Y <
                                                                            (button_navigate.ContentRectangle.Y +
                                                                             button_navigate.ContentRectangle.Height)))
                button_navigate.BackgroundImage = Resources.gohov;
            else
                button_navigate.BackgroundImage = Resources.go;
        }

        private void button_navigate_MouseLeave(object sender, EventArgs e)
        {
            button_navigate.BackgroundImage = Resources.go;
        }

        private void button_navigate_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Location.X >= button_navigate.ContentRectangle.X) && (e.Location.X <= (button_navigate.ContentRectangle.X + button_navigate.ContentRectangle.Width)) && (e.Location.Y >= button_navigate.ContentRectangle.Y) && (e.Location.Y < (button_navigate.ContentRectangle.Y + button_navigate.ContentRectangle.Height)))
                button_navigate.BackgroundImage = Resources.gohov;
            else
                button_navigate.BackgroundImage = Resources.go;
        }

        private bool autoclick_textbox_clicked = false;

        private void textBox_autoclick_Click(object sender, EventArgs e)
        {
            if (!autoclick_textbox_clicked)
            {
                textBox_autoclick.Text = "";
                autoclick_textbox_clicked = true;
            }
        }


        private void referral_txtdrop_doubleclick(object sender, EventArgs e)
        {
            referral_txtdrop.Text = "";               
        }

        private void comboBox_autoclick_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((string)comboBox_autoclick.SelectedItem == "coordinates")
               
            {
                textbox_x.Visible = true;
                textbox_y.Visible = true;
                button_getcoords.Visible = true;
            }
            else
            {
                textbox_x.Visible = false;
                textbox_y.Visible = false;
                button_getcoords.Visible = false;
                
            }
        }

        private void panel_referral_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox_autoclick_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            geckoWebBrowser1.Navigate("about:blank", GeckoLoadFlags.None, null, null, null);
            if (geckoWebBrowser2 != null)
            {
                geckoWebBrowser2.Navigate("about:blank", GeckoLoadFlags.None, null, null, null);
            }
            geckoWebBrowser1.Stop();
            if (geckoWebBrowser2 != null)
            {
                geckoWebBrowser2.Stop();
            }


        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();

        }

        private void geckoWebBrowser1_WindowClosed(object sender, EventArgs e)
        {
            geckoWebBrowser1.Navigate("about:blank", GeckoLoadFlags.None, null, null, null);
            if (geckoWebBrowser2 != null)
            {
                geckoWebBrowser2.Navigate("about:blank", GeckoLoadFlags.None, null, null, null);
            }
            geckoWebBrowser1.Stop();
            if (geckoWebBrowser2 != null)
            {
                geckoWebBrowser2.Stop();
            }
        }

        private void geckoWebBrowser1_ReadyStateChange(object sender, DomEventArgs e)
        {
            if (!geckoWebBrowser1.IsBusy)
            {
                button_refresh.BackgroundImage = Resources.re;
                label_statusbrowser.Text = Resources.Text_Done_Loading;
                progressbar_browser.Value = 0;
                statusstip_browser.Visible = false;
            }
            else
            {
                button_refresh.BackgroundImage = Resources.stop;
                label_statusbrowser.Text = geckoWebBrowser1.Document.ReadyState.ToString();
                progressbar_browser.Value = 0;
                statusstip_browser.Visible = true;
            }
        }

        private void geckoWebBrowser1_RequestProgressChanged(object sender, GeckoRequestProgressEventArgs e)
        {
            if (geckoWebBrowser1.IsBusy)
            {
                statusstip_browser.Visible = true;
                button_refresh.BackgroundImage = Resources.stop;
            }
            else
            {
                statusstip_browser.Visible = false;
                button_refresh.BackgroundImage = Resources.re;
            }
        }
    }
}