using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;


namespace GShineLib.Data
{
    /// <summary>
    /// MS SQL Server操作
    /// </summary>
    public class SQLDBOperator:DBOperator,IDBOperator
    {
        /// <summary>
        /// 构造一个默认不使用事务的SQLDBOperator对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public SQLDBOperator(string connectionString)
            : base(new SqlConnection(connectionString))
        {           
        }

        /// <summary>
        /// 该构造函数允许使用事务，在执行一系列非查询操作时使用事务可保证数据的一致性。
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="launchTransaction">是否开启事务。true表示开启，false表示不开启</param>
        public SQLDBOperator(string connectionString, bool launchTransaction):
            base(new SqlConnection(connectionString),launchTransaction)
        {
        }


        /// <summary>
        /// 执行SQL，返回数据行，类型为object[][]
        /// </summary>
        /// <param name="sql">
        /// 查询语句，特别要求参数格式为@符加数字，如id=@1。占位符从@0开始，
        /// 此外还要求@符号后的数字要与参数列表的索引对应方可保证赋值正确，
        /// 比如@2，对应的参数为values[2]。
        /// </param>
        /// <param name="values">参数列表</param>
        /// <returns></returns>
        public object[][] GetDataRows(string sql, params object[] values)
        {
            List<object[]> data = new List<object[]>();
            using (this.Connection)
            {
                Connection.Open();
                SqlCommand command = (SqlCommand)Connection.CreateCommand();
                command.CommandText = sql;

                for (int i = 0; i < values.Length; i++)
                {
                    command.Parameters.Add(new SqlParameter("@" + i.ToString(), values[i]));
                }

                SqlDataReader reader = command.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        object[] dataRow = new object[reader.FieldCount];
                        reader.GetValues(dataRow);
                        data.Add(dataRow);
                    }
                }

            }
            return data.ToArray();
        }

        /// <summary>
        /// 执行非查询SQL，返回受影响行数
        /// </summary>
        /// <param name="sql">
        /// SQL语句，要求参数格式为@符号加数字，数字要与参数列表中将要赋的值
        /// 的索引一致，否则可能引发错误。
        /// </param>
        /// <param name="values">参数列表</param>
        /// <returns></returns>
        public int ExecuteNonQuerySQL(string sql, params object[] values)
        {
            int count = 0;
            using (this.Connection)
            {
                this.Connection.Open();
                SqlCommand command = (SqlCommand)this.Connection.CreateCommand();
                command.CommandText = sql;

                AddCommandParameterValue(values, command);

                if (this.IsLaunchTransaction)
                {
                    SqlTransaction trans = (SqlTransaction)this.Connection.BeginTransaction();
                    command.Transaction = trans;
                    try
                    {
                        count = command.ExecuteNonQuery();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("执行异常:"+ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        count = command.ExecuteNonQuery();
                    }
                    catch(Exception ex)
                    {
                        throw new Exception("执行异常:"+ex.Message);
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// 为DBCommand对象添加参数
        /// </summary>
        /// <param name="values">参数值列表</param>
        /// <param name="command">DBCommand对象</param>
        private void AddCommandParameterValue(object[] values, SqlCommand command)
        {
            object value = null;
            for (int i = 0; i < values.Length; i++)
            {
                value = values[i] == null ? DBNull.Value : values[i];
                command.Parameters.AddWithValue("@" + i.ToString(), value);
            }
        }

        /// <summary>
        /// 获取DataSet对象
        /// </summary>
        /// <param name="sql">
        /// 查询语句，要求参数格式为@符号加数字，数字要与参数列表中将要赋的值
        /// 的索引一致，否则可能引发错误。
        /// </param>
        /// <param name="values">参数列表</param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql, params object[] values)
        {
            SqlCommand command = (SqlCommand)this.Connection.CreateCommand();
            command.CommandText = sql;
            AddCommandParameterValue(values, command);
            DataSet dataSet = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(command);
            adp.Fill(dataSet);
            return dataSet;
        }

        /// <summary>
        /// 获取DataTable对象
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="values">参数列表</param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql, params object[] values)
        {
            SqlCommand command = (SqlCommand)this.Connection.CreateCommand();
            command.CommandText = sql;
            AddCommandParameterValue(values, command);
            DataTable table = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(command);
            adp.Fill(table);
            return table;
        }
    }
}
