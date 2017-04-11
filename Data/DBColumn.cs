using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GShineLib.Data
{
    /// <summary>
    /// 表示表列信息
    /// </summary>
    public class DBColumn
    {
        string name;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private Enums.DataType dataType;

        /// <summary>
        /// 数据类型(枚举)
        /// </summary>
        public Enums.DataType DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

        /// <summary>
        /// 初始化DBColumn对象
        /// </summary>
        /// <param name="name">列名称（要与数据库中列名对应）</param>
        /// <param name="dataType">列数据类型（枚举值）</param>
        public DBColumn(string name, Enums.DataType dataType)
        {
            this.name = name;
            this.dataType = dataType;
        }
    }
}
