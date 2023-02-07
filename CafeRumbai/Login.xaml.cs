using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace CafeRumbai
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        public Login()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["CafeRumbaiEntities3"].ConnectionString.ToString();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            if (cekLogin(txtUsername.Text, txtPass.Password))
            {
                MessageBox.Show("Login Berhasil", "Berhasil", MessageBoxButton.OK, MessageBoxImage.Information);
                MenuPemesanan menu = new MenuPemesanan();
                menu.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Username atau password salah", "Gagal", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool cekLogin(string username, string password)
        {
            con.Open();
            com.Connection = con;
            com.CommandText = "select IdUser from tblLogin where username='" + username + "' and password='" + password + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToBoolean(dr["IdUser"]) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
