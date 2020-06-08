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
    public class IndexModel : PageModel
    {
        
        private readonly IItemRepository itemRepository;

        public IndexModel(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }
        
        public IEnumerable<Item> Items { get; set; }

        public void OnGet()
        {
            Items = itemRepository.Search(SearchTerm);
        }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        
        
        
    }
}