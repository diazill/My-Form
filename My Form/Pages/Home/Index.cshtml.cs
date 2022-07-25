using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace My_Form.Pages.Home
{
    public class IndexModel : PageModel
    {
        public List<PelamarInfo> ListPelamar = new List<PelamarInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=LAPTOP-IDEAPAD3;Initial Catalog=db_hnp;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM pelamar";
                    
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PelamarInfo info = new PelamarInfo();
                                info.id = "" + reader.GetInt32(0);
                                info.nama = reader.GetString(1);
                                info.tanggal_lahir = reader.GetDateTime(2).ToString("MM/dd/yyyy");
                                info.domisili = reader.GetString(3);
                                info.pendidikan_terakhir = reader.GetString(4);
                                info.merokok = "" + reader.GetInt32(5);
                                info.created_at = reader.GetDateTime(6).ToString("MM/dd/yyyy");
                                info.updated_at = reader.GetDateTime(7).ToString("MM/dd/yyyy");
                                info.id_posisi = "" + reader.GetInt32(8);

                                ListPelamar.Add(info);
                            }
                        }
                    }

                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class PelamarInfo
    {
        public string id;
        public string nama;
        public string tanggal_lahir;
        public string domisili;
        public string pendidikan_terakhir;
        public string merokok;
        public string created_at;
        public string updated_at;
        public string id_posisi;
    }
}