using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Test.Entity
{
	[Serializable]
	[DataContract]
	public partial class Goods
	{
		#region 私有变量
		private Int32 _ID;
		private String _Name = "";
		private Decimal? _Price;
		private String _Unit = "";
		#endregion

		# region 属性方法
		[Description("ID")]
		[DataMember]
		[Key]
		[Required]
		public Int32 ID
		{
			get { return _ID;}
			set { _ID = value;}
		}

		[Description("名称")]
		[DataMember]
		public String Name
		{
			get { return _Name;}
			set { _Name = value;}
		}

		[Description("单价")]
		[DataMember]
		public Decimal? Price
		{
			get { return _Price;}
			set { _Price = value;}
		}

		[Description("单位")]
		[DataMember]
		public String Unit
		{
			get { return _Unit;}
			set { _Unit = value;}
		}

		#endregion
	}
}
