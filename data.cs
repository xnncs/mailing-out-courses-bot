using System.Numerics;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace project
{
    class Data
    {
        public const string path = @"data\data.txt";

        public int hour;
        public int minute;

        public string CourseText;
        public string CourseUrl;

        public const string CourseButtonText = "based";


        public const string adminStartText = "You are an administrator, there is a panel in front of you, you can use it to: upload videos for mailing, change the course description and change the link to the course."
                                        + "\n\nFor sent message use /sent {time} , for example /sent 23:59 ";
        public const string StartText = "I am a bot that will collect and send interesting videos and text materials related to blogger topics and invite users to sign " +
                            "up for a professional video blogging course.";
        public const string SubscribeText = "Congratulations! You are now subscribed to the notifications";
        public const string AlradySubscribeText = "You are alrady subscribed to the notifications";

        public const string UnsubscribeText = "Now you are not subscribed to notifications";
        public const string AlradyUnsubscribeText = "You are alrady unsubscribed to notifications";

        public Data()
        {
            string[] temp = System.IO.File.ReadAllLines(path);

            CourseUrl = temp[0].Trim().Remove(0, "courseurl:".Length);
            CourseText = temp[1].Trim().Remove(0, "coursetext:".Length);

            
            string[] array = temp[2].Trim().Split(' ');

            hour = Convert.ToInt32(array[0].Remove(0, "hour:".Length));
            minute = Convert.ToInt32(array[1].Remove(0, "minute:".Length));

        }

    }
}