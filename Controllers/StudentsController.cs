using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context; // Membuat Constructor

        public StudentsController(ApplicationDbContext context) // Class untuk Constructor
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index() // Method untuk menampilkan data pada view Index atau Halaman Utama
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id) // Method untuk menampilkan data berdasarkan id pada view Details
        {
            if (id == null) // Jika id null atau tidak ditemukan
            {
                return NotFound(); // Maka akan menampilkan halaman not found
            }

            var dataSiswa = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id); // Mendapatkan data berdasarkan id Siswa

            if (dataSiswa == null) // Jika data Siswa tidak ditemukan atau null
            {
                return NotFound(); // Maka akan menampilkan halaman not found
            }

            return View(dataSiswa); // Maka akan menampilkan data Siswa
        }

        // GET: Students/Create
        public IActionResult Create() // Method/Class untuk mengambil dan menampilkan data Siswa dalam view Create
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost] // aksi ini merespon permintaan HTTP POST
        [ValidateAntiForgeryToken] // Memeriksa token anti-forgery untuk mencegah CSRF
        public async Task<IActionResult> Create(DataSiswa dataSiswa) // Method/Class untuk menambahkan data Siswa
        {
            if (ModelState.IsValid) // Jika model valid
            {
                _context.Add(dataSiswa); // Menambahkan data Siswa
                await _context.SaveChangesAsync(); // Menyimpan perubahan

                TempData["AlertMessageAdd"] = "Data berhasil ditambahkan!"; // Menampilkan pesan sukses menggunakan SweetAlert

                return RedirectToAction("Index", "Home"); // Mengarahkan ke halaman Index
            }

            return View(dataSiswa);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id) // Method/Class untuk menampilkan data pada view Edit berdasarkan id
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataSiswa = await _context.Students.FindAsync(id);

            if (dataSiswa == null)
            {
                return NotFound();
            }
            return View(dataSiswa);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NIM,Nama,TanggalLahir,JenisKelamin,Alamat")] DataSiswa dataSiswa) // Method/Class untuk mengupdate data Siswa
        {
            if (id != dataSiswa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataSiswa);
                    await _context.SaveChangesAsync();

                    TempData["AlertMessageUpdate"] = "Data berhasil diupdate!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataSiswaExists(dataSiswa.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(dataSiswa);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id) // Method/Class untuk mengambil dan menampilkan data Siswa dalam view Delete
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataSiswa = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dataSiswa == null)
            {
                return NotFound();
            }

            return View(dataSiswa);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) // Method/Class untuk menghapus data Siswa
        {
            var dataSiswa = await _context.Students.FindAsync(id);
            if (dataSiswa != null)
            {
                _context.Students.Remove(dataSiswa);
            }

            TempData["AlertMessageDelete"] = "Data berhasil dihapus!";

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool DataSiswaExists(int id) // Method/Class untuk memeriksa apakah data Siswa dengan id tertentu ada
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}