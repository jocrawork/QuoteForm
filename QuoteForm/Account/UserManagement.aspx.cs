using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using QuoteForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuoteForm
{
    public partial class UserManagement : Page
    {
        ApplicationDbContext context = new ApplicationDbContext();
        List<ApplicationUser> users = new List<ApplicationUser>();
        
        protected void Page_Load(object sender, EventArgs e)
        {//this is not pretty. sorry

            if (!IsPostBack)
            {
                LoadRoles();
                LoadUsers();
            }
            
            if (Session["DDLchoice"] != null)
            {
                var test = Convert.ToInt32(Session["DDLchoice"]);
                rolesDDL.SelectedIndex = test;
                Session["DDLchoice"] = null;

                LoadUsersInRole();
            }
            else if (!IsPostBack)
            {
                rolesDDL.SelectedIndex = -1;

                repUsers.DataSource = users;
                repUsers.DataBind();
            }
        }

        protected void LoadRoles()
        {
            var roles = context.Roles.ToList();

            ListItem def = new ListItem("Select a Role", null);
            rolesDDL.Items.Add(def);
            foreach(var role in roles)
            {
                ListItem temp = new ListItem(role.Name, role.Id);
                rolesDDL.Items.Add(temp);
            }
        }
        protected void LoadUsers()
        {
            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var users = userManager.Users;

            ListItem def = new ListItem("Select a User", null);
            usersDDL.Items.Add(def);
            foreach (var user in users)
            {
                ListItem temp = new ListItem(user.UserName, user.Id);
                usersDDL.Items.Add(temp);
            }

        }

        protected void AddRole(Object source, EventArgs e)
        {
            context.Roles.Add(new IdentityRole(NewRoleName.Text));
            context.SaveChanges();

            Response.Redirect(Request.RawUrl);
        }

        protected void LoadUsersInRole(Object source, EventArgs e) {LoadUsersInRole();}
        protected void LoadUsersInRole()
        {
            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = Request.GetOwinContext().Get<ApplicationRoleManager>();

            var pairs = roleManager.FindByName(rolesDDL.SelectedItem.Text).Users;


            foreach (var user in pairs)
            {
                users.Add((userManager.FindById(user.UserId)));
            }

            repUsers.DataSource = users;
            repUsers.DataBind();
        }
        
        protected void AddToRole(Object source, EventArgs e)
        {
            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var user = usersDDL.SelectedItem.Value;
            userManager.AddToRole(user,rolesDDL.SelectedItem.Text);

            LoadUsersInRole();
        }
        protected void repUsers_ItemCommand(Object source, CommandEventArgs e)
        {
            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = Request.GetOwinContext().Get<ApplicationRoleManager>();
            var user = userManager.FindByEmail(e.CommandArgument.ToString());

            if(e.CommandName == "Remove")
            {
                Session["DDLchoice"] = rolesDDL.SelectedIndex;
                userManager.RemoveFromRole(user.Id, rolesDDL.SelectedItem.Text);

                userManager.Update(user);
                Context.GetOwinContext().Get<ApplicationDbContext>().SaveChanges();

                Response.Redirect(Request.RawUrl);
            }


        }
    }
}