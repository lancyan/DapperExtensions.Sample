using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Test.Entity.SYS
{
    [Serializable]
    [DataContract]
    public class UserData
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Role { get; set; }
    }

    [Serializable]
    [DataContract]
    public class UserDatas
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string[] Roles { get; set; }
    }
}
