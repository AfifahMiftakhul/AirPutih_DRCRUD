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
        // Langkah 9 – Menghapus Data (event btnDelete_Click)
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (label1.Text == "")
                {
                    MessageBox.Show("Pilih data yang akan dihapus");
                    return;
                }

                DialogResult confirm = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    string query = "DELETE FROM Mahasiswa WHERE NIM = @NIM";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@NIM", label1.Text);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Data berhasil dihapus");
                        ClearForm();
                        btnLoad.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Data tidak ditemukan");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        // Langkah 10 – Menampilkan Data ke Form Saat Baris Dipilih (event CellClick DataGridView)
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                label1.Text = row.Cells["NIM"].Value.ToString();
                label2.Text = row.Cells["Nama"].Value.ToString();
                cmbJK.Text = row.Cells["JenisKelamin"].Value.ToString();
                dtpTanggalLahir.Value = Convert.ToDateTime(row.Cells["TanggalLahir"].Value);
                label4.Text = row.Cells["Alamat"].Value.ToString();
                label6.Text = row.Cells["KodeProdi"].Value.ToString();
            }
        }

        // Langkah 11 – Method Clear Form
        private void ClearForm()
        {
            label1.Text = "";
            label2.Text = "";
            cmbJK.SelectedIndex = -1;
            dtpTanggalLahir.Value = DateTime.Now;
            label4.Text = "";
            label6.Text = "";
            label1.Focus();
        }
    }
}

