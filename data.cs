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
        public static string CourseText = "Course";
        public static string CourseButtonText = "Our course";
        public static string CourseUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";

        public static string StartText = "I am a bot that will collect and send interesting videos and text materials related to blogger topics and invite users to sign " +
                            "up for a professional video blogging course.";
        public static string SubscribeText = "Congratulations! You are now subscribed to the notifications";
        public static string AlradySubscribeText = "You are alrady subscribed to the notifications";

        public static string UnsubscribeText = "Now you are not subscribed to notifications";
        public static string AlradyUnsubscribeText = "You are alrady unsubscribed to notifications";



    }
}