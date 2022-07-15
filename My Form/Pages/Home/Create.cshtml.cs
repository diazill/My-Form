using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace My_Form.Pages.Home
{
    public class CreateModel : PageModel
    {
        public PelamarInfo pelamarInfo = new PelamarInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            pelamarInfo.nama = Request.Form["nama"];
            pelamarInfo.tanggal_lahir = Request.Form["tanggal_lahir"];
            pelamarInfo.domisili = Request.Form["domisili"];
            //pelamarInfo.pendidikan_terakhir = Request.Form["tingkat_pendidikan"];
            pelamarInfo.pendidikan_terakhir = Request.Form["pendidikan_terakhir"];
            pelamarInfo.posisi_dipilih = Request.Form["posisi_dipilih"];
            pelamarInfo.merokok = Request.Form["merokok"];
            pelamarInfo.created_at = System.DateTime.Now.ToString("MM/dd/yyyy");
            pelamarInfo.updated_at = System.DateTime.Now.ToString("MM/dd/yyyy");

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
                                 "(nama, tanggal_lahir, domisili, pendidikan_terakhir, posisi_dipilih, merokok, created_at, updated_at) VALUES " +
                                 "(@nama, @tanggal_lahir, @domisili, @pendidikan_terakhir, @posisi_dipilih, @merokok, @created_at, @updated_at)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nama", pelamarInfo.nama);
                        command.Parameters.AddWithValue("@tanggal_lahir", pelamarInfo.tanggal_lahir);
                        command.Parameters.AddWithValue("@domisili", pelamarInfo.domisili);
                        command.Parameters.AddWithValue("@pendidikan_terakhir", pelamarInfo.pendidikan_terakhir);
                        command.Parameters.AddWithValue("@posisi_dipilih", pelamarInfo.posisi_dipilih);
                        command.Parameters.AddWithValue("@merokok", pelamarInfo.merokok);
                        command.Parameters.AddWithValue("@created_at", pelamarInfo.created_at);
                        command.Parameters.AddWithValue("@updated_at", pelamarInfo.updated_at);


                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


            pelamarInfo.nama = ""; pelamarInfo.tanggal_lahir = ""; pelamarInfo.domisili = ""; pelamarInfo.pendidikan_terakhir = ""; pelamarInfo.posisi_dipilih = ""; pelamarInfo.merokok = ""; pelamarInfo.created_at = ""; pelamarInfo.updated_at = "";
            successMessage = "Berhasil menambahkan Pelamar";    
        }
    }
}
