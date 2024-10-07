namespace Job.ViewModel;

public class ResultViewModel
{
    public ResultViewModel()
    {
        isSuccess = true;
    }

    public ResultViewModel(string message)
    {
        isSuccess = false;
        errorMessage = message;
    }

    public bool isSuccess { get; set; }
    public string errorMessage { get; set; }
}

public class ResultViewModel<T> : ResultViewModel
{
    public ResultViewModel() : base()
    {

    }

    public ResultViewModel(string message) : base(message)
    {

    }

    public T result { get; set; }
}
