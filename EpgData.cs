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

namespace LTC
{
    public class EpgData
    {
        private string _channelListURL = "";
        private int timeZone = 2;
        public int AddHours { get; set; }

        public int TimeZone
        {
            get
            {
                return timeZone;
                
            }
            set
            {
                timeZone = value;
                EpgProgram.TimePlusHours = value;
            }
        }


        public BindingList<EpgChannel> Channels = new BindingList<EpgChannel>();

        public EpgData()
        {
            AddHours = 0;
            TimeZone = 2;
        }

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
        public string Name { get; set; }
        public string URL { get; set; }
        public bool Use { get; set; }

        public BindingList<EpgProgram> Programs = new BindingList<EpgProgram>();

        [XmlIgnore]
        public List<EpgProgram> temp_list = new List<EpgProgram>();

        public EpgChannel()
        {
            Name = "";
            URL = "";
            Use = false;
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
