using System.Collections.Generic;

namespace Store
{
    public interface IInfoRepository
    {
        Info GetInfoById(int id);
        IEnumerable<Info> GetAllInfo();
        bool EditExistingItem(Info item);
    }
}
