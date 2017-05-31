using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Test.Entity.SYS
{
	[Serializable]
	[DataContract]
	public partial class RoleMenus
	{
		#region 私有变量
		private Int32 _RoleId;
		private Int32 _MenuId;
        private Int32 _Status;
        private DateTime? _CreateTime;
        private DateTime? _UpdateTime;

      
		#endregion

		# region 属性方法
		[Description("")]
		[DataMember]
		[Required]
        [Key]
		public Int32 RoleId
		{
			get { return _RoleId;}
			set { _RoleId = value;}
		}

		[Description("")]
		[DataMember]
		[Required]
        [Key]
		public Int32 MenuId
		{
			get { return _MenuId;}
			set { _MenuId = value;}
		}
        [Description("")]
        [DataMember]
        public Int32 Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

		[Description("")]
		[DataMember]
		public DateTime? CreateTime
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
        [Description("")]
        [DataMember]
        public DateTime? UpdateTime
        {
            get
            {
                if (_UpdateTime == null || _UpdateTime == DateTime.MinValue)
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
