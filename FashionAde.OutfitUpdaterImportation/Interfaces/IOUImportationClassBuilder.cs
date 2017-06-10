using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers.RunTime;

namespace FashionAde.OutfitUpdaterImportation.Interfaces
{
    public interface IOUImportationClassBuilder
    {
        DelimitedClassBuilder CreateClassBuilder(string separator, bool haveHeader);
    }
}
