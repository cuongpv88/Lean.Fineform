﻿using System;
using System.Data.Entity;
using System.Linq;
using FineUIPro;

namespace LeanFine.Lf_Admin
{
    public partial class user_view : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreUserView";
            }
        }

        #endregion ViewPower

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            int id = GetQueryIntValue("id");
            Adm_User current = DB.Adm_Users
                .Include(u => u.Roles)
                .Include(u => u.Dept)
                .Include(u => u.Titles)
                .Where(u => u.ID == id).FirstOrDefault();
            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }

            labName.Text = current.Name;
            labRealName.Text = current.ChineseName;
            labCompanyEmail.Text = current.CompanyEmail;
            labEmail.Text = current.Email;
            labCellPhone.Text = current.CellPhone;
            labOfficePhone.Text = current.OfficePhone;
            labOfficePhoneExt.Text = current.OfficePhoneExt;
            labHomePhone.Text = current.HomePhone;
            labRemark.Text = current.Remark;
            labEnabled.Text = current.Enabled ? "启用" : "禁用";
            labGender.Text = current.Gender;

            // 用户所属角色
            labRole.Text = String.Join(",", current.Roles.Select(r => r.Name).ToArray());

            // 用户的职称列表
            labTitle.Text = String.Join(",", current.Titles.Select(t => t.Name).ToArray());

            // 用户所属的部门
            if (current.Dept != null)
            {
                labDept.Text = current.Dept.Name;
            }
        }

        #endregion Page_Load
    }
}