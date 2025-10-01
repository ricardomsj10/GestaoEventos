using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GerenciamentoEventosHoteis.Data;
using GerenciamentoEventosHoteis.Models;

namespace GerenciamentoEventosHoteis.Controllers
{
    public class PagamentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PagamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pagamentos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pagamentos.Include(p => p.Evento).Include(p => p.Participante);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pagamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pagamento = await _context.Pagamentos
                .Include(p => p.Evento)
                .Include(p => p.Participante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pagamento == null)
            {
                return NotFound();
            }

            return View(pagamento);
        }

        // GET: Pagamentos/Create
        public IActionResult Create()
        {
            ViewData["EventoId"] = new SelectList(_context.Eventos, "Id", "Descricao");
            ViewData["ParticipanteId"] = new SelectList(_context.Participantes, "Id", "Email");
            return View();
        }

        // POST: Pagamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParticipanteId,EventoId,Valor,DataPagamento,Aprovado")] Pagamento pagamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pagamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventoId"] = new SelectList(_context.Eventos, "Id", "Descricao", pagamento.EventoId);
            ViewData["ParticipanteId"] = new SelectList(_context.Participantes, "Id", "Email", pagamento.ParticipanteId);
            return View(pagamento);
        }

        // GET: Pagamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pagamento = await _context.Pagamentos.FindAsync(id);
            if (pagamento == null)
            {
                return NotFound();
            }
            ViewData["EventoId"] = new SelectList(_context.Eventos, "Id", "Descricao", pagamento.EventoId);
            ViewData["ParticipanteId"] = new SelectList(_context.Participantes, "Id", "Email", pagamento.ParticipanteId);
            return View(pagamento);
        }

        // POST: Pagamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParticipanteId,EventoId,Valor,DataPagamento,Aprovado")] Pagamento pagamento)
        {
            if (id != pagamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pagamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagamentoExists(pagamento.Id))
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
            ViewData["EventoId"] = new SelectList(_context.Eventos, "Id", "Descricao", pagamento.EventoId);
            ViewData["ParticipanteId"] = new SelectList(_context.Participantes, "Id", "Email", pagamento.ParticipanteId);
            return View(pagamento);
        }

        // GET: Pagamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pagamento = await _context.Pagamentos
                .Include(p => p.Evento)
                .Include(p => p.Participante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pagamento == null)
            {
                return NotFound();
            }

            return View(pagamento);
        }

        // POST: Pagamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pagamento = await _context.Pagamentos.FindAsync(id);
            if (pagamento != null)
            {
                _context.Pagamentos.Remove(pagamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PagamentoExists(int id)
        {
            return _context.Pagamentos.Any(e => e.Id == id);
        }
    }
}
