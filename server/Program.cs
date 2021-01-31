using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eticaret;
namespace server
{
    class Program
    {
        public static string connect_string = @"Data Source=DESKTOP-68FQ1B6\SQLEXPRESS;Initial Catalog=eticaret;Integrated Security=True";
        static void Main(string[] args)
        {
            try {
               
                using (var db = new eticaretDataContext(connect_string))
                {

                    //DateTime a = DateTime.Parse("2019-12-01 12:00");
                    //DateTime b = DateTime.Parse("2019-12-03 14:00");

                    //string fark = b.Subtract(a).ToString();

                    //string[] parcala = fark.Split('.');

                    //string[] time = parcala[1].Split(':');

                    //Console.WriteLine(parcala[0] + " gün fark " + Convert.ToInt32(time[0]) + " saat var" );

                    var siparis_tarihi = (from x in db.Siparis where x.siparis_durumu != "teslim edildi" && x.siparis_durumu != "iptal edildi" select x).ToArray();

                    foreach (var st in siparis_tarihi)
                    {
                        DateTime max_date = st.siparis_tarihi.Value.AddDays(7);
                        //Console.WriteLine(max_date);
                        string fark = DateTime.Now.Subtract(max_date).ToString();
                        //Console.WriteLine(fark);
                        string[] ayir = fark.Split('.');
                        int gun = Convert.ToInt32(ayir[0]);
                        Console.WriteLine(gun);
                        if (gun < 0)
                        {
                            Console.WriteLine("suresi var");
                            //Console.WriteLine(st.siparis_id);
                            //  var guncelle = (from x in db.Siparis where x.siparis_id == st.siparis_id select x).FirstOrDefault();
                            //  guncelle.siparis_durumu = " ";
                            //st.siparis_durumu = " ";
                            //db.SubmitChanges();
                        }
                        else
                        {
                            Console.WriteLine("suresi doldu");
                            //var guncelle = (from x in db.Siparis where x.siparis_id == st.siparis_id select x).FirstOrDefault();
                            //guncelle.siparis_durumu = "iptal edildi.";
                            st.siparis_durumu = "iptal edildi.";
                            db.SubmitChanges();       
                        }
                    }
                }
            }
            catch(Exception r) { }
        }
    }
}
