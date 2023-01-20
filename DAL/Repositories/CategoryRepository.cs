using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class CategoryRepository : AbstractDbRepository<Category>
{
    public CategoryRepository(AppDbContext dbContext, RepositoryContext repoContext) : base(dbContext, repoContext)
    {
    }

    protected override DbSet<Category> ThisDbSet => DbContext.Categories;
}