using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GShineLib.Data
{
    /// <summary>
    /// DBField集合
    /// </summary>
    public class FieldCollection
    {
        private List<DBField> fieldContainer;
        

        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="capacity">容量</param>
        public FieldCollection(int capacity)
        {
            fieldContainer = new List<DBField>(capacity);
        }

        /// <summary>
        /// 以默认方式实例化对象
        /// </summary>
        public FieldCollection()
        {
            fieldContainer = new List<DBField>();
        }

        /// <summary>
        /// 获取DBField对象的数量
        /// </summary>
        public int Count
        {
            get
            {
                return fieldContainer.Count;
            }
        }

        /// <summary>
        /// 获取或设置指定索引位置的DBField对象
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public DBField this[int index]
        {
            get
            {
                return fieldContainer[index];
            }
            set
            {
                fieldContainer[index] = value;
            }
        }

        /// <summary>
        /// 通过名称获取或设置对应的DBField对象
        /// </summary>
        /// <param name="name">字段名称，不区分大小写</param>
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
                    fieldContainer[index] = value;
                }
            }
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
            for (int i = 0; i < fieldContainer.Count; i++)
            {
                if (string.Equals(fieldContainer[i].Column.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        /// <summary>
        /// 通过字段名称获取DBField对象
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <returns></returns>
        public DBField GetFieldByName(string name)
        {
            DBField field = null;
            for (int i = 0; i < fieldContainer.Count; i++)
            {
                if (string.Equals(fieldContainer[i].Column.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    field = fieldContainer[i];
                    break;
                }
            }
            return field;
        }

        /// <summary>
        /// 添加DBField对象并返回新添加的对象
        /// </summary>
        /// <param name="field">添加的DBField对象</param>
        /// <returns></returns>
        public DBField Add(DBField field)
        {
            fieldContainer.Add(field);
            return field;
        }

        /// <summary>
        /// 移除指定索引位置的DBField对象
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public DBField RemoveAt(int index)
        {
            DBField field = fieldContainer[index];
            fieldContainer.RemoveAt(index);
            return field;
        }
    }
}
