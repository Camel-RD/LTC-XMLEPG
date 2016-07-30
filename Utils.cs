using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace LTC
{
    public static class Utils
    {
        public static string GetMyFolder()
        {
            return GetFolder(Application.ExecutablePath);
        }

        //returns path without "\"
        public static string GetFolder(string filename)
        {
            int i, k = filename.Length - 1;
            char c;
            for (i = k; i >= 0; i--)
            {
                c = filename[i];
                if (c == '\\' || c == '/') return filename.Substring(0, i);
            }
            return "";
        }

        public static string GetFileNameFromURL(string url)
        {
            int i, k = url.Length - 1;
            for (i = k; i >= 0; i--)
                if (url[i] == '\\' || url[i] == '/')
                    return url.Substring(i + 1);
            return url;
        }

        public static string RemoveExt(string filename)
        {
            int i, k = filename.Length - 1;
            char c;
            for (i = k; i >= 0; i--)
            {
                c = filename[i];
                if (c == '.') return filename.Substring(0, i);
                if (c == '\\' || c == '/') return filename;
            }
            return filename;
        }

        public static string GetExt(string filename)
        {
            int i, k = filename.Length - 1;
            char c;
            for (i = k; i >= 0; i--)
            {
                c = filename[i];
                if (c == '.') return filename.Substring(i + 1, k - i);
                if (c == '\\' || c == '/') return "";
            }
            return "";
        }

        public static string GetNextFile(string folder, string prefix, string ext)
        {
            string fnm = folder + "\\" + prefix;
            ext = "." + ext;
            int i = 1;
            string s;
            do
            {
                s = fnm + i + ext;
                i++;
            }
            while (File.Exists(s)) ;
            return s;
        }

        public static long GetFileSize(string filename)
        {
            try
            {
                FileInfo fileinfo = new FileInfo(filename);
                return fileinfo.Length;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static bool IN(int val, params int[] list)
        {
            if (list == null || list.Length == 0) return false;
            foreach(int v in list)
                if (v == val) return true;
            return false;
        }

        public static bool StringToDate(string value, out DateTime date)
        {
            return DateTime.TryParseExact(value, "d.M.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out date);
        }
        public static DateTime? StringToDate(string value)
        {
            DateTime dt;
            if (DateTime.TryParseExact(value, "d.M.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out dt))
            {
                return dt;
            }
            return null;
        }

        public static string DateToString(DateTime date)
        {
            return date.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
        }

        public static string ReformatDateString(string date)
        {
            DateTime dt;
            if (!StringToDate(date, out dt)) return date;
            return DateToString(dt);
        }

        public static string[] MonthNames = new string[]
        {
            "janvāris", "februāris", "marts", "aprīlis", "maijs", "jūnijs",
            "jūlijs", "augusts", "septembris", "oktobris", "novembris", "decembris"
        };

        public static bool StringEndsWith(string s, string end)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(end)) return false;
            int ls = s.Length, lend = end.Length;
            if (ls < lend) return false;
            return s.Substring(ls - lend) == end;
        }

        public static string Nz(this string s)
        {
            return s == null ? "" : s;
        }
        public static string Zn(this string s)
        {
            return string.IsNullOrEmpty(s) ? null : s;
        }
        public static string LeftMax(this string s, int count)
        {
            if (s == null) return null;
            if (s.Length < count) count = s.Length;
            return s.Substring(0, count);
        }

        public static int FontSizeX(this Font font)
        {
            return (int) Math.Round(font.SizeInPoints, 0);
        }

        public static bool IsTheSameArray(this int[] arr1, int[] arr2)
        {
            if (arr1 == null || arr2 == null || arr1.Length != arr2.Length) return false;
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] != arr2[i]) return false;
            }
            return true;
        }
        public static int IndexOf(this string[] arr, string value)
        {
            if (arr == null || arr.Length == 0) return -1;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == value) return i;
            }
            return -1;
        }

        //for example: can remove Click events from ToolStripMenuItem (eventname: EventClick)
        public static void ClearEventInvocations(object obj, string eventName)
        {
            PropertyInfo prop = GetProp(obj.GetType(), "Events");
            if (prop == null) return;
            EventHandlerList el = prop.GetValue(obj, null) as EventHandlerList;
            if (el == null) return;

            FieldInfo field = GetField(obj.GetType(), eventName);
            if (field == null) return;
            object o = field.GetValue(obj);
            Delegate d = el[o];
            //el.RemoveHandler(o, el[o]);
            el[o] = null;
        }

        public static bool HasProperty(object obj, string propName)
        {
            PropertyInfo prop = GetProp(obj.GetType(), propName);
            return prop != null;
        }

        public static bool SetProperty(object obj, string propName, object value)
        {
            PropertyInfo prop = GetProp(obj.GetType(), propName);
            if (prop == null) return false;
            try
            {
                prop.SetValue(obj, value, null);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static object GetProperty(object obj, string propName)
        {
            PropertyInfo prop = GetProp(obj.GetType(), propName);
            if (prop == null) return null;
            try
            {
                return prop.GetValue(obj, null);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static object CallMethod(object obj, string methodName)
        {
            MethodInfo method = GetMethod(obj.GetType(), methodName);
            if (method == null) return null;
            return method.Invoke(obj, null);
        }
        public static object CallMethod(object obj, string methodName, params object[] args)
        {
            MethodInfo method = GetMethod(obj.GetType(), methodName);
            if (method == null) return null;
            return method.Invoke(obj, args);
        }

        public static EventInfo GetEvent(Type type, string eventName)
        {
            EventInfo evinfo = null;
            while (type != null)
            {
                evinfo = type.GetEvent(eventName, BindingFlags.Instance | BindingFlags.NonPublic);
                if (evinfo != null) break;

                type = type.BaseType;
            }
            return evinfo;
        }

        public static PropertyInfo GetProp(this Type type, string propName)
        {
            PropertyInfo prop = null;
            while (type != null)
            {
                prop = type.GetProperty(propName, BindingFlags.Public | 
                    BindingFlags.Instance | BindingFlags.Static | 
                    BindingFlags.NonPublic);
                if (prop != null) break;

                type = type.BaseType;
            }
            return prop;
        }

        public static FieldInfo GetField(Type type, string fieldName)
        {
            FieldInfo field = null;
            while (type != null)
            {
                field = type.GetField(fieldName, BindingFlags.Public | 
                    BindingFlags.Static | BindingFlags.Instance | 
                    BindingFlags.NonPublic);
                if (field != null) break;

                type = type.BaseType;
            }
            return field;
        }

        public static MethodInfo GetMethod(Type type, string methodName)
        {
            MethodInfo method = null;
            while (type != null)
            {
                method = type.GetMethod(methodName, BindingFlags.Public | 
                    BindingFlags.Static | BindingFlags.Instance | 
                    BindingFlags.NonPublic);
                if (method != null) break;

                type = type.BaseType;
            }
            return method;
        }

        private static readonly Dictionary<Type, XmlSerializer> _xmlSerializerCache = new Dictionary<Type, XmlSerializer>();

        public static XmlSerializer CreateDefaultXmlSerializer(Type type)
        {
            XmlSerializer serializer;
            if (_xmlSerializerCache.TryGetValue(type, out serializer))
            {
                return serializer;
            }
            else
            {
                var importer = new XmlReflectionImporter();
                var mapping = importer.ImportTypeMapping(type, null, null);
                serializer = new XmlSerializer(mapping);
                return _xmlSerializerCache[type] = serializer;
            }
        }

        public static void SetDGVShowCellToolTipsA(Control c, bool b)
        {
            if (c == null) return;
            if (c is DataGridView)
            {
                (c as DataGridView).ShowCellToolTips = b;
            }
            if (c is Form || c is Panel || c is SplitContainer)
            {
                foreach (var c1 in c.Controls)
                {
                    SetDGVShowCellToolTipsA(c1 as Control, b);
                }
            }
        }

        public static void SetDGVShowCellToolTips(this Form f, bool b)
        {
            SetDGVShowCellToolTipsA(f, b);
        }

        public static string AsString(this object o)
        {
            if (o == null || o == DBNull.Value) return null;
            return (string) o;
        }

        public static decimal AsDecimal(this object o)
        {
            if (o == null || o == DBNull.Value) return 0.00M;
            return (decimal)o;
        }
        public static float AsFloat(this object o)
        {
            if (o == null || o == DBNull.Value) return 0.00f;
            return (float)o;
        }
        public static string UnescapeXMLText(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            return new XText(s).ToString();
        }


    }
}
