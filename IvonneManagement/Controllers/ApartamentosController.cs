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
    public class ApartamentosController : Controller
    {
        private readonly IvonneManagementContext _context;
        private readonly IApartamentoService _serviceApartamento;
        private readonly IInquilinoService _serviceInquilino;

        public ApartamentosController(IvonneManagementContext context, IApartamentoService serviceApartamento, IInquilinoService serviceInquilino)
        {
            _context = context;
            _serviceApartamento = serviceApartamento;
            _serviceInquilino = serviceInquilino;
        }

        // GET: Apartamentos
        public async Task<IActionResult> Index()
        {
            var ivonneManagementContext = _serviceApartamento.GetQuery().Include(a => a.Inquilino);
            return View(await ivonneManagementContext.ToListAsync());
        }

        // GET: Apartamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartamento = await _serviceApartamento.GetQuery()
                .Include(a => a.Inquilino)
                .FirstOrDefaultAsync(m => m.IdApt == id);
            if (apartamento == null)
            {
                return NotFound();
            }

            return View(apartamento);
        }

        // GET: Apartamentos/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["InquilinoId"] = new SelectList(await _serviceInquilino.ObtenerInquilinos(), "Id", "Nombre");
            return View();
        }

        // POST: Apartamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdApt,Nombre,InquilinoId,Estado")] Apartamento apartamento)
        {
            if (ModelState.IsValid)
            {
                if (await _serviceApartamento.CrearApartamento(apartamento))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["InquilinoId"] = new SelectList(await _serviceInquilino.ObtenerInquilinos(), "Id", "Nombre", apartamento.InquilinoId);
            return View(apartamento);
        }

        // GET: Apartamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartamento = await _serviceApartamento.ObtenerApartamento(id);
            if (apartamento == null)
            {
                return NotFound();
            }
            ViewData["InquilinoId"] = new SelectList(await _serviceInquilino.ObtenerInquilinos(), "Id", "Cedula", apartamento.InquilinoId);
            return View(apartamento);
        }

        // POST: Apartamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdApt,Nombre,InquilinoId,Estado")] Apartamento apartamento)
        {
            if (id != apartamento.IdApt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _serviceApartamento.EditarApartamento(id, apartamento);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartamentoExists(apartamento.IdApt))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["InquilinoId"] = new SelectList(await _serviceInquilino.ObtenerInquilinos(), "Id", "Nombre", apartamento.InquilinoId);
            return View(apartamento);
        }

        // GET: Apartamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartamento = await _serviceApartamento.GetQuery()
                .Include(a => a.Inquilino)
                .FirstOrDefaultAsync(m => m.IdApt == id);
            if (apartamento == null)
            {
                return NotFound();
            }

            return View(apartamento);
        }

        // POST: Apartamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apartamento = await _serviceApartamento.GetQuery()
                .Include(r => r.Inquilino)
                .FirstOrDefaultAsync(m => m.IdApt == id);

            await _serviceApartamento.EliminarApartamento(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ApartamentoExists(int id)
        {
            return _serviceApartamento.GetQuery().Any(e => e.IdApt == id);
        }
    }
}
