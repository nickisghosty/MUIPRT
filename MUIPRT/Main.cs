using Gecko;
using Gecko.Events;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#region Main Form GUI

namespace MUIPRT
{
    public partial class Main : Form
    {   //variables
        public string referrer;
        public string agent;
        public string sUserAgent;
        public string url;
        public string[] proxy;
        private Point clickLocation = new Point(0, 0);

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
            geckoWebBrowser1.Navigate("http://google.com", Gecko.GeckoLoadFlags.BypassHistory, referrer, null, null); //home page
            textbox_navigate.Items.Equals(geckoWebBrowser1.History);

            // disable buttons
            button_clearurl.Enabled = false;
            button_clearproxies.Enabled = false;
            button_clearuseragents.Enabled = false;
            button_addref.Enabled = false;
            button_addurl.Enabled = false;
            button_adduseragent.Enabled = false;
            button_setref.Enabled = false;
            button_stop.Enabled = false;
        }

        #region Main function

        private void button_start_Click(object sender, EventArgs e)
        {   // progress bar for application
            progressbar_appprogress.Maximum = (int)numupdown_views.Value;
            progressbar_appprogress.Minimum = 0;
            button_stop.Enabled = true;
            button_start.Enabled = false;

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
                timer_loadurl.Start();
                int interval = (int)numupdown_interval.Value;
                timer_changeproxy.Interval = interval;
                timer_changeproxy.Start();
                list_urls.SelectedIndex = (list_urls.SelectedIndex + 1);
                list_proxies.SelectedIndex = (list_proxies.SelectedIndex + 1);
                label_status.Text = "Running";
                if (list_agents.Items.Count == 0)
                {
                    sUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:52.0) Gecko/20100101 Firefox/52.0.1 Waterfox/52.0.1";
                    GeckoPreferences.User["general.useragent.override"] = sUserAgent;
                }
                else
                {
                    list_agents.SelectedIndex = (list_agents.SelectedIndex + 1);
                    agent = list_agents.GetItemText(list_agents.SelectedItem);
                }
            }
            else if (list_urls.SelectedIndex == list_urls.SelectedIndex - 1)   // if selected url is the last url in list
            {
                url = list_urls.GetItemText(list_urls.SelectedItem);
                agent = list_agents.GetItemText(list_agents.SelectedItem);

                GeckoPreferences.User["general.useragent.override"] = agent;
                timer_loadurl.Start();
                int interval = (int)numupdown_interval.Value;
                timer_changeproxy.Interval = interval;
                timer_changeproxy.Start();
                list_proxies.SelectedIndex = (list_proxies.SelectedIndex + 1);
                label_status.Text = "Running";
                if (list_agents.Items.Count == 0) // if user agents list is empty set a default user agent
                {
                    sUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:52.0) Gecko/20100101 Firefox/52.0.1 Waterfox/52.0.1";
                    GeckoPreferences.User["general.useragent.override"] = sUserAgent;
                }
                else  // if user agents list is full move down the list and set the useragent
                {
                    list_agents.SelectedIndex = (list_agents.SelectedIndex + 1);
                    GeckoPreferences.User["general.useragent.override"] = agent;
                }
            }
            else //proxies list is empty
            {
                string MessageBoxTitle = ("You need Proxies");
                string MessageBoxContent = ("Enter proxies and try again!");

                DialogResult dialogResult = MessageBox.Show(MessageBoxContent, MessageBoxTitle, MessageBoxButtons.OK);
            }
        }

        private void timer_loadurl_Tick(object sender, EventArgs e)  // navigate
        {
            url = list_urls.GetItemText(list_urls.SelectedItem);
            agent = list_agents.GetItemText(list_agents.SelectedItem);
            referrer = referral_txtdrop.Text;

            proxy = list_proxies.SelectedItem.ToString().Split(":".ToCharArray());       //split proxies at  :
            if (proxy.Length != 2)                   //if proxies arent in the ip : port format
            {
                throw new Exception("Wrong format");
            }
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
            geckoWebBrowser1.Navigate(url, Gecko.GeckoLoadFlags.BypassHistory, referrer, null, null);
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

            timer_cleardata.Stop();
        }

        private void timer_changeproxy_Tick(object sender, EventArgs e) //move down the proxy list
        {
            progressbar_appprogress.Value++;
            if (progressbar_appprogress.Value >= progressbar_appprogress.Maximum)      //if app finished job
            {
                timer_changeproxy.Stop();
                timer_loadurl.Stop();
                timer_cleardata.Stop();
                label_status.Text = "Finished!";
                button_start.Enabled = true;
                button_stop.Enabled = false;
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
                    timer_changeproxy.Stop();
                    timer_loadurl.Stop();
                    timer_cleardata.Stop();
                }
                else if (list_urls.SelectedIndex == list_urls.Items.Count - 1)    //if url is last in the list, stop program is finished
                {
                    string MessageBoxTitle = "Finished";
                    string MessageBoxContent = "Program has finished every URL in the list using the selected proxies and useragents.";
                    DialogResult dialogResult = MessageBox.Show(MessageBoxContent, MessageBoxTitle, MessageBoxButtons.OK);
                    label_status.Text = "Finished!";
                    timer_changeproxy.Stop();
                    timer_loadurl.Stop();
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
            timer_loadurl.Interval = ((int)numupdown_interval.Value) / 2;
            timer_cleardata.Interval = ((int)numupdown_interval.Value) / 2;
            timer_loadurl.Start();
        }

        private void button_stop_Click(object sender, EventArgs e) //stop running
        {
            button_stop.Enabled = false;
            button_start.Enabled = true;
            label_status.Text = "Stopped.";
            timer_changeproxy.Stop();
            timer_loadurl.Stop();
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
                    this.list_agents.Items.Add(streamReader.ReadLine());
                }

                streamReader.Close();
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

        private void button_loadproxies_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Proxies",
                InitialDirectory = Environment.ExpandEnvironmentVariables("C:\\Users\\% USERPROFILE %\\"),
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
                    this.list_proxies.Items.Add(streamReader.ReadLine());
                    this.label_proxiesnum.Text = Convert.ToString(list_proxies.Items.Count);
                }

                streamReader.Close();
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

        private void button_clearproxies_Click(object sender, EventArgs e)
        {
            list_proxies.Items.Clear();
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
            geckoWebBrowser1.Navigate(textbox_navigate.Text, Gecko.GeckoLoadFlags.BypassHistory, referrer, null, null);
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
            var iframe = geckoWebBrowser1.Document.GetHtmlElementById("aads") as Gecko.DOM.GeckoIFrameElement;
            if (iframe != null)
            {
                geckoWebBrowser1.Navigate("javascript:$('#aads')[0].scrollIntoView();");

                timer_clickcoords.Interval = 2000;

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
        }

        #endregion Browser automation

        #region Browser events

        private void geckoWebBrowser1_CreateWindow(object sender, GeckoCreateWindowEventArgs e)
        {
            // Full Screen

            GeckoWebBrowser geckoWebBrowser2 = new GeckoWebBrowser();

            Rectangle rect = System.Windows.Forms.Screen.GetWorkingArea(this);

            Form Form2 = new Form();
            Form2.CreateControl();

            geckoWebBrowser2.Navigating += new EventHandler<GeckoNavigatingEventArgs>(geckoWebBrowser2_Navigating);
            geckoWebBrowser2.Dock = DockStyle.Fill;
            geckoWebBrowser2.CreateControl();

            //TabPage tab1 = new TabPage("New WebBrowser");
            //tabBrowser.TabPages.Add(tab1);
            Form2.Controls.Add(geckoWebBrowser2);
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
            geckoWebBrowser2.Navigate(e.Uri);
            e.InitialWidth = rect.Width;
            e.InitialHeight = rect.Height;
        }

        private void geckoWebBrowser1_DocumentCompleted(object sender, Gecko.Events.GeckoDocumentCompletedEventArgs e)
        {
            label_statusbrowser.Text = "Done.. " + geckoWebBrowser1.StatusText;
            textbox_navigate.Text = geckoWebBrowser1.Url.AbsoluteUri;
            progressbar_browser.Value = 0;
            clickad();
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
            clickad();
        }

        private void geckoWebBrowser1_MouseHover(object sender, EventArgs e)
        {
            label_statusbrowser.Text = geckoWebBrowser1.StatusText;
        }

        private void geckoWebBrowser1_Navigating(object sender, Gecko.Events.GeckoNavigatingEventArgs e)
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

        private void geckoWebBrowser1_NavigationError(object sender, GeckoNavigationErrorEventArgs e)
        {
            label_statusbrowser.Text = "Error loading page. Check proxy settings or URL.";
        }

        private void geckoWebBrowser1_Navigated(object sender, GeckoNavigatedEventArgs e)
        {
            clickad();

            label_statusbrowser.Text = "Done.";
            progressbar_browser.Value = 0;
        }

        private void geckoWebBrowser2_Navigating(object sender, GeckoNavigatingEventArgs e)
        {
            throw new NotImplementedException();
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
                Console.WriteLine(ex.Message);
            }
        }

        private void geckoWebBrowser1_Redirecting(object sender, GeckoRedirectingEventArgs e)
        {
            label_statusbrowser.Text = "Redirecting..." + geckoWebBrowser1.StatusText;
            textbox_navigate.Text = geckoWebBrowser1.Url.AbsoluteUri;
        }

        #endregion Browser events

        #endregion Gecko browser
    }
}