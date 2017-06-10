using System.Collections.Generic;

namespace FashionAde.Core
{
    public interface IClosetSelection
    {
        ICollection<Garment> AddedGarments { get; }
        ICollection<Garment> WhishedGarments { get; }

        void AddGarment(Garment g);
        void RemoveGarment(Garment g);
        void SetGarments(ICollection<Garment> garments);

        void AddWishGarment(Garment g);
        void RemoveWishGarment(Garment g);
        void SetWishedGarments(ICollection<Garment> garments);
    }
}