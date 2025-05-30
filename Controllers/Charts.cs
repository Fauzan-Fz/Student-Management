using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Data;

namespace StudentManagementSystem.Controllers
{
    public class Charts : Controller
    {
        private readonly ApplicationDbContext _context; // Membuat Constructor

        public Charts(ApplicationDbContext context) // Class untuk Constructor
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var jenisKelamin_Siswa = _context.Students
                .GroupBy(s => s.JenisKelamin)
                .Select(g => new
                {
                    jenisKelamin = g.Key,
                    jumlah = g.Count()
                })
                .ToList();

            var labels = jenisKelamin_Siswa.Select(g => g.jenisKelamin).ToArray();
            var data = jenisKelamin_Siswa.Select(g => g.jumlah).ToArray();

            ViewBag.Labels = JsonSerializer.Serialize(labels);
            ViewBag.Data = JsonSerializer.Serialize(data);

            var tahunKelahiran = _context.Students
                .GroupBy(s => s.TanggalLahir.Year)
                .Select(g => new
                {
                    tahunKelahiranSiswa = g.Key,
                    jumlah = g.Count()
                })
                .ToList();

            var labelsTahunKelahiran = tahunKelahiran.Select(g => g.tahunKelahiranSiswa).ToArray();
            var dataTahunKelahiran = tahunKelahiran.Select(g => g.jumlah).ToArray();

            ViewBag.LabelsTahun = JsonSerializer.Serialize(labelsTahunKelahiran);
            ViewBag.DataTahun = JsonSerializer.Serialize(dataTahunKelahiran);

            return View();
        }
    }
}