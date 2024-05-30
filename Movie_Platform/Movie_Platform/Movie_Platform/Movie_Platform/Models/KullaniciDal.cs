using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesneProjeDeneme1
{
    public class KullaniciDal
    {
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\mssqllocaldb;initial catalog =FilmLibrary; integrated security = true");
        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
        public List<Kullanici> KullanicilariGetir()
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Select * from Kullanicilar", _connection);

            SqlDataReader reader = command.ExecuteReader();

            List<Kullanici> kullanicilar = new List<Kullanici>();

            while (reader.Read())
            {
                Kullanici kullanici = new Kullanici
                {
                    Id = Convert.ToInt32(reader["k_id"]),
                    TCNo = reader["TCNo"].ToString(),
                    Ad = reader["Ad"].ToString(),
                    Soyad = reader["Soyad"].ToString(),
                    DogumTarihi = Convert.ToDateTime(reader["DogumTarihi"]),
                    Cinsiyet = reader["Cinsiyet"].ToString(),
                    Uyelik = reader["Uyelik"].ToString(),
                    KullaniciAdi = reader["KullaniciAdi"].ToString(),
                    Sifre = reader["Sifre"].ToString()
                };
                kullanicilar.Add(kullanici);
            }
            reader.Close();
            _connection.Close();
            return kullanicilar;
        }
        public void EkleKullanici(Kullanici kullanici)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Insert into Kullanicilar values(@tcno,@ad,@soyad,@dogumtarihi,@cinsiyet,@uyelik,@kAdi,@sifre)", _connection);
            command.Parameters.AddWithValue("@tcno", kullanici.TCNo);
            command.Parameters.AddWithValue("@ad", kullanici.Ad);
            command.Parameters.AddWithValue("@soyad", kullanici.Soyad);
            command.Parameters.AddWithValue("@dogumtarihi", kullanici.DogumTarihi);
            command.Parameters.AddWithValue("@cinsiyet", kullanici.Cinsiyet);
            command.Parameters.AddWithValue("@uyelik", kullanici.Uyelik);
            command.Parameters.AddWithValue("@kAdi", kullanici.KullaniciAdi);
            command.Parameters.AddWithValue("@sifre", kullanici.Sifre);
            command.ExecuteNonQuery();
            _connection.Close();
        }

        //Kullanici Giris islemi
        public bool KullaniciGiris(string username, string password)
        {
            ConnectionControl();
            // harf duyarlılığı için collate kullanımı
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Kullanicilar WHERE KullaniciAdi = @Username COLLATE SQL_Latin1_General_CP1_CS_AS AND Sifre = @Password COLLATE SQL_Latin1_General_CP1_CS_AS", _connection);

            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            int count = (int)command.ExecuteScalar();

            return count > 0;
        }


        public void KullaniciGuncelle(Kullanici kullanici)
        {
            using (SqlConnection connection = new SqlConnection(@"server=(localdb)\mssqllocaldb;initial catalog =FilmLibrary; integrated security = true"))
            {
                connection.Open();

                string sql = "UPDATE Kullanicilar SET TCNo = @TCNo, Ad = @Ad, Soyad = @Soyad WHERE KullaniciAdi = @KullaniciAdi";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@TCNo", kullanici.TCNo);
                    command.Parameters.AddWithValue("@Ad", kullanici.Ad);
                    command.Parameters.AddWithValue("@Soyad", kullanici.Soyad);
                    command.Parameters.AddWithValue("@KullaniciAdi", kullanici.KullaniciAdi);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void HesapSil(string kullaniciAdi)
        {
            ConnectionControl();

            // Filmplaylist tablosundan ilgili kayıtları sil
            using (SqlCommand commandFilmlistesi = new SqlCommand("DELETE FROM Filmplaylist WHERE KisiId IN (SELECT k_id FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi)", _connection))
            {
                commandFilmlistesi.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                commandFilmlistesi.ExecuteNonQuery();
            }

            // Kullanicilar tablosundan kullanıcıyı sil
            using (SqlCommand command = new SqlCommand("DELETE FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi", _connection))
            {
                command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                command.ExecuteNonQuery();
            }

            _connection.Close();
        }
        public string UyelikTuru(string kullaniciAdi)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("SELECT Uyelik FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi", _connection);
            command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);

            string uyelik = command.ExecuteScalar() as string;

            _connection.Close();
            return uyelik.ToLower(); // Küçük harfe dönüştürme
        }
    }
}
