using Job.AppDBContext;
using Job.Dao.Interface;

namespace Job.Dao;

public class TestDao : ITestDao
{
    private readonly AppDbContext _context;

    public TestDao(AppDbContext context)
    {
        _context = context;
    }
}