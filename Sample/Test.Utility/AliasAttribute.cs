using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Utility
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
    public class AliasAttribute : Attribute
    {
        public static readonly AliasAttribute Default = new AliasAttribute();
        private string alias;

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public AliasAttribute()
            : this(string.Empty)
        {
        }

        /// <devdoc>
        ///    <para>Initializes a new instance of the <see cref='System.ComponentModel.DescriptionAttribute'/> class.</para>
        /// </devdoc>
        public AliasAttribute(string alias)
        {
            this.alias = alias;
        }

        /// <devdoc>
        ///    <para>Gets the description stored in this attribute.</para>
        /// </devdoc>
        public virtual string Alias
        {
            get
            {
                return AliasName;
            }
        }

        /// <devdoc>
        ///     Read/Write property that directly modifies the string stored
        ///     in the description attribute. The default implementation
        ///     of the Description property simply returns this value.
        /// </devdoc>
        protected string AliasName
        {
            get
            {
                return alias;
            }
            set
            {
                alias = value;
            }
        }
    }
}
