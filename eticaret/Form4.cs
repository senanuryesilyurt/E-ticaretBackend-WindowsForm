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
using System.Data.SqlClient;
using System.IO;
using System.Data.Linq.SqlClient;

namespace eticaret
{
    public partial class Form4 : Form
    {
        public string connect_string = @"Data Source=DESKTOP-68FQ1B6\SQLEXPRESS;Initial Catalog=eticaret;Integrated Security=True";
        public Form4()
        {
            InitializeComponent();
        }
        int sayac = 0;
        string sec = "";
        public bool menu_durum = false;
        public int count = 165;
        public int count2 = 55;
        public uye l = new uye();

        class mesaj { 
        public int id { get; set; }
        public string[] parcala { get; set; }

        }
        int okunmamis_count = 0;
        private void Form4_Load(object sender, EventArgs e)
        {
            //login bilgilerini gösterir
            label7.Text = l.kullanici_adi;
            label2.Text = l.ad;
            label5.Text = l.soyad;
            label18.Text = l.e_posta;
            label19.Text = l.ceptel;
            label20.Text = l.uyelik_tipi;

            listView7.Visible = false;
            label34.Visible = false;
            listView6.Visible = false;
            button9.Visible = false;
            label33.Visible = false;
            label32.Visible = false;
            label6.Visible = false;
            label29.Visible = false;
            label30.Visible=false;
            label31.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox5.Visible = false;
            panel3.Visible = false;
            panel2.Visible = false;
            listView2.Visible = false;
            listView5.Visible = false;
            panel1.Location = new Point(55, 12);
            groupBox1.Location = new Point(-165, 12);
            menu_durum = false;
            listView9.Visible = false;
            listView10.Visible = false;
            //string[] alici;
            //string[] parcala;
            using(var db=new eticaretDataContext(connect_string))//mail gönderme
            {

                //listView8.Items.Clear();
                var okunanMesajlar = (from x in db.Okunan_mesajlar where x.kullanici_adi == l.kullanici_adi select x.mesaj_id).ToArray();
                var mesajlar = (from x in db.Mesajlar where !okunanMesajlar.Contains(x.Mesaj_Id) select x).ToArray();//okunmayan mesajlar
            
                var tt = (from x in db.Mesajlar where x.Alici == "1:" && !okunanMesajlar.Contains(x.Mesaj_Id) select x).ToArray();
                foreach (var h in tt)
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        h.Mesaj_Id.ToString(),h.Tarih.ToString(),h.Mesaj_Basligi,h.Gönderen
                    });
                    listView8.Items.Add(item);
                    okunmamis_count++;
                }
                foreach (var a in mesajlar)
                {
                    // MessageBox.Show(a.Mesaj_Id.ToString());
                    if (a.Alici == "1:")
                    {
                        /* MessageBox.Show(a.Alici);
                         ListViewItem item = new ListViewItem(new string[]
                             {
                                 a.Gönderen,a.Tarih.ToString(),a.Mesaj_Basligi,a.Mesaj_Icerigi
                             });
                         listView8.Items.Add(item);
                       
                          var mesaj = (from x in db.Mesajlar where x.Alici == "1: " select x).ToArray();
                          listView8.Items.Clear();
                          foreach (var s in mesaj)
                          {
                              ListViewItem item = new ListViewItem(new string[]
                              {
                                  s.Gönderen,s.Tarih.ToString(),s.Mesaj_Basligi,s.Mesaj_Icerigi
                              });
                              listView8.Items.Add(item);
                          }
                          */
                    }
                    else
                    {
                        var c = a.Alici.Split(':')[1].Split(',');
                        if (c.Where(i=>i == l.kullanici_adi).Count() > 0)
                        {
                            
                            ListViewItem item = new ListViewItem(new string[]
                            {
                                a.Mesaj_Id.ToString(),a.Tarih.ToString(),a.Mesaj_Basligi,a.Gönderen
                            });
                            listView8.Items.Add(item);
                            okunmamis_count++;
                           // var o = (from x in db.Okunan_mesajlar where x.kullanici_adi == l.kullanici_adi select x.mesaj_id).ToArray();
                           // var m = (from x in db.Mesajlar where !okunanMesajlar.Contains(x.Mesaj_Id) select x).Count();//okunmayan mesajlar
                           // label35.Text = listView8.Items.Count + " tane okunmamış mesajınız var";
                        }
                    }
                }
                label35.Text = okunmamis_count.ToString();
            }
        }

        public bool urun_ekle(ilan _ilan)
        {
            try
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
            }
            catch (Exception w) { MessageBox.Show("hata"); return false; }
        }

        public bool Ig_ekle(goruntulenme ilan2 )
        {
            try
            {
                using (var db = new eticaretDataContext(connect_string))
                {
                    eticaret.Goruntuleme ig = new eticaret.Goruntuleme();
                    ig.ilan_no = ilan2.ilan_no;
                    ig.goruntulenme_sayisi = ilan2.goruntulenme_sayisi;
                    db.Goruntuleme.InsertOnSubmit(ig);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception r) { MessageBox.Show(r.Message); return false; }  
        }

        /*    private void button1_Click(object sender, EventArgs e)
            {
                timer1.Enabled = true;
                /*   if (menu_durum) // if(menu_durum == true) 
                   { // if(!menu_durum) => if(menu_durum == false)
                       groupBox1.Location = new Point(-165, 12);
                       menu_durum = false;
                   }
                   else
                   {
                          groupBox1.Location = new Point(0, 12);
                       menu_durum = true;
                   }
                timer1.Enabled = true;
                if (!menu_durum && count==165  )
                {
                    count = 165;
                    panel1.Location = new Point(230, 12);
                    count2 = 55;
                    menu_durum = true;
                }
               if(menu_durum && count == 0)
                {
                    count = 0;
                      panel1.Location = new Point(55, 12);
                    count2 = 230;
                    menu_durum = false;
                }
            }
    */

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            if (!menu_durum && count == 165)
            {
                count = 165;
                //  panel1.Location = new Point(230, 12);
                count2 = 55;
                menu_durum = true;

            }
            if (menu_durum && count == 0)
            {
                count = 0;
                //  panel1.Location = new Point(55, 12);
                count2 = 230;
                menu_durum = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (menu_durum && count > 0)
            {
                count -= 5;
                int neg = (-1) * count;
                groupBox1.Location = new Point(neg, 12);
                pictureBox2.Location = new Point(6, 84);
                pictureBox1.Location = new Point(2, 155);
                pictureBox3.Location = new Point(2, 119);
            }
            if (!menu_durum && count < 165)
            {
                count += 5;
                int neg = (-1) * count;
                groupBox1.Location = new Point(neg, 12);
                pictureBox2.Location = new Point(183, 84);
                pictureBox3.Location = new Point(183, 119);
                pictureBox1.Location = new Point(183, 155);
            }
            if (menu_durum && count2 < 230)
            {
                count2 += 5;
                panel1.Location = new Point(count2, 12);
            }
            if (!menu_durum && count2 > 55)
            {
                count2 -= 5;
                panel1.Location = new Point(count2, 12);
            }
        }


        private void label1_Click(object sender, EventArgs e)//urun ekle
        {
            panel2.Visible = false;
            groupBox2.Location = new Point(6, 8);
            groupBox2.Visible = true;
            groupBox3.Visible = false;
            groupBox5.Visible = false;

        }

        private void label4_Click(object sender, EventArgs e)//listele
        {
            button9.Visible = true;
            panel2.Visible = false;
            groupBox3.Visible = true;
            groupBox2.Visible = false;
            groupBox5.Visible = false;
            comboBox4.Items.Clear();
            using (var db = new eticaretDataContext(connect_string))//sql deki kategorileri comboBox'a ekledik
            {
                var filtre = (from x in db.ilanlar select x.kategori).Distinct().ToArray();
                foreach (var a in filtre)
                {
                    comboBox4.Items.Add(a);
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)//sipariş ver
        {
            panel2.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox5.Location = new Point(3, 3);
            groupBox5.Visible = true;
            comboBox3.Items.Clear();
            using (var db = new eticaretDataContext(connect_string))
            {
                var filtre = (from x in db.ilanlar select x.kategori).Distinct().ToArray();
                foreach (var a in filtre)
                {
                    comboBox3.Items.Add(a);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)//urun ekle
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

        private void button2_Click(object sender, EventArgs e)//ilan listesi
        {
            button9.Visible = true;
            using (var db = new eticaretDataContext(connect_string))
            {
                var ilan_listesi = (from x in db.ilanlar where x.kategori == comboBox4.Text where x.ilan_basligi == textBox7.Text select x);
                foreach (var a in ilan_listesi)
                {
                    ListViewItem item = new ListViewItem(new string[]{
                        a.ilan_no.ToString(),a.ilan_basligi,a.ilan_icerigi,a.kullanici_adi,a.kategori,a.ilan_fiyati,a.oncelik.ToString(),a.resim_yolu
                    });
                    listView1.Items.Add(item);
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)//sipariş
        {
            string kategori = comboBox3.Text;
            string ilan_basligi = textBox10.Text;

            using (var db = new eticaretDataContext(connect_string))
            {
                var liste = (from a in db.UYELER where a.kullanici_adi == label7.Text select a).FirstOrDefault();
                var siparis = (from a in db.Siparis select a).ToArray();
                foreach (var x in siparis)
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        x.ilan_no.ToString(),x.kullanici_adi,x.siparis_id.ToString(),x.siparis_tarihi.ToString(),x.siparis_ücreti.ToString(),x.siparis_durumu
                    });
                    listView3.Items.Add(item);
                }
                //listView3.Items.Clear();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //panel3.Location = new Point(3, 321);
            panel2.Visible = true;
            panel3.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new eticaretDataContext(connect_string))
                {
                    var guncelle = (from a in db.UYELER where a.kullanici_adi == label7.Text select a).FirstOrDefault();
                    guncelle.ad = textBox11.Text;
                    guncelle.soyad = textBox12.Text;
                    guncelle.e_posta = textBox15.Text;
                    guncelle.parola = textBox16.Text;
                    guncelle.ceptel = textBox13.Text;
                    guncelle.uyelik_tipi = textBox14.Text;
                    db.SubmitChanges();
                }
                MessageBox.Show("Kayıt Güncellendi");
            }
            catch (Exception r)
            {
                MessageBox.Show("Güncelleme Yapılamadı");
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        { }
        private void label27_Click(object sender, EventArgs e)
        { }

        private void label28_Click(object sender, EventArgs e)//profil
        {
            groupBox3.Visible = false;
            button8.Visible = false;
            panel2.Visible = true;
            try
            {
                using (var db = new eticaretDataContext(connect_string))
                {
                    var order_list = (from x in db.Siparis where x.kullanici_adi == label7.Text select x).Take(10).ToArray();
                    foreach (var a in order_list)
                    {
                        ListViewItem item = new ListViewItem(new string[]
                        {
                            a.siparis_id.ToString(),a.ilan_no.ToString(),a.kullanici_adi,a.siparis_tarihi.ToString(),a.siparis_ücreti.ToString(),a.siparis_durumu
                        });
                        listView4.Items.Add(item);
                    }
                    //listView4.Items.Clear();
                }
            }
            catch (Exception r) { MessageBox.Show("hata"); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView4.SelectedItems[0].Remove();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listView7.Visible = true;
            label33.Visible = true;
            label33.Text = sayac.ToString();
            button7.Visible = false;
            button8.Visible = true;
            button8.Location=new Point(15, 262);
            panel2.Visible = true;
            listView4.Visible = false;
            listView2.Visible = true;
            listView2.Location = new Point(196,9);
          
            try
            {
                using (var db = new eticaretDataContext(connect_string))
                {
                    var liste = (from a in db.ilanlar where a.kullanici_adi == label7.Text select a).ToArray();
                    foreach (var a in liste)
                    {
                        ListViewItem item = new ListViewItem(new string[]
                        {
                        a.ilan_no.ToString(),a.ilan_basligi,a.ilan_icerigi,a.kullanici_adi,a.kategori,a.ilan_fiyati,a.oncelik.ToString(),a.resim_yolu
                        });
                        listView2.Items.Add(item);
                    }
                    //listView2.Items.Clear();
                    var liste3 = (from x in db.Goruntuleme select x).ToArray();
                    foreach (var a in liste3)
                    {
                        ListViewItem item1 = new ListViewItem(new string[]
                        {
                        a.ilan_no.ToString(),a.goruntulenme_sayisi.ToString()
                        });
                        listView7.Items.Add(item1);
                        
                    }
                }
            }
            catch(Exception r) { MessageBox.Show("hata"); }
        }

        private void button9_Click(object sender, EventArgs e)//detay
        {      
            listView1.Visible = false;
            listView6.Visible = true;
            label34.Visible = true;
            string ilan_no = listView1.SelectedItems[0].SubItems[0].Text.ToString();
            if (sec != null) { sayac = sayac + 1; }
            using (var db = new eticaretDataContext(connect_string))
            {
                var liste = (from x in db.ilanlar where x.ilan_no == Convert.ToInt32(ilan_no) select x).ToArray();
                foreach (var a in liste)
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        a.ilan_no.ToString(),a.ilan_basligi,a.ilan_icerigi,a.kullanici_adi,a.oncelik.ToString(),a.kategori,a.ilan_fiyati,a.resim_yolu
                    });
                    listView6.Items.Add(item);
                }
                var _ilan_no = (from x in db.Goruntuleme where x.ilan_no == Convert.ToInt32(ilan_no) select ilan_no).FirstOrDefault();
                if (_ilan_no == null)//ekle
                {
                    bool add = Ig_ekle(new goruntulenme
                    {
                        ilan_no = Convert.ToInt32(ilan_no),
                        goruntulenme_sayisi = sayac++
                    });
                   // MessageBox.Show("ıg_ekle");

                }
                else//guncelle
                {
                    var g = (from x in db.Goruntuleme where x.ilan_no == Convert.ToInt32(ilan_no) select x).FirstOrDefault();
                    int _gs =Convert.ToInt32( g.goruntulenme_sayisi);
                    _gs += 1;
                    g.ilan_no = Convert.ToInt32(ilan_no);
                    g.goruntulenme_sayisi = _gs;
                    db.SubmitChanges();   
                }
              
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //listView7.Visible = true;
            listView2.Visible = false;
            listView5.Visible = true;
            listView4.Visible = false;
            label6.Visible = true;
            label29.Visible = true;
            label30.Visible = true;
            label31.Visible = true;
            label32.Visible = true;
      
            try
            {
                using (var db = new eticaretDataContext(connect_string))
                {
                    string sec2= listView2.SelectedItems[0].SubItems[0].Text.ToString();     
                    var siparis = (from x in db.Siparis where x.ilan_no.ToString() == sec2 select x).ToArray();
                    int adet = siparis.Count();
                    label6.Text = "" + adet;
                    double toplam_kazanc = Convert.ToDouble(siparis.Sum(a => a.siparis_ücreti));
                    label29.Text = toplam_kazanc.ToString();
                    foreach(var a in siparis)
                    {
                        ListViewItem item = new ListViewItem(new string[]
                        {
                            a.siparis_id.ToString(),a.ilan_no.ToString(),a.kullanici_adi,a.siparis_tarihi.ToString(),a.siparis_ücreti.ToString(),a.siparis_durumu
                        });
                        listView5.Items.Add(item);
                    }
                    //listView5.Items.Clear();
                   
                }
            }catch(Exception r) { MessageBox.Show(r.Message); }
        }
       
        private void label32_Click(object sender, EventArgs e)//detay
        {
            button7.Visible = true;
            button8.Visible = false;
            listView5.Visible = false;
            listView2.Visible = true;
        }

        private void label34_Click(object sender, EventArgs e)//geri
        {
            listView1.Visible = true;
            listView6.Visible = false;
            button2.Visible = true;
        }

        //private void button10_Click(object sender, EventArgs e)
        //{
        //    using (var db = new eticaretDataContext(connect_string))
        //    {
        //        var a = (from i in db.ilanlar
        //                 join g in db.Goruntuleme on i.ilan_no equals g.ilan_no into birlesim
        //                 from c in birlesim.DefaultIfEmpty()
        //                 where c.goruntulenme_sayisi == 4
        //                 select new { i, c }).ToArray();

        //        var ilanno = (from x in db.Goruntuleme where x.goruntulenme_sayisi == 4 select x.ilan_no).FirstOrDefault();
        //        var ilanbilgileri = (from x in db.ilanlar where x.ilan_no == ilanno select x).FirstOrDefault();
        //        //var c = (from a in db.ilanlar where a.ilan_no == select a)
        //        foreach (var op in a)
        //        {
        //            MessageBox.Show(ilanbilgileri.kategori);
        //        }
        //    }
        //}
        private void button11_Click(object sender, EventArgs e)
        {
            string dosya = "eticaret.txt";
            File.Delete(dosya);
            Application.Exit();
            MessageBox.Show("Urun Eklendi.");
        }
        public bool mesaj_ekle(okunan_mesaj okunanMesaj)
        {
            using (var db = new eticaretDataContext(connect_string))
            {
                    eticaret.Okunan_mesajlar mesaj = new Okunan_mesajlar();
                    mesaj.kullanici_adi = okunanMesaj.kullanici_adi;
                    mesaj.mesaj_id = okunanMesaj.mesaj_id;
                    mesaj.tarih = DateTime.Now;
                    db.Okunan_mesajlar.InsertOnSubmit(mesaj);
                    db.SubmitChanges();
                    return true;
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {
            listView8.SelectedItems[0].Remove();
        }
        private void button13_Click(object sender, EventArgs e)//okundu
        {
            listView9.Visible = false;
            listView10.Visible = false;
            using (var db = new eticaretDataContext(connect_string))
            {
                mesaj_ekle(new okunan_mesaj
                {
                    kullanici_adi = l.kullanici_adi,
                    mesaj_id = (from x in db.Mesajlar where x.Mesaj_Id == Convert.ToInt32(listView8.SelectedItems[0].Text) select x.Mesaj_Id).FirstOrDefault(),
                }); 
                listView8.SelectedItems[0].Remove();
            }
        }
        private void button14_Click(object sender, EventArgs e)//gelen kutusu
        {
            listView9.Visible = true;
            listView8.Visible = false;
            listView10.Visible = false;
            using (var db = new eticaretDataContext(connect_string))
            {
                listView9.Items.Clear();
                //var okunanMesajlar = (from x in db.Okunan_mesajlar where x.kullanici_adi == l.kullanici_adi select x.mesaj_id).ToArray();
                var mesajlar = (from x in db.Mesajlar select x).ToArray();
                var tt = (from x in db.Mesajlar where x.Alici == "1:"  select x).ToArray();

                foreach (var h in tt)
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                           h.Mesaj_Id.ToString(),h.Tarih.ToString(),h.Mesaj_Basligi,h.Mesaj_Icerigi,h.Gönderen
                    });
                    listView9.Items.Add(item);
                }
                foreach (var a in mesajlar)
                {
                    if (a.Alici == "1:") { }
                    else
                    {
                        var c = a.Alici.Split(':')[1].Split(',');
                        if (c.Where(i => i == l.kullanici_adi).Count() > 0)
                        {
                            ListViewItem item = new ListViewItem(new string[]
                            {
                                    a.Mesaj_Id.ToString(),a.Tarih.ToString(),a.Mesaj_Basligi,a.Mesaj_Icerigi,a.Gönderen
                            });
                            listView9.Items.Add(item);
                        }
                    }
                }
            }
        }
        private void button15_Click(object sender, EventArgs e)//okunan mesajlar
        {
            listView8.Visible = false;
            listView9.Visible = true;
            listView10.Visible = false;
            using (var db = new eticaretDataContext(connect_string))
            {
                var okunanMesaj = (from x in db.Okunan_mesajlar where x.kullanici_adi == l.kullanici_adi select x.mesaj_id).ToArray();
                //var mesajlar = (from a in db.Mesajlar select a).ToArray();
                var mesaj = (from d in db.Mesajlar where okunanMesaj.Contains(d.Mesaj_Id) select d).ToArray();
                foreach (var s in mesaj)
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        s.Mesaj_Id.ToString(),s.Tarih.ToString(),s.Mesaj_Basligi,s.Mesaj_Icerigi,s.Gönderen
                    });
                    listView9.Items.Add(item);
                }
            }
        }

        private void listView8_DoubleClick(object sender, EventArgs e)
        {
            if (listView8.SelectedItems.Count == 1)
            {
                string mesaj_id = listView8.SelectedItems[0].Text;
                using(var db=new eticaretDataContext(connect_string))
                {
                    listView8.Visible = false;
                    listView9.Visible = false;
                    listView10.Visible = true;
                   
                    mesaj_ekle(new okunan_mesaj
                    {
                        kullanici_adi = l.kullanici_adi,
                        mesaj_id = (from x in db.Mesajlar where x.Mesaj_Id == Convert.ToInt32(listView8.SelectedItems[0].Text) select x.Mesaj_Id).FirstOrDefault(),
                    });
                    listView8.SelectedItems[0].Remove();
                    var mesajlar = (from b in db.Mesajlar where b.Mesaj_Id == Convert.ToInt32(mesaj_id) select b);
                    foreach(var a in mesajlar)
                    {
                        ListViewItem item = new ListViewItem(new string[]
                        {
                            a.Mesaj_Id.ToString(),a.Tarih.ToString(),a.Mesaj_Basligi,a.Mesaj_Icerigi,a.Gönderen
                        });
                        listView10.Items.Add(item);
                      
                    }
                   
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)//urun ekle
        {
            timer1.Enabled = true;
            if (!menu_durum && count == 165)
            {
                count = 165;
                //  panel1.Location = new Point(230, 12);
                count2 = 55;
                menu_durum = true;

            }
            if (menu_durum && count == 0)
            {
                count = 0;
                //  panel1.Location = new Point(55, 12);
                count2 = 230;
                menu_durum = false;
            }
            panel2.Visible = false;
            groupBox2.Location = new Point(6, 8);
            groupBox2.Visible = true;
            groupBox3.Visible = false;
            groupBox5.Visible = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)//siparis ver
        {
            timer1.Enabled = true;
            if (!menu_durum && count == 165)
            {
                count = 165;
                //  panel1.Location = new Point(230, 12);
                count2 = 55;
                menu_durum = true;

            }
            if (menu_durum && count == 0)
            {
                count = 0;
                //  panel1.Location = new Point(55, 12);
                count2 = 230;
                menu_durum = false;
            }
            panel2.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox5.Location = new Point(3, 3);
            groupBox5.Visible = true;
            comboBox3.Items.Clear();
            using (var db = new eticaretDataContext(connect_string))
            {
                var filtre = (from x in db.ilanlar select x.kategori).Distinct().ToArray();
                foreach (var a in filtre)
                {
                    comboBox3.Items.Add(a);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            if (!menu_durum && count == 165)
            {
                count = 165;
                //  panel1.Location = new Point(230, 12);
                count2 = 55;
                menu_durum = true;

            }
            if (menu_durum && count == 0)
            {
                count = 0;
                //  panel1.Location = new Point(55, 12);
                count2 = 230;
                menu_durum = false;
            }
            button9.Visible = true;
            panel2.Visible = false;
            groupBox3.Visible = true;
            groupBox2.Visible = false;
            groupBox5.Visible = false;
            comboBox4.Items.Clear();
            using (var db = new eticaretDataContext(connect_string))//sql deki kategorileri comboBox'a ekledik
            {
                var filtre = (from x in db.ilanlar select x.kategori).Distinct().ToArray();
                foreach (var a in filtre)
                {
                    comboBox4.Items.Add(a);
                }
            }
        }
    }
}