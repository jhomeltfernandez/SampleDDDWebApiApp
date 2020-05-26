using SampleDDDWebApiApp.Models.RequestModel;

namespace SampleDDDWebApiApp.Business.Services
{
    public interface IMoneyService
    {
        void Store(StoreMoneyRequestModel request);
        void Send(SendMoneyRequestModel request);
        void Convert(ConvertMoneyRequestModel request);
    }
}