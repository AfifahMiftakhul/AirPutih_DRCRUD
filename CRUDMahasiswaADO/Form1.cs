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

                // Validasi
                if (label1.Text == "")
                {
                    MessageBox.Show("NIM harus diisi");
                    label1.Focus();
                    return;
                }
                if (label2.Text == "")
                {
                    MessageBox.Show("Nama harus diisi");
                    label2.Focus();
                    return;
                }
                if (cmbJK.Text == "")
                {
                    MessageBox.Show("Jenis Kelamin harus dipilih");
                    cmbJK.Focus();
                    return;
                }
                if (label6.Text == "")
                {
                    MessageBox.Show("Kode Prodi harus diisi");
                    label6.Focus();
                    return;
                }

                string query = @"INSERT INTO Mahasiswa 
                    (NIM, Nama, JenisKelamin, TanggalLahir, Alamat, KodeProdi, TanggalDaftar) 
                    VALUES 
                    (@NIM, @Nama, @JK, @TanggalLahir, @Alamat, @KodeProdi, @TanggalDaftar)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NIM", label1.Text);
                cmd.Parameters.AddWithValue("@Nama", label2.Text);
                cmd.Parameters.AddWithValue("@JK", cmbJK.Text);
                cmd.Parameters.AddWithValue("@TanggalLahir", dtpTanggalLahir.Value.Date);
                cmd.Parameters.AddWithValue("@Alamat", label4.Text);
                cmd.Parameters.AddWithValue("@KodeProdi", label6.Text);
                cmd.Parameters.AddWithValue("@TanggalDaftar", DateTime.Now);

                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Data mahasiswa berhasil ditambahkan");
                    ClearForm();
                    btnLoad.PerformClick();
                }
                else
                {
                    MessageBox.Show("Data gagal ditambahkan");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        // Langkah 8 – Mengubah Data (event btnUpdate_Click)
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = @"UPDATE Mahasiswa 
                    SET Nama = @Nama, 
                        JenisKelamin = @JK, 
                        TanggalLahir = @TanggalLahir, 
                        Alamat = @Alamat, 
                        KodeProdi = @KodeProdi 
                    WHERE NIM = @NIM";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NIM", label1.Text);
                cmd.Parameters.AddWithValue("@Nama", label2.Text);
                cmd.Parameters.AddWithValue("@JK", cmbJK.Text);
                cmd.Parameters.AddWithValue("@TanggalLahir", dtpTanggalLahir.Value.Date);
                cmd.Parameters.AddWithValue("@Alamat", label4.Text);
                cmd.Parameters.AddWithValue("@KodeProdi", label6.Text);

                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Data berhasil diupdate");
                    ClearForm();
                    btnLoad.PerformClick();
                }
                else
                {
                    MessageBox.Show("Data tidak ditemukan");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        
