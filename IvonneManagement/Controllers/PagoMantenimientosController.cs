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
    public class PagoMantenimientosController : Controller
    {
        private readonly IvonneManagementContext _context;
        private readonly IPagoMantenimientoService _servicePagoMant;
        private readonly IApartamentoService _serviceApartamento;
        private readonly IInquilinoService _serviceInquilino;

        public PagoMantenimientosController(IvonneManagementContext context, IPagoMantenimientoService servicePagoMant, IApartamentoService serviceApartamento, IInquilinoService serviceInquilino)
        {
            _context = context;
            _servicePagoMant = servicePagoMant;
            _serviceApartamento = serviceApartamento;
            _serviceInquilino = serviceInquilino;
        }


        // GET: PagoMantenimientos
        public async Task<IActionResult> Index()
        {
            var ivonneManagementContext = _servicePagoMant.GetQuery().Include(p => p.Apartamento).Include(p => p.Inquilino);
            return View(await ivonneManagementContext.ToListAsync());
        }

        // GET: PagoMantenimientos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pagoMantenimiento = await _servicePagoMant.GetQuery()
                .Include(p => p.Apartamento)
                .Include(p => p.Inquilino)
                .FirstOrDefaultAsync(m => m.IdPago == id);
            if (pagoMantenimiento == null)
            {
                return NotFound();
            }

            return View(pagoMantenimiento);
        }

        // GET: PagoMantenimientos/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["ApartamentoId"] = new SelectList(await _serviceApartamento.ObtenerApartamentos(), "IdApt", "Nombre");
            ViewData["InquilinoId"] = new SelectList(await _serviceInquilino.ObtenerInquilinos(), "Id", "Nombre");
            return View();
        }

        // POST: PagoMantenimientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPago,InquilinoId,Monto,ApartamentoId,Estado")] PagoMantenimiento pagoMantenimiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pagoMantenimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartamentoId"] = new SelectList(await _serviceApartamento.ObtenerApartamentos(), "IdApt", "Nombre", pagoMantenimiento.ApartamentoId);
            ViewData["InquilinoId"] = new SelectList(await _serviceInquilino.ObtenerInquilinos(), "Id", "Nombre", pagoMantenimiento.InquilinoId);
            return View(pagoMantenimiento);
        }

        // GET: PagoMantenimientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pagoMantenimiento = await _servicePagoMant.ObtenerPagoMantenimiento(id);
            if (pagoMantenimiento == null)
            {
                return NotFound();
            }
            ViewData["ApartamentoId"] = new SelectList(await _serviceApartamento.ObtenerApartamentos(), "IdApt", "Nombre", pagoMantenimiento.ApartamentoId);
            ViewData["InquilinoId"] = new SelectList(await _serviceInquilino.ObtenerInquilinos(), "Id", "Nombre", pagoMantenimiento.InquilinoId);
            return View(pagoMantenimiento);
        }

        // POST: PagoMantenimientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPago,InquilinoId,Monto,ApartamentoId,Estado")] PagoMantenimiento pagoMantenimiento)
        {
            if (id != pagoMantenimiento.IdPago)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pagoMantenimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagoMantenimientoExists(pagoMantenimiento.IdPago))
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
            ViewData["ApartamentoId"] = new SelectList(await _serviceApartamento.ObtenerApartamentos(), "IdApt", "Nombre", pagoMantenimiento.ApartamentoId);
            ViewData["InquilinoId"] = new SelectList(await _serviceInquilino.ObtenerInquilinos(), "Id", "Nombre", pagoMantenimiento.InquilinoId);
            return View(pagoMantenimiento);
        }

        // GET: PagoMantenimientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pagoMantenimiento = await _servicePagoMant.GetQuery()
                .Include(p => p.Apartamento)
                .Include(p => p.Inquilino)
                .FirstOrDefaultAsync(m => m.IdPago == id);
            if (pagoMantenimiento == null)
            {
                return NotFound();
            }

            return View(pagoMantenimiento);
        }

        // POST: PagoMantenimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _servicePagoMant.EliminarPagoMantenimiento(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PagoMantenimientoExists(int id)
        {
            return _servicePagoMant.GetQuery().Any(e => e.IdPago == id);
        }
    }
}
