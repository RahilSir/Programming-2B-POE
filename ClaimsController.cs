using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMCS.Data;


public class ClaimsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ClaimsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Assuming you have a method to fetch claims
    public async Task<IActionResult> Index()
    {
        var claims = await _context.Claims.ToListAsync(); // Fetch claims from the database
        return View(claims);
    }

}