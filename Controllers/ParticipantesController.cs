using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Login.Data;
using MVC_Login.Models;

namespace MVC_Login.Controllers
{
    [Authorize]
    public class ParticipantesController : Controller
    {
        private readonly BancoContext _context;

        public ParticipantesController(BancoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Participantes
                .AsNoTracking()
                .Where(x => x.Usuario == User.Identity.Name)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participantes = await _context.Participantes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (participantes == null)
            {
                return NotFound();
            }

            if (participantes.Usuario != User.Identity.Name)
            {
                return NotFound();
            }

            return View(participantes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Telefone,Atualizacao,Criacao,Usuario")] Participantes participantes)
        {
            if (ModelState.IsValid)
            {
                participantes.Usuario = User.Identity.Name;
                _context.Add(participantes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(participantes);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participantes = await _context.Participantes.FindAsync(id);
            if (participantes == null)
            {
                return NotFound();
            }

            if (participantes.Usuario != User.Identity.Name)
            {
                return NotFound();
            }

            return View(participantes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Telefone")] Participantes participantes)
        {
            if (id != participantes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    participantes.Usuario = User.Identity.Name;
                    participantes.Atualizacao = DateTime.Now;
                    _context.Update(participantes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipantesExists(participantes.Id))
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
            return View(participantes);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participantes = await _context.Participantes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (participantes == null)
            {
                return NotFound();
            }

            if (participantes.Usuario != User.Identity.Name)
            {
                return NotFound();
            }

            return View(participantes);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var participantes = await _context.Participantes.FindAsync(id);
            _context.Participantes.Remove(participantes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipantesExists(int id)
        {
            return _context.Participantes.Any(e => e.Id == id);
        }
    }
}
