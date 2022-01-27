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
    public class RentaParqueosController : Controller
    {
        private readonly IvonneManagementContext _context;
        private readonly IRentaParqueoService _serviceRentaParqueo;
        private readonly IApartamentoService _serviceApartamento;
        private readonly IInquilinoService _serviceinquilino;

        public RentaParqueosController(IvonneManagementContext context, IRentaParqueoService serviceRentaParqueo, IApartamentoService serviceApartamento, IInquilinoService serviceinquilino)
        {
            _context = context;
            _serviceRentaParqueo = serviceRentaParqueo;
            _serviceApartamento = serviceApartamento;
            _serviceinquilino = serviceinquilino;
        }



        // GET: RentaParqueos
        public async Task<IActionResult> Index()
        {
            var ivonneManagementContext = _serviceRentaParqueo.GetQuery().Include(r => r.Apartamento).Include(r => r.Inquilino);
            return View(await ivonneManagementContext.ToListAsync());
        }

        // GET: RentaParqueos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentaParqueo = await _serviceRentaParqueo.GetQuery()
                .Include(r => r.Apartamento)
                .Include(r => r.Inquilino)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rentaParqueo == null)
            {
                return NotFound();
            }

            return View(rentaParqueo);
        }

        // GET: RentaParqueos/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["IdApt"] = new SelectList(await _serviceApartamento.ObtenerApartamentos(), "IdApt", "Nombre");
            ViewData["InquilinoId"] = new SelectList(await _serviceinquilino.ObtenerInquilinos(), "Id", "Nombre");
            return View();
        }

        // POST: RentaParqueos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdApt,InquilinoId,Monto,Estado")] RentaParqueo rentaParqueo)
        {
            if (ModelState.IsValid)
            {
                if (await _serviceRentaParqueo.CrearRentaParqueo(rentaParqueo))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["IdApt"] = new SelectList(await _serviceApartamento.ObtenerApartamentos(), "IdApt", "Nombre", rentaParqueo.IdApt);
            ViewData["InquilinoId"] = new SelectList(await _serviceinquilino.ObtenerInquilinos(), "Id", "Nombre", rentaParqueo.InquilinoId);
            return View(rentaParqueo);
        }

        // GET: RentaParqueos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentaParqueo = await _serviceRentaParqueo.ObtenerRentaParqueo(id);
            if (rentaParqueo == null)
            {
                return NotFound();
            }
            ViewData["IdApt"] = new SelectList(await _serviceApartamento.ObtenerApartamentos(), "IdApt", "Nombre", rentaParqueo.IdApt);
            ViewData["InquilinoId"] = new SelectList(await _serviceinquilino.ObtenerInquilinos(), "Id", "Nombre", rentaParqueo.InquilinoId);
            return View(rentaParqueo);
        }

        // POST: RentaParqueos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdApt,InquilinoId,Monto,Estado")] RentaParqueo rentaParqueo)
        {
            if (id != rentaParqueo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _serviceRentaParqueo.EditarRentaParqueo(id, rentaParqueo);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdApt"] = new SelectList(await _serviceApartamento.ObtenerApartamentos(), "IdApt", "Nombre", rentaParqueo.IdApt);
            ViewData["InquilinoId"] = new SelectList(await _serviceinquilino.ObtenerInquilinos(), "Id", "Nombre", rentaParqueo.InquilinoId);
            return View(rentaParqueo);
        }

        // GET: RentaParqueos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentaParqueo = await _serviceRentaParqueo.GetQuery()
                .Include(r => r.Apartamento)
                .Include(r => r.Inquilino)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rentaParqueo == null)
            {
                return NotFound();
            }

            return View(rentaParqueo);
        }

        // POST: RentaParqueos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*var rentaParqueo = await _context.RentaParqueos.FindAsync(id);
            _context.RentaParqueos.Remove(rentaParqueo);
            await _context.SaveChangesAsync();*/
            await _serviceRentaParqueo.EliminarRentaParqueo(id);
            return RedirectToAction(nameof(Index));
        }

        private bool RentaParqueoExists(int id)
        {
            return _serviceRentaParqueo.GetQuery().Any(e => e.Id == id);
        }
    }
}
