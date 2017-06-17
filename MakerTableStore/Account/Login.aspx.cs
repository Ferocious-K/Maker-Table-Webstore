using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using MakerTableStore.Models;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Diagnostics;

namespace MakerTableStore.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
            // Enable this once you have account confirmation enabled for password reset functionality
            //ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        private void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DataView dv;
            DataSet dataSet11 = new DataSet();

            dv = dataSet11.Tables[0].DefaultView;
            string txtEmail;
            args.IsValid = false;    // Assume False
                                     // Loop through table and compare each record against user's entry
            foreach (DataRowView datarow in dv)
            {
                // Extract e-mail address from the current row
                txtEmail = datarow["Email"].ToString();
                // Compare e-mail address against user's entry
                if (txtEmail == Email.Text)
                {
                    args.IsValid = true;
                }
            }
        }

        protected void LogIn(object sender, EventArgs e)
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
                    sb.Append("SELECT [MakerTableDB].[dbo].[Users] [Password], [Email] ");
                    sb.Append("WHERE [Password] = @Password and [Email] = @Email;");
                    String sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Password", Password.Text);
                        command.Parameters.AddWithValue("@Email", Email.Text);
                        int rowsAffected = command.ExecuteNonQuery();
                        Debug.WriteLine(rowsAffected + " row(s) inserted");
                    }
                }
            }
            catch (SqlException events)
            {
                Debug.WriteLine(events.ToString());
            }

            if (IsValid)
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
                        sb.Append("UPDATE [MakerTableDB].[dbo].[Users] SET LoggedIn = 1");
                        sb.Append("WHERE [Password] = @Password and [Email] = @Email;");
                        String sql = sb.ToString();
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@Password", Password.Text);
                            command.Parameters.AddWithValue("@Email", Email.Text);
                            int rowsAffected = command.ExecuteNonQuery();
                            Debug.WriteLine(rowsAffected + " row(s) affected");
                        }
                    }
                }
                catch (SqlException events)
                {
                    Debug.WriteLine(events.ToString());
                }
            // Validate the user password
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                // This doen't count login failures towards account lockout
                // To enable password failures to trigger lockout, change to shouldLockout: true
                var result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout: false);

                switch (result)
                {
                    case SignInStatus.Success:
                        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                        break;
                    case SignInStatus.LockedOut:
                        Response.Redirect("/Account/Lockout");
                        break;
                    case SignInStatus.RequiresVerification:
                        Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}", 
                                                        Request.QueryString["ReturnUrl"],
                                                        RememberMe.Checked),
                                          true);
                        break;
                    case SignInStatus.Failure:
                    default:
                        FailureText.Text = "Invalid login attempt";
                        ErrorMessage.Visible = true;
                        break;
                }
            }
        }

        protected void AuthenticateUser_Click(object sender, EventArgs events)
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
                    sb.Append("SELECT FROM [MakerTableDB].[dbo].[Users] ([Password], [Email]) ");
                    sb.Append("WHERE USERS.PASSWORD = @Password and USERS.EMAIL = @Email;");
                    String sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Password", Password.Text);
                        command.Parameters.AddWithValue("@Email", Email.Text);
                        int rowsAffected = command.ExecuteNonQuery();
                        Debug.WriteLine(rowsAffected + " row(s) inserted");
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
    }
}