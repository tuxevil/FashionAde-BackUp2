using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactProvider
{
    public interface ISelection
    {
        bool SelectedAll { get; set; }
        List<int> SelectedIndexs { get; set; }
    }
}
