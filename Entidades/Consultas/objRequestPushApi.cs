using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Consultas
{
    public class objRequestPushApi
    {
        public List<string> tokens { get; set; }
        public obj_notification notification { get; set; }
    }
    public class obj_notification
    {
        public string alert { get; set; }
        public obj_ios ios { get; set; }
        public obj_android android { get; set; }
    }
    public class obj_ios
    {
        public string badge { get; set; }
        public string sound { get; set; }
        public string expiry { get; set; }
        public string priority { get; set; }
        public string contentAvailable { get; set; }
        public obj_payload payload { get; set; }
    }
    public class obj_android
    {
        public string collapseKey { get; set; }
        public string delayWhileIdle { get; set; }
        public string timeToLive { get; set; }
        public obj_payload payload { get; set; }
    }
    public class obj_payload
    {
        public string key1 { get; set; }
        public string key2 { get; set; }
    }
}
