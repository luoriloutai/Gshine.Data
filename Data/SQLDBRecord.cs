using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace GShineLib.Data
{
    /// <summary>
    /// MS SQL Server数据类
    /// </summary>
    public class SQLDBRecord:DBRecord
    {
        public SQLDBRecord(string tableName, string identifierName, string connectionString, ColumnCollection columns)
            : base(tableName, identifierName,connectionString,columns)
        {
        }

        /// <summary>
        /// 初始化字段集合
        /// </summary>
        /// <param name="fields">字段集合</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="columns">列集合</param>
        protected override void InitialFields(FieldCollection fields, ColumnCollection columns)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                DBField field = new DBField(columns[i], null);
                fields.Add(field);
            }
        }
    }
}
