using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactProvider
{
    public class Selection : ISelection
    {
        private bool selectedAll;
        private List<int> selectedIndexs;

        public bool SelectedAll
        {
            get { return selectedAll; }
            set { selectedAll = value; }
        }

        public List<int> SelectedIndexs
        {
            get { return selectedIndexs; }
            set { selectedIndexs = value; }
        }

        public Selection() {}
        public Selection(bool selectedAll, List<int> selectedIndexs)
        {
            this.selectedAll = selectedAll;
            this.selectedIndexs = selectedIndexs;
        }
    }
}
