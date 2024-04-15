﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p2d_smt_short_edit.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.PP.daily.P2D.p2d_smt_short_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" ShowBorder="false" ShowHeader="false"  AutoScroll="true" runat="server">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" Icon="SystemClose" EnablePostBack="false" runat="server"
                            Text="<%$ Resources:GlobalResource,WindowsForm_Close%>" >
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:Button ID="btnSaveClose" ValidateForms="SimpleForm1" Icon="SystemSaveClose" OnClick="btnSaveClose_Click"
                            runat="server" Text="<%$ Resources:GlobalResource,WindowsForm_SaveClose%>">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
                        <Items>
                <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" runat="server" BodyPadding="10px"
                    Title="SimpleForm">
                    <Rows>
                        <f:FormRow ID="FormRow2" runat="server">
                            <Items>
                                <f:Label runat="server" Label="生产机种" ID="Promodel"  LabelWidth="120" LabelAlign="Right">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:Label runat="server" Label="完成品" ID="Proitem" LabelWidth="120" LabelAlign="Right">
                                </f:Label>
                                <f:Label runat="server" Label="文本" ID="Proitemtext" LabelWidth="120" LabelAlign="Right">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>
                                <f:Label runat="server" ID="Propcbitem" Label="SAP物料" LabelWidth="120" LabelAlign="Right">
                                </f:Label>
                                <f:Label runat="server" ID="Propcbtext" Label="SAP物料文本" LabelWidth="120" LabelAlign="Right">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:NumberBox runat="server" Label="SAP Short数" ID="Propcbshort" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="9999" DecimalPrecision="2" Required="True" ShowRedStar="True" LabelAlign="Right">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow7" runat="server">
                            <Items>
                                <f:Label runat="server" Label="PCB品号" ID="Propcba" LabelWidth="120" LabelAlign="Right">
                                </f:Label>
                                <f:Label runat="server" Label="PCB品名" ID="Propcbatext" LabelWidth="120" LabelAlign="Right">
                                </f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:NumberBox runat="server" Label="Short数" ID="Proconvertshort" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="9999" DecimalPrecision="2" Required="True" ShowRedStar="True" LabelAlign="Right">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow5" runat="server">
                            <Items>
                                <f:NumberBox runat="server" Label="机器点数" ID="Promachineshort" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="9999" DecimalPrecision="2" Required="True" ShowRedStar="True" LabelAlign="Right">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow8" runat="server">
                            <Items>
                                <f:NumberBox runat="server" Label="手贴点数" ID="Promanualshort" NoDecimal="false" NoNegative="True" DecimalPrecision="5" Required="True" MinValue="0" MaxValue="9999" ShowRedStar="True" LabelAlign="Right" RegexPattern="NUMBER" EmptyText="0">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>

                    </Rows>
                </f:Form>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="700px"
            Height="650px">
        </f:Window>
    </form>
        
</body>
</html>




