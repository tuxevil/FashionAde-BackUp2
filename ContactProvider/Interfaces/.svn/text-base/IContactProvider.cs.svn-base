using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ContactProvider
{
    public interface IContactProvider
    {
        string GetAuthorizationURL();
        void SetToken();
        IList<IContact> GetContacts(int pageNumber, int pageSize, out int totalCount);
        IList<IContact> GetContacts(ISelection selection);
        string Name { get; }
        string FullName { get; }
    }
}
