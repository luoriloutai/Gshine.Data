using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GShineLib.Data
{
    /// <summary>
    /// 提供各种枚举值
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// 数据表连接方式枚举
        /// </summary>
        public enum TableJoinType
        {
            /// <summary>
            /// 左连接，完全显示左表数据
            /// </summary>
            LeftJoin,
            /// <summary>
            /// 内连接，左右表完全匹配后的数据
            /// </summary>
            InnerJoin,
            /// <summary>
            /// 右连接，完全显示右表数据
            /// </summary>
            RightJoin
        }

        /// <summary>
        /// 数据集并集关系枚举
        /// </summary>
        public enum DataSetUnoinType
        {
            /// <summary>
            /// 全合并，合并后数据不进行重复筛选
            /// </summary>
            UnionAll,
            /// <summary>
            /// 筛选合并，合并后的数据如果有重复则自动进行筛选过滤
            /// </summary>
            Union
        }

        /// <summary>
        ///  数据库字段类型枚举
        /// </summary>
        public enum DataType
        {
            BigInt,
            Binary,
            Bit,
            Char,
            Date,
            DateTime,
            DateTime2,
            DateTimeOffSet,
            Decimal,
            Float,
            Geography,
            Geometry,
            HierachyID,
            Image,
            Int,
            Money,
            NChar,
            NText,
            Numeric,
            NVarchar,
            Real,
            SmallDateTime,
            SmallInt,
            SmallMoney,
            Sql_Variant,
            Sysname,
            Text,
            Time,
            Timestamp,
            TinyInt,
            UniqueIdentifier,
            VarBinary,
            VarChar,
            Xml,
            Undefined
        }


        /// <summary>
        /// 数据记录的状态
        /// </summary>
        public enum DBRecordState
        {
            /// <summary>
            /// 已保存
            /// </summary>
            Saved,
            /// <summary>
            /// 新建
            /// </summary>
            New
        }

        /// <summary>
        /// 根据名称字符串获取列数据类型
        /// </summary>
        /// <param name="nameString">名称字符串(不区分大小写)</param>
        /// <returns></returns>
        public static DataType GetColumnDataTypeByNameString(string nameString)
        {
            try
            {
                DataType type = (DataType)Enum.Parse(typeof(DataType), nameString,true);
                return type;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
