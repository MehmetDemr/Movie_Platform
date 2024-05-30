using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesneProjeDeneme1
{
    public class FilmlistesiDal
    {
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\mssqllocaldb;initial catalog =FilmLibrary; integrated security = true");
        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
        public List<Filmlistesi> FilmleriGetir()
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Select * from Filmlistesi", _connection);

            SqlDataReader reader = command.ExecuteReader();

            List<Filmlistesi> filmler = new List<Filmlistesi>();

            while (reader.Read())
            {
                Filmlistesi film = new Filmlistesi
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    FilmAdi = reader["FilmAdi"].ToString(),
                    Yonetmen = reader["Yonetmen"].ToString(),
                    YayinYili = Convert.ToDateTime(reader["YayinYili"]),
                    Degerlendirme = Convert.ToInt32(reader["Degerlendirme"])
                };
                filmler.Add(film);
            }
            reader.Close();
            _connection.Close();
            return filmler;
        }
    }
}
