using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GShineLib.Data
{
    /// <summary>
    /// MS SQL Server 表类
    /// </summary>
    public class SQLDBTable : DBTable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="identifierName">标识字段名称</param>
        /// <param name="connectionString">连接字符串</param>
        public SQLDBTable(string tableName, string identifierName, string connectionString)
            : base(tableName, identifierName, connectionString)
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="identifierName">标识字段名称</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="columns">指定的DBColumn数组</param>
        public SQLDBTable(string tableName, string identifierName, string connectionString, params DBColumn[] columns)
            : base(tableName, identifierName, connectionString, columns)
        {
        }


        /// <summary>
        /// 填充表数据Records
        /// </summary>
        /// <param name="sql">查询语句</param>
        protected override void FillRecords(string sql)
        {
            SQLDBOperator opt = new SQLDBOperator(this.ConnectionString);
            object[][] dataRows = opt.GetDataRows(sql);

            foreach (object[] dataRow in dataRows)
            {
                SQLDBRecord record = new SQLDBRecord(this.TableName, this.IdentifierName, this.ConnectionString, Columns);
                for (int i = 0; i < dataRow.Length; i++)
                {
                    record.Fields[i].Value = dataRow[i];
                }
                record.State = Enums.DBRecordState.Saved;
                this.Records.Add(record);
            }
        }

        /// <summary>
        /// 初始化全部列
        /// </summary>
        protected override void InitializeColumn()
        {
            string sql = @"SELECT sys.syscolumns.name, sys.systypes.name AS type
                            FROM sys.syscolumns INNER JOIN
                            sys.sysobjects ON sys.syscolumns.id = sys.sysobjects.id AND sys.sysobjects.name = @0 AND 
                            sys.sysobjects.xtype = 'U' INNER JOIN
                            sys.systypes ON sys.systypes.xusertype = sys.syscolumns.xusertype";

            SQLDBOperator dbOperator = new SQLDBOperator(this.ConnectionString);
            object[][] rows = dbOperator.GetDataRows(sql, this.TableName);

            for (int i = 0; i < rows.Length; i++)
            {
                Enums.DataType type = Enums.GetColumnDataTypeByNameString(rows[i][1].ToString());
                DBColumn column = new DBColumn(rows[i][0].ToString(), type);
                this.Columns.Add(column);
            }
        }

        /// <summary>
        /// 初始化指定列
        /// </summary>
        /// <param name="columns">初始化的列数组</param>
        protected override void InitializeColumn(params DBColumn[] columns)
        {
            for (int i = 0; i < columns.Length; i++)
            {
                this.Columns.Add(columns[i]);
            }
        }

        /// <summary>
        /// 创建一个DBRecord对象，该对象包含的列由初始化决定。
        /// </summary>
        /// <returns></returns>
        public override DBRecord CreateRecord()
        {
            SQLDBRecord record = new SQLDBRecord(this.TableName, this.IdentifierName, this.ConnectionString, this.Columns);
            return record;
        }

        /// <summary>
        /// 根据标识字段删除记录
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="id">标识字段值</param>
        protected override void DeleteRecord(string sql, object id)
        {
            SQLDBOperator opt = new SQLDBOperator(this.ConnectionString, true);
            opt.ExecuteNonQuerySQL(sql, id);
        }
    }
}
