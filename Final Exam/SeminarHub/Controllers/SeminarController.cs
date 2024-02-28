using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
using SeminarHub.Models;
using System.Security.Claims;
using static SeminarHub.Data.DataConstants;

namespace SeminarHub.Controllers
{
    public class SeminarController : Controller
    {
        private readonly SeminarHubDbContext data;
        public SeminarController(SeminarHubDbContext context)
        {
            data = context;
        }
        public async Task<IActionResult> All()
        {
            var seminars = await data.Seminars
                .AsNoTracking()
                .Select(s => new AllSeminarsViewModel
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    Category = s.Category.Name,
                    Organizer = s.Organizer.UserName,
                    DateAndTime = s.DateAndTime.ToString(DateFormat),
                })
                .ToListAsync();

            return View(seminars);
        }



        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        private async Task<IEnumerable<Category>> GetTypes()
        {
            return await data.Categories
                .AsNoTracking()
                .Select(t => new Category
                {
                    Id = t.Id,
                    Name = t.Name,
                })
                .ToListAsync();

        }
    }
}
