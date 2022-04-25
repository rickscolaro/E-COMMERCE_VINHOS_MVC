using Projeto.Data;
using Projeto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Projeto.Areas.Admin.Controllers {//Lanche

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminProdutosController : Controller {


        private readonly AppDbContext _context;

        public AdminProdutosController(AppDbContext context) {
            _context = context;
        }

        // GET: Admin/AdminProdutos
        public async Task<IActionResult> Index()
        {
           var appDbContext = _context.Produtos.Include(l => l.Categoria);
           return View(await appDbContext.ToListAsync());
        }


        // GET: Admin/AdminProdutos/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if (produto == null) {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Admin/AdminProdutos/Create
        public IActionResult Create() {
            ViewBag.ImagemUrl = "/images/produtos/";
            ViewBag.ImagemThumbnailUrl = "/images/produtos/";
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome");
            return View();
        }

        // POST: Admin/AdminProdutos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProdutoId,Nome,DescricaoCurta,DescritaDetalhada,Preco,Imagem,EmEstoque,CategoriaId")] Produto produto) {
            if (ModelState.IsValid) {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", produto.CategoriaId);
            return View(produto);
        }

        // GET: Admin/AdminProdutos/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", produto.CategoriaId);
            return View(produto);
        }

        // POST: Admin/AdminProdutos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProdutoId,Nome,DescricaoCurta,DescritaDetalhada,Preco,Imagem,EmEstoque,CategoriaId")] Produto produto) {
            if (id != produto.ProdutoId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!ProdutoExists(produto.ProdutoId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", produto.CategoriaId);
            return View(produto);
        }

        // GET: Admin/AdminProdutos/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if (produto == null) {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Admin/AdminProdutos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var produto = await _context.Produtos.FindAsync(id);
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id) {
            return _context.Produtos.Any(e => e.ProdutoId == id);
        }

    }
}