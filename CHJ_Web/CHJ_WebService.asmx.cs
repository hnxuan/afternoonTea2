using BP.DA;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace CHJ_Web
{
    /// <summary>
    /// CHJ_WebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class CHJ_WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        //账单表字段
        struct BillStruct
        {
            public int id;
            public string time;
            public string price;
            public List<goods> cont;
        }
        //商品表字段
        struct goods
        {
            public string name;
            public string num;
        }
        //用户表字段
        struct user
        {
            public string userName;
            public string status;
            public Messages backText;//反馈信息
        }
        //反馈消息
        struct Messages
        {
            public string text;
        }
        [WebMethod]
        public void getUser(string userId)
        {
            try
            {
                Context.Response.ContentType = "text/plain";
                Paras ps = new Paras();
                ps.SQL = "";
                ps.SQL = "select * from [user] where userId =" + ps.DBStr + "userId" + "\r\n";
                ps.Add("userId", userId);
                DataTable dt = DBAccess.RunSQLReturnTable(ps);
                if (dt.Rows.Count > 0)
                {
                    user users = new user();
                    Messages mess = new Messages();
                    mess.text = "有此用户";
                    users.userName = dt.Rows[0]["userName"].ToString().Trim();
                    users.status = dt.Rows[0]["status"].ToString().Trim();
                    users.backText = mess;
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    Context.Response.Write(jss.Serialize(users));
                }
                else
                {
                    user users = new user();
                    Messages mess = new Messages();
                    mess.text = "无该用户";
                    users.status = "0";
                    users.backText = mess;
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    Context.Response.Write(jss.Serialize(users));
                }
            }
            catch
            {
                Context.Response.Write("[]");
            }
            
        }
        [WebMethod]
        public void getGoods(string type)
        {
            try
            {
                Context.Response.ContentType = "text/plain";
                //获取提供该类型商品的所有供货商
                Paras ps = new Paras();
                ps.SQL = "";
                ps.SQL = "select distinct(suppId) from goods where goodsType =" + ps.DBStr + "goodsType" + "\r\n";
                ps.Add("goodsType", type);
                DataTable supplierDt = DBAccess.RunSQLReturnTable(ps);


                if (supplierDt.Rows.Count > 0)
                {
                    user users = new user();
                    Messages mess = new Messages();
                    mess.text = "有此用户";
                    users.userName = supplierDt.Rows[0]["userName"].ToString().Trim();
                    users.status = supplierDt.Rows[0]["status"].ToString().Trim();
                    users.backText = mess;
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    Context.Response.Write(jss.Serialize(users));
                }
                else
                {
                    user users = new user();
                    Messages mess = new Messages();
                    mess.text = "无该用户";
                    users.status = "0";
                    users.backText = mess;
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    Context.Response.Write(jss.Serialize(users));
                }
            }
            catch
            {
                Context.Response.Write("[]");
            }
            
        }
        [WebMethod]
        public void GetBillList(string userId)
        {
            try
            {
                Context.Response.ContentType = "text/plain";
                if (userId == "1")
                {
                    string sql = "select * from goods";
                    DataTable dt = DBAccess.RunSQLReturnTable(sql);
                    BillStruct bill = new BillStruct();
                    bill.id=1;
                    bill.time="2019-1-10";
                    bill.price="2019rmb";
                    goods goods1= new goods();
                    goods1.name="百果园";
                    goods1.num="23";

                    List<goods> goodsList =new List<goods>();
                    goodsList.Add(goods1);
                    bill.cont = goodsList;
                    ArrayList billList = new ArrayList();
                    billList.Add(bill);
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    Context.Response.Write(jss.Serialize(bill));
                }
                else
                {
                    Context.Response.Write("[]");
                }
            }
            catch
            {
                Context.Response.Write("[]");
            }
        }
    }
}
