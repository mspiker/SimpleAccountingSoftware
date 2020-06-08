using SimpleAccountingSoftware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleAccountingSoftware.Services
{
    
    /// <summary>
    /// To change your project to use this implementation of IItemRepository you will 
    /// make one change in the Startup Class of your ASP.NET Core Razor Page project 
    /// in the ConfigureServices method as follows:
    /// 
    /// services.AddScoped<IItemRepository, SQLItemRepository>();
    /// </summary>
    public class SQLItemRepository : IItemRepository
    {
        private readonly AppDbContext context;

        public SQLItemRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Item Add(Item newItem)
        {
            context.Items.Add(newItem);
            context.SaveChanges();
            return newItem;
        }

        public Item Delete(int id)
        {
            Item item = context.Items.Find(id);
            if (item != null)
            {
                context.Items.Remove(item);
                context.SaveChanges();
            }
            return item;
        }

        public IEnumerable<Item> GetAllItems()
        {
            return context.Items;
        }

        public Item GetItem(int id)
        {
            return context.Items.Find(id);
        }

        public IEnumerable<InventoryCount> InventoryCountByLocation()
        {
            // For details on this see LINQ Tutorial from kudvenkat.
            return context.Items.GroupBy(e => e.Location)
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
                return context.Items;
            }
            return context.Items.Where(e => e.Name.ToLower().Contains(SearchTerm.ToLower()) ||
                                        e.Sku.ToLower().Contains(SearchTerm.ToLower()));
        }

        public Item Update(Item updatedItem)
        {
            var item = context.Items.Attach(updatedItem);
            item.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedItem;
        }
    }
}
