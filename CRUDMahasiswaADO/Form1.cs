using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRUDMahasiswaADO
{
    public partial class Form1 : Form
    {
        // Langkah 4 – Variabel Koneksi
        private SqlConnection conn;
        private string connectionString = "Data Source=localhost;Initial Catalog=DESKTOP-4G4UMV8\\AFIFAH;Integrated Security=True";

        // Langkah 5 – Constructor
        public Form1()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        // Langkah 5 – Method Koneksi Database (Event tombol Connect)
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    MessageBox.Show("Koneksi berhasil dibuka");
                }
                else
                {
                    MessageBox.Show("Koneksi sudah terbuka");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka koneksi: " + ex.Message);
            }
        }

        
    }
}
