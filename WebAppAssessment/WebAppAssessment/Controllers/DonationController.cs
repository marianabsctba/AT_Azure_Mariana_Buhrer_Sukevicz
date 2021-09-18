using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Model.Models;
using Infra.Data.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace WebAppAssessment.Controllers
{
    public class DonationController : Controller

    {
        private readonly FinalContext _context;
        private readonly IBlobService _blobService;
        private readonly IConfiguration _configuration;
        private readonly IQueueService _queueService;

        public DonationController(IConfiguration configuration, FinalContext context, IBlobService blobService, IQueueService queueService)
        {
            _context = context;
            _blobService = blobService;
            _configuration = configuration;
            _queueService = queueService;
        }

        // GET: Donation
        public async Task<IActionResult> Index()
        {
            return View(await _context.Donation.ToListAsync());
        }

        // GET: Donation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donation
                .FirstOrDefaultAsync(m => m.Id == id);

            var jsonDonation = JsonConvert.SerializeObject(donation);
            var bytesJsonDonation = UTF8Encoding.UTF8.GetBytes(jsonDonation);
            string jsonDonationBase64 = Convert.ToBase64String(bytesJsonDonation);


            await _queueService.SendAsync(jsonDonationBase64);


            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // GET: Donation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Donation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form, [Bind("Id,DonationName,Description,DateOfRegister,Courier,Quantity,NewOrOld,ImageUri,QuantityView")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                var file = form.Files.SingleOrDefault();
                var streamFile = file.OpenReadStream();
                var uriImage = await _blobService.UploadAsync(streamFile);
                donation.ImageUri = uriImage;

                _context.Add(donation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(donation);
        }

        // GET: Donation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donation.FindAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            return View(donation);
        }

        // POST: Donation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormCollection form, int id, [Bind("Id,DonationName,Description,DateOfRegister,Courier,Quantity,NewOrOld,ImageUri,QuantityView")] Donation donation)
        {
            if (id != donation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var file = form.Files.SingleOrDefault();
              
                    if (file != null)
                    {
                        var newUri = await _context.Donation.FindAsync(id);
                        _context.Remove(newUri);
                        _context.ChangeTracker.DetectChanges();


                        var streamFile = file.OpenReadStream();
                        var uriImage = await _blobService.UpdateAsync((new Uri(newUri.ImageUri)).Segments.Last(), streamFile);
                        donation.ImageUri = uriImage;
                    }

                    _context.Update(donation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonationExists(donation.Id))
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
            return View(donation);
        }

        // GET: Donation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // POST: Donation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donation = await _context.Donation.FindAsync(id);

            await _blobService.DeleteAsync((new Uri(donation.ImageUri)).Segments.Last());

            _context.Donation.Remove(donation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool DonationExists(int id)
        {
            return _context.Donation.Any(e => e.Id == id);
        }
    }
}
