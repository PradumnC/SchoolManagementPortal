﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="StudAttendanceDetails.aspx.cs" Inherits="SchoolManagementSystem.Admin.StudAttendanceDetails" %>
<%@ Register Src="~/StudentAttendanceUC.ascx" TagPrefix="uc" TagName="StudentAttendanceDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc:StudentAttendanceDetails runat="server" ID="StudentAttendanceDetails1"/>

</asp:Content>
