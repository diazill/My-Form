using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace My_Form.Pages.Home
{
    public class CreateModel : PageModel
    {
        public PelamarInfo pelamarInfo = new PelamarInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public List<PosisiInfo> ListPosisi = new List<PosisiInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=LAPTOP-IDEAPAD3;Initial Catalog=db_hnp;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM posisi";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PosisiInfo posisi = new PosisiInfo();
                                posisi.id = "" + reader.GetInt32(0);
                                posisi.nama_posisi = reader.GetString(1);

                                ListPosisi.Add(posisi);
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
            
    
       

        public void OnPost()
        {
            pelamarInfo.nama = Request.Form["nama"];
            pelamarInfo.tanggal_lahir = Request.Form["tanggal_lahir"];
            pelamarInfo.domisili = Request.Form["domisili"];
            //pelamarInfo.pendidikan_terakhir = Request.Form["tingkat_pendidikan"];
            pelamarInfo.pendidikan_terakhir = Request.Form["pendidikan_terakhir"];
            pelamarInfo.merokok = Request.Form["merokok"];
            pelamarInfo.created_at = System.DateTime.Now.ToString("MM/dd/yyyy");
            pelamarInfo.updated_at = System.DateTime.Now.ToString("MM/dd/yyyy");
            pelamarInfo.id_posisi = Request.Form["id_posisi"];

            if (pelamarInfo.nama.Length == 0 || pelamarInfo.domisili.Length == 0 || pelamarInfo.tanggal_lahir.Length == 0 )
            {
                errorMessage = "All the Field Is Required";
                return;
            }

            //save to database
            try
            {
                String connectionString = "Data Source=LAPTOP-IDEAPAD3;Initial Catalog=db_hnp;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO pelamar " +
                                 "(nama, tanggal_lahir, domisili, pendidikan_terakhir, merokok, created_at, updated_at, id_posisi) VALUES " +
                                 "(@nama, @tanggal_lahir, @domisili, @pendidikan_terakhir, @merokok, @created_at, @updated_at, @id_posisi)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nama", pelamarInfo.nama);
                        command.Parameters.AddWithValue("@tanggal_lahir", pelamarInfo.tanggal_lahir);
                        command.Parameters.AddWithValue("@domisili", pelamarInfo.domisili);
                        command.Parameters.AddWithValue("@pendidikan_terakhir", pelamarInfo.pendidikan_terakhir);
                        command.Parameters.AddWithValue("@merokok", pelamarInfo.merokok);
                        command.Parameters.AddWithValue("@created_at", pelamarInfo.created_at);
                        command.Parameters.AddWithValue("@updated_at", pelamarInfo.updated_at);
                        command.Parameters.AddWithValue("@id_posisi", pelamarInfo.id_posisi);


                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


            pelamarInfo.nama = ""; pelamarInfo.tanggal_lahir = ""; pelamarInfo.domisili = ""; pelamarInfo.pendidikan_terakhir = ""; pelamarInfo.merokok = ""; pelamarInfo.created_at = ""; pelamarInfo.updated_at = ""; pelamarInfo.id_posisi = "";
            successMessage = "Berhasil menambahkan Pelamar";    
        }

        public class PosisiInfo
        {
            public string id;
            public string nama_posisi;
        }
    }
}
