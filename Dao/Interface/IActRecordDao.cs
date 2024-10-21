using Job.Model;

namespace Job.Dao.Interface;

public interface IActRecordDao
{
    void CreateActRecord(ActRecordModel actRecordModel);
    ActRecordModel GetActRecord(int id);
    List<ActRecordModel> ActRecordList();
    List<int> GetHotRecord();
}