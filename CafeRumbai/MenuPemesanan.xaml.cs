using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;
using System.Data.SqlClient;
using System.Data;

namespace CafeRumbai
{
    /// <summary>
    /// Interaction logic for MenuPemesanan.xaml
    /// </summary>
    public partial class MenuPemesanan : Window
    {

        int hargaTotal = 0;

        CafeRumbaiEntities4 _db = new CafeRumbaiEntities4();
        private int _JumlahPesan = 1;
        private int tambahan;
        public MenuPemesanan()
        {
            InitializeComponent();
            txtJumlah.Text = _JumlahPesan.ToString();
            setMinuman();
            setUkuran();
            tabelKeranjang.ItemsSource = _db.keranjangs.ToList();
        }
        public void setMinuman()
        {
            var namaMinuman = (from k in _db.minumans
                               select k.NamaMinuman).ToList();
            listminuman.ItemsSource = namaMinuman;
        }
        public void setUkuran()
        {
            var ukuranMinuman = (from k in _db.ukurans
                                 select k.Ukuran1).ToList();
            listukuran.ItemsSource = ukuranMinuman;
        }
        public int JumlahPesan
        {
            get { return _JumlahPesan; }
            set
            {
                _JumlahPesan = value;
                txtJumlah.Text = value.ToString();
            }
        }

        private void tambah_Click(object sender, RoutedEventArgs e)
        {
            JumlahPesan++;
        }

        private void kurang_Click(object sender, RoutedEventArgs e)
        {
            JumlahPesan--;
        }
        private void txtJumlah_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtJumlah == null)
            {
                return;
            }

            if (!int.TryParse(txtJumlah.Text, out _JumlahPesan))
                txtJumlah.Text = _JumlahPesan.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tambahanTopping();
            string minumanSS = listminuman.SelectedItem.ToString();
            string ukuranSS = listukuran.SelectedItem.ToString();
            int hargaminuman = (int)(from k in _db.minumans
                                     where k.NamaMinuman.Contains(minumanSS)
                                     select k.HargaMinuman).Single();

            string hargaukuran = (from k in _db.ukurans
                                  where k.Ukuran1.Contains(ukuranSS)
                                  select k.HargaUkuran).Single();

            decimal hasil = (int.Parse(txtJumlah.Text) * (hargaminuman)) + (int.Parse(txtJumlah.Text) * Decimal.Parse(hargaukuran)) + (int.Parse(txtJumlah.Text) * tambahan);

            txtTotal.Text = hasil.ToString();
        }

        private void tambahanTopping()
        {
            if (Oreo.IsChecked == true)
            {
                tambahan = 3000;
                return;
            }
            else if (Meses.IsChecked == true)
            {
                tambahan = 2000;
                return;
            }
            else if (Pearl.IsChecked == true)
            {
                tambahan = 3000;
                return;
            }
            else if (Caramel.IsChecked == true)
            {
                tambahan = 3000;
                return;
            }
            else if (ChocoChips.IsChecked == true)
            {
                tambahan = 3000;
                return;
            }
            else if (WhipCream.IsChecked == true)
            {
                tambahan = 2000;
                return;
            }
            else if (Coklat.IsChecked == true)
            {
                tambahan = 2000;
                return;
            }
            else if (Jelly.IsChecked == true)
            {
                tambahan = 2000;
                return;
            }
            else if (Cincau.IsChecked == true)
            {
                tambahan = 2000;
                return;
            }
        }

        public void reset()
        {
            listukuran.SelectedIndex = 0;
            listminuman.SelectedIndex = 0;
            txtJumlah.Text = "1";
            txtTotal.Text = "";
            Oreo.IsChecked = false;
            Meses.IsChecked = false;
            Pearl.IsChecked = false;
            Caramel.IsChecked = false;
            ChocoChips.IsChecked = false;
            WhipCream.IsChecked = false;
            Coklat.IsChecked = false;
            Jelly.IsChecked = false;
            Cincau.IsChecked = false;
        }
        private void btnBatal_Click(object sender, RoutedEventArgs e)
        {
            reset();
        }

        private void btnPesan_Click(object sender, RoutedEventArgs e)
        {
            string minuman = listminuman.SelectedItem.ToString();
            string ukuran = listukuran.SelectedItem.ToString();

            var idminuman = (from k in _db.minumans
                             where k.NamaMinuman.Contains(minuman)
                             select k.IdMinuman).Single();
            var top = listTopping.Items.Cast<CheckBox>().Where(x => x.IsChecked == true).Select(x => x.Content).ToList();
            tambahanTopping();

            keranjang newKeranjang = new keranjang()
            {
                //IdKeranjang = idkeranjang,
                IdMinuman = idminuman,
                namaMinuman = minuman,
                ukuranMinuman = ukuran,
                topping = string.Join(", ", top),
                jumlahMinuman = txtJumlah.Text,
                hargaMinuman = int.Parse(txtTotal.Text)
            };
            _db.keranjangs.Add(newKeranjang);
            if (_db.SaveChanges() > 0)
            {
                MessageBox.Show("Penambahan Pesanan Telah Berhasil\n" +
                    "Nama Minuman : " + minuman + "\n" +
                    "Jumlah Minuman : " + txtJumlah.Text + "\n" +
                    "Total Harga: " + txtTotal.Text, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                reset();
                tabelKeranjang.ItemsSource = _db.keranjangs.ToList();
            }
        }

        private void hapus()
        {
            foreach (var Id in _db.keranjangs)
            {
                _db.keranjangs.Remove(Id);
            }

            _db.SaveChanges();
            tabelKeranjang.ItemsSource = _db.keranjangs.ToList();
        }

        private void btnSelesai_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Pembelian Telah Berhasil\n" + "Total Harga: " + sum(), "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            reset();
            hapus();
        }

        private int sum()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLOCALDB;Initial Catalog=CafeRumbai;Integrated Security=True;Pooling=False");
            con.Open();
            SqlCommand command = new SqlCommand("SELECT sum(hargaMinuman) FROM keranjang", con);
            SqlDataReader data = command.ExecuteReader();
            while (data.Read())
            {
                this.hargaTotal = int.Parse(data.GetValue(0).ToString());
            }
            con.Close();
            reset();

            return this.hargaTotal;
        }
    }
}
