using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace My_Form.Pages.Home
{
    public class EditModel : PageModel
    {
        public PelamarInfo pelamarInfo = new PelamarInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=LAPTOP-IDEAPAD3;Initial Catalog=db_hnp;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM pelamar WHERE id=@id";
                    
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                pelamarInfo.id = "" + reader.GetInt32(0);
                                pelamarInfo.nama = reader.GetString(1);
                                pelamarInfo.tanggal_lahir = reader.GetDateTime(2).ToString("MM/dd/yyyy");
                                pelamarInfo.domisili = reader.GetString(3);
                                pelamarInfo.pendidikan_terakhir = reader.GetString(4);
                                pelamarInfo.merokok = "" + reader.GetInt32(5);
                                pelamarInfo.created_at = reader.GetDateTime(6).ToString("MM/dd/yyyy");
                                pelamarInfo.updated_at = reader.GetDateTime(7).ToString("MM/dd/yyyy");
                                pelamarInfo.id_posisi = "" + reader.GetInt32(8);
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            pelamarInfo.id = Request.Form["id"];
            pelamarInfo.nama = Request.Form["nama"];
            pelamarInfo.tanggal_lahir = Request.Form["tanggal_lahir"];
            pelamarInfo.domisili = Request.Form["domisili"];
            //pelamarInfo.pendidikan_terakhir = Request.Form["tingkat_pendidikan"];
            pelamarInfo.pendidikan_terakhir = Request.Form["pendidikan_terakhir"];
            pelamarInfo.merokok = Request.Form["merokok"];
            pelamarInfo.created_at = Request.Form["created_at"];
            pelamarInfo.updated_at = System.DateTime.Now.ToString("MM/dd/yyyy");
            pelamarInfo.id_posisi = Request.Form["id_posisi"];


            if (pelamarInfo.nama.Length == 0 || pelamarInfo.domisili.Length == 0 || pelamarInfo.tanggal_lahir.Length == 0)
            {
                errorMessage = "All the Field Is Required";
                return;
            }

            try
            {
                String connectionString = "Data Source=LAPTOP-IDEAPAD3;Initial Catalog=db_hnp;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE pelamar " +
                                 "SET nama=@nama, tanggal_lahir=@tanggal_lahir, domisili=@domisili, pendidikan_terakhir=@pendidikan_terakhir, merokok=@merokok,  updated_at=@updated_at, id_posisi=@id_posisi" +
                                 "WHERE id=@id";



                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        
                        command.Parameters.AddWithValue("@nama", pelamarInfo.nama);
                        command.Parameters.AddWithValue("@tanggal_lahir", pelamarInfo.tanggal_lahir);
                        command.Parameters.AddWithValue("@domisili", pelamarInfo.domisili);
                        command.Parameters.AddWithValue("@pendidikan_terakhir", pelamarInfo.pendidikan_terakhir);
                        command.Parameters.AddWithValue("@merokok", pelamarInfo.merokok);
                        command.Parameters.AddWithValue("@updated_at", pelamarInfo.updated_at);
                        command.Parameters.AddWithValue("@id", pelamarInfo.id);
                        command.Parameters.AddWithValue("@posisi_dipilih", pelamarInfo.id_posisi);

                        //command.Parameters.Add("@id", SqlDbType.Int).Value = pelamarInfo.id;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Home/Index");
        }
    }


}
