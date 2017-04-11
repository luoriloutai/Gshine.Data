using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace GShineLib.Data
{
    /// <summary>
    /// 数据库操作
    /// </summary>
    public abstract class DBOperator
    {
        DbConnection connection;

        bool isLaunchTransaction;

        /// <summary>
        /// 是否开启事务
        /// </summary>
        public bool IsLaunchTransaction
        {
            get { return isLaunchTransaction; }
            set { isLaunchTransaction = value; }
        }

        public DbConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        /// <summary>
        /// 构造一个不开启事务的DBOperator对象
        /// </summary>
        /// <param name="connection">数据连接</param>
        public DBOperator(DbConnection connection)
        {
            this.connection = connection;
            this.isLaunchTransaction = false;
        }

        /// <summary>
        /// 初始化对象，提供事务处理
        /// </summary>
        /// <param name="connection">数据连接</param>
        /// <param name="launchTransaction">是否开启事务</param>
        public DBOperator(DbConnection connection, bool launchTransaction)
        {
            this.connection = connection;
            this.isLaunchTransaction = launchTransaction;
        }
    }
}
