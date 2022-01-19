using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IExport
    {
        void Export<T>(List<T> data, string path);
    }
}
