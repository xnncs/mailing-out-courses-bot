using System.Data;
using System.Xml;
using Telegram.Bot.Types;

namespace project
{
    internal class CourseData
    {
        public string courseurl {get; private set;}
        public string coursetext {get; private set;}
        public string courseimg {get; private set;}


        public CourseData(string courseurl, string coursetext, string courseimg)
        {
            this.courseurl = courseurl;

            this.coursetext = coursetext;

            this.courseimg = courseimg;
        }
    }
    internal class TimeData
    {
        public int hour {get; private set;}
        public int minute {get; private set;}
        public int second {get; private set;}


        public TimeData(int hour, int minute, int second)
        {
            this.hour = hour;

            this.minute = minute;

            this.second = second;
        }
    }

    class Data
    {
        public const string path = @"data\data.xml";

        private TimeData timedata {get; set;}
        private CourseData coursedata {get; set;}

        public void ChangeCourseData(string courseurl = null, string coursetext = null, string courseimg = null)
        {
            Data data = new Data();
            CourseData courseData = data.GetCourse();
            if(courseurl == null)
            {
                courseurl = courseData.courseurl;
            }
            if(coursetext == null)
            {
                coursetext = courseData.coursetext;
            }
            if(courseimg == null)
            {
                courseimg = courseData.courseimg;
            }

            try
            {
                InputFile.FromUri(courseimg);
            }
            catch
            {
                return;
            }


            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);

            XmlElement xmlDocumentRoot = xmlDocument.DocumentElement;


            XmlNode courseNode = xmlDocumentRoot.SelectSingleNode("course");
            if (courseNode != null)
            {
                xmlDocumentRoot.RemoveChild(courseNode);
            }


            XmlElement courseElement = xmlDocument.CreateElement("course");

            XmlElement urlElement = xmlDocument.CreateElement("courselink");
            XmlElement textElement = xmlDocument.CreateElement("coursetext");
            XmlElement imgElement = xmlDocument.CreateElement("courseimg");

            XmlText urlText = xmlDocument.CreateTextNode(courseurl.ToString());
            XmlText textText = xmlDocument.CreateTextNode(coursetext.ToString());
            XmlText imgText = xmlDocument.CreateTextNode(courseimg.ToString());

            urlElement.AppendChild(urlText);
            textElement.AppendChild(textText);
            imgElement.AppendChild(imgText);

            courseElement.AppendChild(urlElement);
            courseElement.AppendChild(textElement);
            courseElement.AppendChild(imgElement);

            xmlDocumentRoot.AppendChild(courseElement);

            xmlDocument.Save(path);
        }
        public void ChangeTimeData(int hour, int minute, int second)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);

            XmlElement xmlDocumentRoot = xmlDocument.DocumentElement;


            XmlNode timeNode = xmlDocumentRoot.SelectSingleNode("time");

            if (timeNode != null)
            {
                xmlDocumentRoot.RemoveChild(timeNode);
            }

            XmlElement timeElement = xmlDocument.CreateElement("time");

            XmlElement hourElement = xmlDocument.CreateElement("hour");
            XmlElement minuteElement = xmlDocument.CreateElement("minute");
            XmlElement secondElement = xmlDocument.CreateElement("second");

            XmlText hourText = xmlDocument.CreateTextNode(hour.ToString());
            XmlText minuteText = xmlDocument.CreateTextNode(minute.ToString());
            XmlText secondText = xmlDocument.CreateTextNode(second.ToString());

            hourElement.AppendChild(hourText);
            minuteElement.AppendChild(minuteText);
            secondElement.AppendChild(secondText);

            timeElement.AppendChild(hourElement);
            timeElement.AppendChild(minuteElement);
            timeElement.AppendChild(secondElement);

            xmlDocumentRoot.AppendChild(timeElement);

            xmlDocument.Save(path);

        }
        private static TimeData GetTimeData (XmlNode timeNode)
        {
            TimeData data;

            int hour = 0;
            int minute = 0;
            int second = 0;

            //get time data
            foreach (XmlNode node in timeNode.ChildNodes)
            {
                if (node.Name == "hour")
                {
                    hour = Convert.ToInt32(node.InnerText);
                }
                if (node.Name == "minute")
                {
                    minute = Convert.ToInt32(node.InnerText);
                }
                if (node.Name == "second")
                {
                    second = Convert.ToInt32(node.InnerText);
                }
            }
            data = new TimeData(hour, minute, second);
            return data;
        }
        private static CourseData GetCourseData (XmlNode courseNode)
        {
            CourseData data;

            string courselink = "https://www.google.com";
            string coursetext = "none";
            string courseimg = "none";

            //get course data
            foreach (XmlNode node in courseNode.ChildNodes)
            {
                if (node.Name == "courselink")
                {
                    courselink = node.InnerText;
                }
                if (node.Name == "coursetext")
                {
                    coursetext = node.InnerText;
                }
                if (node.Name == "courseimg")
                {
                    courseimg = node.InnerText;
                }
            }
            data = new CourseData(courselink, coursetext, courseimg);
            return data;
        }

        public Data ()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);

            XmlElement xmlDocumentRoot = xmlDocument.DocumentElement;


            XmlNode timeNode = xmlDocumentRoot.SelectSingleNode("time");
            XmlNode courseNode = xmlDocumentRoot.SelectSingleNode("course");

            timedata = GetTimeData(timeNode);
            coursedata = GetCourseData(courseNode);
        }
        public TimeData GetTime()
        {
            return timedata;
        }
        public CourseData GetCourse()
        {
            return coursedata;
        }

    }
}
