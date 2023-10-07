using Telegram.Bot.Types;

namespace project
{
    class Data
    {
        public const string path = @"data\data.txt";

        public int hour = 0;
        public int minute = 0;
        public int second = 0;

        public string CourseText = "smile";
        public string CourseUrl = @"https://www.youtube.com/watch?v=7ZMsZJcjldU&list=LL&index=46";

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

        }

    }
}
