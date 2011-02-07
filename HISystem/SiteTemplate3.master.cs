﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.HtmlControls;

public partial class SiteTemplate3 : System.Web.UI.MasterPage
{
    protected void Page_PreRender(object sender, EventArgs e)
    {



        //if (Roles.IsUserInRole(HttpContext.Current.User.Identity.Name, "Doctor") && Page.Request.IsAuthenticated)
        //{
        //    //make hyperlink invisible
        //    lbl_AdminPrivileges.Visible = true;
        //    imgBtn_addUser.Visible = true;
        //    imgBtn_ManageUser.Visible = true;


        //    img_UserRole.ImageUrl = "~/images/doctor.png";
        //    img_UserRole.ToolTip = "You are logged in as Doctor!";
        //    menu.DataSourceID = "SiteMapDataSource1";
        //    return;
        //}
        //else if (Roles.IsUserInRole(HttpContext.Current.User.Identity.Name, "Midwife") && Page.Request.IsAuthenticated)
        //{
        //    img_UserRole.ImageUrl = "~/images/midwife.png";
        //    img_UserRole.ToolTip = "You are logged in as Midwife!";
        //    menu.DataSourceID = "SiteMapDataSource3";

        //}
        //else if (Roles.IsUserInRole(HttpContext.Current.User.Identity.Name, "Nurse") && Page.Request.IsAuthenticated)
        //{
        //    img_UserRole.ImageUrl = "~/images/nurse.png";
        //    img_UserRole.ToolTip = "You are logged in as Nurse!";
        //    menu.DataSourceID = "SiteMapDataSource2";
        //}
        //else
        //    //else user is guest
        //   menu.DataSourceID = "SiteMapDataSource1";
       
       
      


    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (this.IsPostBack)
        //{
        //    object eventArgument = this.Request["__EVENTARGUMENT"];
        //    if ((eventArgument != null) && (eventArgument.ToString().Trim() == this.TimerControl1.PostBackValue))
        //    {
        //        //Function or code to handle the elapse of the timer. 
        //    }
        //    else
        //    {
        //        // Do Nothing 
        //    }
        //}

        if (Context.Session != null && Context.Session.IsNewSession == true && Page.Request.Headers["Cookie"] != null && Page.Request.Headers["Cookie"].IndexOf("ASP.NET_SessionId") >= 0)
        {
            // session has timed out, log out the user
            if (Page.Request.IsAuthenticated || HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Session.Clear();

            }

        }
        else
        {

                    if (!Page.Request.IsAuthenticated)
                    {

                        //else user is guest
                        menu.DataSourceID = "SiteMapDataSource1";
                    }


                    //redirect to login in 5 seconds
                    else if (Request.Url.AbsolutePath.EndsWith("SessionExpired.aspx", StringComparison.InvariantCultureIgnoreCase))
                    {
                        HtmlMeta meta = new HtmlMeta();
                        meta.HttpEquiv = "Refresh";
                        meta.Content = "5; URL=./Login.aspx";
                        Page.Header.Controls.Add(meta);
                    }
                    //    start session timer if logged in
                    //else if (Page.Request.IsAuthenticated || HttpContext.Current.User.Identity.IsAuthenticated)
                    else if (Page.Request.IsAuthenticated)
                    {

                        string url = Page.ResolveUrl(@"~/Public/SessionExpired.aspx");
                        HttpContext.Current.Response.AppendHeader("Refresh", Convert.ToString((Session.Timeout * 302)) + "; Url=" + url);



                        if (Roles.IsUserInRole(HttpContext.Current.User.Identity.Name, "Doctor") && Page.Request.IsAuthenticated)
                        {
                            //make hyperlink invisible
                            lbl_AdminPrivileges.Visible = true;
                            imgBtn_addUser.Visible = true;
                            imgBtn_ManageUser.Visible = true;


                            img_UserRole.ImageUrl = "~/images/doctor.png";
                            img_UserRole.ToolTip = "You are logged in as Doctor!";
                            menu.DataSourceID = "SiteMapDataSource1";
                            return;
                        }
                        else if (Roles.IsUserInRole(HttpContext.Current.User.Identity.Name, "Midwife") && Page.Request.IsAuthenticated)
                        {
                            img_UserRole.ImageUrl = "~/images/midwife.png";
                            img_UserRole.ToolTip = "You are logged in as Midwife!";
                            menu.DataSourceID = "SiteMapDataSource3";

                        }
                        else if (Roles.IsUserInRole(HttpContext.Current.User.Identity.Name, "Nurse") && Page.Request.IsAuthenticated)
                        {
                            img_UserRole.ImageUrl = "~/images/nurse.png";
                            img_UserRole.ToolTip = "You are logged in as Nurse!";
                            menu.DataSourceID = "SiteMapDataSource2";
                        }
                       

                    }
                    else
                        //else user is guest
                        menu.DataSourceID = "SiteMapDataSource1";

        }
        
        
        
        
        
        
        
        
       

       


        
 
       
    }
    protected void imgBtn_addUser_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Admin/Add Users.aspx");
    }
    protected void imgBtn_ManageUser_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Admin/UsersAndRoles.aspx");
    }
}
