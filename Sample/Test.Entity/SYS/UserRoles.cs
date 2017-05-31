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
	public partial class UserRoles
	{
		#region 私有变量
		private Int32 _UserId;
		private String _UserName = "";
		private Int32 _RoleId;
		#endregion

		# region 属性方法
		[Description("")]
		[DataMember]
		[Required]
        [Key]
		public Int32 UserId
		{
			get { return _UserId;}
			set { _UserId = value;}
		}

		[Description("")]
		[DataMember]
		public String UserName
		{
			get { return _UserName;}
			set { _UserName = value;}
		}

		[Description("")]
		[DataMember]
		[Required]
        [Key]
		public Int32 RoleId
		{
			get { return _RoleId;}
			set { _RoleId = value;}
		}

		#endregion
	}
}
