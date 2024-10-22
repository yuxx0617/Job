using Job.ImportModel;
using Job.ViewModel;

namespace Job.Service.Interface;

public interface IActRecordService
{
    ResultViewModel<List<ActRecordViewModel>> ActRecordList();
    ResultViewModel<ActRecordViewModel> GetActRecord(int r_id);
}