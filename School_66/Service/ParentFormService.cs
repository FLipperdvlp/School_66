using School_66.DataBase;
using School_66.Entities;
using School_66.Interface;
using Microsoft.EntityFrameworkCore;

namespace School_66.Service;

public class ParentFormService : IParentFormService
{
    private readonly AppDbContext _context;

    public ParentFormService(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Parent> CreateFormForParent(Parent parent)
    {
        _context.Parents.Add(parent);
        await _context.SaveChangesAsync(); 
        return parent;
    }
}
