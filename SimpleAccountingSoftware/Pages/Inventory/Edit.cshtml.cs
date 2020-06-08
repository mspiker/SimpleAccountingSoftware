using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleAccountingSoftware.Models;
using SimpleAccountingSoftware.Services;

namespace SimpleAccountingSoftware
{
    public class EditModel : PageModel
    {
        /// <summary>
        /// Examples that are not shown in this file: 
        /// - TempData["variable"] is great to pass some data to another page, once
        ///   the data is read it is cleared so the lifecycle is very short, but works 
        ///   well if you just want to hand off some data.  See Part 18 of ASP.NET core
        ///   razor pages tutorial.  
        /// </summary>

        
        private readonly IItemRepository itemRepository;
        
        // Used to get the full path for storing the uploaded photos.  
        private readonly IWebHostEnvironment webHostEnvironment;

        public EditModel(IItemRepository itemRepository,
                         IWebHostEnvironment webHostEnvironment)
        {
            this.itemRepository = itemRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Item Item { get; set; }
        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Item = itemRepository.GetItem(id.Value);
            }
            else
            {
                Item = new Item();
            }
            if (Item == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }

        // Refer to part 16 of ASP.NET core razor page tutorials for an alternate way if you 
        // want the posted item to be available outside the scope of this method.  
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // If a photo is uploaded
                if (Photo != null)
                {
                    // Check to see if the item has an existing photo, if so remove it.
                    if (Item.PhotoPath != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath,
                            "images", Item.PhotoPath);
                        //System.IO.File.Delete(filePath);
                    }

                    // Upload the newly uploaded photo.
                    Item.PhotoPath = ProcessUploadedFile();
                }

                if (Item.Id > 0)
                {
                    // Update the rest of the record.
                    Item = itemRepository.Update(Item);
                } else
                {
                    Item = itemRepository.Add(Item);
                }
                return RedirectToPage("Index");
            }

            // In the event there are validation issues we can re-render the page
            // so the user can correct any errors.
            return Page(); 
        }

        // Used to store and bind the photo selected on the page in the file input control.  
        [BindProperty]
        public IFormFile Photo { get; set; }


        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if (Photo != null) { 
                string uploadsFolder =
                    Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}