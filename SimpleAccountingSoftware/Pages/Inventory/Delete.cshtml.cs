using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleAccountingSoftware.Models;
using SimpleAccountingSoftware.Services;

namespace SimpleAccountingSoftware
{
    public class DeleteModel : PageModel
    {
        private readonly IItemRepository itemRepository;

        public DeleteModel(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        [BindProperty]
        public Item Item { get; set; }
        public IActionResult OnGet(int id)
        {
            Item = itemRepository.GetItem(id);

            if (Item == null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();
        }
        public IActionResult OnPost()
        {
            Item deletedItem = itemRepository.Delete(Item.Id);

            if (deletedItem == null)
            {
                return RedirectToPage("/NotFound");
            }

            return RedirectToPage("Index");
        }
    }
}