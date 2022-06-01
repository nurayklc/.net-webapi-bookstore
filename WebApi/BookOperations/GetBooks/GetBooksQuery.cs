using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    private readonly _context;
    public class GetBooksQuery(BookStoreDbContext dbContext)
    {
        _context = dbContext;
    }
}