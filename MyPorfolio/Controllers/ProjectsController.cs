using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPorfolio.Models.Context;
using MyPorfolio.Models.Entities;

namespace MyPorfolio.Controllers;

public class ProjectsController : Controller
{
    private readonly MyPorfolioContext _context;

    public ProjectsController(MyPorfolioContext context)
    {
        _context = context;
    }

    // GET: Projects
    public async Task<IActionResult> Index()
    {
        return _context.Projects != null ?
                    View(await _context.Projects.ToListAsync()) :
                    Problem("Entity set 'MySiteContext.Projects'  is null.");
    }

    // GET: Projects/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Projects == null)
        {
            return NotFound();
        }

        var projects = await _context.Projects
            .FirstOrDefaultAsync(m => m.ProjectID == id);
        if (projects == null)
        {
            return NotFound();
        }

        return View(projects);
    }

    // GET: Projects/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Projects/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProjectID,ProjectName,ProjectDescription,ProjectDate,ProjectPic")] Project projects, IFormFile ProjectPic)
    {
        string DirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/Project");
        if (!Directory.Exists(DirectoryPath))
        {
            Directory.CreateDirectory(DirectoryPath);
        }
        if (ModelState.IsValid)
        {
            if (ProjectPic != null)
            {
                string imageName = ProjectPic.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/Project/{imageName}");
                var stream = new FileStream(path, FileMode.Create);
                projects.ProjectPic = imageName;
                ProjectPic.CopyTo(stream);
            }
            _context.Add(projects);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(projects);
    }

    // GET: Projects/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Projects == null)
        {
            return NotFound();
        }

        var projects = await _context.Projects.FindAsync(id);
        if (projects == null)
        {
            return NotFound();
        }
        return View(projects);
    }

    // POST: Projects/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ProjectID,ProjectName,ProjectDescription,ProjectDate,ProjectPic")] Project projects)
    {
        if (id != projects.ProjectID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(projects);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!projectsExists(projects.ProjectID))
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
        return View(projects);
    }

    // GET: Projects/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Projects == null)
        {
            return NotFound();
        }

        var projects = await _context.Projects
            .FirstOrDefaultAsync(m => m.ProjectID == id);
        if (projects == null)
        {
            return NotFound();
        }

        return View(projects);
    }

    // POST: Projects/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Projects == null)
        {
            return Problem("Entity set 'MySiteContext.Projects'  is null.");
        }
        var projects = await _context.Projects.FindAsync(id);
        if (projects != null)
        {
            _context.Projects.Remove(projects);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool projectsExists(int id)
    {
        return (_context.Projects?.Any(e => e.ProjectID == id)).GetValueOrDefault();
    }
}
