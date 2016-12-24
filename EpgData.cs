using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace LTC
{
    public static class MyData
    {
        public static string ChannelListFileName = Utils.GetMyFolderX() + "\\epg_lv_data.xml";
        public static string ExportXMLFileName = Utils.GetMyFolderX() + "\\epg_lv.xml";

        //http://tv.lattelecom.lv/programma/interaktiva/list/ltv1/
        //https://manstv.lattelecom.tv/api/v1.4/get/tv/channels
        //https://manstv.lattelecom.tv/api/v1.4/get/tv/epg/?daynight=2016-05-22

        public static string _str_ch_base_url = "https://tv.lattelecom.lv";
        public static string _str_ch_start = "tag=\"channel-program\" href=\"";
        public static string _str_ch_end = "\" tag=\"#content\"><span ";
        public static string _str_ch_name_start = "<strong>";
        public static string _str_ch_name_end = "</strong>";

        public static string _str10_prog_list_start = "<ul id=\"program-list-view\">";
        public static string _str10_prog_list_end = "</ul>";
        public static string _str11_prog_start = "<li class=";
        public static string _str11_prog_end = "</li>";
        public static string _str12_start_time_start = "><b>";

        public static string _str12_title2_start = "title=\"";
        public static string _str13_title_start = "<a class=\"title\" ";
        public static string _str13_title_end = "</a>";
        //public static string _str14_descr_start = "<p style=";
        public static string _str14_descr_start = "<p";
        public static string _str14_descr_end = "</p>";

        public static int _date_length = 10;

        public static bool GetTimeFromStr(string str, out TimeSpan time)
        {
            time = new TimeSpan(0);
            if (str.Length != 5) return false;
            int hr, min;
            if (!int.TryParse(str.Substring(0, 2), out hr)) return false;
            if (!int.TryParse(str.Substring(3, 2), out min)) return false;
            time = new TimeSpan(hr, min, 0);
            return true;
        }
    }

    public class EpgData
    {
        private string _channelListURL = "";
        private int timeZone = 2;
        public int AddHours { get; set; } = 0;

        public string Source = "?";

        public int TimeZone
        {
            get { return timeZone; }
            set
            {
                timeZone = value;
                EpgProgram.TimePlusHours = value;
            }
        }

        public BindingList<EpgChannel> Channels = new BindingList<EpgChannel>();

        public string ChannelListURL
        {
            get { return _channelListURL; }
            set
            {
                if (value == _channelListURL) return;
                _channelListURL = value;
            }
        }

        public int CountUsedChannels()
        {
            int k = 0;
            foreach (var ch in Channels)
            {
                if (ch.Use) k++;
            }
            return k;
        }

        [XmlIgnore]
        public Dictionary<long, EpgProgram> Dict_EpgProgram = new Dictionary<long, EpgProgram>();

        public void MakeDict()
        {
            Dict_EpgProgram.Clear();
            if (Channels.Count == 0) return;
            foreach (var ch in Channels)
            {
                foreach (var pr in ch.Programs)
                {
                    Dict_EpgProgram[pr.Id] = pr;
                }
            }
        }


        public bool ReadChannels(string jspage)
        {
            Channels.Clear();
            if (string.IsNullOrEmpty(jspage)) return false;
            dynamic dobj = JObject.Parse(jspage);
            foreach (var item in dobj.items)
            {
                var ch1 = new EpgChannel()
                {
                    Id = item.id,
                    xprs_id = item.xprs_id,
                    Name = item.name
                };
                Channels.Add(ch1);
            }
            SortChannels();
            Source = "api";
            return Channels.Count > 0;
        }

        public bool ReadProgramms(string jspage, DateTime date)
        {
            if (string.IsNullOrEmpty(jspage)) return false;
            if (Source == "web") return false;
            dynamic dobj = JObject.Parse(jspage);
            if (dobj.items == null) return false;
            int ct = 0;
            foreach (var item in dobj.items)
            {
                string chid = item.channel_id;
                var epg_channel = Channels.Where(d => d.xprs_id == chid).FirstOrDefault();
                if (epg_channel == null || !epg_channel.Use) continue;
                var prog = new EpgProgram();
                prog.Id = long.Parse((string)item.id);
                prog.Title = item.title;
                prog.Description = item.description;
                prog.Start = item.time_start;
                prog.End = item.time_stop;
                if (Dict_EpgProgram.ContainsKey(prog.Id)) continue;
                prog.Channel = epg_channel;
                epg_channel.temp_list.Add(prog);
                ct++;
            }
            return ct > 0;

        }

        public bool ReadChannelsA(string html_page)
        {
            Channels.Clear();
            int pos1 = html_page.IndexOf("<dl id=\"channel-list\">");
            if (pos1 == -1) return false;
            int pos2 = pos1 + 1;
            int pos3;
            EpgChannel ch1;
            while (true)
            {
                pos2 = html_page.IndexOf(MyData._str_ch_start, pos2);
                if (pos2 == -1) break;
                pos2 = pos2 + MyData._str_ch_start.Length;

                pos3 = html_page.IndexOf(MyData._str_ch_end, pos2);
                if (pos3 == -1) break;

                ch1 = new EpgChannel();
                var s1 = html_page.Substring(pos2, pos3 - pos2 - MyData._date_length);
                ch1.URL = MyData._str_ch_base_url + s1;

                pos2 = html_page.IndexOf(MyData._str_ch_name_start, pos3);
                if (pos2 == -1) break;

                pos2 += MyData._str_ch_name_start.Length;

                pos3 = html_page.IndexOf(MyData._str_ch_name_end, pos2);
                if (pos3 == -1) break;

                ch1.Name = html_page.Substring(pos2, pos3 - pos2);
                Channels.Add(ch1);

                pos2 = pos3;
            }
            SortChannels();
            Source = "web";
            return Channels.Count > 0;
        }

        public void RemoveDates(DateTime date1, DateTime date2)
        {
            foreach (var ch in Channels)
            {
                ch.RemoveDates(date1, date2);
            }
        }

        public void AddFromTemp()
        {
            foreach (var c in Channels)
                c.AddFromTemp();
        }

        public void CheckEndTimes()
        {
            foreach (var c in Channels)
                if(c.Use) c.CheckEndTimes();
        }

        public void ClearTemp()
        {
            foreach (var c in Channels)
                c.temp_list.Clear();
        }

        public void ClearAllPrograms()
        {
            foreach (var ch in Channels)
            {
                ch.ClearPrograms();
            }
        }

        public void SetReferences()
        {
            foreach (var ch in Channels)
            {
                foreach (var pr in ch.Programs)
                {
                    pr.Channel = ch;
                }
            }
        }

        public void SortChannels()
        {
            var lord = Channels
                .OrderBy(d => d.Use ? 0 : 1)
                .ThenBy(d => d.Name)
                .ToList();
            Channels.Clear();
            lord.ForEach(d => Channels.Add(d));
        }


        public static EpgData Load(string filename)
        {
            EpgData epgdata = new EpgData();
            if (!File.Exists(filename)) return epgdata;
            XmlSerializer xs = null;
            FileStream fs = null;
            try
            {
                //xs = new XmlSerializer(typeof(KlonsSettings));
                xs = Utils.CreateDefaultXmlSerializer(typeof(EpgData));
                fs = new FileStream(filename, FileMode.Open);
                epgdata = (EpgData)xs.Deserialize(fs);
                if (epgdata != null)
                    epgdata.SetReferences();
                epgdata.SortChannels();
                if (epgdata.Source == "?")
                {
                    if (epgdata._channelListURL != null && epgdata._channelListURL.Contains("/api/"))
                        epgdata.Source = "api";
                    else if (epgdata._channelListURL != null && epgdata._channelListURL.Contains("/api/"))
                        epgdata.Source = "web";
                }
                return epgdata;
            }
            catch (Exception)
            {
                //LogError(e.Message);
                epgdata = new EpgData();
                return epgdata;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        public bool Save(string filename)
        {
            XmlSerializer xs = Utils.CreateDefaultXmlSerializer(typeof(EpgData));
            TextWriter wr = null;
            try
            {
                wr = new StreamWriter(filename);
                xs.Serialize(wr, this);
            }
            catch (Exception)
            {
                //LogError(e.Message);
                return false;
            }
            finally
            {
                if (wr != null) wr.Close();
            }
            return true;
        }

        public void WriteData(string filename)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter xmlWriter = XmlWriter.Create(filename, settings))
            {
                WriteData(xmlWriter);
            }
        }

        public void WriteData(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("tv");
            foreach (EpgChannel ch in Channels)
            {
                if(ch.Use)
                    ch.WriteXML(xmlWriter);
            }

            foreach (EpgChannel ch in Channels)
            {
                foreach (EpgProgram prog in ch.Programs)
                {
                    prog.WriteXML(xmlWriter, AddHours);
                }
            }

            xmlWriter.WriteEndElement();
            xmlWriter.Close();
        }
    }

    public class EpgChannel
    {
        public string Id { get; set; } = null;
        public string xprs_id { get; set; } = null;

        public string Name { get; set; } = "";
        public string URL { get; set; } = "";
        public bool Use { get; set; } = false;

        public BindingList<EpgProgram> Programs = new BindingList<EpgProgram>();

        [XmlIgnore]
        public List<EpgProgram> temp_list = new List<EpgProgram>();


        public bool ReadProgrammsA(string html_page, DateTime date)
        {
            int pos1 = html_page.IndexOf(MyData._str10_prog_list_start);
            if (pos1 == -1) return false;
            int pos2 = pos1 + 1;

            int list_pos_end = html_page.IndexOf(MyData._str10_prog_list_end, pos2);
            if (list_pos_end == -1) return false;

            int pos3, pos4;
            int prog_pos_start, prog_pos_end, title_pos_end;
            string s1;
            TimeSpan time;

            EpgProgram prog;

            while (true)
            {
                pos2 = html_page.IndexOf(MyData._str11_prog_start, pos2, list_pos_end - pos2);
                if (pos2 == -1) return true;

                prog_pos_end = html_page.IndexOf(MyData._str11_prog_end, pos2, list_pos_end - pos2);
                if (prog_pos_end == -1) return false;

                pos3 = html_page.IndexOf(MyData._str12_start_time_start, pos2, prog_pos_end - pos2);
                if (pos3 == -1) return false;
                pos3 += MyData._str12_start_time_start.Length;

                prog = new EpgProgram();
                prog.Channel = this;

                s1 = html_page.Substring(pos3, 5);
                if (!MyData.GetTimeFromStr(s1, out time)) return false;
                prog.Start = date.Add(time);

                pos3 = html_page.IndexOf(MyData._str13_title_start, pos2, prog_pos_end - pos2);
                if (pos3 == -1) return false;
                pos3 += MyData._str13_title_start.Length;
                pos4 = html_page.IndexOf(MyData._str13_title_end, pos3, prog_pos_end - pos3);
                if (pos4 == -1) return false;
                title_pos_end = pos4 + MyData._str13_title_end.Length;
                pos3 = html_page.IndexOf(">", pos3, prog_pos_end - pos3);
                if (pos3 == -1) return false;
                pos3++;

                pos3 = html_page.IndexOf(MyData._str12_title2_start, pos2, prog_pos_end - pos2);
                if (pos3 > -1)
                {
                    pos3 += MyData._str12_title2_start.Length;
                    pos4 = html_page.IndexOf("\"", pos3, prog_pos_end - pos3);
                    if (pos4 == -1) return false;
                }

                pos3 = html_page.IndexOf(MyData._str14_descr_start, title_pos_end, prog_pos_end - title_pos_end);
                if (pos3 > -1)
                {
                    pos3 += MyData._str14_descr_start.Length;
                    pos3 = html_page.IndexOf(">", pos3, prog_pos_end - pos3);
                    if (pos3 == -1) return false;
                    pos3++;
                    pos4 = html_page.IndexOf(MyData._str14_descr_end, pos3, prog_pos_end - pos3);
                    if (pos4 == -1) return false;
                }

                temp_list.Add(prog);
                pos2 = prog_pos_end + MyData._str11_prog_end.Length;
            }
        }



        public void AddFromTemp()
        {
            foreach (var p in temp_list)
            {
                Programs.Add(p);
            }
            temp_list.Clear();
        }

        public void CheckEndTimes()
        {
            EpgProgram pr1,pr2;
            if (Programs.Count == 0) return;
            Sort();
            for (int i = 0; i < Programs.Count - 1; i++)
            {
                pr1 = Programs[i];
                pr2 = Programs[i+1];
                if (pr1.Start.AddHours(10) < pr2.Start)
                {
                    pr1.End = pr1.Start;
                }
                else
                {
                    pr1.End = pr2.Start;
                }
            }
            pr1 = Programs[Programs.Count - 1];
            pr1.End = pr1.Start;
        }

        public void RemoveDates(DateTime date1, DateTime date2)
        {
            int i = 0;
            while (true)
            {
                if (i >= Programs.Count) return;
                var dt = Programs[i].Start.Date;
                if (dt >= date1 && dt <= date2)
                {
                    Programs.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }

        public void ClearPrograms()
        {
            Programs.Clear();
        }

        public void Sort()
        {
            if (Programs.Count == 0) return;
            List<EpgProgram> sl = Programs.OrderBy(p => p.Start).ToList();
            Programs = new BindingList<EpgProgram>(sl);
        }

        public bool WriteXML(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("channel");
            xmlWriter.WriteAttributeString("id", Name);
            xmlWriter.WriteStartElement("display-name");
            xmlWriter.WriteString(Name);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
            return true;
        }
    }

    public class EpgProgram
    {
        public long Id { get; set; } = -1;
        internal static int TimePlusHours = 2;
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        [XmlIgnore]
        public EpgChannel Channel = null;

        public EpgProgram()
        {
            Title = "";
            Description = "";
            Start = DateTime.MaxValue;
            End = DateTime.MaxValue;
        }

        public string GetTimeString(DateTime dt, int plusthours)
        {
            string s = dt.ToString("yyyyMMddHHmm00", CultureInfo.InvariantCulture);
            if (plusthours == 0) return s;
            s += plusthours > 0 ? " +" : " -";
            s += plusthours.ToString("D2") + "00";
            return s;
        }

        public bool WriteXML(XmlWriter xmlWriter, int addhours)
        {
            xmlWriter.WriteStartElement("programme");
            xmlWriter.WriteAttributeString("start", GetTimeString(Start.AddHours(addhours), TimePlusHours));
            xmlWriter.WriteAttributeString("stop", GetTimeString(End.AddHours(addhours), TimePlusHours));
            xmlWriter.WriteAttributeString("channel", Channel.Name);

            xmlWriter.WriteStartElement("title");
            xmlWriter.WriteString(Title);
            xmlWriter.WriteEndElement();

            /*
            xmlWriter.WriteStartElement("sub-title");
            if (SubTitleLang != "")
                xmlWriter.WriteAttributeString("lang", SubTitleLang);
            xmlWriter.WriteString(SubTitle);
            xmlWriter.WriteEndElement();
            */

            if (!string.IsNullOrEmpty(Description))
            {
                xmlWriter.WriteStartElement("desc");
                xmlWriter.WriteString(Description);
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
            return true;
        }

    }

}
