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
	public partial class Bills
	{
		#region 私有变量
		private Int32 _ID;
		private Int32? _UserID;
		private Int32? _GoodID;
		private Int32? _Count;
		private Decimal? _PriceSum;
		private DateTime? _CreateTime;
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

		[Description("用户ID")]
		[DataMember]
		public Int32? UserID
		{
			get { return _UserID;}
			set { _UserID = value;}
		}

		[Description("商品ID")]
		[DataMember]
		public Int32? GoodID
		{
			get { return _GoodID;}
			set { _GoodID = value;}
		}

		[Description("数量")]
		[DataMember]
		public Int32? Count
		{
			get { return _Count;}
			set { _Count = value;}
		}

		[Description("总价")]
		[DataMember]
		public Decimal? PriceSum
		{
			get { return _PriceSum;}
			set { _PriceSum = value;}
		}

		[Description("创建时间")]
		[DataMember]
		public DateTime? CreateTime
		{
			get { return _CreateTime;}
			set { _CreateTime = value;}
		}

		#endregion
	}
}
