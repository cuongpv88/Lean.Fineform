﻿using FineUIPro;
using NPOI.SS.Formula.Functions;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;

namespace Lean.Fineform.Lf_Manufacturing.PP.daily.P2D
{

    public partial class p2d_daily_new : PageBase
    {
        public static string lclass, OPHID, ConnStr, nclass, ncode;
        public static int rowID, delrowID, editrowID, totalSum;

        public static string userid, badSum;
        public static string Prolot, Prolinename, Prodate, Prorealqty, strPline, strmaxDate, strminDate, Probadqty, Probadtotal, Probadcou, strProModel;
        public static string mysql;

        public int Prorate, ParentID;

        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP2DOutputNew";
            }
        }

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckData();

                LoadData();
            }
        }

        private void CheckData()
        {
            string inputDate = DateTime.Now.ToString("yyyyMM");
            Pp_Efficiency rate = DB.Pp_Efficiencys.Where(u => u.Proratedate == inputDate).FirstOrDefault();

            if (rate == null)
            {

                //Alert alert = new Alert();
                //alert.Message = "请维护 " + inputDate + " 生产赁率！";
                //alert.IconUrl = "~/Lf_Resources/images/message/warring.png";
                //alert.Target = Target.Top;
                Alert.ShowInTop("请维护 " + inputDate + " 生产赁率！", MessageBoxIcon.Warning);


                //return;
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());

            }

        }


        private void LoadData()
        {
            prodirect.Text = "1";
            proindirect.Text = "1";
            prolinename.Text = "制二课";
            //上月第一天
            DateTime last = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-1);
            //本月第一天时间 
            DateTime dt = DateTime.Today;
            DateTime dt_First = dt.AddDays(-(dt.Day) + 1);


            //本月最后一天时间 
            DateTime dt2 = dt.AddMonths(1);
            DateTime dt_Last = dt2.AddDays(-(dt.Day));




            //每月10号
            //string Date10 = DateTime.Now.ToString("yyyyMM10");
            //string nowDate = DateTime.Now.ToString("yyyyMMdd");
            //将日期字符串转换为日期对象
            //第一天
            //DateTime.ParseExact(dateString, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            DateTime Date10 = Convert.ToDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10));
            DateTime nowDate = Convert.ToDateTime((DateTime.Now));

            //DateTime editDate = Convert.ToDateTime(DateTime.ParseExact(prodate.SelectedDate.Value.ToString("yyyyMMdd"), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture));

            //上个月
            DateTime lastDate = DateTime.Now.AddMonths(-1);
            //通过DateTIme.Compare()进行比较（）
            int compNum = DateTime.Compare(Date10, nowDate);
            //int compEdit= DateTime.Compare(nowDate, editDate);




            if (compNum == -1)
            {

                this.prodate.SelectedDate = DateTime.Now.AddDays(-1);
                this.prodate.MinDate = dt_First;
                this.prodate.MaxDate = dt_Last;


            }
            if (compNum == 1)
            {

                this.prodate.SelectedDate = DateTime.Now.AddDays(-1);
                this.prodate.MinDate = last;
                this.prodate.MaxDate = dt_Last;

            }
            userid = GetIdentityName();
            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            // 初始化用户
            //InitNoticeUser();

            // 初始化用户所属部门
            //InitNoticeDept();
            BindDDLorder();

            //BindDDLline();






        }







        #endregion
        #region BindGrid
        #endregion
        #region BindDDLData

        //DDL查询LOT
        private void BindDDLorder()
        {
            //UpdatingHelper.UpdatePorderQty();

            //查询LINQ去重复



            var a = from c in DB.Pp_Manhours
                    where c.isDelete == 0
                    where c.Prowctext.Contains("二")
                    select c.Proitem;

            var q = from e in DB.Pp_Orders

                    where a.Contains(e.Porderhbn)
                    select new
                    {
                        e.Porderno
                    };

            //        var q = from a in DB.Pp_Orders
            //                    //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
            //                    //where b.Proecnno == strecn
            //                    //where b.Proecnbomitem == stritem
            //                where a.Porderqty> 0
            //&& (from d in DB.Pp_Manhours
            //     where d.isDelete == 0
            //     where d.Prowctext.Contains("二")
            //     where d.Proitem == a.Porderhbn
            //     select d.Proitem)//20220815修改之前是d.Prolots
            //     .Contains(a.Porderno)//20220815修改之前是p.Prolots
            //                orderby a.Porderno
            //                select new
            //                {
            //                    //b.Proecnmodel,
            //                    a.Porderno

            //                };

            var qs = q.Select(E => new { E.Porderno }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            proorder.DataSource = qs;
            proorder.DataTextField = "Porderno";
            proorder.DataValueField = "Porderno";
            proorder.DataBind();
        }





        //DDL查询班组
        private void BindDDLline()
        {

            ////查询LINQ去重复
            //var q = from a in DB.Pp_Lines
            //            //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
            //            //where b.Proecnno == strecn
            //        where a.lineclass == "P"

            //        select new
            //        {
            //            a.linecode,
            //            a.linename,

            //        };

            //var qs = q.Select(E => new { E.linecode, E.linename }).ToList().Distinct();
            ////var list = (from c in DB.ProSapPorders
            ////                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            ////                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            ////3.2.将数据绑定到下拉框
            //prolinename.DataSource = qs;
            //prolinename.DataTextField = "linename";
            //prolinename.DataValueField = "linename";
            //prolinename.DataBind();


        }
        //DDL查询不良各类

        #endregion
        #region Events        
        //protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        //{
        //    //object[] keys = Grid1.DataKeys[e.RowIndex];

        //    ////selID = Convert.ToInt32(keys[0].ToString());
        //    //string ss = keys[1].ToString();


        //    ////获取ID号
        //    //int del_ID = Convert.ToInt32(Grid1.DataKeys[e.RowIndex][0]);

        //    //if (e.CommandName == "Delete")
        //    //{
        //    //    // 在操作之前进行权限检查
        //    //    if (!CheckPower("CoreOutputDelete"))
        //    //    {
        //    //        CheckPowerFailWithAlert();
        //    //        return;
        //    //    }

        //    //    Pp_P1d_Output current = DB.Pp_P1d_Outputs.Find(del_ID);
        //    //    //删除日志
        //    //    string Newtext = current.ID + "," + current.Prolinename + "," + current.Prolot + "," + current.Prodate + "," + current.Prorealqty + "," + current.Prongclass + "," + current.Prongcode + "," + current.Probadqty + "," + current.Probadtotal + "," + current.Probadcou;
        //    //   string OperateType = "删除";//操作标记
        //    //    string OperateNotes = "Del生产* " + Newtext + " *Del 的记录已删除";
        //    //    OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "生产日报删除", OperateNotes);
        //    //    //删除记录
        //    //    //DB.Pp_Ecns.Where(l => l.ID == del_ID).Delete();
        //    //    current.isDelete = 1;
        //    //    DB.SaveChanges();
        //    //    //重新绑定


        //    //}
        //}

        //protected void Grid1_PreDataBound(object sender, EventArgs e)
        //{
        //    // 数据绑定之前，进行权限检查
        //    CheckPowerWithLinkButtonField("CoreOutputDelete", Grid1, "deleteField");
        //    // 设置LinkButtonField的点击客户端事件


        //}
        private void OPHRate()//稼动率
        {
            Pp_Efficiency current = DB.Pp_Efficiencys

                .OrderByDescending(u => u.Proratedate).FirstOrDefault();


            Prorate = current.Prorate;
        }
        //判断修改内容||判断重复
        private void CheckRepeatItem(string strOrder, string strLine, string strDate)
        {

            //int id = GetQueryIntValue("id");
            //proManhour current = DB.Pp_Manhours.Find(id);
            //Cmc001 = current.Prodate;
            //Cmc002 = current.Proitem;
            //Cmc004 = current.Proshort.ToString();
            //Cmc005 = current.Prost.ToString();
            //Cmc008 = current.Prodesc;

            //if (this.Prodate.SelectedDate.Value.ToString("yyyyMMdd") == Cmc001)
            //{
            //    if (this.Proitem.Text == Cmc002)
            //    {
            //        if (this.Proshort.Text == Cmc004)
            //        {
            //            if (this.Prost.Text == Cmc005)
            //            {
            //                if (this.Prodesc.Text == Cmc008)
            //                {
            //                    Alert alert = new Alert();
            //                    alert.Message = global::Resources.GlobalResource.sys_Msg_Noedit;
            //                    alert.IconFont = IconFont.Warning;
            //                    alert.Target = Target.Top;
            //                    Alert.ShowInTop();
            //                }
            //            }
            //        }
            //    }

            //    //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //}


            //int id = GetQueryIntValue("id");
            //proLinestop current = DB.proLinestops.Find(id);
            ////decimal cQcpd005 = current.Qcpd005;
            //string checkdata1 = current.Prostoptext;


            //if (this.Prostoptext.Text == checkdata1)//decimal.Parse(this.LF001.Text) == cLF001 && this.Qcpd005.Text == cQcpd004)
            //{
            //    Alert alert = new Alert();
            //    alert.Message = global::Resources.GlobalResource.sys_Msg_Noedit;
            //    alert.IconFont = IconFont.Warning;
            //    alert.Target = Target.Top;
            //    Alert.ShowInTop();
            //    //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //}

            Pp_P2d_Output redata = DB.Pp_P2d_Outputs.Where(u => u.Proorder.ToString() + u.Prodate + u.Prolinename == strOrder + strDate + strLine).FirstOrDefault();

            if (redata != null)
            {
                Alert.ShowInTop(strOrder + "||" + strLine + "||" + strDate + ",生产工单在同一天同一班组不能重复输入！", MessageBoxIcon.Warning);
                return;
            }
            //保存数据
            SaveItem();
            //更新单头机种
            UpdateItem(strProModel);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        private void SaveItem()//新增生产日报单头
        {
            try
            {
                

                Pp_P2d_Output item = new Pp_P2d_Output();

                //ophID();
                //item.OPHID = int.Parse(OPHID) + 1 + 1000;
                item.GUID = Guid.NewGuid();
                //item.Prolineclass = prolinename.SelectedValue.ToString();

                item.Prolinename = "制二课";//prolinename.SelectedItem.Text;


                item.Prodate = prodate.SelectedDate.Value.ToString("yyyyMMdd");
                item.Prodirect = 1;// int.Parse(prodirect.Text);
                item.Proindirect = 1;// int.Parse(proindirect.Text);
                item.Prolot = prolot.Text;
                item.Prohbn = prohbn.Text;//prohbn.Text;
                if (prosn.Text == "")
                {
                    item.Prosn = "DTA0000~9999";
                }
                else
                {
                    item.Prosn = prosn.Text;
                }

                item.Proorderqty = Decimal.Parse(prolotqty.Text);
                item.Promodel = "二";// promodel.Text;
                item.Prost = 0;//Decimal.Parse(prost.Text);
                //item.Prosubst = Decimal.Parse(prosubst.Text);
                item.Prostdcapacity = 0; //Decimal.Parse(prostdcapacity.Text);

                item.Proorder = proorder.SelectedItem.Text;

                //item.Prostime = prostime.Text;
                //item.Proetime = proetime.Text;
                //item.Proplanqty = int.Parse(proplanqty.Text);
                //item.Prorealqty = int.Parse(prorealqty.Text);

                //item.Proplantotal = int.Parse(proplantotal.Text);
                //item.Prorealtotal = int.Parse(prorealtotal.Text);

                //item.Prolinestop = prolinestop.Checked;
                //item.Prolinestopmin = int.Parse(prolinestopmin.Text);


                //item.Probadcou = probadcou.Text;

                ////string sss = proetime.SelectedDate.Value.Subtract(prostime.SelectedDate.Value).Duration().TotalMinutes.ToString();//计算时间差（分钟）
                ////时间差的绝对值 ，测试你的代码运行了多长时间。
                //item.Prorealtime = int.Parse(proetime.SelectedDate.Value.Subtract(prostime.SelectedDate.Value).Duration().TotalMinutes.ToString()) - int.Parse(prolinestopmin.Text);
                //item.Proratio = int.Parse(proratio.Text);
                // 添加所有用户

                item.isDelete = 0;
                item.Remark = remark.Text;
                item.CreateTime = DateTime.Now;
                item.Creator = GetIdentityName();
                DB.Pp_P2d_Outputs.Add(item);
                DB.SaveChanges();
                //读取ID值
                OPHID = item.GUID.ToString();
                ParentID = item.ID;
                //新增日志

                string Contectext = ParentID + "," + "制二课" + "," + prodate.SelectedDate.Value.ToString("yyyyMMdd") + "," + prolot.Text + "," + prohbn.Text + "," + prostdcapacity.Text;
                string OperateType = "新增";//操作标记
                string OperateNotes = "New生产OPH* " + Contectext + " *New生产OPH 的记录已新增";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "生产日报新增", OperateNotes);


                SaveSubItem();
            }
            catch (ArgumentNullException Message)
            {
                Alert.ShowInTop("空参数传递(err:null):" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("使用无效的类:" + Message);
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                //var errorMessages = ex.EntityValidationErrors
                //        .SelectMany(x => x.ValidationErrors)
                //        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                //var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                //var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                //throw new System.Data.Entity.Validation.DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

                //判断字段赋值
                var msg = string.Empty;
                var errors = (from u in ex.EntityValidationErrors select u.ValidationErrors).ToList();
                foreach (var item in errors)
                    msg += item.FirstOrDefault().ErrorMessage;
                Alert.ShowInTop("实体验证失败,赋值有异常:" + msg);
            }

        }

        //新增新增生产日报单身
        /// <summary>
        /// 新增单身数据
        /// </summary>
        private void SaveSubItem()
        {
            try
            {
                var hbn = (from a in DB.Pp_Orders
                           where a.Porderno.Contains(proorder.SelectedItem.Text)
                           select a).ToList();

                if (hbn.Any())
                {
                    var res = (from p in DB.Pp_Manhours
                               where p.Proitem.Contains(prohbn.Text)
                               orderby p.Prowctext
                               //where p.Age > 30 && p.Department == "研发部"
                               select p).ToList();
                    int icount = res.Count();
                    //判断查询是否为空
                    if (res.Any())
                    {

                        //遍历
                        for (int i = 0; i < icount; i++)
                        {
                            if (res[i].Prowctext.ToString().Contains("SMT"))
                            {
                                for (int j = 0; j < 2; j++)
                                {
                                    string[] list = new string[2] { "A", "B" };
                                    foreach (var val in list)
                                    {

                                        Pp_P2d_OutputSub item = new Pp_P2d_OutputSub();

                                        // 添加父ID
                                        //Pp_P1d_Output ID = Attach<Pp_P1d_Output>(Convert.ToInt32(ParentID));
                                        item.Parent = ParentID;
                                        item.Prolinename = "SMT" + (j + 1).ToString();
                                        item.Prodate = prodate.SelectedDate.Value.ToString("yyyyMMdd");
                                        item.Prodirect = int.Parse(prodirect.Text);
                                        item.Proindirect = int.Parse(proindirect.Text);
                                        item.Prolot = prolot.Text;
                                        item.Prohbn = prohbn.Text;//prohbn.Text;
                                        item.Proorderqty = Decimal.Parse(prolotqty.Text);
                                        item.Promodel = res[i].Promodel.ToString();
                                        strProModel = res[i].Promodel.ToString();
                                        item.Prorate = Decimal.Parse(res[i].Prorate.ToString());
                                        item.Prost = Decimal.Parse(res[i].Prost.ToString());
                                        item.Proshort = Decimal.Parse(res[i].Proshort.ToString());
                                        item.Propbctype = val;
                                        //item.Prosubst = Decimal.Parse(prosubst.Text);
                                        item.Prostdcapacity = Decimal.Parse(prostdcapacity.Text);
                                        item.Totaltag = true;
                                        item.Proorder = proorder.SelectedItem.Text;
                                        item.GUID = Guid.Parse(OPHID);
                                        item.Prostime = "SMT" + (j + 1).ToString();
                                        item.Proetime = "SMT" + (j + 1).ToString();
                                        //item.Udf001 = prodate.SelectedDate.Value.ToString("yyyyMMdd");
                                        //item.Udf002 = this.prolinename.SelectedItem.Text;
                                        item.Remark = remark.Text;
                                        item.CreateTime = DateTime.Now;
                                        item.Creator = GetIdentityName();
                                        item.isDelete = 0;
                                        DB.Pp_P2d_OutputSubs.Add(item);
                                        DB.SaveChanges();

                                        //新增日志

                                        string Newtext = ParentID + "," + res[i].Prowctext + "~" + res[i].Prowctext + "," + "制二课" + "," + prodate.SelectedDate.Value.ToString("yyyyMMdd") + "," + prolot.Text + "," + prohbn.Text + "," + prostdcapacity.Text;
                                        string OperateType = "新增";

                                        string OperateNotes = "New生产OPH_SUB* " + Newtext + " *New生产OPH_SUB 的记录已新增";
                                        OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "OPH实绩新增", OperateNotes);


                                    }


                                }


                            }
                            if (res[i].Prowctext.ToString().Contains("手"))
                            {

                                string[] list = new string[] { "手插A", "手插B", "手插C", "手插D", "修正A", "修正B", "修正C", "修正D" };
                                foreach (var val in list)
                                {

                                    Pp_P2d_OutputSub item = new Pp_P2d_OutputSub();

                                    // 添加父ID
                                    //Pp_P1d_Output ID = Attach<Pp_P1d_Output>(Convert.ToInt32(ParentID));
                                    item.Parent = ParentID;
                                    item.Prolinename = val;
                                    item.Prodate = prodate.SelectedDate.Value.ToString("yyyyMMdd");
                                    item.Prodirect = int.Parse(prodirect.Text);
                                    item.Proindirect = int.Parse(proindirect.Text);
                                    item.Prolot = prolot.Text;
                                    item.Prohbn = prohbn.Text;//prohbn.Text;
                                    item.Proorderqty = Decimal.Parse(prolotqty.Text);
                                    item.Promodel = res[i].Promodel.ToString();
                                    strProModel = res[i].Promodel.ToString();
                                    item.Prorate = Decimal.Parse(res[i].Prorate.ToString());
                                    item.Prost = Decimal.Parse(res[i].Prost.ToString());
                                    item.Proshort = Decimal.Parse(res[i].Proshort.ToString());
                                    item.Propbctype = "";
                                    //item.Prosubst = Decimal.Parse(prosubst.Text);
                                    item.Prostdcapacity = Decimal.Parse(prostdcapacity.Text);
                                    item.Totaltag = true;
                                    item.Proorder = proorder.SelectedItem.Text;
                                    item.GUID = Guid.Parse(OPHID);
                                    item.Prostime = val;
                                    item.Proetime = val;
                                    //item.Udf001 = prodate.SelectedDate.Value.ToString("yyyyMMdd");
                                    //item.Udf002 = this.prolinename.SelectedItem.Text;
                                    item.Remark = remark.Text;
                                    item.CreateTime = DateTime.Now;
                                    item.Creator = GetIdentityName();
                                    item.isDelete = 0;
                                    DB.Pp_P2d_OutputSubs.Add(item);
                                    DB.SaveChanges();

                                    //新增日志

                                    string Newtext = ParentID + "," + res[i].Prowctext + "~" + res[i].Prowctext + "," + "制二课" + "," + prodate.SelectedDate.Value.ToString("yyyyMMdd") + "," + prolot.Text + "," + prohbn.Text + "," + prostdcapacity.Text;
                                    string OperateType = "新增";

                                    string OperateNotes = "New生产OPH_SUB* " + Newtext + " *New生产OPH_SUB 的记录已新增";
                                    OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "OPH实绩新增", OperateNotes);




                                }


                            }
                            if (res[i].Prowctext.ToString().Contains("自"))
                            {

                                string[] list = new string[] { "自插A", "自插B", "自插C", "自插D" };
                                foreach (var val in list)
                                {

                                    Pp_P2d_OutputSub item = new Pp_P2d_OutputSub();

                                    // 添加父ID
                                    //Pp_P1d_Output ID = Attach<Pp_P1d_Output>(Convert.ToInt32(ParentID));
                                    item.Parent = ParentID;
                                    item.Prolinename = val;
                                    item.Prodate = prodate.SelectedDate.Value.ToString("yyyyMMdd");
                                    item.Prodirect = int.Parse(prodirect.Text);
                                    item.Proindirect = int.Parse(proindirect.Text);
                                    item.Prolot = prolot.Text;
                                    item.Prohbn = prohbn.Text;//prohbn.Text;
                                    item.Proorderqty = Decimal.Parse(prolotqty.Text);
                                    item.Promodel = res[i].Promodel.ToString();
                                    strProModel = res[i].Promodel.ToString();
                                    item.Prorate = Decimal.Parse(res[i].Prorate.ToString());
                                    item.Prost = Decimal.Parse(res[i].Prost.ToString());
                                    item.Proshort = Decimal.Parse(res[i].Proshort.ToString());
                                    item.Propbctype = "";
                                    //item.Prosubst = Decimal.Parse(prosubst.Text);
                                    item.Prostdcapacity = Decimal.Parse(prostdcapacity.Text);
                                    item.Totaltag = true;
                                    item.Proorder = proorder.SelectedItem.Text;
                                    item.GUID = Guid.Parse(OPHID);
                                    item.Prostime = val;
                                    item.Proetime = val;
                                    //item.Udf001 = prodate.SelectedDate.Value.ToString("yyyyMMdd");
                                    //item.Udf002 = this.prolinename.SelectedItem.Text;
                                    item.Remark = remark.Text;
                                    item.CreateTime = DateTime.Now;
                                    item.Creator = GetIdentityName();
                                    item.isDelete = 0;
                                    DB.Pp_P2d_OutputSubs.Add(item);
                                    DB.SaveChanges();

                                    //新增日志

                                    string Newtext = ParentID + "," + res[i].Prowctext + "~" + res[i].Prowctext + "," + "制二课" + "," + prodate.SelectedDate.Value.ToString("yyyyMMdd") + "," + prolot.Text + "," + prohbn.Text + "," + prostdcapacity.Text;
                                    string OperateType = "新增";

                                    string OperateNotes = "New生产OPH_SUB* " + Newtext + " *New生产OPH_SUB 的记录已新增";
                                    OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "OPH实绩新增", OperateNotes);




                                }


                            }
                        }
                        //更新单头机种名称

                    }

                }

            }
            catch (ArgumentNullException Message)
            {
                Alert.ShowInTop("空参数传递(err:null):" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("使用无效的类:" + Message);
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                //var errorMessages = ex.EntityValidationErrors
                //        .SelectMany(x => x.ValidationErrors)
                //        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                //var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                //var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                //throw new System.Data.Entity.Validation.DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

                //判断字段赋值
                var msg = string.Empty;
                var errors = (from u in ex.EntityValidationErrors select u.ValidationErrors).ToList();
                foreach (var item in errors)
                    msg += item.FirstOrDefault().ErrorMessage;
                Alert.ShowInTop("实体验证失败,赋值有异常:" + msg);
            }



        }
        /// <summary>
        /// 更新单头机种
        /// </summary>
        /// <param name="UpdatestrModel"></param>
        private void UpdateItem(string UpdatestrModel)//更新生产日报单头，机种
        {
            //int id = GetQueryIntValue("id");
            Pp_P2d_Output item = DB.Pp_P2d_Outputs

                .Where(u => u.ID == ParentID).FirstOrDefault();

            item.Promodel = UpdatestrModel;


            item.ModifyTime = DateTime.Now;
            item.Modifier = GetIdentityName();
            //DB.Proophs.Add(item);
            DB.SaveChanges();

            SaveSubItem();

            //修改后日志
            string ModifiedText = prolinename.Text.Trim() + "," + prodate.Text + "," + prolot.Text + "," + prohbn.Text + "," + promodel.Text;
            string OperateType = "修改";
            string OperateNotes = "*afEdit生产OPH " + ModifiedText + "*afEdit生产OPH 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "OPH修改", OperateNotes);


        }

        /// <summary>
        /// 新增订单不良合计表
        /// </summary>
        private void SaveDefect()
        {

            Pp_DefectTotal item = new Pp_DefectTotal();

            item.Prolot = prolot.Text;
            item.Prolinename = "制二课";
            item.Prodate = prodate.SelectedDate.Value.ToString("yyyyMMdd");
            item.Proorder = proorder.SelectedItem.Text;
            item.Proorderqty = Convert.ToInt32(decimal.Parse(prolotqty.Text));
            item.Promodel = strProModel;
            item.Prorealqty = 0;
            item.Pronobadqty = 0;
            item.Probadtotal = 0;
            item.Prodirectrate = 0;
            item.Probadrate = 0;
            item.isDelete = 0;
            item.Remark = "";
            //item.Promodel = promodel.Text;
            item.GUID = Guid.NewGuid();
            item.Creator = GetIdentityName();
            item.CreateTime = DateTime.Now;
            DB.Pp_DefectTotals.Add(item);
            DB.SaveChanges();

            //新增日志
            string Contectext = prolot.Text + "制二课" + "," + prodate.SelectedDate.Value.ToString("yyyyMMdd") + "," + prolot.Text + "," + proorder.SelectedItem.Text + "," + prolotqty.Text;
            string OperateType = "新增";
            string OperateNotes = "New生产订单不良* " + Contectext + " New生产订单不良* 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不良管理", "工单不良集计新增", OperateNotes);
        }

        /// <summary>
        /// 更新订单不良合计表
        /// </summary>
        private void SaveDefectUpdate()
        {

            //判断重复
            string strorder = proorder.SelectedItem.Text.Trim();
            string strlot = prolot.Text.Trim();

            var line = (from p in DB.Pp_P2d_OutputSubs
                            //join b in DB.Pp_P1d_Outputs on p.OPHID equals b.OPHID
                        where p.Proorder == (strorder)

                        select new
                        {
                            p.Prolinename
                        }).Distinct().ToList();
            if (line.Any())
            {
                for (int i = 0; i < line.Count(); i++)
                {
                    strPline += line[i].Prolinename.ToString() + ",";
                }
            }
            var maxdate =
                                    (from p in DB.Pp_P2d_OutputSubs
                                         //join b in DB.proOutputs on p.OPHID equals b.OPHID
                                     where p.Proorder == (strorder)
                                     //.Where(s => s.Prolot.Contains(strPlot))
                                     // p.Prodate.Substring(0, 6).CompareTo(strDpdate) <= 0
                                     group p by p.Prolot into g
                                     select new
                                     {
                                         g.Key,
                                         mdate = g.Max(p => p.Prodate)
                                     }).Distinct().ToList();
            if (maxdate.Any())
            {
                strmaxDate = maxdate[0].mdate;
            }

            var mindate =
                        (from p in DB.Pp_P2d_OutputSubs
                             //join b in DB.proOutputs on p.OPHID equals b.OPHID
                         where p.Proorder == (strorder)
                         //.Where(s => s.Prolot.Contains(strPlot))
                         //where p.Prodate.Substring(0, 6).CompareTo(strDpdate) <= 0
                         group p by p.Prolot into g
                         select new
                         {
                             g.Key,
                             mdate = g.Min(p => p.Prodate)
                         }).Distinct().ToList();
            if (mindate.Any())
            {
                strminDate = mindate[0].mdate;
            }

            string sedate = strminDate + "~" + strmaxDate;
            DB.Pp_DefectTotals
               .Where(s => s.Proorder == strorder)

               .ToList()
               .ForEach(x => { x.Promodel = strProModel;  x.Prolinename = strPline; x.Prodate = sedate; x.Modifier = GetIdentityName(); x.ModifyTime = DateTime.Now; });
            DB.SaveChanges();


            //新增日志
            string Contectext = strorder + "," + strlot + "," + strPline + "," + sedate;
            string OperateType = "修改";
            string OperateNotes = "Edit生产不良*" + Contectext + " *Edit生产不良 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不良管理", "工单不良集计修改", OperateNotes);
            strPline = "";
            strmaxDate = "";
            strminDate = "";
            sedate = "";

        }







        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            Distinctdata();



        }
        //判断OPH重复单头
        private void Distinctdata()
        {
            try
            {
                //保存OPH数据
                CheckRepeatItem(proorder.SelectedItem.Text, "制二课", prodate.SelectedDate.Value.ToString("yyyyMMdd")); 
            }
            catch (ArgumentNullException Message)
            {
                Alert.ShowInTop("空参数传递(err:null):" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("使用无效的类:" + Message);
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                //var errorMessages = ex.EntityValidationErrors
                //        .SelectMany(x => x.ValidationErrors)
                //        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                //var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                //var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                //throw new System.Data.Entity.Validation.DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

                //判断字段赋值
                var msg = string.Empty;
                var errors = (from u in ex.EntityValidationErrors select u.ValidationErrors).ToList();
                foreach (var erroritem in errors)
                    msg += erroritem.FirstOrDefault().ErrorMessage;
                Alert.ShowInTop("实体验证失败,赋值有异常:" + msg);
            }

            //判断重复
            string input = proorder.SelectedItem.Text.Trim();


            Pp_DefectTotal current = DB.Pp_DefectTotals.Where(u => u.Proorder == input).FirstOrDefault();

            if (current != null)
            {
                SaveDefectUpdate();
            }
            else
            {
                SaveDefect();
            }



            //表格数据已重新绑定

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());


        }




        #endregion

        #region Times
        //计算时间
        /// <summary>
        /// 计算时间差
        /// </summary>
        /// <param name="DateTime1"></param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        private string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            TimeSpan ts = DateTime1.Subtract(DateTime2).Duration();
            dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
            return dateDiff;
        }
        #endregion







        protected void proorder_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.proorder.SelectedIndex != -1 && this.proorder.SelectedIndex != 0)
            {
                try
                {
                    var q = from a in DB.Pp_Orders
                                //join b in DB.Pp_Manhours on a.Porderhbn equals b.Proitem
                                //join c in DB.Pp_SapMaterials on a.Proecnoldhbn equals c.D_SAP_ZCA1D_Z002
                            where a.Porderno == proorder.SelectedItem.Text
                            //where a.Proecnmodel == strProecnmodel
                            //where a.Proecnbomitem == strProecnbomitem
                            //where a.Proecnoldhbn == strProecnoldhbn
                            //where a.Proecnnewhbn == strProecnnewhbn
                            orderby a.Porderno descending
                            select new
                            {
                                a.Porderno,
                                a.Porderhbn,
                                a.Porderlot,
                                a.Porderqty,
                                a.Porderdate,
                                a.Porderserial,
                                a.Porderreal
                                //c.D_SAP_ZCA1D_Z033,

                            };

                    if (q.Any())
                    {

                        // 切勿使用 source.Count() > 0
                        var qs = q.Distinct().Take(1).ToList();
                        if (qs[0].Porderno != "")
                        {
                            prolinename.Text = "制二课";
                            prohbn.Text = qs[0].Porderhbn;
                            //promodel.Text = qs[0].Promodel;
                            prolot.Text = (qs[0].Porderlot == "" ? proorder.SelectedItem.Text + "||" + qs[0].Porderqty.ToString() : (qs[0].Porderlot));
                            //prost.Text = qs[0].Prost.ToString();
                            prolotqty.Text = qs[0].Porderqty.ToString();
                            //prorealqty.Text = (decimal.Parse(DSstr1.Tables[0].Rows[0][3].ToString()) - decimal.Parse(DSstr1.Tables[0].Rows[0][16].ToString())).ToString();
                            prosn.Text = qs[0].Porderserial;
                            HourQty();
                        }
                        else
                        {
                            // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                            Alert.ShowInTop("ST为0！", String.Empty, ActiveWindow.GetHideReference());
                            return;
                        }

                    }
                    else
                    {
                        // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                        Alert.ShowInTop("此物料没有ST，请通知技术部门录入！", String.Empty, ActiveWindow.GetHideReference());
                        return;
                    }
                }

                catch (ArgumentNullException Message)
                {
                    Alert.ShowInTop("异常1:" + Message);
                }
                catch (InvalidCastException Message)
                {
                    Alert.ShowInTop("异常2:" + Message);
                }
                catch (Exception Message)
                {
                    Alert.ShowInTop("异常3:" + Message);

                }


            }
        }



        protected void prodirect_TextChanged(object sender, EventArgs e)
        {
            HourQty();
        }


        //计算标准产能
        /// <summary>
        /// 标准产能计算
        /// </summary>
        private void HourQty()
        {

            decimal stdiff = (decimal.Parse(prost.Text));
            if (stdiff != 0)
            {
                OPHRate();
                decimal rate = (decimal)Prorate / 100;
                prostdcapacity.Text = ((decimal.Parse(prodirect.Text) * 60) / (decimal.Parse(prost.Text)) * rate).ToString("0.0");
            }
        }









    }

}
