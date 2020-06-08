using SimpleAccountingSoftware.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SimpleAccountingSoftware.Services
{
    public class MockItemRepository : IItemRepository
    {
        private List<Item> _itemList;
        public MockItemRepository()
        {
            _itemList = new List<Item>()
            {
                new Item() { Id=1, Sku="A1019", Name="Red Lipstick", Cost=3.31M, Retail=6.49M,
                    Location=Location.UpstairsOffice, OnHand=5, BackOrdered=0, Ordered=2, PhotoPath="A1019.jpg" },
                new Item() { Id=2, Sku="R5019", Name="Pen Applicator", Cost=2.15M, Retail=4.99M,
                    Location=Location.BasementOfficeShelf, OnHand=12, BackOrdered=0, Ordered=1, PhotoPath="R5019.jpg" },
                new Item() { Id=3, Sku="C8190", Name="Mary Kay Carry Tote", Cost=9.83M, Retail=23.99M,
                    Location=Location.UpstairsOffice, OnHand=3, BackOrdered=1, Ordered=5, PhotoPath="C8190.jpg" },
                new Item() { Id=4, Sku="A5090", Name="Mascara Bundle", Cost=5.18M, Retail=12.99M,
                    Location=Location.CarryBag, OnHand=8, BackOrdered=0, Ordered=1 },
            };
        }

        public Item Add(Item newItem)
        {
            newItem.Id = _itemList.Max(e => e.Id) + 1;
            _itemList.Add(newItem);
            return newItem;
        }

        public Item Delete(int id)
        {
            Item itemToDelete = _itemList.FirstOrDefault(e => e.Id == id);
            if (itemToDelete != null)
            {
                _itemList.Remove(itemToDelete);
            }
            return itemToDelete;
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _itemList;
        }

        public Item GetItem(int id)
        {
            return _itemList.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<InventoryCount> InventoryCountByLocation()
        {
            // For details on this see LINQ Tutorial from kudvenkat.
            return _itemList.GroupBy(e => e.Location)
                    .Select(g => new InventoryCount()
                    {
                        Location = g.Key.Value,
                        Count = g.Count()
                    }).ToList();
        }

        public IEnumerable<Item> Search(string SearchTerm)
        {
            if (string.IsNullOrEmpty(SearchTerm))
            {
                return _itemList;
            }
            return _itemList.Where(e => e.Name.ToLower().Contains(SearchTerm.ToLower()) ||
                                        e.Sku.ToLower().Contains(SearchTerm.ToLower()));
        }

        public Item Update(Item updatedItem)
        {
            Item item = _itemList.FirstOrDefault(e => e.Id == updatedItem.Id);
            
            if(item != null)
            {
                item.Name = updatedItem.Name;
                item.Sku = updatedItem.Sku;
                item.OnHand = updatedItem.OnHand;
                item.Ordered = updatedItem.Ordered;
                item.BackOrdered = updatedItem.BackOrdered;
                item.Location = updatedItem.Location;
                item.Cost = updatedItem.Cost;
                item.Retail = updatedItem.Retail;
                item.PhotoPath = updatedItem.PhotoPath;
            }

            return item;
        }
    }
}
