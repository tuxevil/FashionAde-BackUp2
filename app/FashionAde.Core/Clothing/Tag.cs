using SharpArch.Core.DomainModel;

namespace FashionAde.Core.Clothing
{
    public abstract class Tag : Entity
    {
        private string description;
        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}