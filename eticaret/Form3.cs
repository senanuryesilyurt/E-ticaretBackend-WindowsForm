using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eticaret
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public int notice_number;
        public string notice_header = "";
        public string notice_content = "";
        public string user_name = "";
        public bool priority;
        public string category = "";
        public int notice_price;
        public string notice_picture = "";

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label1.Text="notice number: " +Convert.ToInt32(notice_number);
            label2.Text = "notice header: " + notice_content;
            label3.Text = "notice content: " + notice_content;
            label4.Text = "user name: " + user_name;
            label5.Text = "priority: " + priority;
            label6.Text = "category: " + category;
            label7.Text = "notice price: " + notice_price;
            label8.Text = "notice picture: " + notice_picture;
        }
    }
}
