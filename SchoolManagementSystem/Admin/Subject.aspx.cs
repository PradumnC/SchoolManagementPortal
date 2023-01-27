﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SchoolManagementSystem.Models.CommonFn;


namespace SchoolManagementSystem.Admin
{
    public partial class Subject : System.Web.UI.Page
    {

        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                GetClass();
                GetSubject();
            }
        }
        private void GetClass()
        {
            DataTable dt = fn.Fetch("Select * from Class");
            ddlClass.DataSource = dt;
            ddlClass.DataTextField = "ClassName";
            ddlClass.DataValueField = "ClassId";
            ddlClass.DataBind();
            ddlClass.Items.Insert(0, "Select Class");

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {
                string classVal = ddlClass.SelectedItem.Text;
                DataTable dt = fn.Fetch("Select * from Subject Where ClassId='" + ddlClass.SelectedItem.Value + 
                                        "'and SubjectName='"+txtSubject.Text.Trim()+"'  ");
                if (dt.Rows.Count == 0)
                {
                    string query = "Insert into Subject values('" + ddlClass.SelectedItem.Value + "','" + txtSubject.Text.Trim() + "')";
                    fn.Query(query);
                    lblmsg.Text = "Inserted Sucessfully !";
                    lblmsg.CssClass = "alert alert-success";
                    ddlClass.SelectedIndex = 0;
                    txtSubject.Text = string.Empty;
                    GetSubject();
                }
                else
                {
                    lblmsg.Text = "Entered Subject already exists for<b>'" + classVal + "'</b>!";
                    lblmsg.CssClass = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

        private void GetSubject()
        {
            DataTable dt = fn.Fetch(@"Select Row_NUMBER() Over(order by(select 1)) as [sr.No], s.SubjectId, s.ClassId, c.ClassName, 
                                    s.SubjectName from Subject s inner join class c on c.ClassId = s.ClassId");

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetSubject();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetSubject();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetSubject();
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int SubjId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string ClassId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlSubject")).SelectedValue;
                string SubjName = (row.FindControl("txtSubjectName") as TextBox).Text;
                fn.Query("Update Subject set ClassId='" + ClassId + "', SubjectName = '"+ SubjName +"' where SubjectId = '"+ SubjId +"' ");
                lblmsg.Text = "Subject Updated Sucessfully";
                lblmsg.CssClass = "alert alert-sucess";
                GridView1.EditIndex = -1;
                GetSubject();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}