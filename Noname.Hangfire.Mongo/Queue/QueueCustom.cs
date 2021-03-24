using System.Collections.Generic;

namespace Noname.Hangfire.Mongo.Models
{
    /// <summary>
    ///
    /// </summary>
    public class QueueCustom
    {
        /// <summary>
        ///
        /// </summary>
        public const string Queue0 = "0";

        /// <summary>
        ///
        /// </summary>
        public const string Queue1 = "1";

        /// <summary>
        ///
        /// </summary>
        public const string Queue10 = "10";

        /// <summary>
        ///
        /// </summary>
        public const string Queue100 = "100";

        /// <summary>
        ///
        /// </summary>
        public const string Queue11 = "11";

        /// <summary>
        ///
        /// </summary>
        public const string Queue12 = "12";

        /// <summary>
        ///
        /// </summary>
        public const string Queue13 = "13";

        /// <summary>
        ///
        /// </summary>
        public const string Queue2 = "2";

        /// <summary>
        ///
        /// </summary>
        public const string Queue20 = "20";

        /// <summary>
        ///
        /// </summary>
        public const string Queue3 = "3";

        /// <summary>
        ///
        /// </summary>
        public const string Queue4 = "4";

        /// <summary>
        ///
        /// </summary>
        public const string Queue5 = "5";

        /// <summary>
        ///
        /// </summary>
        public const string Queue6 = "6";

        /// <summary>
        ///
        /// </summary>
        public const string Queue7 = "7";

        /// <summary>
        ///
        /// </summary>
        public const string Queue8 = "8";

        /// <summary>
        ///
        /// </summary>
        public const string Queue9 = "9";

        /// <summary>
        ///
        /// </summary>
        public const string QueueDefault = "default";

        /// <summary>
        ///
        /// </summary>
        public static List<string> GetQueues() => new List<string>()
        {
            GetValue(Queue0), GetValue(Queue1), GetValue(Queue2), GetValue(Queue3), GetValue(Queue4), GetValue(Queue5),
            GetValue(Queue6), GetValue(Queue7), GetValue(Queue8), GetValue(Queue9), GetValue(Queue10), GetValue(Queue11),
            GetValue(Queue12), GetValue(Queue13), GetValue(Queue20), GetValue(Queue100),
            GetValue(QueueDefault), GetValue(QueueDefault.ToUpper()),QueueDefault,QueueDefault.ToUpper()
        };

        /// <summary>
        ///
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        public static string GetValue(string queue)
        {
            var res = QueueDefault;
            switch (queue)
            {
                case Queue0:
                case Queue1:
                case Queue2:
                case Queue3:
                case Queue4:
                case Queue5:
                case Queue6:
                case Queue7:
                case Queue8:
                case Queue9:
                case Queue10:
                case Queue11:
                case Queue12:
                case Queue13:
                case Queue20:
                case Queue100:
                    res = queue;
                    break;

                default:
                    res = QueueDefault;
                    break;
            }

            return res;
        }
    }
}