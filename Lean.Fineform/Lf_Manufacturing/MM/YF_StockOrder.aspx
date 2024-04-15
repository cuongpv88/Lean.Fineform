﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YF_StockOrder.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.MM.YF_StockOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            font-size: small;
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="TabStrip1" />
        <f:TabStrip ID="TabStrip1" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ActiveTabIndex="0"
            runat="server">
            <Tabs>
                <f:Tab Title="H100" BodyPadding="10px" Layout="VBox" runat="server">
                    <Items>

                        <f:Form ID="Form2" runat="server" ShowHeader="false" ShowBorder="false" LabelAlign="Right">
                            <Rows>
                                <f:FormRow ID="FormRow2" runat="server">
                                    <Items>
                                        <f:DatePicker runat="server" Label="<%$ Resources:GlobalResource,Query_Startdate%>" DateFormatString="yyyyMMdd" EmptyText="<%$ Resources:GlobalResource,Query_Startdate_EmptyText%>" AutoPostBack="true"
                                            ID="H_DpStartDate" ShowRedStar="True" OnTextChanged="H_DpStartDate_TextChanged">
                                        </f:DatePicker>
                                        <f:DatePicker ID="H_DpEndDate" Readonly="false" Width="300px" CompareControl="H_DpStartDate" DateFormatString="yyyyMMdd" AutoPostBack="true"
                                            CompareOperator="GreaterThan" CompareMessage="<%$ Resources:GlobalResource,Query_Enddate_EmptyText%>" Label="<%$ Resources:GlobalResource,Query_Enddate%>"
                                            runat="server" ShowRedStar="True" OnTextChanged="H_DpEndDate_TextChanged">
                                        </f:DatePicker>
                                        <f:ToolbarFill ID="ToolbarFill4" runat="server"></f:ToolbarFill>
                                        <f:TwinTriggerBox ID="H_ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Enter_EmptyText%>"
                                            Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="H_ttbSearchMessage_Trigger2Click"
                                            OnTrigger1Click="H_ttbSearchMessage_Trigger1Click">
                                        </f:TwinTriggerBox>
                                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                        <f:Button ID="Btn_tac" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false"
                                            runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" OnClick="Btn_tac_Click" CssClass="marginr">
                                        </f:Button>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                        <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false" ForceFit="true"
                            DataKeyNames="TG003" AllowSorting="true" SortField="TG003" EnableTextSelection="true"
                            SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true"
                            OnPageIndexChange="Grid1_PageIndexChange"
                            EnableMultiSelect="false" EnableRowDoubleClickEvent="true"
                            OnSort="Grid1_Sort"
                            OnRowDataBound="Grid1_RowDataBound"
                            OnPreRowDataBound="Grid1_PreRowDataBound">
                            <%--<Toolbars>

                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>                                
                                <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>

                                <f:Button ID="BtnExport" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false" 
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" OnClick="BtnExport_Click" CssClass="marginr">  </f:Button>
                                
                            </Items>
                        </f:Toolbar>
                    </Toolbars>--%>
                            <PageItems>
                                <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                                </f:ToolbarSeparator>
                                <f:ToolbarText ID="ToolbarText1" runat="server" Text="<%$ Resources:GlobalResource,sys_Grid_Pagecount%>">
                                </f:ToolbarText>
                                <f:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="H_ddlGridPageSize_SelectedIndexChanged"
                                    runat="server">
                                    <f:ListItem Text="10" Value="10" />
                                    <f:ListItem Text="20" Value="20" />
                                    <f:ListItem Text="50" Value="50" />
                                    <f:ListItem Text="100" Value="100" />
                                </f:DropDownList>

                            </PageItems>
                            <Columns>

                                <f:BoundField DataField="TH001" SortField="TH001" Width="80px" HeaderText="单号" />
                                <f:BoundField DataField="TG003" SortField="TG003" Width="80px" HeaderText="日期" />
                                <f:BoundField DataField="TG005" SortField="TG005" Width="80px" HeaderText="业者" />
                                <f:BoundField DataField="TG007" SortField="TG007" Width="80px" HeaderText="币种" />
                                <f:BoundField DataField="TH004" SortField="TH004" Width="80px" HeaderText="物料" />
                                <f:BoundField DataField="TH005" SortField="TH005" Width="80px" HeaderText="描述" />
                                <f:BoundField DataField="TH007" SortField="TH007" Width="80px" HeaderText="数量" />
                                <f:BoundField DataField="TH015" SortField="TH015" Width="80px" HeaderText="验收" />
                                <f:BoundField DataField="TH011" SortField="TH011" Width="80px" HeaderText="采购单" />
                                <f:BoundField DataField="TH018" SortField="TH018" Width="80px" HeaderText="进价" />
                                <f:BoundField DataField="TH019" SortField="TH019" Width="80px" HeaderText="进货金额" />






                            </Columns>
                        </f:Grid>

                    </Items>
                </f:Tab>
                <f:Tab Title="C100" BodyPadding="10px" Layout="VBox" runat="server">
                    <Items>
                        <f:Form ID="Form3" runat="server" ShowHeader="false" ShowBorder="false" LabelAlign="Right">
                            <Rows>
                                <f:FormRow ID="FormRow1" runat="server">
                                    <Items>
                                        <f:DatePicker runat="server" Label="<%$ Resources:GlobalResource,Query_Startdate%>" DateFormatString="yyyyMMdd" EmptyText="<%$ Resources:GlobalResource,Query_Startdate_EmptyText%>" AutoPostBack="true"
                                            ID="C_DpStartDate" ShowRedStar="True" OnTextChanged="C_DpStartDate_TextChanged">
                                        </f:DatePicker>
                                        <f:DatePicker ID="C_DpEndDate" Readonly="false" Width="300px" CompareControl="C_DpStartDate" DateFormatString="yyyyMMdd" AutoPostBack="true"
                                            CompareOperator="GreaterThan" CompareMessage="<%$ Resources:GlobalResource,Query_Enddate_EmptyText%>" Label="<%$ Resources:GlobalResource,Query_Enddate%>"
                                            runat="server" ShowRedStar="True" OnTextChanged="C_DpEndDate_TextChanged">
                                        </f:DatePicker>
                                        <f:ToolbarFill ID="ToolbarFill3" runat="server"></f:ToolbarFill>
                                        <f:TwinTriggerBox ID="C_ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Enter_EmptyText%>"
                                            Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="C_ttbSearchMessage_Trigger2Click"
                                            OnTrigger1Click="C_ttbSearchMessage_Trigger1Click">
                                        </f:TwinTriggerBox>
                                        <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>
                                        <f:Button ID="Btn_dta" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false"
                                            runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" CssClass="marginr" OnClick="Btn_dta_Click">
                                        </f:Button>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                        <f:Grid ID="Grid2" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false" ForceFit="true"
                            DataKeyNames="TG003" AllowSorting="true" SortField="TG003" EnableTextSelection="true"
                            SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true"
                            OnPageIndexChange="Grid2_PageIndexChange"
                            EnableMultiSelect="false" EnableRowDoubleClickEvent="true"
                            OnSort="Grid2_Sort"
                            OnRowDataBound="Grid2_RowDataBound"
                            OnPreRowDataBound="Grid2_PreRowDataBound">
                            <%--<Toolbars>

                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>                                
                                <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>

                                <f:Button ID="BtnExport" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false" 
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" OnClick="BtnExport_Click" CssClass="marginr">  </f:Button>
                                
                            </Items>
                        </f:Toolbar>
                    </Toolbars>--%>
                            <PageItems>
                                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                </f:ToolbarSeparator>
                                <f:ToolbarText ID="ToolbarText2" runat="server" Text="<%$ Resources:GlobalResource,sys_Grid_Pagecount%>">
                                </f:ToolbarText>
                                <f:DropDownList ID="DropDownList1" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="C_ddlGridPageSize_SelectedIndexChanged"
                                    runat="server">
                                    <f:ListItem Text="10" Value="10" />
                                    <f:ListItem Text="20" Value="20" />
                                    <f:ListItem Text="50" Value="50" />
                                    <f:ListItem Text="100" Value="100" />
                                </f:DropDownList>

                            </PageItems>
                            <Columns>

                                <f:BoundField DataField="TH001" SortField="TH001" Width="80px" HeaderText="单号" />
                                <f:BoundField DataField="TG003" SortField="TG003" Width="80px" HeaderText="日期" />
                                <f:BoundField DataField="TG005" SortField="TG005" Width="80px" HeaderText="业者" />
                                <f:BoundField DataField="TG007" SortField="TG007" Width="80px" HeaderText="币种" />
                                <f:BoundField DataField="TH004" SortField="TH004" Width="80px" HeaderText="物料" />
                                <f:BoundField DataField="TH005" SortField="TH005" Width="80px" HeaderText="描述" />
                                <f:BoundField DataField="TH007" SortField="TH007" Width="80px" HeaderText="数量" />
                                <f:BoundField DataField="TH015" SortField="TH015" Width="80px" HeaderText="验收" />
                                <f:BoundField DataField="TH011" SortField="TH011" Width="80px" HeaderText="采购单" />
                                <f:BoundField DataField="TH018" SortField="TH018" Width="80px" HeaderText="进价" />
                                <f:BoundField DataField="TH019" SortField="TH019" Width="80px" HeaderText="进货金额" />


                            </Columns>
                        </f:Grid>

                    </Items>
                </f:Tab>

            </Tabs>
        </f:TabStrip>
        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank" Width="800px"
            Height="500px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>
