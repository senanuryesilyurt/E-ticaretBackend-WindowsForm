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
namespace eticaret
{
    public partial class Form5 : Form
    {
        public string connect_string = @"Data Source=DESKTOP-68FQ1B6\SQLEXPRESS;Initial Catalog=eticaret;Integrated Security=True";
        public Form5()
        {
            InitializeComponent();
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Tüm Kullanıcılar");
            comboBox1.Items.Add("İlan Sahipleri");
            comboBox1.Items.Add("Müşteriler");
            comboBox1.Items.Add("Bütün İlan Sahipleri");
        }
        string no;
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Tüm Kullanıcılar")
                no = "1";
            if (comboBox1.Text == "İlan Sahipleri")
                no = "2";
            if (comboBox1.Text == "Müşteriler")
                no = "3";
            if (comboBox1.Text == "Bütün İlan Sahipleri")
                no = "4";   
        }
        private void button1_Click(object sender, EventArgs e)
        { 
            try
            {
                using (var db = new eticaretDataContext(connect_string))
                {
                    eticaret.Mesajlar mesajlar = new eticaret.Mesajlar();
                    mesajlar.Alici =no+":" + textBox4.Text;
                    mesajlar.Gönderen = textBox1.Text;
                    mesajlar.Mesaj_Basligi = textBox2.Text;
                    mesajlar.Mesaj_Icerigi = textBox3.Text;
                    mesajlar.Tarih = DateTime.Now;
                    db.Mesajlar.InsertOnSubmit(mesajlar);
                    db.SubmitChanges();
                }
                MessageBox.Show("Mesaj Gönderildi..");
            }
            catch(Exception r) { MessageBox.Show("Mesaj Gönderilemedi."); }
        }
    }
}
