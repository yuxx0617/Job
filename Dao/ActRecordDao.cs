using Job.AppDBContext;
using Job.Dao.Interface;
using Job.Model;
using Job.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Job.Dao;

public class ActRecordDao : IActRecordDao
{
    private readonly AppDbContext _context;

    public ActRecordDao(AppDbContext context)
    {
        _context = context;
    }
    #region 新增活動紀錄
    public void CreateActRecord(ActRecordModel actRecordModel)
    {
        _context.ActRecord.Add(actRecordModel);
        _context.SaveChanges();
    }
    #endregion
    #region 取得指定活動紀錄
    public ActRecordModel GetActRecord(int id)
    {
        return _context.ActRecord.FirstOrDefault(a => a.r_id == id);
    }
    #endregion
    #region 取得所有活動紀錄
    public List<ActRecordModel> ActRecordList()
    {
        return _context.ActRecord.ToList();
    }
    #endregion
}