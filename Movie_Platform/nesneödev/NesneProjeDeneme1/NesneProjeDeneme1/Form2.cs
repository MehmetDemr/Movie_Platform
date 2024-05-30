using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace NesneProjeDeneme1
{
    public partial class Form2 : Form
    {
        private Form1 _form1;

        private string connectionString = @"server=(localdb)\mssqllocaldb;initial catalog=FilmLibrary;integrated security=true";

        public Form2(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;

        }
        public void SetData(List<Filmlistesi> filmListesi)
        {
            // DataGridView kontrolüne veriyi yükleyin
            dgvFilmOneriListe.DataSource = filmListesi;
        }

        private void btnProfilimeDon_Click(object sender, EventArgs e)
        {
            // Form1'i gösterin (yeniden açın)
            _form1.Show();

            // Form2'yi kapatın
            this.Close();
        }

        private void btnFilmListeGoruntule_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Filmler";

            // SqlConnection oluştur
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SqlDataAdapter oluştur
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    // DataTable oluştur
                    DataTable dataTable = new DataTable();

                    // Verileri DataTable'e doldur
                    adapter.Fill(dataTable);

                    // DataGridView'e DataTable'i bağla
                    dgvFilmOneriListe.DataSource = dataTable;
                    dgvFilmOneriListe.Columns["DegerlendirmePuani"].DefaultCellStyle.Format = "F2";
                    dgvFilmOneriListe.Columns["Id"].Visible = false;
                    dgvFilmOneriListe.Columns["YoneticiId"].Visible = false;
                }
            }
        }

        private void btnDegerlendirmeyeGore_Click(object sender, EventArgs e)
        {
            try
            {
                // DataGridView'deki verileri DataTable'e al
                DataTable dataTable = (DataTable)dgvFilmOneriListe.DataSource;

                // Veri kaynağı null değilse devam et
                if (dataTable != null)
                {
                    // DataTable'i "Değerlendirme" sütununa göre sırala
                    dataTable.DefaultView.Sort = "DegerlendirmePuani DESC"; // "DESC" büyükten küçüğe sıralama demektir

                    // Sıralanmış DataTable'i DataGridView'e bağla
                    dgvFilmOneriListe.DataSource = dataTable;
                }
                else
                {
                    // DataGridView'in veri kaynağı null ise buraya düşer
                    // İsterseniz bir mesaj gösterebilir veya başka bir işlem yapabilirsiniz
                    MessageBox.Show("DataGridView'in veri kaynağı bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajını ekrana yazdırabilirsiniz
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
        }


        private void btnCikisTarihi_Click(object sender, EventArgs e)
        {


            try
            {
                // DataGridView'deki verileri DataTable'e al
                DataTable dataTable = (DataTable)dgvFilmOneriListe.DataSource;

                // Veri kaynağı null değilse devam et
                if (dataTable != null)
                {
                    
                    // DataTable'i "Çıkış Tarihi" sütununa göre sırala
                    dataTable.DefaultView.Sort = "YayinYili ASC"; // "ASC" küçükten büyüğe sıralama demektir

                    // Sıralanmış DataTable'i DataGridView'e bağla
                    dgvFilmOneriListe.DataSource = dataTable;
                }
                else
                {
                    // DataGridView'in veri kaynağı null ise buraya düşer
                    // İsterseniz bir mesaj gösterebilir veya başka bir işlem yapabilirsiniz
                    MessageBox.Show("DataGridView'in veri kaynağı bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajını ekrana yazdırabilirsiniz
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }





        }

        private void btnYonetmeneGoreListe_Click(object sender, EventArgs e)
        {



            try
            {
                // DataGridView'deki verileri DataTable'e al
                DataTable dataTable = (DataTable)dgvFilmOneriListe.DataSource;

                // Veri kaynağı null değilse devam et
                if (dataTable != null)
                {

                    // DataTable'i "Yönetmen İsmi" sütununa göre sırala
                    dataTable.DefaultView.Sort = "Yonetmen ASC"; // "ASC" küçükten büyüğe sıralama demektir

                    // Sıralanmış DataTable'i DataGridView'e bağla
                    dgvFilmOneriListe.DataSource = dataTable;




                }
                else
                {
                    // DataGridView'in veri kaynağı null ise buraya düşer
                    // İsterseniz bir mesaj gösterebilir veya başka bir işlem yapabilirsiniz
                    MessageBox.Show("DataGridView'in veri kaynağı bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajını ekrana yazdırabilirsiniz
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }


        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //TODO
        }
    }
}
