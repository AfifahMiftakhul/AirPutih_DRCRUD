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

        // Langkah 6 – Menampilkan Data dengan SqlDataReader (event btnLoad_Click)
        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = "SELECT NIM, Nama, JenisKelamin, TanggalLahir, Alamat, KodeProdi FROM Mahasiswa";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        // Langkah 7 – Menambahkan Data (event btnInsert_Click)
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                
    }
}
