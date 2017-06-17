using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using MakerTableStore.Models;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Diagnostics;
using MakerTableStore;

namespace MakerTableStore.Account
{
    public partial class Register : Page
    {



    protected void CreateUser_Click(object sender, EventArgs events)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "makertable.database.windows.net";
                builder.UserID = "admin1";
                builder.Password = "Lespaul@9";
                builder.InitialCatalog = "MakerTableDB";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO [MakerTableDB].[dbo].[Users] ([FirstName], [LastName], [Password], [Email]) ");
                    sb.Append("VALUES (@FirstName, @LastName, @Password, @Email);");
                    String sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", fName.Text);
                        command.Parameters.AddWithValue("@LastName", lName.Text);
                        command.Parameters.AddWithValue("@Password", Password.Text);
                        command.Parameters.AddWithValue("@Email", Email.Text);
                        int rowsAffected = command.ExecuteNonQuery();
                        Debug.WriteLine(rowsAffected + " row(s) inserted");
                        connection.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signinmanager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text };
            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {
                signinmanager.SignIn(user, isPersistent: false, rememberBrowser: false);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["returnurl"], Response);
            }
            else
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}