using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context) // Constructor
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index() // Method untuk menampilkan data pada view Index
        {
            // Mendapatkan data Siswa dari database
            var dataSiswa = await _context.Students
                .ToListAsync();

            // Menghitung jumlah data gender Siswa Laki-Laki secara keseluruhan
            ViewBag.TotalLaki = _context.Students
                .Count(s => s.JenisKelamin == "Laki-Laki");

            // Menghitung jumlah data gender Siswa Perempuan secara keseluruhan
            ViewBag.TotalPerempuan = _context.Students
                .Count(s => s.JenisKelamin == "Perempuan");

            // Menghitung jumlah keseluruhan gender Siswa
            ViewBag.TotalGender = _context.Students
                .Select(s => s.JenisKelamin)
                .Count();

            return View(dataSiswa);
        }

        public IActionResult Privacy() // Method untuk menampilkan data pada view Privacy
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}