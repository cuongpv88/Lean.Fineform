﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Report
{
    /// <summary>
    /// pp_defect 的摘要说明
    /// </summary>
    public class pp_defect : IHttpHandler
    {
        private LeanFineContext DBCharts = new LeanFineContext();
        private JavaScriptSerializer jsS = new JavaScriptSerializer();
        private List<object> lists = new List<object>();

        public void ProcessRequest(HttpContext context)
        {
            string atedate = System.Web.HttpUtility.UrlDecode(context.Request["TransDate"], Encoding.UTF8);//结束时间
            //获取一同发送过来的参数
            //string command = context.Request["cmd"];
            context.Response.ContentType = "text/plain";

            //查询在特定日期的全部工单
            //查询在特定日期的全部工单
            var q = from a in DBCharts.Pp_Defect_Totals
                        //join b in DBCharts.PP_Outputs on a.Prolot equals b.Prolot
                    where a.IsDeleted == 0
                    //where b.IsDeleted == 0
                    where a.Prodate.Contains(atedate)
                    //where a.Prorealqty != a.Pronobadqty
                    select new
                    {
                        a.Prolot,
                        a.Proorder,
                        a.Proorderqty,
                        a.Prorealqty,
                        a.Pronobadqty,
                        a.Probadtotal,
                    };
            q = q.Distinct();
            //查询在特定日期的全部批次并统计
            var qs = from a in q

                         //where a.Proorderqty == a.Prorealqty
                     group a by new
                     {
                         a.Prolot,
                     }
                      into g
                     select new
                     {
                         Prolot = g.Key.Prolot,
                         Prolotqty = g.Sum(p => p.Proorderqty),
                         Prorealqty = g.Sum(p => p.Prorealqty),
                         Pronobadqty = g.Sum(p => p.Pronobadqty),
                         Probadtotal = g.Sum(p => p.Probadtotal),
                         Prodirectrate = (g.Sum(p => p.Pronobadqty) != 0 ? ((g.Sum(p => p.Pronobadqty) * 1.0 / g.Sum(p => p.Prorealqty)) * 100) : 0),
                         Probadrate = (g.Sum(p => p.Probadtotal) != 0 ? (g.Sum(p => p.Probadtotal) * 1.0 / g.Sum(p => p.Prorealqty)) * 100 : 0),
                     };

            qs = qs.OrderByDescending(a => a.Pronobadqty).Take(15);
            var qss = (from a in qs
                       orderby a.Pronobadqty descending
                       select a)
                     .ToList().Select(
                p => new
                {
                    p.Prolot,
                    p.Prolotqty,
                    p.Prorealqty,
                    p.Pronobadqty,
                    p.Probadtotal,
                    Prodirectrate = p.Prodirectrate.ToString("0.00"),
                    Probadrate = p.Probadrate.ToString("0.00"),
                }
                ).ToList();

            if (qss.Any())
            {
                DataSet ds = new DataSet();
                DataTable dt = ConvertHelper.LinqConvertToDataTable(qss.AsQueryable());
                ds.Tables.Add(ConvertHelper.LinqConvertToDataTable(qss.AsQueryable()));
                //用来传回去的内容
                //List<object> lists = new List<object>();                       //创建object类型的泛型
                foreach (DataRow dr in dt.Rows)
                {
                    var obj = new { name = dr["Prolot"], value1 = dr["Pronobadqty"], value2 = dr["Probadtotal"], value3 = dr["Prodirectrate"], value4 = dr["Probadrate"] };  //key，value
                    lists.Add(obj);
                }
                //var jsS = new JavaScriptSerializer();                           //创建json对象
                context.Response.Write(jsS.Serialize(lists));                   //返回数据
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}