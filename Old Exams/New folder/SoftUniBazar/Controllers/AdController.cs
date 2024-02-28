using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SoftUniBazar.Data;
using SoftUniBazar.Models;
using System.Globalization;
using System.Security.Claims;
using static SoftUniBazar.Data.DataConstants;

namespace SoftUniBazar.Controllers
{
    [Authorize]
    public class AdController : Controller
    {
        private readonly BazarDbContext data;
        public AdController(BazarDbContext context)
        {
            data = context;
        }

        public async Task<IActionResult> All()
        {
            var events = await data.Ads
                .AsNoTracking()
                .Select(a => new AdAllViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Category = a.Category.Name,
                    Description = a.Description,
                    Price = a.Price,
                    Owner = a.Owner.UserName,
                    CreatedOn = a.CreatedOn.ToString(DataFormat),
                    ImageUrl = a.ImageUrl,
                })
                .ToListAsync();

            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            var userId = GetUserId();

            var model = await data.AdsBuyers
                .Where(ad => ad.BuyerId == userId)
                .AsNoTracking()
                .Select(ad => new AdAllViewModel()
                {
                    Id = ad.Ad.Id,
                    CreatedOn = ad.Ad.CreatedOn.ToString(DataFormat),
                    Category = ad.Ad.Category.Name,
                    Description = ad.Ad.Description,
                    Price = ad.Ad.Price,
                    Owner = ad.Ad.Owner.UserName,
                    ImageUrl = ad.Ad.ImageUrl,
                    Name = ad.Ad.Name,
                })
                .ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var userId = GetUserId();


            var adAndBuyer = await data.AdsBuyers
                .FirstOrDefaultAsync(ad => ad.AdId == id && ad.BuyerId == userId);

            var ad = await data.Ads
                .FirstOrDefaultAsync(ad => ad.Id == id);

            if (adAndBuyer == null)
            {
                var newAdAndBuyer = new AdBuyer()
                {
                    BuyerId = userId,
                    AdId = id
                };

                await data.AddRangeAsync(newAdAndBuyer);
                await data.SaveChangesAsync();

                return RedirectToAction("Cart");
            }

            return RedirectToAction("All");
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            string userId = GetUserId();

            var ad = await data.Ads.FirstOrDefaultAsync(ad => ad.Id == id);

            if(ad == null)
            {
                return BadRequest();
            }

            var adBuyer = await data.AdsBuyers.FirstOrDefaultAsync(b => b.BuyerId == userId && b.AdId == id);

            if (adBuyer == null)
            {
                return BadRequest();
            }

            data.AdsBuyers.Remove(adBuyer);
            await data.SaveChangesAsync();

            return RedirectToAction("All");
        }

        [HttpGet]

        public async Task<IActionResult> Add()
        {
            var model = new AdFormViewModel();
            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdFormViewModel model)
        {
            var allCategories = await GetCategories();

            if (!allCategories.Any(c => c.Id == model.CategoryId))
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist!");
            }
            string currentUserId = GetUserId();

            //If something is not valid in the newAd
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entity = new Ad()
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                CategoryId = model.CategoryId,
                CreatedOn = DateTime.Now,
                OwnerId = currentUserId,
            };

            await data.Ads.AddAsync(entity);
            await data.SaveChangesAsync();

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var ad = await data.Ads.FindAsync(id);
            var userId = GetUserId();

            if (ad == null)
            {
                return BadRequest();
            }
            if (ad.OwnerId != userId)
            {
                return Unauthorized();
            }

            var model = new AdFormViewModel()
            {
                Name = ad.Name,
                Description = ad.Description,
                ImageUrl = ad.ImageUrl,
                Price = ad.Price,
                CategoryId = ad.CategoryId,
            };

            model.Categories = await GetCategories();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AdFormViewModel model, int id)
        {
            var ad = await data.Ads.FindAsync(id);
            var userId = GetUserId();

            if (ad == null)
            {
                return BadRequest();
            }
            if (ad.OwnerId != userId)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();
                return View(model);
            }
            
            ad.Name = model.Name;
            ad.Description = model.Description;
            ad.ImageUrl = model.ImageUrl;
            ad.Price = model.Price;
            ad.CategoryId = model.CategoryId;

            await data.SaveChangesAsync();
            return RedirectToAction("All");
        }


        private async Task<IEnumerable<Category>> GetCategories()
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


        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
