using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GShineLib.Data
{
    public interface IDBOperator
    {
        /// <summary>
        /// 执行一条非查询SQL语句
        /// 返回受影响行数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="values">参数值列表</param>
        /// <returns></returns>
        int ExecuteNonQuerySQL(string sql, params object[] values);

        /// <summary>
        /// 根据语句获取数据行
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="values">参数值列表</param>
        /// <returns></returns>
        object[][] GetDataRows(string sql, params object[] values);

        /// <summary>
        /// 获取DataSet对象
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="values">参数值列表</param>
        /// <returns></returns>
        DataSet GetDataSet(string sql, params object[] values);

        /// <summary>
        /// 获取DataTable对象
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="values">参数值列表</param>
        /// <returns></returns>
        DataTable GetDataTable(string sql, params object[] values);
    }
}
