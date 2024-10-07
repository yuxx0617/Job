using Job.Dao.Interface;
using Job.Service.Interface;

namespace Job.Service;

public class TestService : ITestService
{
    private readonly ITestDao _dao;
    public TestService(ITestDao dao)
    {
        _dao = dao;
    }
}