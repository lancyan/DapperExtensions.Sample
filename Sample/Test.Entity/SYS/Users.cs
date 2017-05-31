using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Test.Entity.SYS
{
    [Serializable]
    [DataContract]
    public partial class Users
    {
        #region 私有变量
        private Int32 _ID;
        private String _UserName = "";
        private String _NickName = "";
        private Int32 _Sex = 0;
        private DateTime _Birthday;
        private String _Password = "";
        private String _Email = "";
        private String _CardID = "";
        private String _Tel = "";
        private String _Mobile = "";
        private Int32 _Status;
        private DateTime _CreateTime;
        private DateTime _UpdateTime;

        //private bool isSetBirthday = false;
        //private bool isSetCreateTime = false;
        //private bool isSetUpdateTime = false;
        #endregion

        public Users()
        {
        }

        # region 属性方法
        [Description("ID")]
        [Key]
        [DataMember]
        [Required]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        [Description("用户名")]
        [DataMember]
        public String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        [Description("昵称")]
        [DataMember]
        public String NickName
        {
            get { return _NickName; }
            set { _NickName = value; }
        }

        [Description("性别")]
        [DataMember]
        public Int32 Sex
        {
            get { return _Sex; }
            set { _Sex = value; }
        }

        [Description("生日")]
        [DataMember]
        //[JsonConverter(typeof(ChinaDateTimeConverter))]
        public DateTime Birthday
        {
            get
            {
                if (_Birthday == null || _Birthday == DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                return _Birthday;
            }
            set { _Birthday = value; }
        }

        [Description("密码")]
        [DataMember]
        [Required]
        public String Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        [Description("邮箱")]
        [DataMember]
        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        [Description("身份证")]
        [DataMember]
        [Required]
        public String CardId
        {
            get { return _CardID; }
            set { _CardID = value; }
        }

        [Description("电话")]
        [DataMember]
        [Required]
        public String Tel
        {
            get { return _Tel; }
            set { _Tel = value; }
        }

        [Description("手机号码")]
        [DataMember]
        public String Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }

        [Description("状态")]
        [DataMember]
        public Int32 Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        [Description("创建时间")]
        [DataMember]
        //[JsonConverter(typeof(ChinaDateTimeConverter))]
        [Required]
        public DateTime CreateTime
        {
            get
            {
                if (_CreateTime == null || _CreateTime == DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                return _CreateTime;
            }
            set { _CreateTime = value; }
        }

        [Description("更新时间")]
        [DataMember]
        //[JsonConverter(typeof(ChinaDateTimeConverter))]
        [Required]
        public DateTime UpdateTime
        {
            get
            {
                if (_UpdateTime == null || _CreateTime == DateTime.MinValue)
                {
                    return new DateTime(1900, 1, 1);
                }
                return _UpdateTime;
            }
            set { _UpdateTime = value; }
        }

        #endregion
    }
}
