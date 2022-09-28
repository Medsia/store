using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Memory
{
    public class InfoRepository : IInfoRepository
    {
        private List<Info> infos = new List<Info>();   //TemporaryData.infos;

        public Info GetInfoById(int id)
        {
            if (id < 0)
                throw new IndexOutOfRangeException("id cannot be less then 0");

            int itemDbId = infos.FindIndex(infosItem => infosItem.Id == id);
            return infos[itemDbId];
        }

        public IEnumerable<Info> GetAllInfo()
        {
            return infos;
        }

        public bool EditExistingItem(Info item)
        {
            int itemDbId = infos.FindIndex(infosItem => infosItem.Id == item.Id);

            if (itemDbId == -1)
                return false;

            infos[itemDbId] = item;
            return true;
        }
    }
}