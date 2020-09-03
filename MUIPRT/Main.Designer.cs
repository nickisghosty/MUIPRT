using System.Drawing;
using System.Windows.Forms;
using MUIPRT.Properties;

namespace MUIPRT
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.picture_programbanner = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel_controls = new System.Windows.Forms.Panel();
            this.button_skip = new System.Windows.Forms.Button();
            this.numupdown_interval = new System.Windows.Forms.NumericUpDown();
            this.numupdown_views = new System.Windows.Forms.NumericUpDown();
            this.label_interval = new System.Windows.Forms.Label();
            this.label_views = new System.Windows.Forms.Label();
            this.button_stop = new System.Windows.Forms.Button();
            this.button_start = new System.Windows.Forms.Button();
            this.panel_status = new System.Windows.Forms.Panel();
            this.label_statustext = new System.Windows.Forms.Label();
            this.progressbar_appprogress = new System.Windows.Forms.ProgressBar();
            this.label_status = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.panel_scrape = new System.Windows.Forms.Panel();
            this.Button_getcoords2 = new System.Windows.Forms.Button();
            this.textbox_y2 = new System.Windows.Forms.TextBox();
            this.textbox_x2 = new System.Windows.Forms.TextBox();
            this.Button_getcoords = new System.Windows.Forms.Button();
            this.textbox_y = new System.Windows.Forms.TextBox();
            this.textbox_x = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_referral = new System.Windows.Forms.Panel();
            this.button_addref = new System.Windows.Forms.Button();
            this.button_setref = new System.Windows.Forms.Button();
            this.referral_txtdrop = new System.Windows.Forms.ComboBox();
            this.referalurl_label = new System.Windows.Forms.Label();
            this.panel_urls = new System.Windows.Forms.Panel();
            this.button_clearurl = new System.Windows.Forms.Button();
            this.button_loadurllist = new System.Windows.Forms.Button();
            this.button_addurl = new System.Windows.Forms.Button();
            this.textbox_url = new System.Windows.Forms.TextBox();
            this.list_urls = new System.Windows.Forms.ListBox();
            this.panel_agents = new System.Windows.Forms.Panel();
            this.button_clearuseragents = new System.Windows.Forms.Button();
            this.button_loaduseragentslist = new System.Windows.Forms.Button();
            this.button_adduseragent = new System.Windows.Forms.Button();
            this.textbox_agent = new System.Windows.Forms.TextBox();
            this.list_agents = new System.Windows.Forms.ListBox();
            this.panel_proxies = new System.Windows.Forms.Panel();
            this.label_currentproxnum = new System.Windows.Forms.Label();
            this.list_proxies = new System.Windows.Forms.ListBox();
            this.label_proxiesformat = new System.Windows.Forms.Label();
            this.button_clearproxies = new System.Windows.Forms.Button();
            this.button_loadproxies = new System.Windows.Forms.Button();
            this.label_proxiesnum = new System.Windows.Forms.Label();
            this.label_proxies = new System.Windows.Forms.Label();
            this.outof = new System.Windows.Forms.Label();
            this.panel_browser = new System.Windows.Forms.Panel();
            this.geckoWebBrowser1 = new Gecko.GeckoWebBrowser();
            this.statusstip_browser = new System.Windows.Forms.StatusStrip();
            this.progressbar_browser = new System.Windows.Forms.ToolStripProgressBar();
            this.label_statusbrowser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.button_goback = new System.Windows.Forms.ToolStripButton();
            this.button_goforward = new System.Windows.Forms.ToolStripButton();
            this.button_refresh = new System.Windows.Forms.ToolStripButton();
            this.button_stopnav = new System.Windows.Forms.ToolStripButton();
            this.textbox_navigate = new System.Windows.Forms.ToolStripComboBox();
            this.button_navigate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.timer_cleardata = new System.Windows.Forms.Timer(this.components);
            this.timer_setcoords = new System.Windows.Forms.Timer(this.components);
            this.timer_clickcoords = new System.Windows.Forms.Timer(this.components);
            this.timer_refreshproxylist = new System.Windows.Forms.Timer(this.components);
            this.timer_load = new System.Windows.Forms.Timer(this.components);
            this.timer_setcoords2 = new System.Windows.Forms.Timer(this.components);
            this.timer_clickcoords2 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture_programbanner)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel_controls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numupdown_interval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numupdown_views)).BeginInit();
            this.panel_status.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel_scrape.SuspendLayout();
            this.panel_referral.SuspendLayout();
            this.panel_urls.SuspendLayout();
            this.panel_agents.SuspendLayout();
            this.panel_proxies.SuspendLayout();
            this.panel_browser.SuspendLayout();
            this.statusstip_browser.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.DarkRed;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetPartial;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.picture_programbanner, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.DarkRed;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.11765F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.88235F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(782, 553);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // picture_programbanner
            // 
            this.picture_programbanner.BackColor = System.Drawing.Color.CadetBlue;
            this.picture_programbanner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picture_programbanner.Image = global::MUIPRT.Properties.Resources.HEADER;
            this.picture_programbanner.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.picture_programbanner.Location = new System.Drawing.Point(7, 8);
            this.picture_programbanner.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picture_programbanner.Name = "picture_programbanner";
            this.picture_programbanner.Size = new System.Drawing.Size(768, 94);
            this.picture_programbanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture_programbanner.TabIndex = 47;
            this.picture_programbanner.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.DarkRed;
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel_browser, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.ForeColor = System.Drawing.Color.Black;
            this.tableLayoutPanel2.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 113);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(770, 434);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.DarkRed;
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel_urls, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel_agents, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel_proxies, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 238F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(760, 229);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.CadetBlue;
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.panel_controls, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel_status, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(142, 229);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // panel_controls
            // 
            this.panel_controls.BackColor = System.Drawing.Color.CadetBlue;
            this.panel_controls.Controls.Add(this.button_skip);
            this.panel_controls.Controls.Add(this.numupdown_interval);
            this.panel_controls.Controls.Add(this.numupdown_views);
            this.panel_controls.Controls.Add(this.label_interval);
            this.panel_controls.Controls.Add(this.label_views);
            this.panel_controls.Controls.Add(this.button_stop);
            this.panel_controls.Controls.Add(this.button_start);
            this.panel_controls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_controls.Location = new System.Drawing.Point(5, 5);
            this.panel_controls.Name = "panel_controls";
            this.panel_controls.Size = new System.Drawing.Size(144, 150);
            this.panel_controls.TabIndex = 0;
            // 
            // button_skip
            // 
            this.button_skip.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_skip.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_skip.FlatAppearance.BorderSize = 3;
            this.button_skip.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_skip.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_skip.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_skip.Location = new System.Drawing.Point(38, 110);
            this.button_skip.Name = "button_skip";
            this.button_skip.Size = new System.Drawing.Size(55, 30);
            this.button_skip.TabIndex = 12;
            this.button_skip.Text = "Skip";
            this.button_skip.UseVisualStyleBackColor = false;
            this.button_skip.Click += new System.EventHandler(this.button_skip_Click);
            // 
            // numupdown_interval
            // 
            this.numupdown_interval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numupdown_interval.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.numupdown_interval.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numupdown_interval.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numupdown_interval.Location = new System.Drawing.Point(56, 38);
            this.numupdown_interval.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numupdown_interval.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numupdown_interval.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numupdown_interval.Name = "numupdown_interval";
            this.numupdown_interval.Size = new System.Drawing.Size(77, 19);
            this.numupdown_interval.TabIndex = 2;
            this.numupdown_interval.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // numupdown_views
            // 
            this.numupdown_views.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numupdown_views.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.numupdown_views.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numupdown_views.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numupdown_views.Location = new System.Drawing.Point(56, 6);
            this.numupdown_views.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numupdown_views.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numupdown_views.Name = "numupdown_views";
            this.numupdown_views.Size = new System.Drawing.Size(77, 19);
            this.numupdown_views.TabIndex = 1;
            // 
            // label_interval
            // 
            this.label_interval.AutoSize = true;
            this.label_interval.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.label_interval.Location = new System.Drawing.Point(1, 41);
            this.label_interval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_interval.Name = "label_interval";
            this.label_interval.Size = new System.Drawing.Size(58, 17);
            this.label_interval.TabIndex = 11;
            this.label_interval.Text = "Interval:";
            // 
            // label_views
            // 
            this.label_views.AutoSize = true;
            this.label_views.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.label_views.Location = new System.Drawing.Point(4, 10);
            this.label_views.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_views.Name = "label_views";
            this.label_views.Size = new System.Drawing.Size(44, 17);
            this.label_views.TabIndex = 10;
            this.label_views.Text = "Views";
            // 
            // button_stop
            // 
            this.button_stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_stop.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_stop.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_stop.FlatAppearance.BorderSize = 3;
            this.button_stop.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_stop.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_stop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_stop.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.button_stop.Location = new System.Drawing.Point(73, 72);
            this.button_stop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(55, 30);
            this.button_stop.TabIndex = 8;
            this.button_stop.Text = "Stop";
            this.button_stop.UseVisualStyleBackColor = false;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // button_start
            // 
            this.button_start.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_start.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_start.FlatAppearance.BorderSize = 3;
            this.button_start.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_start.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_start.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_start.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.button_start.Location = new System.Drawing.Point(9, 72);
            this.button_start.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(55, 30);
            this.button_start.TabIndex = 7;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = false;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // panel_status
            // 
            this.panel_status.BackColor = System.Drawing.Color.CadetBlue;
            this.panel_status.Controls.Add(this.label_statustext);
            this.panel_status.Controls.Add(this.progressbar_appprogress);
            this.panel_status.Controls.Add(this.label_status);
            this.panel_status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_status.Location = new System.Drawing.Point(5, 163);
            this.panel_status.Name = "panel_status";
            this.panel_status.Size = new System.Drawing.Size(144, 61);
            this.panel_status.TabIndex = 0;
            // 
            // label_statustext
            // 
            this.label_statustext.AutoSize = true;
            this.label_statustext.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.label_statustext.Location = new System.Drawing.Point(4, 4);
            this.label_statustext.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_statustext.Name = "label_statustext";
            this.label_statustext.Size = new System.Drawing.Size(52, 17);
            this.label_statustext.TabIndex = 14;
            this.label_statustext.Text = "Status:";
            // 
            // progressbar_appprogress
            // 
            this.progressbar_appprogress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressbar_appprogress.ForeColor = System.Drawing.Color.DarkRed;
            this.progressbar_appprogress.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.progressbar_appprogress.Location = new System.Drawing.Point(3, 23);
            this.progressbar_appprogress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressbar_appprogress.Name = "progressbar_appprogress";
            this.progressbar_appprogress.Size = new System.Drawing.Size(130, 25);
            this.progressbar_appprogress.TabIndex = 0;
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.label_status.Location = new System.Drawing.Point(73, 4);
            this.label_status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(30, 17);
            this.label_status.TabIndex = 13;
            this.label_status.Text = "Idle";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.BackColor = System.Drawing.Color.CadetBlue;
            this.tableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.panel_scrape, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.panel_referral, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(610, 6);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(144, 229);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // panel_scrape
            // 
            this.panel_scrape.BackColor = System.Drawing.Color.CadetBlue;
            this.panel_scrape.Controls.Add(this.Button_getcoords2);
            this.panel_scrape.Controls.Add(this.textbox_y2);
            this.panel_scrape.Controls.Add(this.textbox_x2);
            this.panel_scrape.Controls.Add(this.Button_getcoords);
            this.panel_scrape.Controls.Add(this.textbox_y);
            this.panel_scrape.Controls.Add(this.textbox_x);
            this.panel_scrape.Controls.Add(this.label1);
            this.panel_scrape.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_scrape.Location = new System.Drawing.Point(5, 5);
            this.panel_scrape.Name = "panel_scrape";
            this.panel_scrape.Size = new System.Drawing.Size(134, 127);
            this.panel_scrape.TabIndex = 0;
            // 
            // Button_getcoords2
            // 
            this.Button_getcoords2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_getcoords2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Button_getcoords2.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Button_getcoords2.FlatAppearance.BorderSize = 3;
            this.Button_getcoords2.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.Button_getcoords2.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.Button_getcoords2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Button_getcoords2.Location = new System.Drawing.Point(75, 90);
            this.Button_getcoords2.Name = "Button_getcoords2";
            this.Button_getcoords2.Size = new System.Drawing.Size(52, 23);
            this.Button_getcoords2.TabIndex = 7;
            this.Button_getcoords2.Text = "Get";
            this.Button_getcoords2.UseVisualStyleBackColor = false;
            this.Button_getcoords2.Click += new System.EventHandler(this.Button_getcoords2_Click);
            // 
            // textbox_y2
            // 
            this.textbox_y2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_y2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.textbox_y2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textbox_y2.Location = new System.Drawing.Point(40, 90);
            this.textbox_y2.Name = "textbox_y2";
            this.textbox_y2.Size = new System.Drawing.Size(31, 16);
            this.textbox_y2.TabIndex = 6;
            // 
            // textbox_x2
            // 
            this.textbox_x2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_x2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.textbox_x2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textbox_x2.Location = new System.Drawing.Point(6, 90);
            this.textbox_x2.Name = "textbox_x2";
            this.textbox_x2.Size = new System.Drawing.Size(31, 16);
            this.textbox_x2.TabIndex = 5;
            // 
            // Button_getcoords
            // 
            this.Button_getcoords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_getcoords.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Button_getcoords.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Button_getcoords.FlatAppearance.BorderSize = 3;
            this.Button_getcoords.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.Button_getcoords.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.Button_getcoords.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Button_getcoords.Location = new System.Drawing.Point(75, 56);
            this.Button_getcoords.Name = "Button_getcoords";
            this.Button_getcoords.Size = new System.Drawing.Size(52, 23);
            this.Button_getcoords.TabIndex = 4;
            this.Button_getcoords.Text = "Get";
            this.Button_getcoords.UseVisualStyleBackColor = false;
            this.Button_getcoords.Click += new System.EventHandler(this.button_setcoords_Click);
            // 
            // textbox_y
            // 
            this.textbox_y.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_y.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.textbox_y.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textbox_y.Location = new System.Drawing.Point(40, 56);
            this.textbox_y.Name = "textbox_y";
            this.textbox_y.Size = new System.Drawing.Size(31, 16);
            this.textbox_y.TabIndex = 2;
            // 
            // textbox_x
            // 
            this.textbox_x.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_x.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.textbox_x.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textbox_x.Location = new System.Drawing.Point(6, 56);
            this.textbox_x.Name = "textbox_x";
            this.textbox_x.Size = new System.Drawing.Size(31, 16);
            this.textbox_x.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 58);
            this.label1.TabIndex = 0;
            this.label1.Text = "Auto click at coord (x. y)  on page load";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel_referral
            // 
            this.panel_referral.BackColor = System.Drawing.Color.CadetBlue;
            this.panel_referral.Controls.Add(this.button_addref);
            this.panel_referral.Controls.Add(this.button_setref);
            this.panel_referral.Controls.Add(this.referral_txtdrop);
            this.panel_referral.Controls.Add(this.referalurl_label);
            this.panel_referral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_referral.Location = new System.Drawing.Point(5, 140);
            this.panel_referral.Name = "panel_referral";
            this.panel_referral.Size = new System.Drawing.Size(134, 84);
            this.panel_referral.TabIndex = 0;
            // 
            // button_addref
            // 
            this.button_addref.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_addref.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_addref.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_addref.FlatAppearance.BorderSize = 3;
            this.button_addref.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_addref.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_addref.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_addref.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.button_addref.Location = new System.Drawing.Point(72, 42);
            this.button_addref.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_addref.Name = "button_addref";
            this.button_addref.Size = new System.Drawing.Size(55, 30);
            this.button_addref.TabIndex = 8;
            this.button_addref.Text = "Add";
            this.button_addref.UseVisualStyleBackColor = false;
            this.button_addref.Click += new System.EventHandler(this.button_addref_Click);
            // 
            // button_setref
            // 
            this.button_setref.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_setref.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_setref.FlatAppearance.BorderSize = 3;
            this.button_setref.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_setref.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_setref.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_setref.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.button_setref.Location = new System.Drawing.Point(11, 42);
            this.button_setref.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_setref.Name = "button_setref";
            this.button_setref.Size = new System.Drawing.Size(55, 30);
            this.button_setref.TabIndex = 7;
            this.button_setref.Text = "Set";
            this.button_setref.UseVisualStyleBackColor = false;
            this.button_setref.Click += new System.EventHandler(this.button_setref_Click);
            // 
            // referral_txtdrop
            // 
            this.referral_txtdrop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.referral_txtdrop.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.referral_txtdrop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.referral_txtdrop.FormattingEnabled = true;
            this.referral_txtdrop.Location = new System.Drawing.Point(36, 11);
            this.referral_txtdrop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.referral_txtdrop.Name = "referral_txtdrop";
            this.referral_txtdrop.Size = new System.Drawing.Size(91, 24);
            this.referral_txtdrop.TabIndex = 7;
            this.referral_txtdrop.TextChanged += new System.EventHandler(this.referral_txtdrop_TextChanged);
            this.referral_txtdrop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.referral_txtdrop_KeyDown);
            // 
            // referalurl_label
            // 
            this.referalurl_label.AutoSize = true;
            this.referalurl_label.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.referalurl_label.Location = new System.Drawing.Point(1, 16);
            this.referalurl_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.referalurl_label.Name = "referalurl_label";
            this.referalurl_label.Size = new System.Drawing.Size(40, 17);
            this.referalurl_label.TabIndex = 0;
            this.referalurl_label.Text = "URL:";
            // 
            // panel_urls
            // 
            this.panel_urls.BackColor = System.Drawing.Color.CadetBlue;
            this.panel_urls.Controls.Add(this.button_clearurl);
            this.panel_urls.Controls.Add(this.button_loadurllist);
            this.panel_urls.Controls.Add(this.button_addurl);
            this.panel_urls.Controls.Add(this.textbox_url);
            this.panel_urls.Controls.Add(this.list_urls);
            this.panel_urls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_urls.Location = new System.Drawing.Point(157, 6);
            this.panel_urls.Name = "panel_urls";
            this.panel_urls.Size = new System.Drawing.Size(142, 229);
            this.panel_urls.TabIndex = 0;
            // 
            // button_clearurl
            // 
            this.button_clearurl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_clearurl.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_clearurl.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_clearurl.FlatAppearance.BorderSize = 3;
            this.button_clearurl.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_clearurl.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_clearurl.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_clearurl.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.button_clearurl.Location = new System.Drawing.Point(78, 111);
            this.button_clearurl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_clearurl.Name = "button_clearurl";
            this.button_clearurl.Size = new System.Drawing.Size(55, 30);
            this.button_clearurl.TabIndex = 14;
            this.button_clearurl.Text = "Clear";
            this.button_clearurl.UseVisualStyleBackColor = false;
            this.button_clearurl.Click += new System.EventHandler(this.button_clearurl_Click);
            // 
            // button_loadurllist
            // 
            this.button_loadurllist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_loadurllist.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_loadurllist.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_loadurllist.FlatAppearance.BorderSize = 3;
            this.button_loadurllist.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_loadurllist.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_loadurllist.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_loadurllist.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.button_loadurllist.Location = new System.Drawing.Point(9, 111);
            this.button_loadurllist.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_loadurllist.Name = "button_loadurllist";
            this.button_loadurllist.Size = new System.Drawing.Size(55, 30);
            this.button_loadurllist.TabIndex = 13;
            this.button_loadurllist.Text = "Load List";
            this.button_loadurllist.UseVisualStyleBackColor = false;
            this.button_loadurllist.Click += new System.EventHandler(this.button_loadurllist_Click);
            // 
            // button_addurl
            // 
            this.button_addurl.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_addurl.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_addurl.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_addurl.FlatAppearance.BorderSize = 3;
            this.button_addurl.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_addurl.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_addurl.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_addurl.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.button_addurl.Location = new System.Drawing.Point(44, 145);
            this.button_addurl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_addurl.Name = "button_addurl";
            this.button_addurl.Size = new System.Drawing.Size(55, 30);
            this.button_addurl.TabIndex = 12;
            this.button_addurl.Text = "Add";
            this.button_addurl.UseVisualStyleBackColor = false;
            this.button_addurl.Click += new System.EventHandler(this.button_addurl_Click);
            // 
            // textbox_url
            // 
            this.textbox_url.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_url.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.textbox_url.Location = new System.Drawing.Point(6, 185);
            this.textbox_url.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textbox_url.Name = "textbox_url";
            this.textbox_url.Size = new System.Drawing.Size(130, 23);
            this.textbox_url.TabIndex = 3;
            this.textbox_url.Text = "URLs to be viewed";
            this.textbox_url.TextChanged += new System.EventHandler(this.textbox_url_TextChanged);
            this.textbox_url.Enter += new System.EventHandler(this.textbox_url_Enter);
            this.textbox_url.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textbox_url_KeyDown);
            this.textbox_url.Leave += new System.EventHandler(this.textbox_url_Leave);
            // 
            // list_urls
            // 
            this.list_urls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_urls.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.list_urls.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.list_urls.FormattingEnabled = true;
            this.list_urls.ItemHeight = 16;
            this.list_urls.Location = new System.Drawing.Point(0, -3);
            this.list_urls.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.list_urls.Name = "list_urls";
            this.list_urls.Size = new System.Drawing.Size(142, 96);
            this.list_urls.TabIndex = 10;
            // 
            // panel_agents
            // 
            this.panel_agents.BackColor = System.Drawing.Color.CadetBlue;
            this.panel_agents.Controls.Add(this.button_clearuseragents);
            this.panel_agents.Controls.Add(this.button_loaduseragentslist);
            this.panel_agents.Controls.Add(this.button_adduseragent);
            this.panel_agents.Controls.Add(this.textbox_agent);
            this.panel_agents.Controls.Add(this.list_agents);
            this.panel_agents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_agents.Location = new System.Drawing.Point(308, 6);
            this.panel_agents.Name = "panel_agents";
            this.panel_agents.Size = new System.Drawing.Size(142, 229);
            this.panel_agents.TabIndex = 0;
            // 
            // button_clearuseragents
            // 
            this.button_clearuseragents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_clearuseragents.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_clearuseragents.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_clearuseragents.FlatAppearance.BorderSize = 3;
            this.button_clearuseragents.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_clearuseragents.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_clearuseragents.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_clearuseragents.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.button_clearuseragents.Location = new System.Drawing.Point(80, 110);
            this.button_clearuseragents.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_clearuseragents.Name = "button_clearuseragents";
            this.button_clearuseragents.Size = new System.Drawing.Size(55, 30);
            this.button_clearuseragents.TabIndex = 19;
            this.button_clearuseragents.Text = "Clear";
            this.button_clearuseragents.UseVisualStyleBackColor = false;
            this.button_clearuseragents.Click += new System.EventHandler(this.button_clearuseragents_Click);
            // 
            // button_loaduseragentslist
            // 
            this.button_loaduseragentslist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_loaduseragentslist.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_loaduseragentslist.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_loaduseragentslist.FlatAppearance.BorderSize = 3;
            this.button_loaduseragentslist.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_loaduseragentslist.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_loaduseragentslist.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_loaduseragentslist.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.button_loaduseragentslist.Location = new System.Drawing.Point(11, 110);
            this.button_loaduseragentslist.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_loaduseragentslist.Name = "button_loaduseragentslist";
            this.button_loaduseragentslist.Size = new System.Drawing.Size(55, 30);
            this.button_loaduseragentslist.TabIndex = 4;
            this.button_loaduseragentslist.Text = "Load List";
            this.button_loaduseragentslist.UseVisualStyleBackColor = false;
            this.button_loaduseragentslist.Click += new System.EventHandler(this.button_loaduseragentslist_Click);
            // 
            // button_adduseragent
            // 
            this.button_adduseragent.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_adduseragent.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_adduseragent.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_adduseragent.FlatAppearance.BorderSize = 3;
            this.button_adduseragent.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_adduseragent.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_adduseragent.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_adduseragent.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.button_adduseragent.Location = new System.Drawing.Point(46, 144);
            this.button_adduseragent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_adduseragent.Name = "button_adduseragent";
            this.button_adduseragent.Size = new System.Drawing.Size(55, 30);
            this.button_adduseragent.TabIndex = 17;
            this.button_adduseragent.Text = "Add";
            this.button_adduseragent.UseVisualStyleBackColor = false;
            this.button_adduseragent.Click += new System.EventHandler(this.button_adduseragent_Click);
            // 
            // textbox_agent
            // 
            this.textbox_agent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_agent.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.textbox_agent.Location = new System.Drawing.Point(6, 184);
            this.textbox_agent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textbox_agent.Name = "textbox_agent";
            this.textbox_agent.Size = new System.Drawing.Size(130, 23);
            this.textbox_agent.TabIndex = 5;
            this.textbox_agent.Text = "Useragents to use";
            this.textbox_agent.TextChanged += new System.EventHandler(this.textbox_agent_TextChanged);
            this.textbox_agent.Enter += new System.EventHandler(this.textbox_agent_Enter);
            this.textbox_agent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textbox_agent_KeyDown);
            this.textbox_agent.Leave += new System.EventHandler(this.textbox_agent_Leave);
            // 
            // list_agents
            // 
            this.list_agents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_agents.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.list_agents.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.list_agents.FormattingEnabled = true;
            this.list_agents.ItemHeight = 16;
            this.list_agents.Location = new System.Drawing.Point(0, -4);
            this.list_agents.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.list_agents.Name = "list_agents";
            this.list_agents.Size = new System.Drawing.Size(141, 96);
            this.list_agents.TabIndex = 15;
            // 
            // panel_proxies
            // 
            this.panel_proxies.BackColor = System.Drawing.Color.CadetBlue;
            this.panel_proxies.Controls.Add(this.label_currentproxnum);
            this.panel_proxies.Controls.Add(this.list_proxies);
            this.panel_proxies.Controls.Add(this.label_proxiesformat);
            this.panel_proxies.Controls.Add(this.button_clearproxies);
            this.panel_proxies.Controls.Add(this.button_loadproxies);
            this.panel_proxies.Controls.Add(this.label_proxiesnum);
            this.panel_proxies.Controls.Add(this.label_proxies);
            this.panel_proxies.Controls.Add(this.outof);
            this.panel_proxies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_proxies.Location = new System.Drawing.Point(459, 6);
            this.panel_proxies.Name = "panel_proxies";
            this.panel_proxies.Size = new System.Drawing.Size(142, 229);
            this.panel_proxies.TabIndex = 0;
            // 
            // label_currentproxnum
            // 
            this.label_currentproxnum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_currentproxnum.AutoSize = true;
            this.label_currentproxnum.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.label_currentproxnum.Location = new System.Drawing.Point(25, 116);
            this.label_currentproxnum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_currentproxnum.Name = "label_currentproxnum";
            this.label_currentproxnum.Size = new System.Drawing.Size(16, 17);
            this.label_currentproxnum.TabIndex = 17;
            this.label_currentproxnum.Text = "0";
            // 
            // list_proxies
            // 
            this.list_proxies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_proxies.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.list_proxies.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.list_proxies.FormattingEnabled = true;
            this.list_proxies.ItemHeight = 16;
            this.list_proxies.Location = new System.Drawing.Point(0, -4);
            this.list_proxies.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.list_proxies.Name = "list_proxies";
            this.list_proxies.Size = new System.Drawing.Size(142, 96);
            this.list_proxies.TabIndex = 16;
            // 
            // label_proxiesformat
            // 
            this.label_proxiesformat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_proxiesformat.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.label_proxiesformat.Location = new System.Drawing.Point(4, 174);
            this.label_proxiesformat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_proxiesformat.Name = "label_proxiesformat";
            this.label_proxiesformat.Size = new System.Drawing.Size(143, 38);
            this.label_proxiesformat.TabIndex = 11;
            this.label_proxiesformat.Text = "(Proxies must be in [IP:Port] format)";
            // 
            // button_clearproxies
            // 
            this.button_clearproxies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_clearproxies.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_clearproxies.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_clearproxies.FlatAppearance.BorderSize = 3;
            this.button_clearproxies.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_clearproxies.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_clearproxies.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_clearproxies.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.button_clearproxies.Location = new System.Drawing.Point(74, 137);
            this.button_clearproxies.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_clearproxies.Name = "button_clearproxies";
            this.button_clearproxies.Size = new System.Drawing.Size(55, 30);
            this.button_clearproxies.TabIndex = 10;
            this.button_clearproxies.Text = "Clear";
            this.button_clearproxies.UseVisualStyleBackColor = false;
            this.button_clearproxies.Click += new System.EventHandler(this.button_clearproxies_Click);
            // 
            // button_loadproxies
            // 
            this.button_loadproxies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_loadproxies.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_loadproxies.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_loadproxies.FlatAppearance.BorderSize = 3;
            this.button_loadproxies.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_loadproxies.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_loadproxies.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_loadproxies.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.button_loadproxies.Location = new System.Drawing.Point(10, 137);
            this.button_loadproxies.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_loadproxies.Name = "button_loadproxies";
            this.button_loadproxies.Size = new System.Drawing.Size(55, 30);
            this.button_loadproxies.TabIndex = 6;
            this.button_loadproxies.Text = "Load List";
            this.button_loadproxies.UseVisualStyleBackColor = false;
            this.button_loadproxies.Click += new System.EventHandler(this.button_loadproxies_Click);
            // 
            // label_proxiesnum
            // 
            this.label_proxiesnum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_proxiesnum.AutoSize = true;
            this.label_proxiesnum.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.label_proxiesnum.Location = new System.Drawing.Point(87, 116);
            this.label_proxiesnum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_proxiesnum.Name = "label_proxiesnum";
            this.label_proxiesnum.Size = new System.Drawing.Size(16, 17);
            this.label_proxiesnum.TabIndex = 8;
            this.label_proxiesnum.Text = "0";
            // 
            // label_proxies
            // 
            this.label_proxies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_proxies.AutoSize = true;
            this.label_proxies.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.label_proxies.Location = new System.Drawing.Point(2, 101);
            this.label_proxies.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_proxies.Name = "label_proxies";
            this.label_proxies.Size = new System.Drawing.Size(86, 17);
            this.label_proxies.TabIndex = 7;
            this.label_proxies.Text = "# of Proxies:";
            // 
            // outof
            // 
            this.outof.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.outof.AutoSize = true;
            this.outof.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.outof.Location = new System.Drawing.Point(58, 116);
            this.outof.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.outof.Name = "outof";
            this.outof.Size = new System.Drawing.Size(12, 17);
            this.outof.TabIndex = 18;
            this.outof.Text = "/";
            // 
            // panel_browser
            // 
            this.panel_browser.BackColor = System.Drawing.Color.DarkRed;
            this.panel_browser.Controls.Add(this.geckoWebBrowser1);
            this.panel_browser.Controls.Add(this.statusstip_browser);
            this.panel_browser.Controls.Add(this.toolStrip1);
            this.panel_browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_browser.Location = new System.Drawing.Point(5, 242);
            this.panel_browser.Name = "panel_browser";
            this.panel_browser.Size = new System.Drawing.Size(760, 187);
            this.panel_browser.TabIndex = 0;
            // 
            // geckoWebBrowser1
            // 
            this.geckoWebBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.geckoWebBrowser1.FrameEventsPropagateToMainWindow = true;
            this.geckoWebBrowser1.Location = new System.Drawing.Point(0, 31);
            this.geckoWebBrowser1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.geckoWebBrowser1.Name = "geckoWebBrowser1";
            this.geckoWebBrowser1.Size = new System.Drawing.Size(760, 126);
            this.geckoWebBrowser1.TabIndex = 15;
            this.geckoWebBrowser1.UseHttpActivityObserver = false;
            this.geckoWebBrowser1.Navigating += new System.EventHandler<Gecko.Events.GeckoNavigatingEventArgs>(this.geckoWebBrowser1_Navigating);
            this.geckoWebBrowser1.Navigated += new System.EventHandler<Gecko.GeckoNavigatedEventArgs>(this.geckoWebBrowser1_Navigated);
            this.geckoWebBrowser1.NavigationError += new System.EventHandler<Gecko.Events.GeckoNavigationErrorEventArgs>(this.geckoWebBrowser1_NavigationError);
            this.geckoWebBrowser1.Redirecting += new System.EventHandler<Gecko.GeckoRedirectingEventArgs>(this.geckoWebBrowser1_Redirecting);
            this.geckoWebBrowser1.DocumentCompleted += new System.EventHandler<Gecko.Events.GeckoDocumentCompletedEventArgs>(this.geckoWebBrowser1_DocumentCompleted);
            this.geckoWebBrowser1.ProgressChanged += new System.EventHandler<Gecko.GeckoProgressEventArgs>(this.geckoWebBrowser1_ProgressChanged);
            this.geckoWebBrowser1.CreateWindow += new System.EventHandler<Gecko.GeckoCreateWindowEventArgs>(this.geckoWebBrowser1_CreateWindow);
            this.geckoWebBrowser1.Load += new System.EventHandler<Gecko.DomEventArgs>(this.geckoWebBrowser1_Load);
            this.geckoWebBrowser1.DOMContentLoaded += new System.EventHandler<Gecko.DomEventArgs>(this.geckoWebBrowser1_DOMContentLoaded);
            this.geckoWebBrowser1.LocationChanged += new System.EventHandler(this.geckoWebBrowser1_LocationChanged);
            this.geckoWebBrowser1.MouseHover += new System.EventHandler(this.geckoWebBrowser1_MouseHover);
            // 
            // statusstip_browser
            // 
            this.statusstip_browser.BackColor = System.Drawing.Color.CadetBlue;
            this.statusstip_browser.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusstip_browser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressbar_browser,
            this.label_statusbrowser});
            this.statusstip_browser.Location = new System.Drawing.Point(0, 152);
            this.statusstip_browser.Name = "statusstip_browser";
            this.statusstip_browser.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusstip_browser.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusstip_browser.Size = new System.Drawing.Size(760, 35);
            this.statusstip_browser.TabIndex = 0;
            this.statusstip_browser.Text = "statusStrip1";
            // 
            // progressbar_browser
            // 
            this.progressbar_browser.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.progressbar_browser.AutoSize = false;
            this.progressbar_browser.ForeColor = System.Drawing.Color.DarkRed;
            this.progressbar_browser.MarqueeAnimationSpeed = 500;
            this.progressbar_browser.Name = "progressbar_browser";
            this.progressbar_browser.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.progressbar_browser.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.progressbar_browser.Size = new System.Drawing.Size(177, 27);
            this.progressbar_browser.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // label_statusbrowser
            // 
            this.label_statusbrowser.Name = "label_statusbrowser";
            this.label_statusbrowser.Size = new System.Drawing.Size(151, 29);
            this.label_statusbrowser.Text = "toolStripStatusLabel1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.InfoText;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.button_goback,
            this.button_goforward,
            this.button_refresh,
            this.button_stopnav,
            this.textbox_navigate,
            this.button_navigate,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(760, 28);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // button_goback
            // 
            this.button_goback.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_goback.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_goback.Image = global::MUIPRT.Properties.Resources.BACKWARD;
            this.button_goback.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_goback.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.button_goback.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_goback.Name = "button_goback";
            this.button_goback.Size = new System.Drawing.Size(39, 25);
            this.button_goback.Text = "toolStripButton1";
            this.button_goback.Click += new System.EventHandler(this.button_goback_Click);
            // 
            // button_goforward
            // 
            this.button_goforward.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_goforward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_goforward.Image = global::MUIPRT.Properties.Resources.FORWARD;
            this.button_goforward.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_goforward.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.button_goforward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_goforward.Name = "button_goforward";
            this.button_goforward.Size = new System.Drawing.Size(39, 25);
            this.button_goforward.Text = "toolStripButton2";
            this.button_goforward.Click += new System.EventHandler(this.button_goforward_Click);
            // 
            // button_refresh
            // 
            this.button_refresh.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_refresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_refresh.Image = global::MUIPRT.Properties.Resources.refreshfin;
            this.button_refresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_refresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.button_refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(39, 25);
            this.button_refresh.Text = "toolStripButton3";
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // button_stopnav
            // 
            this.button_stopnav.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_stopnav.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_stopnav.Image = global::MUIPRT.Properties.Resources.stop;
            this.button_stopnav.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_stopnav.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.button_stopnav.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_stopnav.Name = "button_stopnav";
            this.button_stopnav.Size = new System.Drawing.Size(39, 25);
            this.button_stopnav.Text = "toolStripButton4";
            this.button_stopnav.Click += new System.EventHandler(this.button_stopnav_Click);
            // 
            // textbox_navigate
            // 
            this.textbox_navigate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textbox_navigate.AutoSize = false;
            this.textbox_navigate.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.textbox_navigate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textbox_navigate.Name = "textbox_navigate";
            this.textbox_navigate.Size = new System.Drawing.Size(560, 28);
            this.textbox_navigate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textbox_navigate_KeyDown);
            // 
            // button_navigate
            // 
            this.button_navigate.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.button_navigate.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_navigate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_navigate.Image = global::MUIPRT.Properties.Resources.GO;
            this.button_navigate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.button_navigate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_navigate.Name = "button_navigate";
            this.button_navigate.Size = new System.Drawing.Size(39, 25);
            this.button_navigate.Text = "toolStripButton5";
            this.button_navigate.Click += new System.EventHandler(this.button_navigate_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // timer_cleardata
            // 
            this.timer_cleardata.Tick += new System.EventHandler(this.timer_cleardata_Tick);
            // 
            // timer_setcoords
            // 
            this.timer_setcoords.Tick += new System.EventHandler(this.timer_setcoords_Tick);
            // 
            // timer_clickcoords
            // 
            this.timer_clickcoords.Tick += new System.EventHandler(this.timer_clickcoords_Tick);
            // 
            // timer_refreshproxylist
            // 
            this.timer_refreshproxylist.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer_load
            // 
            this.timer_load.Tick += new System.EventHandler(this.timer_load_Tick);
            // 
            // timer_setcoords2
            // 
            this.timer_setcoords2.Tick += new System.EventHandler(this.timer_setcoords2_Tick);
            // 
            // timer_clickcoords2
            // 
            this.timer_clickcoords2.Tick += new System.EventHandler(this.timer_clickcoords2_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkRed;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Main";
            this.Text = "MUIPRT";
            this.Load += new System.EventHandler(this.Main_Load);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picture_programbanner)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel_controls.ResumeLayout(false);
            this.panel_controls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numupdown_interval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numupdown_views)).EndInit();
            this.panel_status.ResumeLayout(false);
            this.panel_status.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel_scrape.ResumeLayout(false);
            this.panel_scrape.PerformLayout();
            this.panel_referral.ResumeLayout(false);
            this.panel_referral.PerformLayout();
            this.panel_urls.ResumeLayout(false);
            this.panel_urls.PerformLayout();
            this.panel_agents.ResumeLayout(false);
            this.panel_agents.PerformLayout();
            this.panel_proxies.ResumeLayout(false);
            this.panel_proxies.PerformLayout();
            this.panel_browser.ResumeLayout(false);
            this.panel_browser.PerformLayout();
            this.statusstip_browser.ResumeLayout(false);
            this.statusstip_browser.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox picture_programbanner;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Panel panel_controls;
        private System.Windows.Forms.Label label_interval;
        private System.Windows.Forms.Label label_views;
        private System.Windows.Forms.NumericUpDown numupdown_interval;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.NumericUpDown numupdown_views;
        private System.Windows.Forms.Panel panel_status;
        private System.Windows.Forms.Label label_statustext;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.ProgressBar progressbar_appprogress;
        private System.Windows.Forms.Panel panel_urls;
        private System.Windows.Forms.Button button_clearurl;
        private System.Windows.Forms.Button button_loadurllist;
        private System.Windows.Forms.Button button_addurl;
        private System.Windows.Forms.TextBox textbox_url;
        private System.Windows.Forms.ListBox list_urls;
        private System.Windows.Forms.Panel panel_agents;
        private System.Windows.Forms.Button button_clearuseragents;
        private System.Windows.Forms.Button button_loaduseragentslist;
        private System.Windows.Forms.Button button_adduseragent;
        private System.Windows.Forms.TextBox textbox_agent;
        private System.Windows.Forms.Panel panel_proxies;
        private System.Windows.Forms.Label label_proxiesformat;
        private System.Windows.Forms.Button button_clearproxies;
        private System.Windows.Forms.Button button_loadproxies;
        private System.Windows.Forms.Label label_proxiesnum;
        private System.Windows.Forms.Label label_proxies;
        private System.Windows.Forms.ListBox list_proxies;
        private System.Windows.Forms.Panel panel_scrape;
        private System.Windows.Forms.Panel panel_referral;
        private System.Windows.Forms.Button button_addref;
        private System.Windows.Forms.Button button_setref;
        private System.Windows.Forms.ComboBox referral_txtdrop;
        private System.Windows.Forms.Label referalurl_label;
        private System.Windows.Forms.Panel panel_browser;
        private Gecko.GeckoWebBrowser geckoWebBrowser1;
        private System.Windows.Forms.StatusStrip statusstip_browser;
        private System.Windows.Forms.ToolStripProgressBar progressbar_browser;
        private System.Windows.Forms.ToolStripStatusLabel label_statusbrowser;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton button_goback;
        private System.Windows.Forms.ToolStripButton button_goforward;
        private System.Windows.Forms.ToolStripButton button_refresh;
        private System.Windows.Forms.ToolStripButton button_stopnav;
        private System.Windows.Forms.ToolStripComboBox textbox_navigate;
        private System.Windows.Forms.ToolStripButton button_navigate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ListBox list_agents;
        private System.Windows.Forms.Timer timer_cleardata;
        private System.Windows.Forms.Button Button_getcoords;
        private System.Windows.Forms.TextBox textbox_y;
        private System.Windows.Forms.TextBox textbox_x;
        private System.Windows.Forms.Timer timer_setcoords;
        private System.Windows.Forms.Timer timer_clickcoords;
        private System.Windows.Forms.Label label_currentproxnum;
        private System.Windows.Forms.Label outof;
        private System.Windows.Forms.Timer timer_refreshproxylist;
        private System.Windows.Forms.Timer timer_load;
        private System.Windows.Forms.TextBox textbox_y2;
        private System.Windows.Forms.TextBox textbox_x2;
        private System.Windows.Forms.Timer timer_setcoords2;
        private System.Windows.Forms.Timer timer_clickcoords2;
        private System.Windows.Forms.Button Button_getcoords2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_skip;
    }
}

