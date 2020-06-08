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
    public class DetailsModel : PageModel
    {
        private readonly IItemRepository itemRepository;

        public DetailsModel(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public Item Item { get; private set; }

        public IActionResult OnGet(int id)
        {
            Item = itemRepository.GetItem(id);

            if(Item == null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();
        }
    }
}