using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GShineLib.Data
{
    /// <summary>
    /// DBColumn集合
    /// </summary>
    public class ColumnCollection
    {
        private List<DBColumn> columnContainer;


        public int Count
        {
            get
            {
                return columnContainer.Count;
            }
        }

        /// <summary>
        /// 以默认方式实例化一个对象
        /// </summary>
        public ColumnCollection()
        {
            columnContainer = new List<DBColumn>();
        }

        /// <summary>
        /// 实例化一个对象
        /// </summary>
        /// <param name="capacity">容量</param>
        public ColumnCollection(int capacity)
        {
            columnContainer = new List<DBColumn>(capacity);
        }

        /// <summary>
        /// 获取指定索引位置的DBColumn对象
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public DBColumn this[int index]
        {
            get
            {
                return columnContainer[index];
            }
        }

        /// <summary>
        /// 通过列名称获取DBColumn对象的索引。未找到返回-1。
        /// </summary>
        /// <param name="name">列名称(不区分大小写)</param>
        /// <returns></returns>
        public int GetIndexByName(string name)
        {
            int index = -1;
            for (int i = 0; i < columnContainer.Count; i++)
            {
                if (string.Equals(name, columnContainer[i].Name,StringComparison.OrdinalIgnoreCase))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        /// <summary>
        /// 向集合中添加一个新对象，并返回新添加的对象。
        /// </summary>
        /// <param name="column">要添加的DBColumn对象</param>
        /// <returns></returns>
        public DBColumn Add(DBColumn column)
        {
            columnContainer.Add(column);
            return column;
        }

        /// <summary>
        /// 判断是否存在指定名称的列
        /// </summary>
        /// <param name="columnName">要查询的列名</param>
        /// <returns></returns>
        public bool Exist(string columnName)
        {
            bool exist = false;
            int index = GetIndexByName(columnName);
            if (index != -1)
            {
                exist = true;
            }
            return exist;
        }

    }
}
