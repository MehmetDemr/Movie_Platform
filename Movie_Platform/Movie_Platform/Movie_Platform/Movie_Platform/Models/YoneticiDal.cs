using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace NesneProjeDeneme1
{
    public class YoneticiDal
    {
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\mssqllocaldb;initial catalog =FilmLibrary; integrated security = true");
        public void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
        public List<Yonetici> YoneticileriGetir()
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Select * from Yoneticiler", _connection);

            SqlDataReader reader =  command.ExecuteReader();

            List<Yonetici> yoneticiler = new List<Yonetici>();

            while (reader.Read())
            {
                Yonetici yonetici = new Yonetici
                {
                    Id = Convert.ToInt32(reader["y_id"]),
                    TCNo = reader["TCNo"].ToString(),
                    Ad = reader["Ad"].ToString(),
                    Soyad = reader["Soyad"].ToString(),
                    DogumTarihi = Convert.ToDateTime(reader["DogumTarihi"]),
                    Cinsiyet = reader["Cinsiyet"].ToString(),
                    KullaniciAdi = reader["KullaniciAdi"].ToString(),
                    Sifre = reader["Sifre"].ToString()
                };
                yoneticiler.Add(yonetici);
            }
            reader.Close();
            _connection.Close();
            return yoneticiler;    
        }
        // Her yöneticinin kendi filmini çekebilmek için ayrı bir işlem
        public Yonetici YoneticiBilgileriniGetir(int yoneticiId)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("SELECT * FROM Yoneticiler WHERE y_id = @YoneticiId", _connection);
            command.Parameters.AddWithValue("@YoneticiId", yoneticiId);

            SqlDataReader reader = command.ExecuteReader();

            Yonetici yonetici = null;

            if (reader.Read())
            {
                yonetici = new Yonetici
                {
                    Id = Convert.ToInt32(reader["y_id"]),
                    TCNo = reader["TCNo"].ToString(),
                    Ad = reader["Ad"].ToString(),
                    Soyad = reader["Soyad"].ToString(),
                    DogumTarihi = Convert.ToDateTime(reader["DogumTarihi"]),
                    Cinsiyet = reader["Cinsiyet"].ToString(),
                    KullaniciAdi = reader["KullaniciAdi"].ToString(),
                    Sifre = reader["Sifre"].ToString()
                };
            }

            reader.Close();
            _connection.Close();

            return yonetici;
        }
        public DataTable YoneticiyeAitFilmleriGetirDataTable(int yoneticiId)
        {
            ConnectionControl();

            SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM Filmler WHERE YoneticiId = {yoneticiId}", _connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            return dataTable;
        }
        public void EkleYonetici(Yonetici yonetici)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Insert into Yoneticiler values(@tcno,@ad,@soyad,@dogumtarihi,@cinsiyet,@kAdi,@sifre)", _connection);
            command.Parameters.AddWithValue("@tcno", yonetici.TCNo);
            command.Parameters.AddWithValue("@ad", yonetici.Ad);
            command.Parameters.AddWithValue("@soyad", yonetici.Soyad);
            command.Parameters.AddWithValue("@dogumtarihi", yonetici.DogumTarihi);
            command.Parameters.AddWithValue("@cinsiyet", yonetici.Cinsiyet);
            command.Parameters.AddWithValue("@kAdi", yonetici.KullaniciAdi);
            command.Parameters.AddWithValue("@sifre", yonetici.Sifre);
            command.ExecuteNonQuery();
            _connection.Close();
        }
        // YÖNETİCİ GİRİŞ İŞLEMİ
        public bool YoneticiGiris(string username, string password)
        {
            ConnectionControl();
            // harf duyarlılığı için collate kullanımı
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Yoneticiler WHERE KullaniciAdi = @Username COLLATE SQL_Latin1_General_CP1_CS_AS AND Sifre = @Password COLLATE SQL_Latin1_General_CP1_CS_AS", _connection);

            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            int count = (int)command.ExecuteScalar();

            return count > 0;
        }

        public void YoneticiGuncelle(Yonetici yonetici)
        {
            using (SqlConnection connection = new SqlConnection(@"server=(localdb)\mssqllocaldb;initial catalog =FilmLibrary; integrated security = true"))
            {
                connection.Open();

                string sql = "UPDATE Yoneticiler SET TCNo = @TCNo, Ad = @Ad, Soyad = @Soyad WHERE KullaniciAdi = @KullaniciAdi";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@TCNo", yonetici.TCNo);
                    command.Parameters.AddWithValue("@Ad", yonetici.Ad);
                    command.Parameters.AddWithValue("@Soyad", yonetici.Soyad);
                    command.Parameters.AddWithValue("@KullaniciAdi", yonetici.KullaniciAdi);

                    command.ExecuteNonQuery();
                }
            }
        }
        public int GetYoneticiIdByKullaniciAdi(string kullaniciAdi)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand("SELECT y_id FROM Yoneticiler WHERE KullaniciAdi = @KullaniciAdi", _connection);
            command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);

            int yoneticiId = 0; // Varsayılan değer

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                yoneticiId = Convert.ToInt32(reader["y_id"]);
            }

            reader.Close();
            _connection.Close();

            return yoneticiId;
        }


        public void HesapSil(string kullaniciAdi)
        {
            ConnectionControl();

            // YoneticiId'yi bul
            int yoneticiId = GetYoneticiIdByKullaniciAdi(kullaniciAdi);

            // YoneticiId'ye ait filmleri sil
            FilmleriSilByYoneticiId(yoneticiId);

            // Yoneticiyi sil
            SqlCommand command = new SqlCommand("DELETE FROM Yoneticiler WHERE KullaniciAdi = @KullaniciAdi", _connection);
       
            command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
            command.ExecuteNonQuery();

            _connection.Close();
        }
        public void FilmleriSilByYoneticiId(int yoneticiId)
        {
            ConnectionControl();

            using (SqlCommand command = new SqlCommand("DELETE FROM Filmler WHERE YoneticiId = @YoneticiId", _connection))
            {
                command.Parameters.AddWithValue("@YoneticiId", yoneticiId);
                command.ExecuteNonQuery();
            }
        }

    }
}
