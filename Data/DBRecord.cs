using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace GShineLib.Data
{
    /// <summary>
    /// 对应于数据库的一条记录，该类为抽象类
    /// </summary>
    public abstract class DBRecord
    {
        DBField identifierField;

        /// <summary>
        /// 标识字段，主键
        /// </summary>
        public DBField IdentifierField
        {
            get { return identifierField; }
            set { identifierField = value; }
        }

        string tableName;

        /// <summary>
        /// 数据记录所属表
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }


        private FieldCollection fields;

        /// <summary>
        /// 记录所包含的字段集合
        /// </summary>
        public FieldCollection Fields
        {
            get { return fields; }
            set { fields = value; }
        }


        /// <summary>
        /// 获取或设置记录指定索引处的字段对象。字段索引默认与表的列的排列顺序相同，即Record[1]对应于表列索引为1的那个列的字段对象。
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public DBField this[int index]
        {
            get
            {
                return fields[index];
            }
            set
            {
                fields[index] = value;
            }
        }

        /// <summary>
        /// 获取或设置记录指定列名的字段对象
        /// </summary>
        /// <param name="name">列名,不区分大小写</param>
        /// <returns></returns>
        public DBField this[string name]
        {
            get
            {
                return GetFieldByName(name);
            }

            set
            {
                int index = GetIndexOfFieldsByName(name);
                if (index > -1)
                {
                    fields[index] = value;
                }
            }
        }

        /// <summary>
        /// 通过字段名称获取字段对象
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <returns></returns>
        public DBField GetFieldByName(string name)
        {
            DBField field = null;
            for (int i = 0; i < fields.Count; i++)
            {
                if (string.Equals(fields[i].Column.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    field = fields[i];
                    break;
                }
            }
            return field;
        }

        /// <summary>
        /// 通过字段名称获取字段在记录集合中的索引位置
        ///     返回-1表示未找到
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <returns></returns>
        public int GetIndexOfFieldsByName(string name)
        {
            int index = -1;
            for (int i = 0; i < fields.Count; i++)
            {
                if (string.Equals(fields[i].Column.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private Enums.DBRecordState state;

        /// <summary>
        /// 数据状态
        /// </summary>
        public Enums.DBRecordState State
        {
            get { return state; }
            set { state = value; }
        }

        private int fieldCount;

        /// <summary>
        /// 数据列数
        /// </summary>
        public int FieldCount
        {
            get { return fieldCount; }
        }

        private string connectionString = string.Empty;

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get { return connectionString; }
        }


        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="identifierName">主键字段</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="columns">列集合</param>
        public DBRecord(string tableName, string identifierName, string connectionString, ColumnCollection columns)
        {
            this.tableName = tableName;
            this.state = Enums.DBRecordState.New;
            this.connectionString = connectionString;
            this.fields = new FieldCollection(5);
            InitialFields(this.fields, columns);

            //取得标识字段名称，此时该字段并未有值
            this.IdentifierField = GetFieldByName(identifierName);
            this.fieldCount = fields.Count;
        }

        /// <summary>
        /// 初始化字段列表
        /// </summary>
        /// <param name="fields">字段集合</param>
        /// <param name="columns">列集合</param>
        protected abstract void InitialFields(FieldCollection fields, ColumnCollection columns);

        /// <summary>
        /// 当前索引位置的字段是否是集合中的最后一个
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        protected bool IsLastField(int index)
        {
            bool isLast = true;
            if (index < fields.Count - 1)
            {
                isLast = false;
            }

            if (index > fieldCount - 1)
            {
                throw new Exception("索引超出字段列表界限！");
            }
            return isLast;
        }

        /// <summary>
        /// 保存记录
        /// </summary>
        public virtual void Save()
        {
            if (this.state == Enums.DBRecordState.New)
            {
                Insert();
            }
            else
            {
                Update();
            }
        }


        /// <summary>
        /// 新增数据
        /// </summary>
        /// <returns></returns>
        protected int Insert()
        {
            int effectLines = 0;
            string sql = string.Format(@"insert into [{0}] (", this.tableName);

            for (int i = 0; i < fields.Count; i++)
            {
                sql += string.Format(@"[{0}]", fields[i].Column.Name);
                if (!IsLastField(i))
                {
                    sql += ",";
                }
            }

            sql += ") values (";

            List<object> parameters = new List<object>(5);
            for (int i = 0; i < fields.Count; i++)
            {
                sql += string.Format("@{0}", i.ToString());
                if (!IsLastField(i))
                {
                    sql += ",";
                }
                parameters.Add(fields[i].Value);
            }

            sql += ")";

            SQLDBOperator opt = new SQLDBOperator(this.connectionString, true);
            effectLines = opt.ExecuteNonQuerySQL(sql, parameters.ToArray());

            return effectLines;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <returns></returns>
        protected int Update()
        {
            int effectLines = 0;
            string sql = string.Format("update [{0}] set ", this.tableName);
            List<object> parameters = new List<object>(5);
            for (int i = 0; i < fields.Count; i++)
            {
                sql += string.Format("[{0}]=@{1}", fields[i].Column.Name, i.ToString());

                if (!IsLastField(i))
                {
                    sql += ",";
                }
                parameters.Add(fields[i].Value);
            }

            if (this.identifierField == null)
            {
                throw new Exception("未找到标识字段，请检查初始化DBTable对象时的标识字段名称是否正确！");
            }

            sql += string.Format(" where {0}='{1}'", this.identifierField.Column.Name, this.identifierField.Value);

            SQLDBOperator opt = new SQLDBOperator(this.connectionString);
            opt.IsLaunchTransaction = true;
            effectLines = opt.ExecuteNonQuerySQL(sql, parameters.ToArray());
            return effectLines;
        }

        /// <summary>
        /// 删除当前Record
        /// </summary>
        public virtual void Delete()
        {
            string sql = string.Format("Delete from [{0}] where [{1}]='{2}'", this.tableName, this.identifierField.Column.Name, this.identifierField.Value);
            SQLDBOperator opt = new SQLDBOperator(connectionString, true);
            opt.ExecuteNonQuerySQL(sql);
        }
    }
}
