using System.Collections.Generic;
using FashionAde.Core.Accounts;
using FashionAde.Core.Clothing;
using FashionAde.Core.MVCInteraction;
using SharpArch.Core.PersistenceSupport;

namespace FashionAde.Core.DataInterfaces
{
    public interface IClosetRepository : IRepository<Closet>
    {
        ClosetGarment SaveClosetGarment(ClosetGarment o);
        List<WebClosetGarment> GetWebClosetGarments(RegisteredUser registeredUser);
        ClosetGarment GetClosetGarment(int id);
        IList<ColorFamily> GetColorFamilyList();        
        void RemoveGarmentFromCloset(int closetId, int garmentId);
        IList<Closet> Search(int userId, string searchText, int? styleType, int pageSize, int pageNumber, out int totalCount);
        IList<ClosetGarment> GetGarmentsForUser(RegisteredUser user);
        void ProcessClosetFile(string fileName);
        void CompleteClosetCreation(int closetId);        
    }
}