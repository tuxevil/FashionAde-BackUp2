using System.Collections.Generic;

namespace FashionAde.Core
{
    public interface IUserDiscriminator
    {
        ICollection<EventType> EventTypes { get; }
        ICollection<UserFlavor> UserFlavors { get; }
        string ZipCode { get; }
    }
}