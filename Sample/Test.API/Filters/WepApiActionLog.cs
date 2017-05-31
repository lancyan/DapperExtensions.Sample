using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.API.Filters
{
    class WepApiActionLog
    {
        public Guid Id { get; set; }

        public string actionName { get; set; }

        public string controllerName { get; set; }

        public DateTime enterTime { get; set; }

        public double costTime { get; set; }

        public string navigator { get; set; }

        public string token { get; set; }

        public string ip { get; set; }

        public string userHostName { get; set; }

        public string urlReferrer { get; set; }

        public string browser { get; set; }

        public string paramaters { get; set; }

        public string executeResult { get; set; }

        public string comments { get; set; }

        public string requestUri { get; set; }

        public string userId { get; set; }


        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Id " + Id);
            sb.AppendLine("actionName " + actionName);
            sb.AppendLine("controllerName " + controllerName);
            sb.AppendLine("enterTime " + enterTime);
            sb.AppendLine("costTime " + costTime);
            sb.AppendLine("navigator " + navigator);
            sb.AppendLine("browser " + browser);
            sb.AppendLine("token " + token);
            sb.AppendLine("ip " + ip);
            sb.AppendLine("userHostName " + userHostName);
            sb.AppendLine("urlReferrer " + urlReferrer);
            sb.AppendLine("paramaters " + paramaters);
            sb.AppendLine("requestUri " + requestUri);
            sb.AppendLine("executeResult " + executeResult);

            return sb.ToString();
        }
    }
}
