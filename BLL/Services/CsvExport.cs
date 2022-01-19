using BLL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
namespace BLL.Services
{
    public class CsvExport : IExport
    {
        public void Export<T>(List<T> data, string name)
        {
            var lines = new List<string>();
            IEnumerable<PropertyDescriptor> props = TypeDescriptor.GetProperties(typeof(T)).OfType<PropertyDescriptor>();
            var header = string.Join(",", props.Select(x => x.Name));
            lines.Add(header);
            var valueLines = data.Select(row => string.Join(",", header.Split(',').Select(a => row.GetType().GetProperty(a).GetValue(row, null))));
            lines.AddRange(valueLines);
            File.WriteAllLines("wwwroot/" + name + ".csv", lines.ToArray());
        }
    }
}
