using System.ComponentModel;

namespace StudentManagementSystem.Models
{
    public class DataSiswa
    {
        public int Id { get; set; }

        public int NIM { get; set; }

        public string Nama { get; set; }

        [DisplayName("Tanggal Lahir")]
        public DateOnly TanggalLahir { get; set; }

        [DisplayName("Jenis Kelamin")]
        public string JenisKelamin { get; set; }

        public string Alamat { get; set; }
    }
}