using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IvonneManagement.Context;
using IvonneManagement.Models;
using IvonneManagement.Services;

namespace IvonneManagement.Controllers
{
    public class InquilinosController : Controller
    {
        private readonly IvonneManagementContext _context;
        private readonly IInquilinoService _service;

        public InquilinosController(IvonneManagementContext context, IInquilinoService service)
        {
            _context = context;
            _service = service;
        }

        // GET: Inquilinos
        public async Task<IActionResult> Index()
        {
            return View(await _service.ObtenerInquilinos());
        }

        // GET: Inquilinos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return View(await _service.ObtenerInquilino(id));
        }

        // GET: Inquilinos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inquilinos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellidos,Cedula,NumeroTelefono,EstadoInquilino")] Inquilino inquilino)
        {
            if (await _service.CrearInquilino(inquilino))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(inquilino);
            }
        }

        // GET: Inquilinos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inquilino = await _context.Inquilinos.FindAsync(id);
            if (inquilino == null)
            {
                return NotFound();
            }
            return View(inquilino);
        }

        // POST: Inquilinos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellidos,Cedula,NumeroTelefono,EstadoInquilino")] Inquilino inquilino)
        {
            if (await _service.EditarInquilino(id, inquilino))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(inquilino);
            }
        }

        // GET: Inquilinos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inquilino = await _context.Inquilinos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inquilino == null)
            {
                return NotFound();
            }

            return View(inquilino);
        }

        // POST: Inquilinos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _service.EliminarInquilino(id))
            {
                return RedirectToAction(nameof(Index));
            }
            return View(id);
        }

        private bool InquilinoExists(int id)
        {
            return _context.Inquilinos.Any(e => e.Id == id);
        }
    }
}
