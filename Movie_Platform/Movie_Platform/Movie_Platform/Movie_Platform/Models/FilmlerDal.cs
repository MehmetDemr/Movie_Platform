using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesneProjeDeneme1
{
    public class FilmlerDal
    {
        private SqlConnection _connection; 

        public FilmlerDal(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
        public List<Filmler> YoneticiyeAitFilmleriGetir(int yoneticiId)
        {
            ConnectionControl();

            SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM Filmler WHERE YoneticiId = {yoneticiId}", _connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            return ConvertDataTableToList(dataTable);
        }

        private List<Filmler> ConvertDataTableToList(DataTable dataTable)
        {
            List<Filmler> filmler = new List<Filmler>();

            foreach (DataRow row in dataTable.Rows)
            {
                Filmler film = new Filmler
                {
                    // Film sınıfının özelliklerini, veritabanındaki sütun adlarına göre doldurun
                    Id = Convert.ToInt32(row["Id"]),
                    FilmAdi = row["FilmAdi"].ToString(),
                    Yonetmen = row["Yonetmen"].ToString(),
                    YayinYili = Convert.ToDateTime(row["YayinYili"]),
                    Tur = row["Tur"].ToString(),
                    YoneticiId = Convert.ToInt32(row["YoneticiId"])
                    // Diğer özellikleri de benzer şekilde doldurun
                };
                filmler.Add(film);
            }

            return filmler;
        }
        // DataGridView'den verileri çek
        public DataTable FilmleriAl()
        {
            ConnectionControl();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Filmler", _connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            return dataTable;
        }
        public void FilmSil(int filmId)
        {
            ConnectionControl();

            SqlCommand cmd = new SqlCommand("DELETE FROM Filmler WHERE Id = @FilmId", _connection);
            cmd.Parameters.AddWithValue("@FilmId", filmId);

            cmd.ExecuteNonQuery();
        }
    }
}


