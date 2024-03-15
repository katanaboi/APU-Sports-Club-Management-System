using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APU_Sports_Club_Management_System
{
    internal class User
    {
        private string username; 
        private string password;

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public string Login(string un)
        {
            string status = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCS"].ToString());
            con.Open();

            SqlCommand cmd = new SqlCommand("select count(*) from User where name=@a and password=@b", con);
            cmd.Parameters.AddWithValue("name", username);
            cmd.Parameters.AddWithValue("password", password);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count > 0) //if login success 
            {
                SqlCommand cmd2 = new SqlCommand("Select role from users where username =@a and password =@b", con);
                cmd2.Parameters.AddWithValue("@a", username);
                cmd2.Parameters.AddWithValue("@b", password);

                string userRole = cmd2.ExecuteScalar().ToString();

                if (userRole == "admin")
                {
                    AdminHome a = new AdminHome(un);
                    a.ShowDialog();
                }
                else if (userRole == "coach")
                {
                    CoachHome c = new CoachHome(un);
                    c.ShowDialog();
                }
                else if (userRole == "manager")
                {
                    ManagerHome m = new ManagerHome(un);
                }
                else if (userRole == "member")
                {
                    MemberHome mm = new MemberHome(un);
                }
            }
            else
                status = "Incorrect username/password";

            con.Close();

            return status;
        }


    }
}
