using Microsoft.AspNetCore.Mvc;
using SimpleAccountingSoftware.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAccountingSoftware.ViewComponents
{
    public class InventoryCountViewComponent : ViewComponent
    {
        private readonly IItemRepository itemRepository;

        public InventoryCountViewComponent(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public IViewComponentResult Invoke()
        {
            var result = itemRepository.InventoryCountByLocation();
            return View(result);
        }
    }
}
