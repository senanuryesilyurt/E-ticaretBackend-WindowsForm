using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sena;
using eticaret;
namespace eticaret
{
    public partial class Form2 : Form
    {
        public string connect_string = @"Data Source=DESKTOP-68FQ1B6\SQLEXPRESS;Initial Catalog=eticaret;Integrated Security=True";
        public Form2()
        {
            InitializeComponent();
        }

        public ilan[] ilanlari_al() 
        {
            List<ilan> ilan = new List<ilan>();
            using (var db = new eticaretDataContext(connect_string))
            {
                var ilanlar = (from x in db.ilanlar select x).ToArray();
                foreach(var a in ilanlar)
                {
                    ilan.Add(new ilan
                    {
                        ilan_baslıgı = a.ilan_basligi,
                        ilan_icerigi = a.ilan_icerigi,
                        ilan_fiyati= a.ilan_fiyati,
                        ilan_no=a.ilan_no,
                        kategori=a.kategori,
                        oncelik=a.oncelik,
                        resim_yolu=a.resim_yolu

                    });
                }

            }
            return ilan.ToArray();
        }
        public bool urun_ekle(ilan _ilan)
        {
            using (var db = new eticaretDataContext(connect_string))
            {
                eticaret.ilanlar urun = new ilanlar();
                urun.ilan_basligi = _ilan.ilan_baslıgı;
                urun.ilan_icerigi = _ilan.ilan_icerigi;
                urun.ilan_fiyati = _ilan.ilan_fiyati;
                urun.ilan_no = _ilan.ilan_no;
                urun.oncelik = _ilan.oncelik;
                urun.resim_yolu = _ilan.resim_yolu;
                urun.kategori = _ilan.kategori;
                urun.kullanici_adi = _ilan.kullanici_adi;
                db.ilanlar.InsertOnSubmit(urun);
                db.SubmitChanges();
                return true;
            }
           // return false;
        }
   
        ilan[] _ilanlar;
        public uye l = new uye();

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = l.ad;     //login olan uye bılgılerı
            label2.Text = l.soyad;
            label3.Text = l.kullanici_adi;
            label4.Text = l.parola;
            label5.Text = l.e_posta;
            label6.Text = l.ceptel;
            label7.Text = l.uyelik_tipi;

            _ilanlar = ilanlari_al();

            comboBox1.Items.Add("ilan basligi");    //listelemede 
            comboBox1.Items.Add("ilan iceriği");
            comboBox1.Items.Add("kullanici adi");
            comboBox1.Items.Add("ilan fiyati");
            comboBox1.Items.Add("kategori");

          
            List<ilan> listele = new List<ilan>();
            using (var db = new eticaretDataContext(connect_string))
            {
                try
                {
                    var liste = (from x in db.ilanlar select x).ToArray();
                    foreach (var a in liste)
                    {
                        ListViewItem item = new ListViewItem(new string[]
                        {
                          a.ilan_no.ToString(),a.ilan_basligi,a.ilan_icerigi,a.kullanici_adi,a.oncelik.ToString(),a.kategori,a.ilan_fiyati,a.resim_yolu
                        });
                        listView1.Items.Add(item);
                    }
                }
                catch (Exception r)
                {
                    MessageBox.Show(r.ToString());
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool add = urun_ekle(new ilan
            {
                kullanici_adi = textBox1.Text,
                ilan_baslıgı = textBox2.Text,
                ilan_icerigi = textBox3.Text,
                kategori = textBox4.Text,
                ilan_fiyati = textBox5.Text,
                resim_yolu = textBox6.Text,
                oncelik = Convert.ToBoolean(textBox8.Text)
            });
            MessageBox.Show("Urun Eklendi.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ilan[] newilanlar;
            string filtre = comboBox1.Text;
            string ad = textBox7.Text;
            listView1.Items.Clear();
            
            if (filtre=="ilan basligi") { newilanlar = _ilanlar.Where(i => i.ilan_baslıgı == ad).ToArray(); }
            else if(filtre== "ilan iceriği") { newilanlar = _ilanlar.Where(i => i.ilan_icerigi == ad).ToArray(); }
            else if (filtre == "kullanici adi") { newilanlar = _ilanlar.Where(i => i.kullanici_adi == ad).ToArray(); }
            else if (filtre == "ilan fiyati") { newilanlar = _ilanlar.Where(i => i.ilan_fiyati == ad).ToArray(); }
            else if (filtre == "kategori") { newilanlar = _ilanlar.Where(i => i.kategori == ad).ToArray(); }
            else { newilanlar = _ilanlar; }

          //  newilanlar = newilanlar.OrderByDescending(a => a.ilan_no).ToArray();    //ilan numaralarına göre tersten sıralama yapıyor
          //  newilanlar = _ilanlar.Where(i => i.ilan_baslıgı == ad).ToArray();
            if (ad == "") { newilanlar = _ilanlar; newilanlar = newilanlar.OrderByDescending(a => a.ilan_no).ToArray(); }

          /*  foreach (var a in newilanlar)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                          a.ilan_no.ToString(),a.ilan_baslıgı,a.ilan_icerigi,a.kullanici_adi,a.oncelik.ToString(),a.kategori,a.ilan_fiyati,a.resim_yolu
                });
                listView1.Items.Add(item);
            }*/
            foreach (var a in newilanlar)
            {
                if (a.oncelik == true)
                {
                    ListViewItem item = new ListViewItem(new string[]
                        {
                            a.ilan_no.ToString(),a.ilan_baslıgı,a.ilan_icerigi,a.kullanici_adi,a.oncelik.ToString(),a.kategori,a.ilan_fiyati,a.resim_yolu
                        });
                    listView1.Items.Add(item);

                }
            }
            foreach (var a in newilanlar)
            {
                if (a.oncelik == false)
                {

                    ListViewItem item = new ListViewItem(new string[]
                    {
                          a.ilan_no.ToString(),a.ilan_baslıgı,a.ilan_icerigi,a.kullanici_adi,a.oncelik.ToString(),a.kategori,a.ilan_fiyati,a.resim_yolu
                    });
                    listView1.Items.Add(item);

                }

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            string item = listView1.SelectedItems[0].SubItems[0].Text.ToString();
           // label18.Text = item;
            string item1 = listView1.SelectedItems[0].SubItems[1].Text.ToString();
            string item2 = listView1.SelectedItems[0].SubItems[2].Text.ToString();
            string item3 = listView1.SelectedItems[0].SubItems[3].Text.ToString();
            string item4 = listView1.SelectedItems[0].SubItems[4].Text.ToString();
            string item5 = listView1.SelectedItems[0].SubItems[5].Text.ToString();
            string item6 = listView1.SelectedItems[0].SubItems[6].Text.ToString();
            string item7 = listView1.SelectedItems[0].SubItems[7].Text.ToString();
            

            form3.notice_number = Convert.ToInt32(item);
            form3.notice_header = item1;
            form3.notice_content = item2;
            form3.user_name = item3;
            form3.priority = Convert.ToBoolean(item4);
            form3.category = item5;
            form3.notice_price = Convert.ToInt32(item6);
            form3.notice_picture = item7;
            form3.Show();
        }
    }

 }

