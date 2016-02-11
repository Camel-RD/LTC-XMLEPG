using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Net;

namespace LTC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tbURL.DataBindings.Add("Text", EpgData1, "ChannelListURL");
            tbAddHours.DataBindings.Add("Text", EpgData1, "AddHours");
            tbTimeZone.DataBindings.Add("Text", EpgData1, "TimeZone");
        }

        public static string ChannelListFileName = GetBasePath() + "\\epg_lv_data.xml";
        public static string ExportXMLFileName = GetBasePath() + "\\epg_lv.xml";

        //http://tv.lattelecom.lv/programma/interaktiva/list/ltv1/

        public EpgData EpgData1 = new EpgData();

        private static string _str_ch_base_url = "https://tv.lattelecom.lv";
        private static string _str_ch_start = "tag=\"channel-program\" href=\"";
        private static string _str_ch_end = "\" tag=\"#content\"><span ";
        private static string _str_ch_name_start = "<strong>";
        private static string _str_ch_name_end = "</strong>";

        private static string _str10_prog_list_start = "<ul id=\"program-list-view\">";
        private static string _str10_prog_list_end = "</ul>";
        private static string _str11_prog_start = "<li class=";
        private static string _str11_prog_end = "</li>";
        private static string _str12_start_time_start = "><b>";

        private static string _str12_title2_start = "title=\"";
        private static string _str13_title_start = "<a class=\"title\" ";
        private static string _str13_title_end = "</a>";
        //private static string _str14_descr_start = "<p style=";
        private static string _str14_descr_start = "<p";
        private static string _str14_descr_end = "</p>";

        private static int _date_length = 10;

        private bool WaitingForDownload = false;
        private Action<string> OnDownloadComplete = null;
        
        private void Form1_Load(object sender, EventArgs e)
        {
            tbDateFrom.Text = Utils.DateToString(DateTime.Today);
            tbDateTo.Text = tbDateFrom.Text;
        }

        public bool ReadChannels(string html_page)
        {
            EpgData1.Channels.Clear();
            int pos1 = html_page.IndexOf("<dl id=\"channel-list\">");
            if (pos1 == -1) return false;
            int pos2 = pos1 + 1;
            int pos3;
            EpgChannel ch1;
            while (true)
            {
                pos2 = html_page.IndexOf(_str_ch_start, pos2);
                if (pos2 == -1) break;
                pos2 = pos2 + _str_ch_start.Length;
                
                pos3 = html_page.IndexOf(_str_ch_end, pos2);
                if (pos3 == -1) break;

                ch1 = new EpgChannel();
                var s1 = html_page.Substring(pos2, pos3 - pos2 - _date_length);
                ch1.URL = _str_ch_base_url + s1;
                
                pos2 = html_page.IndexOf(_str_ch_name_start, pos3);
                if (pos2 == -1) break;
                
                pos2 += _str_ch_name_start.Length;
                
                pos3 = html_page.IndexOf(_str_ch_name_end, pos2);
                if (pos3 == -1) break;

                ch1.Name = html_page.Substring(pos2, pos3 - pos2);
                EpgData1.Channels.Add(ch1);

                pos2 = pos3;
            }
            return EpgData1.Channels.Count > 0;
        }

        public bool ReadProgramms(string html_page, EpgChannel epg_channel, DateTime date)
        {
            int pos1 = html_page.IndexOf(_str10_prog_list_start);
            if (pos1 == -1) return false;
            int pos2 = pos1 + 1;

            int list_pos_end = html_page.IndexOf(_str10_prog_list_end, pos2);
            if (list_pos_end == -1) return false;

            int pos3, pos4;
            int prog_pos_start, prog_pos_end, title_pos_end;
            string s1;
            TimeSpan time;

            EpgProgram prog;

            while (true)
            {
                pos2 = html_page.IndexOf(_str11_prog_start, pos2, list_pos_end - pos2);
                if (pos2 == -1) return true;

                prog_pos_end = html_page.IndexOf(_str11_prog_end, pos2, list_pos_end - pos2);
                if (prog_pos_end == -1) return false;

                pos3 = html_page.IndexOf(_str12_start_time_start, pos2, prog_pos_end - pos2);
                if (pos3 == -1) return false;
                pos3 += _str12_start_time_start.Length;

                prog = new EpgProgram();
                prog.Channel = epg_channel;

                s1 = html_page.Substring(pos3, 5);
                if (!GetTimeFromStr(s1, out time)) return false;
                prog.Start = date.Add(time);

                pos3 = html_page.IndexOf(_str13_title_start, pos2, prog_pos_end - pos2);
                if (pos3 == -1) return false;
                pos3 += _str13_title_start.Length;
                pos4 = html_page.IndexOf(_str13_title_end, pos3, prog_pos_end - pos3);
                if (pos4 == -1) return false;
                title_pos_end = pos4 + _str13_title_end.Length;
                pos3 = html_page.IndexOf(">", pos3, prog_pos_end - pos3);
                if (pos3 == -1) return false;
                pos3++;
                prog.Title = Utils.UnescapeXMLText(html_page.Substring(pos3, pos4 - pos3));

                pos3 = html_page.IndexOf(_str12_title2_start, pos2, prog_pos_end - pos2);
                if (pos3 > -1)
                {
                    pos3 += _str12_title2_start.Length;
                    pos4 = html_page.IndexOf("\"", pos3, prog_pos_end - pos3);
                    if (pos4 == -1) return false;
                    prog.Title = Utils.UnescapeXMLText(html_page.Substring(pos3, pos4 - pos3));
                }

                pos3 = html_page.IndexOf(_str14_descr_start, title_pos_end, prog_pos_end - title_pos_end);
                if (pos3 > -1)
                {
                    pos3 += _str14_descr_start.Length;
                    pos3 = html_page.IndexOf(">", pos3, prog_pos_end - pos3);
                    if (pos3 == -1) return false;
                    pos3++;
                    pos4 = html_page.IndexOf(_str14_descr_end, pos3, prog_pos_end - pos3);
                    if (pos4 == -1) return false;
                    prog.Description = Utils.UnescapeXMLText(html_page.Substring(pos3, pos4 - pos3));
                }

                epg_channel.temp_list.Add(prog);
                pos2 = prog_pos_end + _str11_prog_end.Length;
            }
        }

        public bool GetTimeFromStr(string str, out TimeSpan time)
        {
            time = new TimeSpan(0);
            if (str.Length != 5) return false;
            int hr, min;
            if (!int.TryParse(str.Substring(0, 2), out hr)) return false;
            if (!int.TryParse(str.Substring(3, 2), out min)) return false;
            time = new TimeSpan(hr, min, 0);
            return true;
        }

        public static bool DesignModeX
        {
            get
            {
                string dir = new DirectoryInfo(Utils.GetMyFolder()).Name;
                return dir == "Debug" || dir == "Release";
            }
        }

        public static string GetBasePath()
        {
            string s = Utils.GetMyFolder();
            /*
            if (DesignMode)
            {
                DirectoryInfo dir1 = new DirectoryInfo(s);
                s = dir1.Parent.Parent.FullName;
            }
             */
            return s;
        }

        public void SaveData()
        {
            EpgData1.Save(ChannelListFileName);
        }

        public void LoadData()
        {
            bsChannels.DataSource = null;
            bsPrograms.DataSource = null;
            
            tbURL.DataBindings.Clear();
            tbAddHours.DataBindings.Clear();
            tbTimeZone.DataBindings.Clear();

            EpgData1 = EpgData.Load(ChannelListFileName);
            bsChannels.DataSource = EpgData1.Channels;

            tbURL.DataBindings.Add("Text", EpgData1, "ChannelListURL");
            tbAddHours.DataBindings.Add("Text", EpgData1, "AddHours");
            tbTimeZone.DataBindings.Add("Text", EpgData1, "TimeZone");
        }

        private void tbURL_Leave(object sender, EventArgs e)
        {

        }

        private void bsChannels_CurrentChanged(object sender, EventArgs e)
        {
            EpgChannel ch = bsChannels.Current as EpgChannel;
            if (ch == null)
            {
                bsPrograms.DataSource = null;
                return;
            }
            bsPrograms.DataSource = ch.Programs;
        }

        public async void GetChannelListFromURL(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                MessageBox.Show("Wrong URL");
                return;
            }

            string s = await ADownloader.GetString(url, LTC.Properties.Settings.Default.HTTPClientTimeOut);

            if (string.IsNullOrEmpty(s))
            {
                MessageBox.Show("Download failed");
                return;
            }
            if (!ReadChannels(s))
            {
                MessageBox.Show("Failed to read channel list");
                return;
            }
            bsChannels.DataSource = EpgData1.Channels;
            MessageBox.Show("Done!");

        }

        public async void GetChannelListFromFile(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                MessageBox.Show("Wrong URL");
                return;
            }

            string s = File.ReadAllText(filename);

            if (string.IsNullOrEmpty(s))
            {
                MessageBox.Show("File read failed");
                return;
            }
            if (!ReadChannels(s))
            {
                MessageBox.Show("Failed to read channel list");
                return;
            }
            bsChannels.DataSource = EpgData1.Channels;
            MessageBox.Show("Done!");

        }

        public string GetURL(EpgChannel ch, DateTime date)
        {
            return string.Format("{0}{1}", ch.URL, date.ToString("dd.MM.yyyy"));
        }

        public bool GetDates(out DateTime startdate, out DateTime enddate)
        {
            startdate = DateTime.MaxValue;
            enddate = DateTime.MaxValue;
            if (!Utils.StringToDate(tbDateFrom.Text, out startdate) ||
                !Utils.StringToDate(tbDateTo.Text, out enddate) ||
                (startdate > enddate))
            {
                MessageBox.Show("Bad dates");
                return false;
            }
            return true;
        }

        private bool task_started = false;
        private int task_ch_nr = -1;
        private EpgChannel task_cur_ch = null;
        private bool task_do_all = false;
        private DateTime task_date = DateTime.MaxValue;
        private DateTime task_date_start = DateTime.MaxValue;
        private DateTime task_date_end = DateTime.MaxValue;
        private float task_inc_progres = 0.0f;
        private float task_cur_progres = 0.0f;
        private TaskForm task_form = null;
        private bool task_cancel = false;


        private void OpenTaskForm()
        {
            if (task_form == null || task_form.IsDisposed)
                task_form = new TaskForm();
            task_form.Reset();
            if (!task_form.Visible)
                task_form.Show(this);
            task_form.OnCancel = DoTaskCancel;
            task_form.CanClose = false;
            task_form.Focus();
            task_form.Select();
            this.Enabled = false;
        }

        private void TaskFormMsg(string msg)
        {
            if (task_form == null || task_form.IsDisposed || !task_form.Visible) return;
            task_form.AddMessage(msg);
        }

        private void CloseTaskForm()
        {
            this.Enabled = true;
            this.Refresh();
            //var ds = bsPrograms.DataSource;
            //bsPrograms.DataSource = null;
            //bsPrograms.DataSource = ds;
            dgvPrograms.Refresh();
            if (task_form == null) return;
            if (task_form.IsDisposed) task_form = null;
            task_form.CanClose = true;
            if(task_form.Visible)
            {
                //task_form.Close();
            }
        }

        private void UpdateProgress()
        {
            task_cur_progres += task_inc_progres;
            if (task_cur_progres > 100.0f) task_cur_progres = 100.0f;
            if (task_form == null || task_form.IsDisposed || !task_form.Visible) return;
            task_form.SetProgress((int) Math.Floor(task_cur_progres));
        }

        private void CalcProgressInc()
        {
            int days = (task_date_end - task_date_start).Days + 1;
            if (task_do_all)
            {
                task_inc_progres = 100.0f / (float)(days * EpgData1.CountUsedChannels());
            }
            else
            {
                task_inc_progres = 100.0f / days;
            }
        }

        private void DoTaskCancel()
        {
            task_cancel = true;
        }

        public async Task<bool> GetCurrent()
        {
            DateTime startdate, enddate;
            if (!GetDates(out startdate, out enddate)) return false;
            if (EpgData1.Channels.Count == 0) return true;
            if (bsChannels.Current == null) return false;
            task_cur_ch = bsChannels.Current as EpgChannel;
            task_ch_nr = EpgData1.Channels.IndexOf(task_cur_ch);
            if (task_ch_nr == -1) return false;
            task_do_all = false;
            task_date_start = startdate;
            task_date_end = enddate;
            task_date = startdate;
            task_cur_progres = 0.0f;
            
            CalcProgressInc();
            OpenTaskForm();
            task_cancel = false;
            task_started = true;

            task_cur_ch.RemoveDates(startdate, enddate);
            EpgData1.ClearTemp();

            while (task_date <= task_date_end)
            {
                if (task_cancel) break;

                bool ret = await GetData(task_cur_ch, task_date);

                await Task.Delay(200);

                UpdateProgress();
                if (!ret) break;
                task_date = task_date.AddDays(1);
            }

            task_cur_ch.AddFromTemp();
            task_cur_ch.CheckEndTimes();
            
            Task_done();

            return true;
        }

        public async Task<bool> GetAll()
        {
            DateTime startdate, enddate;
            if (!GetDates(out startdate, out enddate)) return false;
            if (EpgData1.Channels.Count == 0) return true;
            if (bsChannels.Current == null) return false;
            task_date_start = startdate;
            task_date_end = enddate;
            task_date = startdate;
            task_do_all = true;
            task_cur_progres = 0.0f;

            CalcProgressInc();
            OpenTaskForm();
            task_cancel = false;
            task_started = true;

            EpgData1.RemoveDates(startdate, enddate);
            EpgData1.ClearTemp();

            foreach (var ch in EpgData1.Channels)
            {
                if (!ch.Use) continue;
                task_cur_ch = ch;
                task_date = task_date_start;
                while (task_date <= task_date_end)
                {
                    if (task_cancel) break;

                    bool ret = await GetData(task_cur_ch, task_date);

                    await Task.Delay(200);

                    UpdateProgress();
                    if (!ret) break;
                    task_date = task_date.AddDays(1);
                }
                if (task_cancel) break;
            }

            EpgData1.AddFromTemp();
            EpgData1.CheckEndTimes();

            Task_done();

            return true;
        }

        public bool GetCurrentFromFile()
        {
            DateTime startdate, enddate;
            if (!GetDates(out startdate, out enddate)) return false;
            if (EpgData1.Channels.Count == 0) return true;
            if (bsChannels.Current == null) return false;
            task_cur_ch = bsChannels.Current as EpgChannel;
            task_ch_nr = EpgData1.Channels.IndexOf(task_cur_ch);
            if (task_ch_nr == -1) return false;
            task_date = startdate;

            task_cur_ch.RemoveDates(startdate, enddate);
            EpgData1.ClearTemp();

            string page = File.ReadAllText(tbURL.Text);
            bool ret = ReadProgramms(page, task_cur_ch, task_date);

            task_cur_ch.AddFromTemp();
            task_cur_ch.CheckEndTimes();

            return true;
        }

        private async Task<bool> GetData(EpgChannel ch, DateTime date)
        {
            string url = GetURL(ch, date);

            string page = await ADownloader.GetString(url, LTC.Properties.Settings.Default.HTTPClientTimeOut);

            if (page == null)
            {
                TaskFormMsg(
                    string.Format("Failed to download programs for {0}: {1}",
                        task_cur_ch.Name, task_date.ToString("yyyy.MM.dd"))
                    );
                return false;
            }

            //File.WriteAllText(GetBasePath()+"\\e1.html", page);

            bool ret1 = await Task<bool>.Run(() =>
            {
                return ReadProgramms(page, task_cur_ch, task_date);
            });

            if (!ret1)
            {
                TaskFormMsg(
                    string.Format("Failed to read programs for {0}: {1}",
                        task_cur_ch.Name, task_date.ToString("yyyy.MM.dd"))
                    );
                return false;
            }

            return true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        public void Task_done()
        {
            task_started = false;
            CloseTaskForm();
            MessageBox.Show("Done");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void getFromWebpageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetChannelListFromURL(tbURL.Text);
        }

        private void getCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrent();
        }

        private void getAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetAll();
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EpgData1.ClearAllPrograms();
        }

        private void clearCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bsChannels.Current == null) return;
            var ch = bsChannels.Current as EpgChannel;
            ch.ClearPrograms();
        }

        private void clearAllForDatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime startdate, enddate;
            if (!GetDates(out startdate, out enddate)) return ;
            if (EpgData1.Channels.Count == 0) return ;
            EpgData1.RemoveDates(startdate, enddate);
        }

        private void clearCurrentForDatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime startdate, enddate;
            if (!GetDates(out startdate, out enddate)) return ;
            if (EpgData1.Channels.Count == 0) return ;
            if (bsChannels.Current == null) return;
            var ch = bsChannels.Current as EpgChannel;
            ch.RemoveDates(startdate, enddate);
        }

        private void esportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EpgData1.WriteData(ExportXMLFileName);
            MessageBox.Show("Done!");
        }

        private void getFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetChannelListFromFile(tbURL.Text);
        }

        private void getCurrentFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentFromFile();
        }
    }
}
