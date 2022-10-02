using Store.Data.Content;

namespace Store
{
    public interface IInfoRepository
    {
        InfoSO GetJsData();
        void WriteJsData();
    }
}
