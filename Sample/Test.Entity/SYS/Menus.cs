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
	public partial class Menus
	{
		#region 私有变量
		private Int32 _Id;
		private Int32? _ParentId;
        private String _ParentName;
		private String _Name = "";
        private String _Code = "";
        private String _Action = "";
		private String _Url = "";
		//private String _RightCode = "";
		private String _Ico = "";
		private Int32? _Sort;
		private Int32? _Type;
		private Int32? _Status;
		//private String _Remark = "";
		#endregion

		# region 属性方法
		[Description("id")]
		[DataMember]
		[Key]
		[Required]
		public Int32 Id
		{
			get { return _Id;}
			set { _Id = value;}
		}

		[Description("父id")]
		[DataMember]
		public Int32? ParentId
		{
			get { return _ParentId;}
			set { _ParentId = value;}
		}
        [Description("父名称")]
        [DataMember]
        public String ParentName
        {
            get { return _ParentName; }
            set { _ParentName = value; }
        }
		[Description("名称")]
		[DataMember]
		public String Name
		{
			get { return _Name;}
			set { _Name = value;}
		}

		[Description("链接")]
		[DataMember]
		public String Url
		{
			get { return _Url;}
			set { _Url = value;}
		}

		[Description("操作码")]
		[DataMember]
		public String Code
		{
			get { return _Code;}
			set { _Code = value;}
		}
        [Description("方法")]
        [DataMember]
        public String Action
        {
            get { return _Action; }
            set { _Action = value; }
        }
		[Description("图标")]
		[DataMember]
		public String Ico
		{
			get { return _Ico;}
			set { _Ico = value;}
		}

		[Description("排序")]
		[DataMember]
		public Int32? Sort
		{
			get { return _Sort;}
			set { _Sort = value;}
		}

        [Description("0功能1模块2菜单")]
		[DataMember]
		public Int32? Type
		{
			get { return _Type;}
			set { _Type = value;}
		}

		[Description("菜单是否可见")]
		[DataMember]
		public Int32? Status
		{
			get { return _Status;}
			set { _Status = value;}
		}

        //[Description("")]
        //[DataMember]
        //public String Remark
        //{
        //    get { return _Remark;}
        //    set { _Remark = value;}
        //}

		#endregion
	}
}
