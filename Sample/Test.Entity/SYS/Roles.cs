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
	public partial class Roles
	{
		#region 私有变量
		private Int32 _Id;
        private Int32? _ParentId;
		private String _Name = "";
		private String _Code = "";
		#endregion

		# region 属性方法
		[Description("")]
		[DataMember]
		[Key]
		[Required]
		public Int32 Id
		{
			get { return _Id;}
			set { _Id = value;}
		}
        [Description("")]
        [DataMember]
        [Required]
        public Int32? ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }
		[Description("")]
		[DataMember]
		public String Name
		{
			get { return _Name;}
			set { _Name = value;}
		}

		[Description("")]
		[DataMember]
		public String Code
		{
			get { return _Code;}
			set { _Code = value;}
		}

		#endregion
	}
}
