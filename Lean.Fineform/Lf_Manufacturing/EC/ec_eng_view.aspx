﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ec_eng_view.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.EC.ec_eng_view" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        /*.f-grid-row .f-grid-cell-inner {
            font-size:small;
            white-space: normal;
            word-break: break-all;
        }*/
        .f-grid-row .f-grid-cell-IsManage {
            font-size: 75%;
            /*background-color: #66CCCC;*/
            /*color: #fff;*/
        }

        .f-grid-row .f-grid-cell-UDF06 {
            font-size: 75%;
            /*background-color: #66CCCC;*/
            /*color: #fff;*/
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" AjaxTimeout="3600" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="设计变更ECN">
            <Toolbars>
                <f:Toolbar ID="Toolbar3" runat="server">
                    <Items>
                        <f:DatePicker runat="server" Label="<%$ Resources:GlobalResource,Query_Startdate%>" DateFormatString="yyyyMMdd" EmptyText="<%$ Resources:GlobalResource,Query_Startdate_EmptyText%>" AutoPostBack="true"
                            ID="DpStartDate" ShowRedStar="True" OnTextChanged="DpStartDate_TextChanged">
                        </f:DatePicker>
                        <f:ToolbarSeparator ID="ToolbarSeparator3" runat="server"></f:ToolbarSeparator>
                        <f:DatePicker ID="DpEndDate" Readonly="false" CompareControl="DpStartDate" DateFormatString="yyyyMMdd" AutoPostBack="true"
                            CompareOperator="GreaterThan" CompareMessage="<%$ Resources:GlobalResource,Query_Enddate_EmptyText%>" Label="<%$ Resources:GlobalResource,Query_Enddate%>"
                            runat="server" ShowRedStar="True" OnTextChanged="DpEndDate_TextChanged">
                        </f:DatePicker>
                        <f:ToolbarFill ID="ToolbarFill3" runat="server"></f:ToolbarFill>
                        <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Pp_EC_EmptyText%>"
                            Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                            OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                        </f:TwinTriggerBox>
                    </Items>
                </f:Toolbar>
            </Toolbars>

            <Items>
                <f:Form ID="Form3" runat="server" ShowHeader="false" ShowBorder="false" LabelAlign="Right">
                    <Rows>
                        <f:FormRow ID="FormRow2" runat="server">
                            <Items>
                                <f:RadioButtonList ID="rblAuto" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="rblAuto_SelectedIndexChanged" >                                    
                                    <f:RadioItem Text="<%$ Resources:GlobalResource,sys_Status_All%>" Value="0" Selected="true"/>
                                    <f:RadioItem Text="<%$ Resources:GlobalResource,sys_Status_Region%>" Value="1" />
                                    <f:RadioItem Text="<%$ Resources:GlobalResource,co_Dept_MM%>" Value="2" />
                                    <f:RadioItem Text="<%$ Resources:GlobalResource,sys_Status_Internal%>" Value="3" />
                                    <f:RadioItem Text="<%$ Resources:GlobalResource,co_Dept_TE%>" Value="4" />
                                    
                                </f:RadioButtonList>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                <f:Button ID="btnNew" runat="server" Icon="Add" EnablePostBack="false" Text="<%$ Resources:GlobalResource,sys_Button_New%>">
                                </f:Button>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>

                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false" ForceFit="true" IsFluid="true"
                    DataKeyNames="GUID,Ec_no" AllowSorting="true" SortField="Ec_issuedate"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true"
                    OnSort="Grid1_Sort"
                    OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound"
                    OnRowCommand="Grid1_RowCommand"
                    OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText1" runat="server" Text="<%$ Resources:GlobalResource,sys_Grid_Pagecount%>">
                        </f:ToolbarText>
                        <f:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged"
                            runat="server">
                            <f:ListItem Text="10" Value="10" />
                            <f:ListItem Text="20" Value="20" />
                            <f:ListItem Text="50" Value="50" />
                            <f:ListItem Text="100" Value="100" />
                        </f:DropDownList>
                    </PageItems>
                    <Columns>
                        <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,sys_Button_Edit%>" Width="100px" Icon="TableEdit" ColumnID="editField" CommandName="Edit" Text="<%$ Resources:GlobalResource,sys_Button_Edit%>" />

                        <f:BoundField DataField="Ec_issuedate" SortField="Ec_issuedate" Width="90px" MinWidth="100px" HeaderText="发行日期" />
                        <f:HyperLinkField DataTextField="Ec_no" SortField="Ec_no" Width="90px" HeaderText="设变号码" MinWidth="100px" DataNavigateUrlFields="Ec_documents" />
                        <f:BoundField DataField="UDF06" ColumnID="UDF06" SortField="UDF06" Width="200px" MinWidth="200px" HtmlEncode="false" HeaderText="设变标题" />
                        <f:BoundField DataField="Ec_leader" SortField="Ec_leader" Width="80px" MinWidth="80px" HeaderText="担当" />
                        <f:BoundField DataField="Ec_distinction" SortField="Ec_distinction" Width="80px" MinWidth="80px" HeaderText="管理区分" />
                        <%--<f:BoundField DataField="IsSopUpdate" SortField="IsSopUpdate" Width="80px" MinWidth="80px" HeaderText="SOP" />
                        <f:BoundField DataField="IsManage" ColumnID="IsManage" SortField="IsManage" Width="80px" MinWidth="80px" HeaderText="物料管理区分" />
                        <f:BoundField DataField="IsMmManage" ColumnID="IsMmManage" SortField="IsMmManage" Width="80px" MinWidth="80px" HeaderText="部管" />
                        <f:BoundField DataField="IsPcbaManage" ColumnID="IsPcbaManage" SortField="IsPcbaManage" Width="80px" MinWidth="80px" HeaderText="制二" />
                        <f:BoundField DataField="IsAssyManage" ColumnID="IsAssyManage" SortField="IsAssyManage" Width="80px" MinWidth="80px" HeaderText="组立" />--%>
                        <f:BoundField DataField="Ec_status" SortField="Ec_status" Width="80px" MinWidth="80px" HeaderText="状态" />
                        <f:HyperLinkField DataTextField="Ec_letterno" Width="100px" HeaderText="技联NO." MinWidth="100px" DataNavigateUrlFields="Ec_letterdoc" />
                        <f:HyperLinkField DataTextField="Ec_eppletterno" Width="100px" HeaderText="P番(DTA)" MinWidth="100px" DataNavigateUrlFields="Ec_eppletterdoc" />
                        <f:HyperLinkField DataTextField="Ec_teppletterno" Width="100px" HeaderText="P番(TCJ)" MinWidth="100px" DataNavigateUrlFields="Ec_teppletterdoc" />
                        <f:RenderField DataField="CreateDate" ColumnID="CreateDate" SortField="CreateDate" EnableLock="true" Width="120px" HeaderText="登录日期" FieldType="Date"
                            Renderer="Date" RendererArgument="yyyyMMdd" />

                        <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete"
                            HeaderText="<%$ Resources:GlobalResource,sys_Button_Delete%>" Text="<%$ Resources:GlobalResource,sys_Button_Delete%>"
                            ToolTip="<%$ Resources:GlobalResource,sys_Button_Delete%>" ConfirmText="<%$ Resources:GlobalResource,sys_Button_DeleteConfirmText%>"
                            ConfirmTarget="Top" CommandName="Delete" Width="100px" />
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank" Width="1000px"
            Height="800px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>





