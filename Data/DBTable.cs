using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GShineLib.Data
{
    /// <summary>
    /// 对应于数据库的表或视图
    /// </summary>
    public abstract class DBTable
    {
        private string tableName;

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }


        ColumnCollection columns;

        /// <summary>
        /// 表列集合
        /// </summary>
        public ColumnCollection Columns
        {
            get { return columns; }
            set { columns = value; }
        }

        private RecordCollection records;

        /// <summary>
        /// 记录集合
        /// </summary>
        public RecordCollection Records
        {
            get { return records; }
            set { records = value; }
        }

        private string connectionString;

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        private string identifierName;

        /// <summary>
        /// 标识列名称
        /// </summary>
        public string IdentifierName
        {
            get { return identifierName; }
        }

        /// <summary>
        /// 构造一个DBTable对象
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="identifierName">标识字段名称</param>
        /// <param name="connectionString">数据库连接字符串</param>
        public DBTable(string tableName, string identifierName, string connectionString)
        {
            this.connectionString = connectionString;
            this.tableName = tableName;
            this.records = new RecordCollection(5);
            this.columns = new ColumnCollection(5);
            this.identifierName = identifierName;
            InitializeColumn();
        }

        /// <summary>
        /// 构造一个对象。该方法需手动指定所需的数据字段。
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="identifierName">标识字段名称</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="columns">DBColumn数组</param>
        public DBTable(string tableName, string identifierName, string connectionString, params DBColumn[] columns)
        {
            this.connectionString = connectionString;
            this.tableName = tableName;
            this.identifierName = identifierName;
            this.records = new RecordCollection(5);
            this.columns = new ColumnCollection(5);
            InitializeColumn(columns);
        }

        /// <summary>
        /// 初始化全部列。自动获取指定表的全部数据字段，如果需指定某些字段，请使用有参数的重载方法。
        /// </summary>
        protected abstract void InitializeColumn();

        /// <summary>
        /// 初始化指定表列。
        /// </summary>
        /// <param name="column">DBColumn数组，表示要初始化的列</param>
        protected abstract void InitializeColumn(params DBColumn[] columns);

        /// <summary>
        /// 判断当前索引列是否是最后一列
        /// </summary>
        /// <param name="index">当前索引</param>
        /// <returns></returns>
        protected bool IsLastColumn(int index)
        {
            bool isLast = true;
            if (index < this.columns.Count - 1)
            {
                isLast = false;
            }
            if (index > this.columns.Count - 1)
            {
                throw new Exception("当前索引超出了列索引！");
            }
            return isLast;
        }

        /// <summary>
        /// 加载全部数据，填充Records。
        /// </summary>
        public void LoadData()
        {
            string sql = GetSelectString(null, null);
            FillRecords(sql);
        }

        /// <summary>
        /// 根据查询条件加载数据，并填充Records。
        /// </summary>
        /// <param name="sqlWhere">查询条件，SQL语句无需加“Where”字样，直接写查询条件。</param>
        public void LoadData(string sqlWhere)
        {
            string sql = GetSelectString(sqlWhere, null);
            FillRecords(sql);
        }

        /// <summary>
        /// 填充Records
        /// </summary>
        /// <param name="sql">查询语句</param>
        protected abstract void FillRecords(string sql);


        /// <summary>
        /// 根据查询语句加载数据
        /// </summary>
        /// <param name="where">查询条件，SQL无需加“Where”字样，直接写查询条件。</param>
        /// <param name="order">排序条件，SQL无需加“order by”字样，直接写排序字段。</param>
        public void LoadData(string where, string order)
        {
            string sql = GetSelectString(where, order);
            FillRecords(sql);
        }

        /// <summary>
        /// 获取查询语句
        /// </summary>
        /// <returns></returns>
        protected string GetSelectString()
        {
            return GetSelectString(null, null);
        }

        /// <summary>
        /// 获取查询语句
        /// </summary>
        /// <param name="where">查询条件,不需要在SQL语句中加“where”字样，只需直接写查询条件即可。</param>
        /// <param name="order">排序条件,不需要在SQL语句中加“order by”字样，只需直接写排序字段。</param>
        /// <returns></returns>
        protected string GetSelectString(string where, string order)
        {
            string col = "";
            for (int i = 0; i < Columns.Count; i++)
            {
                col += string.Format("[{0}]", Columns[i].Name);
                if (!IsLastColumn(i))
                {
                    col += ",";
                }
            }
            string sql = string.Empty;
            if (string.IsNullOrEmpty(where) || string.IsNullOrWhiteSpace(where))
            {
                sql = @"select {0} from [{1}] ";
                sql = string.Format(sql, col, this.tableName);
            }
            else
            {
                sql = @"select {0} from [{1}] where {2} ";
                sql = string.Format(sql, col, this.tableName, where);
            }

            if (!string.IsNullOrEmpty(order) || !string.IsNullOrWhiteSpace(order))
            {
                sql += " order by " + order;
            }

            return sql;
        }


        /// <summary>
        /// 创建一个DBRecord对象，该对象包含的列由初始化决定。
        /// </summary>
        /// <returns></returns>
        public abstract DBRecord CreateRecord();

        /// <summary>
        /// 根据条件删除记录
        /// </summary>
        /// <param name="sqlWhere">查询条件，注意该条件在写SQL语句时不需要加“Where”字样，直接写查询条件。值为null或空时删除全部数据。</param>
        public void DeleteRecords(string sqlWhere)
        {
            if (string.IsNullOrEmpty(sqlWhere))
            {
                throw new Exception("查询条件不能为空！");
            }
            string sql = "";
            if (string.IsNullOrEmpty(sqlWhere) || string.IsNullOrWhiteSpace(sqlWhere))
            {
                sql = string.Format("delete from [{0}] ", this.tableName);
            }
            else
            {
                sql = string.Format("delete from [{0}] where {1}", this.tableName, sqlWhere);
            }
            SQLDBOperator opt = new SQLDBOperator(this.connectionString);
            opt.ExecuteNonQuerySQL(sql);
        }

        /// <summary>
        /// 清除全部数据
        /// </summary>
        public void ClearRecords()
        {
            string sql = "";
            sql = string.Format("delete from [{0}] ", this.tableName);
            SQLDBOperator opt = new SQLDBOperator(this.connectionString);
            opt.ExecuteNonQuerySQL(sql);
        }

        /// <summary>
        /// 根据主键删除记录
        /// </summary>
        /// <param name="recordID">主键</param>
        public void DeleteRecord(object recordID)
        {
            string sql = string.Format("delete from [{0}] where [{1}]=@0", this.tableName, this.identifierName);
            DeleteRecord(sql, recordID);
        }

        ///<summary>
        ///根据标识字段删除记录
        ///</summary>
        ///<param name="sql">SQL语句</param>
        ///<param name="id">标识字段值</param>
        protected abstract void DeleteRecord(string sql, object id);
    }
}
