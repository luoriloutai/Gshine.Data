using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GShineLib.Data;
using System.IO;
using System.Data.SqlClient;

namespace GShine
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 连接字符串，使用时改成自己的配置
            string conStr = @"Data Source=GSHINE-THINK;Initial Catalog=TestDB;Integrated Security=True";

            
            //
            //  示例操作
            //
            // ===初始化，默认===
            DBTable table = new SQLDBTable("M_CustomerOrderGoods", "ID", conStr);
            
            // ===初始化，指定所需列 ===
            //DBTable tableSpecialColumns = new SQLDBTable("M_CustomerOrderGoods", "id", conStr,
                //new DBColumn("price", Enums.DataType.Decimal));

            //===============
            // DBTable对象的使用
            //===============

            // ===根据标识字段值删除一条记录===
            //table.DeleteRecord("c4679b10-04dd-4960-b97f-2d2ca771c8dd");
            
            // ===按条件删除数据===
            //table.DeleteRecords("billNo='3'");

            //===删除表的全部数据===
            //table.ClearRecords();

            // ===加载表数据，将数据载入到DBTable.Records当中===
            ////加载全部数据
            table.LoadData();

            if (table.Records[0].Fields[0].Column.DataType == Enums.DataType.Char)
            {
                string value = table.Records[0].Fields[0].GetStringValue();
            }
            
            //加载指定条件的数据
            //table.LoadData("billno='1'", "billno");

            //  ==========其他属性==================
            //DBColumn column = table.Columns[0];
            //int index = table.Columns.GetIndexByName("billno");
            //bool isExist = table.Columns.Exist("md");
            //RecordCollection records = table.Records;
            //FieldCollection fields = records[0].Fields;
            //DBField field = fields["billno"];

            // 指定列的对象操作同上
            //tableSpecialColumns.LoadData();
           
            //================================
            // DBTable.Records、DBTable.Records.Fields的使用
            //================================

            // ===创建记录并保存===
            //创建记录
            //DBRecord record = table.CreateRecord();

            //string colName1 = record[0].Column.Name;
            //string colName2 = record[1].Column.Name;
            //string colName3 = record[2].Column.Name;

            ////为DBRecord对象赋值,未赋值的字段默认没数据
            //record["material"].Value = "{D751FA62-656E-46FF-8825-A9EC22F79A90}";
            //record["price"].Value = 23.05;
            //record["billNO"].Value = 4;
            //record["orderquantity"].Value = 3000;
            ////保存数据
            //record.Save();

            // ===更新记录===
            //加载数据时，自动读取当前状态为已保存，修改数据后调用Save()方法则自动更新数据
            //table.LoadData();
            //if (table.Records.Count > 0)
            //{
            //    object id = table.Records[0].Fields["id"].Value;
            //    object id1 = table.Records[0]["id"].Value;
            //    table.Records[0]["material"].Value = Guid.NewGuid();
            //    table.Records[0]["price"].Value = 2500;
            //    table.Records[0].Save();
            //}

            // === 删除记录 ===
            //// 先加载数据,将数据载入table.Records当中
            //table.LoadData();
            //// 对Records执行相应操作
            //if (table.Records.Count > 0)
            //{
            //    table.Records[0].Delete();
            //}




            MessageBox.Show("ok");

            

            //string info = record.TableName+"\n";
            //for (int i = 0; i < record.FieldCount; i++)
            //{
            //    info += record[i].Name + ":" + record[i].DataType + "\n";
            //}

            //MessageBox.Show(info);
            //MessageBox.Show(record.IdentifierField.Name+":"+record.IdentifierField.DataType);

            //string testStr = "Hello!";
            //byte[] binary=Encoding.Default.GetBytes(testStr);

            //SqlConnection connection = new SqlConnection("Data Source=BLUEKEY-PC;Initial Catalog=Test;Integrated Security=True");
            //SqlCommand command = connection.CreateCommand();


            //command.CommandText = "select BinaryCol from [user] where username='1'";
            //connection.Open();
            //SqlDataReader reader = command.ExecuteReader();
            //if (reader.Read())
            //{
            //    //将二进制数据读取成字符串
            //    string str = Encoding.UTF8.GetString((byte[])reader[0]);
            //    MessageBox.Show(str);
            //}
            //connection.Close();

            //binary类型实际上为byte[]，不可以直接填写
            //bit类型值可为布尔值也可以为0或1
            //real类型 实际类型为single
            //时间日期 字符串
            //datetimeoffset 时间字符串
            //geography 地理信息，可表现为地图，查询结果即为地图
            //geoametry 几何图形
            //sql_variant 可以为数字和字符串

//            command.CommandText = @"insert into [user](UserName,
//BinaryCol,
//BitCol,
//DateCol,
//DateTimeCol,
//DateTime2Col,
//DateTimeOffSetcol
//) values(
//@userName,
//@binary,
//@bit,
//@date,
//@datetime,
//@datetime2,
//@datetimeoffset)";
//            command.Parameters.Add(new SqlParameter("@userName", "5"));
//            command.Parameters.Add(new SqlParameter("@binary", binary));
//            command.Parameters.AddWithValue("@bit", 0); 
//            command.Parameters.AddWithValue("@date","2012-10-3");
//            command.Parameters.AddWithValue("@datetime", "2012-10-3");
//            command.Parameters.AddWithValue("@datetime2", DateTime.Now);
//            command.Parameters.AddWithValue("@datetimeoffset", DateTime.Now);

//            connection.Open();
//            command.ExecuteNonQuery();
//            connection.Close();

            
            //MessageBox.Show("OK");
        }
    }
}
