using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakerTableStore
{
    public partial class Products : System.Web.UI.Page
    {
        //These variables will be used to create a shopping cart item and eventually the values are sent back to the database
        public int OrderQuant { get; set;}
        public int ProdID { get; set;}
        public string UserEmail;
        public int LoggedIn;
        public int UserID=0;
        //string CartID = System.Web.HttpContext.Current.Session.SessionID;
        public int CartID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //On page load, the method grabs the user's email from the authentication
            UserEmail = Context.User.Identity.GetUserName();
            if (UserEmail != "")
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "makertable.database.windows.net";
                builder.UserID = "admin1";
                builder.Password = "Lespaul@9";
                builder.InitialCatalog = "MakerTableDB";

                //The user's email address is used to query the database to find the UserID that matches the email
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT UserID, LoggedIn FROM [MakerTableDB].[dbo].[Users]");
                    sb.Append("WHERE @UserEmail = Email;");
                    String sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@UserEmail", UserEmail);
                        SqlDataReader reader = command.ExecuteReader();
                        reader.Read();
                        UserID = reader.GetInt32(0);
                        LoggedIn = reader.GetInt32(1);
                        ErrorMessage.Text = "Logged In:  " + LoggedIn + "  UserID: " + UserID;
                        reader.Close();
                        
                    }
                        StringBuilder sb1 = new StringBuilder();
                        sb.Append("SELECT CartID FROM [MakerTableDB].[dbo].[ShoppingCart]");
                        sb.Append("WHERE @UserID = UserID and ItemID=(SELECT max(ItemID);");
                        String sql1 = sb1.ToString();
                        using (SqlCommand command = new SqlCommand(sql1, connection))
                        {
                            command.Parameters.AddWithValue("@UserID", UserID);
                            SqlDataReader reader = command.ExecuteReader();
                            reader.Read();
                            CartID = reader.GetInt32(0);
                            Debug.WriteLine("CartId:  " + CartID);
                            reader.Close();

                        }
                        connection.Close();
                }
            }
            catch (SqlException e2)
            {
                Debug.WriteLine(e2.ToString());
            }

        }

        //I will need to create a custom method for each product. This method sets the variables to the correct ProductID as well as the users desired order quantity
        protected void PorkCutBtn_Click(object sender, EventArgs e)
        {
            int OrderQuant = 0;
            Int32.TryParse(PorkCutQty.Text, out OrderQuant);
            ProdID = 21;
            ErrorMessage.Text = "ProductID: " + ProdID + "    Order Quantity: " + OrderQuant;
            addToCart(OrderQuant, ProdID);
        }

        private void addToCart(int OrderQuant, int ProdID)
        {
            if (LoggedIn == 1)
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
                        sb.Append("INSERT INTO [MakerTableDB].[dbo].[ShoppingCart] ([CartID], [UserID], [ProductID], [Quantity]) ");
                        sb.Append("VALUES (@CartID, @UserID, @ProductID, @Quantity);");
                        String sql = sb.ToString();
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            Debug.WriteLine("CartID:  " + CartID);
                            Debug.WriteLine("ProductID:  " + ProdID);
                            Debug.WriteLine("  UserID:  " + UserID);
                            Debug.WriteLine("Order Quantity:  " + OrderQuant);
                            command.Parameters.AddWithValue("@CartID", CartID);
                            command.Parameters.AddWithValue("@UserID", UserID);
                            command.Parameters.AddWithValue("@ProductID", ProdID);
                            command.Parameters.AddWithValue("@Quantity", OrderQuant);
                            int rowsAffected = command.ExecuteNonQuery();
                            ErrorMessage.Text = rowsAffected + " row(s) inserted";
                            connection.Close();
                        }
                    }
                }
                catch (SqlException e3)
                {
                    ErrorMessage.Text = e3.ToString();
                }
            }
            else
            {
                ErrorMessage.Text = "Please log in to add items to shopping cart";
            }
        }

        //shoppingCartInsert(OrderQuant, ProdID)
        //{
        //    try
        //    {
        //        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        //        builder.DataSource = "makertable.database.windows.net";
        //        builder.UserID = "admin1";
        //        builder.Password = "Lespaul@9";
        //        builder.InitialCatalog = "MakerTableDB";

        //        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
        //        {
        //            connection.Open();
        //            StringBuilder sb = new StringBuilder();
        //            sb.Append("INSERT INTO [MakerTableDB].[dbo].[ShoppingCart] ([CartID], [UserID], [ProductID], [Quantity]) ");
        //            sb.Append("VALUES (@CartID, @UserID, @ProductID, @Quantity);");
        //            String sql = sb.ToString();
        //            using (SqlCommand command = new SqlCommand(sql, connection))
        //            {
        //                Debug.WriteLine("CartID:  " + CartID);
        //                Debug.WriteLine("ProductID:  " + ProdID);
        //                Debug.WriteLine("  UserID:  " + UserID);
        //                Debug.WriteLine("Order Quantity:  " + OrderQuant);
        //                command.Parameters.AddWithValue("@CartID", CartID);
        //                command.Parameters.AddWithValue("@UserID", UserID);
        //                command.Parameters.AddWithValue("@ProductID", ProdID);
        //                command.Parameters.AddWithValue("@Quantity", OrderQuant);
        //                int rowsAffected = command.ExecuteNonQuery();
        //                ErrorMessage.Text = rowsAffected + " row(s) inserted";
        //                connection.Close();
        //            }
        //        }
        //    }
        //    catch (SqlException e3)
        //    {
        //        ErrorMessage.Text = e3.ToString();
        //    }

        //}

        //The shopping cart btn method gathers all of the shopping cart items and then executes an insert statement to the Azure database
        protected void ShoppingCartBtn_Click(object sender, EventArgs e)
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
                    sb.Append("INSERT INTO [MakerTableDB].[dbo].[Orders] ([UserID], [ProductID], [Quantity]) ");
                    sb.Append("VALUES (@UserID, @ProductID, @Quantity);");
                    String sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        Debug.WriteLine("ProductID:  " + ProdID);
                        Debug.WriteLine("  UserID:  " + UserID);
                        Debug.WriteLine("Order Quantity:  " + OrderQuant);
                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.Parameters.AddWithValue("@ProductID", ProdID);
                        command.Parameters.AddWithValue("@Quantity", OrderQuant);
                        int rowsAffected = command.ExecuteNonQuery();
                        ErrorMessage.Text = rowsAffected + " row(s) inserted";
                        connection.Close();
                    }
                }
            }
            catch (SqlException e3)
            {
                ErrorMessage.Text = e3.ToString();
            }
        }
    }
}