using SimpleAccountingSoftware.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleAccountingSoftware.Services
{
    public interface IItemRepository
    {
        IEnumerable<Item> Search(string SearchTerm);
        IEnumerable<Item> GetAllItems();
        Item GetItem(int id);
        Item Update(Item updatedItem);
        Item Add(Item newItem);
        Item Delete(int id);
        IEnumerable<InventoryCount> InventoryCountByLocation();
    }
}
