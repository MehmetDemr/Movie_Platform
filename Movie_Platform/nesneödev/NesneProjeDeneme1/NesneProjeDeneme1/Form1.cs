using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NesneProjeDeneme1
{
    public partial class Form1 : Form
    {
        

        YoneticiDal _yoneticiDal = new YoneticiDal();
        KullaniciDal _kullaniciDal = new KullaniciDal();

        private Timer labelTimer;
        public FilmlerDal filmlerDal;
        private SqlConnection _connection;
        private const string ConnectionString = @"server=(localdb)\mssqllocaldb;initial catalog=FilmLibrary;integrated security=true";

        public Form1()
        {
            InitializeComponent();
            


            _connection = new SqlConnection(@"server=(localdb)\mssqllocaldb;initial catalog=FilmLibrary;integrated security=true");
            filmlerDal = new FilmlerDal(@"server=(localdb)\mssqllocaldb;initial catalog=FilmLibrary;integrated security=true");
            dgvKullaniciProfil.CellEndEdit += dgvKullaniciProfil_CellEndEdit;
            YenileDataGridView();

            //Timer oluşturun
            labelTimer = new Timer();
            labelTimer.Interval = 750;//1000 milisaniye=1 saniye
            labelTimer.Tick += LabelTimer_Tick;//Her bir zaman aşıldığında tetiklenen olay
            labelTimer.Start();
           
           
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // FilmleriAl metodunu kullanarak DataTable alınır.
            DataTable filmTable = filmlerDal.FilmleriAl();

            // DataGridView'e veriler atanır.
            dgvFilmler.DataSource = filmTable;

            // yonetici_id sütununu gizle
            dgvFilmler.Columns["YoneticiId"].Visible = false;
            dgvFilmler.Columns["Id"].Visible = false;
        }


        public bool filmeklendi = false;

         
       
        // SAYFALAR ARASI GEÇİŞ 
        private void btnKullaniciGiris_Click(object sender, EventArgs e)
        {
            tabGiris.SelectedIndex = 1;
        }
        private void btnYoneticiGiris_Click(object sender, EventArgs e)
        {
            tabGiris.SelectedIndex = 2;
        }
        private void btnKullaniciKayitSayfasinaGit_Click(object sender, EventArgs e)
        {
            tabGiris.SelectedIndex = 3;
        }
        private void btnGeriKullanıcıKayit_Click(object sender, EventArgs e)
        {
            tabGiris.SelectedIndex = 1;
        }
        private void btnGeriKullaniciGirisi_Click(object sender, EventArgs e)
        {
            tabGiris.SelectedIndex = 0;
        }
        private void btnGeriYoneticiGirisi_Click(object sender, EventArgs e)
        {
            tabGiris.SelectedIndex = 0;
        }
        private void btnYoneticiKayitSayfasinaGit_Click(object sender, EventArgs e)
        {
            tabGiris.SelectedIndex = 4;
        }
        private void btnGeriYoneticiKayit_Click(object sender, EventArgs e)
        {
            tabGiris.SelectedIndex = 2;
        }
        private void lblYoneticiProfilSayfasinaGit_Click(object sender, EventArgs e)
        {
            tabGiris.SelectedIndex = 8;
        }
        private void lblYoneticiAnasayfayaGit_Click(object sender, EventArgs e)
        {
            tabGiris.SelectedIndex = 7;
        }
        private void LBLYoneticiCikisYap_Click(object sender, EventArgs e)
        {
            tabGiris.SelectedIndex = 0;
            tbxYKullaniciAdiGiris.Clear();
            tbxYSifreGiris.Clear();
        }
        private void lblYoneticiCikisYap2_Click(object sender, EventArgs e)
        {
            tabGiris.SelectedIndex = 0;
            tbxYKullaniciAdiGiris.Clear();
            tbxYSifreGiris.Clear();
        }
        private void label30_Click(object sender, EventArgs e)
        {
            tabGiris.SelectedIndex = 0;
            tbxYKullaniciAdiGiris.Clear();
            tbxYSifreGiris.Clear();
        }
        private void lblKullaniciProfileGit_Click(object sender, EventArgs e)
        {
            tabGiris.SelectedIndex = 6;
        }

        private void lblKullaniciCikisYap2_Click(object sender, EventArgs e)
        {
            tabGiris.SelectedIndex = 0;
            tbxKKullaniciAdiGiris.Clear();
            tbxKKullaniciSifreGiris.Clear();
        }

        private void lblKullaniciAnasayfayaGit_Click(object sender, EventArgs e)
        {
            tabGiris.SelectedIndex = 5;
        }

        private void lblKullaniciCikisYap_Click(object sender, EventArgs e)
        {
            tabGiris.SelectedIndex = 0;
            tbxKKullaniciAdiGiris.Clear();
            tbxKKullaniciSifreGiris.Clear();
           
        }
        //Veritabanına yönetici ekleme
        private void btnYoneticiKayit_Click(object sender, EventArgs e)
        {
            // Alanlardaki boşlukları ve maksimum uzunluğu kontrol et
            if (string.IsNullOrEmpty(tbxYoneticiTC.Text) || string.IsNullOrEmpty(tbxYoneticiAd.Text) || string.IsNullOrEmpty(tbxYoneticiSoyad.Text) || cbxYoneticiCinsiyet.SelectedItem == null || string.IsNullOrEmpty(tbxYoneticiKullaniciAdi.Text) || string.IsNullOrEmpty(tbxYoneticiSifre.Text))
            {
                MessageBox.Show("Lütfen tüm bilgileri eksiksiz giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // TC kimlik numarası kontrolü
                if (tbxYoneticiTC.Text.Length != 11)
                {
                    MessageBox.Show("TC kimlik numarası 11 haneli olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Maksimum uzunluk kontrolü
                int maxUzunluk = 20; // maksimum uzunluğu 20 karakter olarak düşünelim

                if (tbxYoneticiAd.Text.Length > maxUzunluk || tbxYoneticiSoyad.Text.Length > maxUzunluk || tbxYoneticiKullaniciAdi.Text.Length > maxUzunluk || tbxYoneticiSifre.Text.Length > maxUzunluk)
                {
                    MessageBox.Show($"Her bir alan maksimum {maxUzunluk} karakter uzunluğunda olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kullanıcı adında boşluk kontrolü
                if (tbxYoneticiKullaniciAdi.Text.Contains(" "))
                {
                    MessageBox.Show("Kullanıcı adında boşluk karakteri olmamalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Şifre kontrolü
                if (string.IsNullOrWhiteSpace(tbxYoneticiSifre.Text) || !tbxYoneticiSifre.Text.Any(char.IsUpper) || !tbxYoneticiSifre.Text.Any(char.IsDigit))
                {
                    MessageBox.Show("Şifre boş olmamalı, en az bir büyük harf ve bir rakam içermelidir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Diğer bilgileri ekleme işlemi
                _yoneticiDal.EkleYonetici(new Yonetici
                {
                    TCNo = tbxYoneticiTC.Text,
                    Ad = tbxYoneticiAd.Text,
                    Soyad = tbxYoneticiSoyad.Text,
                    DogumTarihi = dtpYoneticiDTarih.Value,
                    Cinsiyet = cbxYoneticiCinsiyet.SelectedItem.ToString(),
                    KullaniciAdi = tbxYoneticiKullaniciAdi.Text,
                    Sifre = tbxYoneticiSifre.Text
                });

                MessageBox.Show("Başarıyla kayıt olundu!!");

                // Kutucukları temizle
                tbxYoneticiTC.Clear();
                tbxYoneticiAd.Clear();
                tbxYoneticiSoyad.Clear();
                dtpYoneticiDTarih.Value = DateTime.Now;
                cbxYoneticiCinsiyet.SelectedIndex = -1;
                tbxYoneticiKullaniciAdi.Clear();
                tbxYoneticiSifre.Clear();
            }
        }

        private void btnKullaniciKayit_Click(object sender, EventArgs e)
        {
            // Alanlardaki boşlukları ve maksimum uzunluğu kontrol et
            if (string.IsNullOrEmpty(tbxKullaniciTC.Text) || string.IsNullOrEmpty(tbxKullaniciAd.Text) || string.IsNullOrEmpty(tbxKullaniciSoyad.Text) || cbxKullaniciCinsiyet.SelectedItem == null || string.IsNullOrEmpty(tbxKullaniciKayitKullaniciAdi.Text) || string.IsNullOrEmpty(tbxKullaniciKayitSifre.Text))
            {
                MessageBox.Show("Lütfen tüm bilgileri eksiksiz giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // TC kimlik numarası kontrolü
                if (tbxKullaniciTC.Text.Length != 11)
                {
                    MessageBox.Show("TC kimlik numarası 11 haneli olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Maksimum uzunluk kontrolü
                int maxUzunluk = 20; // maksimum uzunluğu 20 karakter olarak düşünelim

                if (tbxKullaniciAd.Text.Length > maxUzunluk || tbxKullaniciSoyad.Text.Length > maxUzunluk || tbxKullaniciKayitKullaniciAdi.Text.Length > maxUzunluk || tbxKullaniciKayitSifre.Text.Length > maxUzunluk)
                {
                    MessageBox.Show($"Her bir alan maksimum {maxUzunluk} karakter uzunluğunda olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kullanıcı adında boşluk kontrolü
                if (tbxKullaniciKayitKullaniciAdi.Text.Contains(" "))
                {
                    MessageBox.Show("Kullanıcı adında boşluk karakteri olmamalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Şifre kontrolü
                if (string.IsNullOrWhiteSpace(tbxKullaniciKayitSifre.Text) || !tbxKullaniciKayitSifre.Text.Any(char.IsUpper) || !tbxKullaniciKayitSifre.Text.Any(char.IsDigit))
                {
                    MessageBox.Show("Şifre boş olmamalı, en az bir büyük harf ve bir rakam içermelidir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Diğer bilgileri ekleme işlemi
                _kullaniciDal.EkleKullanici(new Kullanici
                {
                    TCNo = tbxKullaniciTC.Text,
                    Ad = tbxKullaniciAd.Text,
                    Soyad = tbxKullaniciSoyad.Text,
                    DogumTarihi = dtpKullaniciDTarih.Value,
                    Cinsiyet = cbxKullaniciCinsiyet.SelectedItem.ToString(),
                    Uyelik = cbxUyelik.SelectedItem.ToString().ToLower(),
                    KullaniciAdi = tbxKullaniciKayitKullaniciAdi.Text,
                    Sifre = tbxKullaniciKayitSifre.Text
                });

                MessageBox.Show("Başarıyla kayıt olundu!!");

                // Kutucukları temizle
                tbxKullaniciTC.Clear();
                tbxKullaniciAd.Clear();
                tbxKullaniciSoyad.Clear();
                dtpKullaniciDTarih.Value = DateTime.Now;
                cbxUyelik.SelectedIndex = -1;
                cbxKullaniciCinsiyet.SelectedIndex = -1;
                tbxKullaniciKayitKullaniciAdi.Clear();
                tbxKullaniciKayitSifre.Clear();
            }
        }
        private void UyelikLabeliniGuncelle(string kullaniciAdi)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                // Kullanicilar tablosunda arama yap
                string selectUserQuery = "SELECT Uyelik FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi";

                using (SqlCommand selectUserCommand = new SqlCommand(selectUserQuery, connection))
                {
                    selectUserCommand.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);

                    object uyelikValue = selectUserCommand.ExecuteScalar();

                    if (uyelikValue != null)
                    {
                        string uyelik = uyelikValue.ToString();
                        lblTurBelirleme.Text = uyelik.ToUpper();
                    }
                    else
                    {
                        lblTurBelirleme.Text = "Bilinmeyen";
                    }
                }
            }
        }
        // kullanıcı giriş yapma işlemi
        private void btnKullaniciGirisYap_Click(object sender, EventArgs e)
        {
            string username = tbxKKullaniciAdiGiris.Text;
            string password = tbxKKullaniciSifreGiris.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kullanıcı girişi kontrolü
            if (_kullaniciDal.KullaniciGiris(username, password))
            {
                MessageBox.Show("Giriş başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabGiris.SelectedIndex = 5;
                UyelikLabeliniGuncelle(username);

                if(filmeklendi)
                {
                    notifyIcon1.ShowBalloonTip(100);
                   

                    filmeklendi = false;    

                }


                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Kullanıcı adını al
                    string kullaniciAdi = tbxKKullaniciAdiGiris.Text;

                    // Kullanıcı adına göre Kullanicilar tablosundan k_id'yi çek
                    string selectUserIdQuery = "SELECT k_id FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi";

                    using (SqlCommand selectUserIdCommand = new SqlCommand(selectUserIdQuery, connection))
                    {
                        selectUserIdCommand.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);

                        object result = selectUserIdCommand.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int kisiId))
                        {
                            // Sadece belirli KisiId'ye ait Filmplaylist verilerini çek
                            string selectQuery = "SELECT * FROM Filmplaylist WHERE KisiId = @KisiId";
                            using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, connection))
                            {
                                adapter.SelectCommand.Parameters.AddWithValue("@KisiId", kisiId);

                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);


                                // DataGridView'e verileri bağla
                                dataGridView1.DataSource = dataTable;
                                dataGridView1.Columns["Degerlendirme"].DefaultCellStyle.Format = "F2";
                                dataGridView1.Columns["Id"].Visible = false;
                                dataGridView1.Columns["KisiId"].Visible = false;
                            }
                        }
                    }
                }





                // Kullanıcının uyelik türüne göre uygun sınıftan bir örnek oluştur
                TemelKullanici kullanici;
                if (_kullaniciDal.UyelikTuru(username) == "standart")
                {
                    kullanici = new StandartKullanici();
                    tbxKAEfilmAdi.Visible = true;
                    tbxKAEyorum.Visible = true;
                    tbxKAEdegerlendirme.Visible = false;
                    label24.Visible = true;
                    label25.Visible = true;
                    label26.Visible = false;
                    label36.Visible = false;
                    btnKAEgonder.Visible = true;
                    label39.Visible = true;
                }
                else if (_kullaniciDal.UyelikTuru(username) == "premium")
                {
                    kullanici = new PremiumKullanici();
                    tbxKAEfilmAdi.Visible = true;
                    tbxKAEyorum.Visible = true;
                    tbxKAEdegerlendirme.Visible = true;
                    label24.Visible = true;
                    label25.Visible = true;
                    label26.Visible = true;
                    label36.Visible = true;
                    btnKAEgonder.Visible = true;
                    label39.Visible = false;
                }
                else
                {
                    MessageBox.Show("Bilinmeyen üyelik türü.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kullanıcı bilgilerini al ve göster
                KullaniciBilgileri bilgiler = kullanici.HesaplaVeGoster();
                MessageBox.Show($"Ücret: {bilgiler.Ucret:C} TL\nDeneme Süresi: {bilgiler.DenemeSuresi}");
            }
            else
            {
                MessageBox.Show("Geçersiz kullanıcı adı veya şifre.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            KullaniciGirisYapti(username);
        }
        // Sütunları DataGridView'e ekleyin (örneğin, formun başlangıcında yapabilirsiniz)
        private void KullaniciDataGridViewSutunlariniOlustur()
        {
            dgvKullaniciProfil.ColumnCount = 2; // Toplamda iki sütun ekleyeceğiz, birincisi etiket, ikincisi değer

            // Etiket sütunu
            dgvKullaniciProfil.Columns[0].Name = "Property";
            dgvKullaniciProfil.Columns[0].HeaderText = "Özellik";

            // Değer sütunu
            dgvKullaniciProfil.Columns[1].Name = "Value";
            dgvKullaniciProfil.Columns[1].HeaderText = "Değer";
        }
        // Kullanıcı giriş yaptıktan sonra bu metot çağrılabilir
        private void KullaniciGirisYapti(string kullaniciAdi)
        {


            dgvKullaniciProfil.Rows.Clear();
            dgvKullaniciProfil.Columns.Clear();
            

            // DataGridView sütunlarını oluştur
            KullaniciDataGridViewSutunlariniOlustur();

            // Kullanıcı adını kullanarak kullanıcı bilgilerini veritabanından al
            Kullanici kullanici = _kullaniciDal.KullanicilariGetir().FirstOrDefault(k => k.KullaniciAdi == kullaniciAdi);

            if (kullanici != null)
            {


               
                // DataGridView'e satırları ekle

                dgvKullaniciProfil.Rows.Add("TC No", kullanici.TCNo);
                dgvKullaniciProfil.Rows.Add("Ad", kullanici.Ad);
                dgvKullaniciProfil.Rows.Add("Soyad", kullanici.Soyad);
                dgvKullaniciProfil.Rows.Add("Doğum Tarihi", kullanici.DogumTarihi.ToShortDateString());
                dgvKullaniciProfil.Rows.Add("Cinsiyet", kullanici.Cinsiyet);
                dgvKullaniciProfil.Rows.Add("Üyelik Türü", kullanici.Uyelik);
            }
            else
            {
                MessageBox.Show("Kullanıcı bilgileri getirilemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Yönetici giriş yapma işlemi
        private void btnYoneticiGirisYap_Click(object sender, EventArgs e)
        {
            string username = tbxYKullaniciAdiGiris.Text;
            string password = tbxYSifreGiris.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_yoneticiDal.YoneticiGiris(username, password))
            {
                MessageBox.Show("Giriş başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabGiris.SelectedIndex = 7;
                YoneticiGirisYapti(username);
            }
            else
            {
                MessageBox.Show("Geçersiz kullanıcı adı veya şifre.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void YoneticiDataGridViewSutunlariniOlustur()
        {
            dgvYoneticiProfil.ColumnCount = 2; // Toplamda iki sütun ekleyeceğiz, birincisi etiket, ikincisi değer

            // Etiket sütunu
            dgvYoneticiProfil.Columns[0].Name = "Property";
            dgvYoneticiProfil.Columns[0].HeaderText = "Özellik";

            // Değer sütunu
            dgvYoneticiProfil.Columns[1].Name = "Value";
            dgvYoneticiProfil.Columns[1].HeaderText = "Değer";
        }
        private void YoneticiGirisYapti(string kullaniciAdi)
        {
            // Yönetici bilgilerini al
            Yonetici yonetici = _yoneticiDal.YoneticileriGetir().FirstOrDefault(y => y.KullaniciAdi == kullaniciAdi);

            // DataGridView'i temizle
            dgvYoneticiProfil.Rows.Clear();
            dgvYoneticiProfil.Columns.Clear();

            // Sütunları oluştur
            YoneticiDataGridViewSutunlariniOlustur();

            // Verileri ekle
            dgvYoneticiProfil.Rows.Add("TC No", yonetici.TCNo);
            dgvYoneticiProfil.Rows.Add("Ad", yonetici.Ad);
            dgvYoneticiProfil.Rows.Add("Soyad", yonetici.Soyad);
            dgvYoneticiProfil.Rows.Add("Doğum Tarihi", yonetici.DogumTarihi.ToShortDateString());
            dgvYoneticiProfil.Rows.Add("Cinsiyet", yonetici.Cinsiyet);

            // Yöneticinin kendi filmlerini çek ve DataGridView'e ekle
            DataTable yoneticiFilmleriDataTable = _yoneticiDal.YoneticiyeAitFilmleriGetirDataTable(yonetici.Id);
            // DataGridView'e veri kaynağını atama
            dgvKendiFilmi.DataSource = yoneticiFilmleriDataTable;

            // Yonetici Id'sini Label'a yazdır
            lblYoneticiid.Text = "Yönetici ID: " + yonetici.Id.ToString();
            dgvKendiFilmi.Columns["id"].Visible = false;

        }
        private void YenileDataGridView()
        {
            // Filmleri veritabanından çek
            DataTable filmTable = filmlerDal.FilmleriAl();

            // DataGridView'in veri kaynağını güncelle
            dgvFilmler.DataSource = filmTable;
        }

        private void btnFilmEkle_Click_2(object sender, EventArgs e)
        {
            try
            {
                _connection = new SqlConnection(@"server=(localdb)\mssqllocaldb;initial catalog=FilmLibrary;integrated security=true");
                // DataGridView'deki değişiklikleri al
                DataTable filmTable = (DataTable)dgvKendiFilmi.DataSource;

                // SqlDataAdapter ve SqlCommandBuilder oluştur
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Filmler", _connection);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

                // Veritabanındaki değişiklikleri uygula
                adapter.Update(filmTable);

                // DataGridView'i yenile
                YenileDataGridView();
                YoneticiGirisYapti(tbxYKullaniciAdiGiris.Text);

                MessageBox.Show("Değişikler başarıyla kaydedildi!!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                filmeklendi = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnFilmSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvKendiFilmi.SelectedRows.Count > 0)
                {
                    // Seçilen satırı al
                    DataGridViewRow selectedRow = dgvKendiFilmi.SelectedRows[0];

                    // Seçilen film ID'sini al
                    int filmId = Convert.ToInt32(selectedRow.Cells["id"].Value);

                    // Veritabanından filmi sil
                    filmlerDal.FilmSil(filmId);

                    // DataGridView'i yenile
                    YenileDataGridView();
                    YoneticiGirisYapti(tbxYKullaniciAdiGiris.Text);

                    MessageBox.Show("Film başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Lütfen bir film seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvYoneticiProfil_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Düzenlenen hücrenin satır ve sütun indekslerini al
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;

            // Eğer düzenlenen hücre Value sütunundaysa
            if (columnIndex == 1)
            {
                // DataGridView'deki güncellenen değeri al
                string updatedValue = dgvYoneticiProfil.Rows[rowIndex].Cells[columnIndex].Value.ToString();

                // Giriş yapan yönetici bilgilerini al
                string kullaniciAdi = tbxYKullaniciAdiGiris.Text; // Giriş yapan yöneticinin adını burada belirtmelisiniz.
                Yonetici yonetici = _yoneticiDal.YoneticileriGetir().FirstOrDefault(y => y.KullaniciAdi == kullaniciAdi);

                // Güncelleme işlemini yap
                if (yonetici != null)
                {
                    // Hangi özelliği güncellemek istiyorsanız onu seçebilirsiniz
                    switch (dgvYoneticiProfil.Rows[rowIndex].Cells[0].Value.ToString())
                    {
                        case "TC No":
                            yonetici.TCNo = updatedValue;
                            break;
                        case "Ad":
                            yonetici.Ad = updatedValue;
                            break;
                        case "Soyad":
                            yonetici.Soyad = updatedValue;
                            break;
                            // Diğer özellikler için case'leri ekleyebilirsiniz.
                    }

                    // Veritabanında güncelleme işlemini gerçekleştir
                    _yoneticiDal.YoneticiGuncelle(yonetici);
                }
                else
                {
                    MessageBox.Show("Yönetici bilgileri getirilemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnYoneticiHesapSil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Hesabınızı silmek istediğinizden emin misiniz?", "Hesap Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string kullaniciAdi = tbxYKullaniciAdiGiris.Text; // Kullanıcı adını doğrulamak için
                string sifre = tbxYSifreGiris.Text; // Şifreyi doğrulamak için

                // Kullanıcının doğrulanıp doğrulanmadığını kontrol et
                if (_yoneticiDal.YoneticiGiris(kullaniciAdi, sifre))
                {
                    // Hesabı silme işlemini gerçekleştir
                    _yoneticiDal.HesapSil(kullaniciAdi);

                    MessageBox.Show("Hesap başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    tbxYKullaniciAdiGiris.Clear();
                    tbxYSifreGiris.Clear();
                    tabGiris.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı. Hesap silme işlemi başarısız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void dgvKullaniciProfil_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;

            if (columnIndex == 1) // Check if the edited column is the "Value" column
            {
                string updatedValue = dgvKullaniciProfil.Rows[rowIndex].Cells[columnIndex].Value.ToString();

                string kullaniciAdi = tbxKKullaniciAdiGiris.Text;
                Kullanici kullanici = _kullaniciDal.KullanicilariGetir().FirstOrDefault(y => y.KullaniciAdi == kullaniciAdi);


                if (kullanici != null)
                {
                    switch (dgvKullaniciProfil.Rows[rowIndex].Cells[0].Value.ToString())
                    {
                        case "TC No":
                            kullanici.TCNo = updatedValue;
                            break;
                        case "Ad":
                            kullanici.Ad = updatedValue;
                            break;
                        case "Soyad":
                            kullanici.Soyad = updatedValue;
                            break;
                            // Add more cases for other properties if needed
                    }

                    _kullaniciDal.KullaniciGuncelle(kullanici);

                    MessageBox.Show("Kullanıcı bilgisi güncelleme başarılı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Kullanıcı bilgileri getirilemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Hesabınızı silmek istediğinizden emin misiniz?", "Hesap Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string kullaniciAdi = tbxKKullaniciAdiGiris.Text; // Kullanıcı adını doğrulamak için
                string sifre = tbxKKullaniciSifreGiris.Text; // Şifreyi doğrulamak için

                // Kullanıcının doğrulanıp doğrulanmadığını kontrol et
                if (_kullaniciDal.KullaniciGiris(kullaniciAdi, sifre))
                {
                    // Hesabı silme işlemini gerçekleştir
                    _kullaniciDal.HesapSil(kullaniciAdi);

                    MessageBox.Show("Hesap başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    tbxKKullaniciAdiGiris.Clear();
                    tbxKKullaniciSifreGiris.Clear();
                    tabGiris.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı. Hesap silme işlemi başarısız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string searchedFilm = SearchTextBox.Text;
            // Filmler tablosunda arama
            if (IsFilmInFilmler(searchedFilm))
            {
                // Eğer film bulunduysa Filmplaylist'e ekle
                AddFilmToFilmplaylist(searchedFilm);

                // DataGridView'i güncelle
                RefreshDataGridView();
            }
            else
            {
                MessageBox.Show("Aranan film bulunamadı.");
            }
        }
        private bool IsFilmInFilmler(string filmAdi)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Filmler WHERE FilmAdi = @FilmAdi";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FilmAdi", filmAdi);
                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }
        private void AddFilmToFilmplaylist(string filmAdi)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string kullaniciAdi = tbxKKullaniciAdiGiris.Text;

                // Kullanıcı adına göre Kullanicilar tablosundan k_id'yi çek
                string selectUserIdQuery = "SELECT k_id FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi";

                using (SqlCommand selectUserIdCommand = new SqlCommand(selectUserIdQuery, connection))
                {
                    selectUserIdCommand.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);

                    int kisiId;

                    object result = selectUserIdCommand.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out kisiId))
                    {
                        // Kontrol et: Eğer aynı film zaten varsa ekleme
                        if (!FilmAlreadyExists(kisiId, filmAdi))
                        {
                            // Filmler tablosunda arama yap
                            string selectQuery = "SELECT FilmAdi, Yonetmen, YayinYili, DegerlendirmePuani FROM Filmler WHERE FilmAdi = @FilmAdi";

                            using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                            {
                                selectCommand.Parameters.AddWithValue("@FilmAdi", filmAdi);

                                using (SqlDataReader reader = selectCommand.ExecuteReader())
                                {
                                    // Eğer film bulunduysa Filmplaylist'e ekle
                                    if (reader.Read())
                                    {
                                        // Diğer değerleri al
                                        string filmAdiValue = reader["FilmAdi"].ToString();
                                        string yonetmenValue = reader["Yonetmen"].ToString();
                                        DateTime yilValue = (DateTime)reader["YayinYili"];
                                        double degerlendirmeValue = Convert.ToDouble(reader["DegerlendirmePuani"]);

                                        // SqlDataReader nesnesini kapat
                                        reader.Close();

                                        // Filmplaylist'e ekle
                                        string insertQuery = "INSERT INTO Filmplaylist (FilmAdi, Yonetmen, Yil, Degerlendirme, KisiId) VALUES (@FilmAdi, @Yonetmen, @Yil, @Degerlendirme, @KisiId)";

                                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                                        {
                                            // Diğer değerleri kullanarak Filmplaylist'e ekle
                                            insertCommand.Parameters.AddWithValue("@FilmAdi", filmAdiValue);
                                            insertCommand.Parameters.AddWithValue("@Yonetmen", yonetmenValue);
                                            insertCommand.Parameters.AddWithValue("@Yil", yilValue);
                                            insertCommand.Parameters.AddWithValue("@Degerlendirme", degerlendirmeValue);
                                            insertCommand.Parameters.AddWithValue("@KisiId", kisiId);

                                            insertCommand.ExecuteNonQuery();
                                            MessageBox.Show("Film başarıyla eklendi.");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Aranan film bulunamadı.");
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bu film zaten ekli.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı bulunamadı.");
                    }
                }
                // NotifyIcon ile bildirim gönderiliyor
                string bildirimMesaji = $"Yeni film eklendi: {filmAdi}";
               
            }
        }
        private bool FilmAlreadyExists(int kisiId, string filmAdi)
        {
            // Kontrol için Filmplaylist tablosunda aynı film adına sahip kayıt olup olmadığını kontrol et
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string selectQuery = "SELECT COUNT(*) FROM Filmplaylist WHERE KisiId = @KisiId AND FilmAdi = @FilmAdi";

                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@KisiId", kisiId);
                    selectCommand.Parameters.AddWithValue("@FilmAdi", filmAdi);

                    int count = (int)selectCommand.ExecuteScalar();

                    return count > 0;
                }
            }
        }

        private void RefreshDataGridView()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                // Kullanıcı adını al
                string kullaniciAdi = tbxKKullaniciAdiGiris.Text;

                // Kullanıcı adına göre Kullanicilar tablosundan k_id'yi çek
                string selectUserIdQuery = "SELECT k_id FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi";

                using (SqlCommand selectUserIdCommand = new SqlCommand(selectUserIdQuery, connection))
                {
                    selectUserIdCommand.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);

                    object result = selectUserIdCommand.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int kisiId))
                    {
                        // Sadece belirli KisiId'ye ait Filmplaylist verilerini çek
                        string selectQuery = "SELECT * FROM Filmplaylist WHERE KisiId = @KisiId";
                        using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, connection))
                        {
                            adapter.SelectCommand.Parameters.AddWithValue("@KisiId", kisiId);

                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // DataGridView'e verileri bağla


                            dataGridView1.DataSource = dataTable;
                            dataGridView1.Columns["Degerlendirme"].DefaultCellStyle.Format = "F2";
                            dataGridView1.Columns["Id"].Visible = false;
                            dataGridView1.Columns["KisiId"].Visible = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı bulunamadı.");
                    }
                }
            }
        }
        private void RemoveFilmFromFilmplaylist(string filmAdi)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string kullaniciAdi = tbxKKullaniciAdiGiris.Text;

                // Kullanıcı adına göre Kullanicilar tablosundan k_id'yi çek
                string selectUserIdQuery = "SELECT k_id FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi";

                using (SqlCommand selectUserIdCommand = new SqlCommand(selectUserIdQuery, connection))
                {
                    selectUserIdCommand.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);

                    int kisiId;

                    object result = selectUserIdCommand.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out kisiId))
                    {
                        // Kontrol et: Eğer belirtilen film zaten varsa silme
                        if (FilmAlreadyExists(kisiId, filmAdi))
                        {
                            // Filmplaylist'ten filmi kaldır
                            string deleteQuery = "DELETE FROM Filmplaylist WHERE KisiId = @KisiId AND FilmAdi = @FilmAdi";

                            using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                            {
                                deleteCommand.Parameters.AddWithValue("@KisiId", kisiId);
                                deleteCommand.Parameters.AddWithValue("@FilmAdi", filmAdi);

                                deleteCommand.ExecuteNonQuery();
                                MessageBox.Show("Film başarıyla çıkarıldı.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bu film listede bulunmamaktadır.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı bulunamadı.");
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //Film çıkarma butonu
            string filmAdi = SearchTextBox.Text;
            RemoveFilmFromFilmplaylist(filmAdi);
            RefreshDataGridView();

        }
        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this);
            this.Hide();
            form2.Show();
        }

        
        private void Yenile()
        {

            string query = "SELECT * FROM Filmyorumlari";

            // SqlConnection oluştur
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // SqlDataAdapter oluştur
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    // DataTable oluştur
                    DataTable dataTable = new DataTable();

                    // Verileri DataTable'e doldur
                    adapter.Fill(dataTable);

                    // DataGridView'e DataTable'i bağla
                    dgvFilmYorumDegerlendirmeListe.DataSource = dataTable;
                    dgvFilmYorumDegerlendirmeListe.Columns["Id"].Visible = false;
                }
            }
        }
        private void Yorum(string filmAdi)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string kullaniciAdi = tbxKKullaniciAdiGiris.Text;
                string yorum = tbxKAEyorum.Text;

                // Kullanicilar tablosunda arama yap
                string selectUserQuery = "SELECT Uyelik FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi";

                using (SqlCommand selectUserCommand = new SqlCommand(selectUserQuery, connection))
                {
                    selectUserCommand.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);

                    object uyelikValue = selectUserCommand.ExecuteScalar();

                    if (uyelikValue != null && uyelikValue.ToString().ToLower() == "premium")
                    {

                        // Premium üye, değerlendirme yap
                        if (float.TryParse(tbxKAEdegerlendirme.Text, out float degerlendirme) && degerlendirme >= 0 && degerlendirme <= 10)
                        {
                            // Filmler tablosunda arama yap
                            string selectFilmQuery = "SELECT FilmAdi, Yonetmen, DegerlendirmePuani FROM Filmler WHERE FilmAdi = @FilmAdi";

                            using (SqlCommand selectFilmCommand = new SqlCommand(selectFilmQuery, connection))
                            {
                                selectFilmCommand.Parameters.AddWithValue("@FilmAdi", filmAdi);

                                using (SqlDataReader reader = selectFilmCommand.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        string filmAdiValue = reader["FilmAdi"].ToString();
                                        string yonetmenValue = reader["Yonetmen"].ToString();
                                        float eskiDegerlendirme = Convert.ToSingle(reader["DegerlendirmePuani"]);

                                        reader.Close();

                                        // Filmyorumlari tablosuna ekleme
                                        string insertQuery = "INSERT INTO Filmyorumlari (FilmAdi, Yonetmen, KullaniciAdi, Yorum, Degerlendirme) VALUES (@FilmAdi, @Yonetmen, @KullaniciAdi, @Yorum, @Degerlendirme)";

                                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                                        {
                                            // Diğer değerleri kullanarak Filmyorumlari'ye ekle
                                            insertCommand.Parameters.AddWithValue("@FilmAdi", filmAdiValue);
                                            insertCommand.Parameters.AddWithValue("@Yonetmen", yonetmenValue);
                                            insertCommand.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                                            insertCommand.Parameters.AddWithValue("@Yorum", yorum);
                                            insertCommand.Parameters.AddWithValue("@Degerlendirme", degerlendirme);

                                            insertCommand.ExecuteNonQuery();
                                            MessageBox.Show("Yorum başarıyla eklendi.");

                                            // Filmler tablosunu güncelle
                                            string updateFilmlerQuery = "UPDATE Filmler SET DegerlendirmePuani = (SELECT AVG(Degerlendirme) FROM Filmyorumlari WHERE FilmAdi = @FilmAdi) WHERE FilmAdi = @FilmAdi";

                                            using (SqlCommand updateFilmlerCommand = new SqlCommand(updateFilmlerQuery, connection))
                                            {
                                                updateFilmlerCommand.Parameters.AddWithValue("@FilmAdi", filmAdi);

                                                updateFilmlerCommand.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }
                        }


                        else
                        {
                            MessageBox.Show("Değerlendirme 0-10 arasında bir sayı olmalı.");
                        }
                    }
                    else
                    {
                        // Premium üye değilse sadece yorum ekle
                        // Filmler tablosunda arama yap
                        string selectFilmQuery = "SELECT FilmAdi, Yonetmen FROM Filmler WHERE FilmAdi = @FilmAdi";

                        using (SqlCommand selectFilmCommand = new SqlCommand(selectFilmQuery, connection))
                        {
                            selectFilmCommand.Parameters.AddWithValue("@FilmAdi", filmAdi);

                            using (SqlDataReader reader = selectFilmCommand.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string filmAdiValue = reader["FilmAdi"].ToString();
                                    string yonetmenValue = reader["Yonetmen"].ToString();
                                    reader.Close();

                                    // Filmyorumlari tablosuna ekleme
                                    string insertQuery = "INSERT INTO Filmyorumlari (FilmAdi, Yonetmen, KullaniciAdi, Yorum) VALUES (@FilmAdi, @Yonetmen, @KullaniciAdi, @Yorum)";

                                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                                    {
                                        // Diğer değerleri kullanarak Filmyorumlari'ye ekle
                                        insertCommand.Parameters.AddWithValue("@FilmAdi", filmAdiValue);
                                        insertCommand.Parameters.AddWithValue("@Yonetmen", yonetmenValue);
                                        insertCommand.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                                        insertCommand.Parameters.AddWithValue("@Yorum", yorum);

                                        insertCommand.ExecuteNonQuery();
                                        MessageBox.Show("Yorum başarıyla eklendi.");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Duzenleme(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // "Degerlendirme" sütununu biçimlendir
            if (e.Value != null && e.ColumnIndex == dgvFilmYorumDegerlendirmeListe.Columns["Degerlendirme"].Index)
            {
                double deger;
                if (double.TryParse(e.Value.ToString(), out deger))
                {
                    e.Value = deger.ToString("F2");
                    e.FormattingApplied = true;
                }
            }
        }
        private void btnKAEgonder_Click_1(object sender, EventArgs e)
        {
            string searchedFilm = tbxKAEfilmAdi.Text;

            if (IsFilmInFilmler(searchedFilm))
            {
                Yorum(searchedFilm);
                Yenile();
            }
        }
        private void btnKAEyorumlarıGor_Click_1(object sender, EventArgs e)
        {

            string query = "SELECT * FROM Filmyorumlari";

            // SqlConnection oluştur
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // SqlDataAdapter oluştur
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    // DataTable oluştur
                    DataTable dataTable = new DataTable();

                    // Verileri DataTable'e doldur
                    adapter.Fill(dataTable);

                    // DataGridView'e DataTable'i bağla
                    dgvFilmYorumDegerlendirmeListe.DataSource = dataTable;

                    dgvFilmYorumDegerlendirmeListe.CellFormatting += Duzenleme;

                    dgvFilmYorumDegerlendirmeListe.Columns["Id"].Visible = false;
                }
            }
        }
        private void LabelTimer_Tick(object sender, EventArgs e)
        {
            // Timer her aşıldığında çalışan olay
            lblKullaniciGiris.Visible = !lblKullaniciGiris.Visible; // Görünürlüğü tersine çevir
            lblYoneticiGiris.Visible = !lblYoneticiGiris.Visible;
            LBLtime.Visible = !LBLtime.Visible;
            label17.Visible = !label17.Visible;
        }

       
    }
}


