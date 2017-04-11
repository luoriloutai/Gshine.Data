using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;

namespace GShineLib.Data
{
    /// <summary>
    /// 表示数据表的字段
    /// </summary>
    public class DBField
    {
        object objectValue;

        /// <summary>
        /// 对象值
        /// </summary>
        public object Value
        {
            get { return this.objectValue; }
            set { this.objectValue = value; }
        }

        /// <summary>
        /// 获取字段的int型值
        /// </summary>
        /// <returns></returns>
        public int GetIntValue()
        {
            int value = 0;
            if (this.objectValue != null)
            {
                value = int.Parse(this.objectValue.ToString());
            }
            return value;
        }

        /// <summary>
        /// 获取字段的string型值
        /// </summary>
        /// <returns></returns>
        public string GetStringValue()
        {
            string value = "";
            if (this.objectValue != null)
            {
                value = this.objectValue.ToString();
            }
            return value;
        }

        /// <summary>
        /// 获取字段的float型值
        /// </summary>
        /// <returns></returns>
        public float GetFloatValue()
        {
            float value = 0;
            if (this.objectValue != null)
            {
                value = float.Parse(this.objectValue.ToString());
            }
            return value;
        }

        /// <summary>
        /// 获取该字段的double型值
        /// </summary>
        /// <returns></returns>
        public double GetDoubleValue()
        {
            double value = 0;
            if (this.objectValue != null)
            {
                value = double.Parse(this.objectValue.ToString());
            }
            return value;
        }

        /// <summary>
        /// 获取该字段的Guid型值
        /// </summary>
        /// <returns></returns>
        public Guid GetGuidValue()
        {
            Guid value = Guid.Empty;
            if (this.objectValue != null)
            {
                value = Guid.Parse(this.objectValue.ToString());
            }
            return value;
        }

        /// <summary>
        /// 获取该字段的decimal型值
        /// </summary>
        /// <returns></returns>
        public decimal GetDecimalValue()
        {
            decimal value = 0;
            if (this.objectValue != null)
            {
                value = decimal.Parse(this.objectValue.ToString());
            }
            return value;
        }

        /// <summary>
        /// 获取该字段的DateTime型值
        /// </summary>
        /// <returns></returns>
        public DateTime? GetDateTimeValue()
        {
            DateTime? value = null;
            if (this.objectValue != null)
            {
                value = Convert.ToDateTime(this.objectValue);
            }
            return value;
        }

        /// <summary>
        /// 获取该字段的long型值
        /// </summary>
        /// <returns></returns>
        private long GetLongValue()
        {
            long value = 0;
            if (this.objectValue != null)
            {
                value = long.Parse(this.objectValue.ToString());
            }
            return value;
        }


        DBColumn column;

        /// <summary>
        /// 字段所属列
        /// </summary>
        public DBColumn Column
        {
            get { return column; }
            set { column = value; }
        }



        /// <summary>
        /// 初始化数据字段
        /// </summary>
        /// <param name="column">字段所属列</param>
        ///<param name="value">值</param>
        public DBField(DBColumn column, object value)
        {
            this.column = column;
            this.objectValue = value;
        }
    }
}
