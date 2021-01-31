using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using eticaret;
using sena;
using System.IO;

namespace eticaret
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string connect_string = @"Data Source=DESKTOP-68FQ1B6\SQLEXPRESS;Initial Catalog=eticaret;Integrated Security=True";
        public bool register(uye bilgiler)
        {
            using (var db = new eticaretDataContext(connect_string))
            {
                try
                {
                    eticaret.UYELER kayit = new eticaret.UYELER();
                    kayit.ad = bilgiler.ad;
                    kayit.soyad = bilgiler.soyad;
                    kayit.kullanici_adi = bilgiler.kullanici_adi;
                    kayit.parola = bilgiler.parola;
                    kayit.ceptel = bilgiler.ceptel;
                    kayit.e_posta = bilgiler.e_posta;
                    kayit.uyelik_tipi = bilgiler.uyelik_tipi;
                    db.UYELER.InsertOnSubmit(kayit);
                    db.SubmitChanges();
                    return true;
                }
                catch(Exception e)
                {
                    return false;
                }
            }
        }
        public bool eposta_formati(string eposta)
        {
            int at_indis = eposta.IndexOf("@");
            int com_indis = eposta.IndexOf(".com");
            textBox6.Text = at_indis.ToString();
            textBox7.Text = com_indis.ToString();
            if (at_indis != -1)
            {
                string[] parcala = eposta.Split('@');
                if (parcala[1].IndexOf(".com") != -1)
                {
                    int a = com_indis - at_indis;
                    if (a >= 5)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else { return false; }  
            }
            else { return false; }
        }
     
        public static uye login_user;

        public string ad="";
        public string soyad = "";
        public string kullanici_adi = "";
        public string parola = "";
        public string e_posta = "";
        public string ceptel = "";
        public string uyelik_tipi = "";

        public uye login(string kullanici_adi, string sifre)
        {
            uye uye = new uye();
            try
            {
                using (var sql = new eticaretDataContext(connect_string))
                {
                    var _login = (from x in sql.UYELER where x.kullanici_adi == kullanici_adi && x.parola == sifre select x).FirstOrDefault();
                    if (_login!=null)
                    {
                        uye.kullanici_adi = _login.kullanici_adi;
                        uye.parola = _login.parola;
                        uye.ad = _login.ad;
                        uye.soyad = _login.soyad;
                        uye.e_posta = _login.e_posta;
                        uye.ceptel = _login.ceptel;
                        uye.uyelik_tipi = _login.uyelik_tipi;
                        
                    }
                    if(_login == null)
                    {
                        MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı");
                        return null;
                    }
                }
            }
            catch (Exception v) { MessageBox.Show(v.Message); }
            return uye;
        }

        int count=0,max_yanlis_sayisi,waiting_time;
        string uyari;

        private void Form1_Load(object sender, EventArgs e)
        {
            //Form5 f5 = new Form5();
            //f5.Show();
            //this.Hide();
            try
            {
                groupBox2.Visible = true;
                groupBox1.Visible = false;
                this.Size = new Size(409, 453);
                groupBox2.Location = new Point(21, 12);

                using (var db = new eticaretDataContext(connect_string))
                {
                    var hatali_giris_sayisi = (from x in db.Ayarlar where x.Ayar_Baslıgı == "hatali_giris_sayisi" select x.Ayar_Icerigi).FirstOrDefault();
                    var bekleme_suresi = (from a in db.Ayarlar where a.Ayar_Baslıgı == "hatali_giris_bekleme" select a.Ayar_Icerigi).FirstOrDefault();
                    var hata_mesaji = (from c in db.Ayarlar where c.Ayar_Baslıgı == "hatali_giris_uyari" select c.Ayar_Icerigi).FirstOrDefault();

                    max_yanlis_sayisi = Convert.ToInt32(hatali_giris_sayisi);
                    waiting_time = Convert.ToInt32(bekleme_suresi);
                    uyari = hata_mesaji.ToString();
                }
            string dosya_yolu = "eticaret.txt";
            FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
            StreamReader sw = new StreamReader(fs);
            string metin = sw.ReadLine();
            if (metin != null)
            {
                string[] parcala = metin.Split('*');
                int ayrac = metin.IndexOf("*");
                textBox8.Text = parcala[0];
                textBox9.Text = parcala[1];
                login_user = login(textBox8.Text, textBox9.Text);
                
            }
            sw.Close();
            fs.Close();
        }
            catch (Exception f) { }


            //try
            //{
            //    if (login_user.ad == null)
            //    {
            //        MessageBox.Show("giriş yapılamadı");

            //    }
            //    else
            //    {
            if (login_user != null) { 
                this.Hide();
                Form4 f4 = new Form4();
                f4.l = login_user;
                f4.Show();
            }
            
            //    }
            //}
            //catch (Exception f) { }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)   
        {
            if (checkBox1.Checked)
                textBox4.PasswordChar = '\0';
            else
                textBox4.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (eposta_formati(e_posta) == true && textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox6.Text != "" && textBox7.Text != "")
            {
   
                register(new uye
                {
                    ad = textBox1.Text,
                    soyad = textBox2.Text,
                    kullanici_adi = textBox3.Text,
                    parola = textBox4.Text,
                    ceptel = textBox5.Text,
                    e_posta = textBox6.Text,
                    uyelik_tipi = textBox7.Text
                });
                MessageBox.Show("kayıt yapıldı");
            }
         
            else if (eposta_formati(e_posta) == false && textBox5.Text.Length>0)
            {
                MessageBox.Show("Eposta Formatı Dogru Değil");
            }
            else
            {
                MessageBox.Show("Gerekli Alanları Doldurun");
            }
        }

        private void label10_Click_1(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            groupBox1.Visible = false;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                login_user = login(textBox8.Text, textBox9.Text);
                /*   Form2 f2 = new Form2();
                   f2.l = login_user;
                   f2.Show();
                   this.Hide();*/
               
                if(login_user!=null)                      
                {
                    Form4 f4 = new Form4();
                    f4.l = login_user;
                    f4.Show();
                    this.Hide();
                }
                else
                {
                    count++;
                    if (count == max_yanlis_sayisi)
                    {
                        MessageBox.Show(uyari);
                        using (var db = new eticaretDataContext(connect_string))
                        {
                            var simdi = DateTime.Now;
                            var block_time = simdi.AddMinutes(waiting_time);
                            while (DateTime.Now != block_time)
                            {
                                button2.Enabled = false;
                                button4.Enabled = false;
                            }
                        }
                    }
                }
            }
            catch(Exception a){ MessageBox.Show(a.Message); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            groupBox1.Visible = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)//beni hatırla
        {
            string dosya_yolu = "eticaret.txt";
            FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(textBox8.Text + "*");
            sw.Write(textBox9.Text);
            sw.Close();
            fs.Close();
        }

    }
}
