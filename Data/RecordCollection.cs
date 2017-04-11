using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GShineLib.Data
{
    /// <summary>
    /// DBRecord集合
    /// </summary>
    public class RecordCollection
    {
        private List<DBRecord> recordContainer;

        /// <summary>
        /// 获取DBRecord对象数量
        /// </summary>
        public int Count
        {
            get { return recordContainer.Count; }
        }

        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="capacity">容量</param>
        public RecordCollection(int capacity)
        {
            recordContainer = new List<DBRecord>(capacity);
        }

        /// <summary>
        /// 以默认方式实例化对象
        /// </summary>
        public RecordCollection()
        {
            recordContainer = new List<DBRecord>();
        }
        
        /// <summary>
        /// 获取或设置指定索引位置的DBRecord对象
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public DBRecord this[int index]
        {
            get
            {
                return recordContainer[index];
            }
            set
            {
                recordContainer[index] = value;
            }
        }


        /// <summary>
        /// 添加DBRecord对象并返回新添加的对象
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public DBRecord Add(DBRecord record)
        {
            recordContainer.Add(record);
            return record;
        }

        /// <summary>
        /// 移除指定索引位置的DBrecord对象
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DBRecord RemoveAt(int index)
        {
            DBRecord record = recordContainer[index];
            recordContainer.RemoveAt(index);
            return record;
        }
    }
}
