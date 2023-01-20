using DAL;
using DAL.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApp.MyLibraries.PageModels;

namespace WebApp.Pages.Items;

public class ManageStock : EntityModel<Item>
{
    public ManageStock(RepositoryContext ctx) : base(ctx)
    {
    }
    
    [BindProperty] public int ItemInStockId { get; set; }
    [BindProperty] public string? Location { get; set; }
    [BindProperty] public int? Quantity { get; set; }
    [BindProperty] public bool UpdateItemInStock { get; set; }
    [BindProperty] public bool RemoveItemInStock { get; set; }

    [BindProperty] public ItemInStock ItemInStock { get; set; } = default!;
    [BindProperty] public bool AddItemInStock { get; set; }

    protected override ItemRepository Repository => Ctx.Items;

    public async Task<IActionResult> OnPostAsync()
    {
        if (RemoveItemInStock)
        {
            await Ctx.ItemInStocks.RemoveAsync(ItemInStockId);
            await Ctx.SaveChangesAsync();
            return Reset;
        }
        
        if (AddItemInStock)
        {
            Ctx.ItemInStocks.Add(ItemInStock);
            await Ctx.SaveChangesAsync();
            return Reset;
        }

        if (UpdateItemInStock)
        {
            var itemInStock = await Ctx.ItemInStocks.GetByIdAsync(ItemInStockId);
            if (itemInStock == null) return Reset;
            if (Location != null) itemInStock.Location = Location;
            if (Quantity != null) itemInStock.Quantity = Quantity.Value;
            Ctx.ItemInStocks.Update(itemInStock);
            await Ctx.SaveChangesAsync();
            return Reset;
        }

        return Reset;
    }
}