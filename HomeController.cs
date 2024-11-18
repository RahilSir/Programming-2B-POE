using Microsoft.AspNetCore.Mvc;
using test.Data; // Ensure this namespace is included
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using test.Models;
using test.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Welcome page
    public IActionResult Welcome()
    {
        return View(new LoginViewModel());
    }

    // POST: Handle login (redirect to Index after form submission)
    //[HttpPost]
    //public IActionResult Login(LoginViewModel model)
    //{

    //    if (model.Role == "Academic Manager")
    //    {
    //        return RedirectToAction("Index"); // Show claims table for Academic Manager
    //    }

    //    if (model.Role == "Lecturer")
    //    {
    //        return RedirectToAction("LecturerDashboard"); // Redirect to lecturer dashboard
    //    }



    //    // Redirect to other pages for Lecturer or Programme Coordinator if needed
    //    return View("Welcome"); // Example for other roles


    //}

    // POST: Handle login (redirect to Index after form submission)
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        // Validate the model state
        if (!ModelState.IsValid)
        {
            return View("Welcome", model); // Return the welcome view with model errors
        }

        // Fetch the user from the database
        var user = await GetUserByUsernameAsync(model.Username);

        // Check if the user exists
        if (user == null)
        {
            ModelState.AddModelError("", "Invalid username or password.");
            return View("Welcome", model); // Return the welcome view with error message
        }

        // Check if the user is a Manager and validate password for them
        if (user.Role == "Manager")
        {
            if (!VerifyPassword(model.Password, user.Password))
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View("Welcome", model); // Return the welcome view with error message
            }

            // Sign in the manager
            await SignInUserAsync(user);
            return RedirectToAction("Index"); // Redirect to manager's dashboard (Index)
        }

        // If the user is a Lecturer, skip password verification and allow login
        if (user.Role == "lecturer")
        {
            // Sign in the lecturer without password check
            await SignInUserAsync(user);
            return RedirectToAction("LecturerDashboard"); // Redirect to lecturer dashboard
        }

        // Check for Programme Coordinator role and sign them in
        if (user.Role == "coordinator") // Ensure there's no space before the role
        {
            // Sign in the coordinator
            await SignInUserAsync(user);
            return RedirectToAction("Index"); // Redirect to coordinator's dashboard (Index)
        }

        // Return the welcome view with error message if role doesn't match
        ModelState.AddModelError("", "Invalid user role.");
        return View("Welcome", model);
    }
    // view lecturer claims
    public async Task<IActionResult> MyClaims()
    {
        var username = User.Identity.Name; // Get the logged-in user's username
        var claims = await _context.Claims
                                    .Where(c => c.LecturerName == username)
                                    .ToListAsync(); // Fetch claims where the lecturer name matches the username

        return View(claims); // Return the claims to the view
    }







    // verification
    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
    private bool VerifyPassword(string inputPassword, string storedPassword)
    {
        // Implement your password verification logic here
        // This can be a simple string comparison or hashing check based on your storage method
        return inputPassword == storedPassword; // Adjust this logic as needed
    }
    private async Task SignInUserAsync(User user)
    {
        // Use the fully qualified name for Claim to avoid ambiguity
        var claims = new List<System.Security.Claims.Claim>
    {
        new System.Security.Claims.Claim(ClaimTypes.Name, user.Username),
        new System.Security.Claims.Claim(ClaimTypes.Role, user.Role)
    };

        var claimsIdentity = new ClaimsIdentity(claims, "login");

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
    }












    public IActionResult LecturerDashboard()
    {
        return View(); // Return the Lecturer Dashboard View
    }




    [HttpPost]
    public async Task<IActionResult> SubmitClaim(test.Models.Claim claim, IFormFile supportingDocument)
    {


        if (claim.HoursWorked <= 40 && claim.HourlyRate <= 300)
        {
            claim.Status = "Approved";
        }
        else
        {
            claim.Status = "Pending";
        }

        try
        {
            // Remove validation errors for 'Status' and 'SupportingDocument' if they exist
            ModelState.Remove(nameof(claim.Status));
            ModelState.Remove(nameof(claim.SupportingDocument));
            claim.CreatedAt = DateTime.Now;

            // Model validation check
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                ViewBag.Message = "There are validation errors: " + string.Join(", ", errors);
                return View("LecturerDashboard", claim);
            }

            // Handle file upload for supporting document
            if (supportingDocument != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                // Ensure the directory exists
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath); // Create the directory if it doesn't exist
                }

                var filePath = Path.Combine(uploadsFolderPath, supportingDocument.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await supportingDocument.CopyToAsync(stream);
                }
                claim.SupportingDocument = "/uploads/" + supportingDocument.FileName;
            }

            // Set status and created date

            claim.CreatedAt = DateTime.Now;


            // Add claim to the database and save changes
            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();

            ViewBag.Message = "Claim submitted successfully!";
        }
        catch (Exception ex)
        {
            var innerExceptionMessage = ex.InnerException?.Message ?? "No inner exception available.";
            ViewBag.Message = $"Error submitting the claim: {ex.Message}. Inner Exception: {innerExceptionMessage}";
        }

        return View("LecturerDashboard", claim);
    }



    [HttpPost]
    [Authorize(Roles = "Manager,coordinator")]

    public async Task<IActionResult> ApproveClaim(int id)
    {
        var claim = await _context.Claims.FindAsync(id);

        if (claim == null)
        {
            return NotFound();
        }

        // Update the claim status to "Approved"
        claim.Status = "Approved";
        _context.Update(claim);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }


    [HttpPost]
    [Authorize(Roles = "Manager,coordinator")]
    public async Task<IActionResult> RejectClaim(int id)
    {
        // Fetch the claim from the database by ID
        var claim = await _context.Claims.FindAsync(id);

        if (claim == null)
        {
            return NotFound(); // Claim doesn't exist
        }

        // Update the claim status to "Rejected"
        claim.Status = "Rejected";
        _context.Update(claim);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index"); // Redirect to claims list
    }



    // hr dashboard
    public async Task<IActionResult> HRDashboard()
    {
        var approvedClaims = await _context.Claims
            .Where(c => c.Status == "Approved") // Ensure the status is correctly matched
            .ToListAsync();


        var totalAmount = approvedClaims.Sum(c => c.TotalClaim); // Adjust if you need to sum another field



        var users = _context.Users.ToList();


        var viewModel = new HRDashboardViewModel
        {
            ApprovedClaims = approvedClaims,
            Users = users
        };

        // Pass the ViewModel to the view
        return View(viewModel);




    }




    // GET: Display pending claims 
  


    public async Task<IActionResult> Index()
    {
        
        var pendingClaims = await _context.Claims
            .Where(c => c.Status == "Pending")
            .ToListAsync();

        return View(pendingClaims);
    }



}

