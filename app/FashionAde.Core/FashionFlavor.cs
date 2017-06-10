using SharpArch.Core.DomainModel;
using System;

namespace FashionAde.Core
{
    [Serializable]
    public class FashionFlavor : Entity
    {
        public FashionFlavor(int id)
        {
            this.Id = id;
        }
        public FashionFlavor(int id, string name)
        {
            this.Id = id;
            this.name = name;
        }

        public FashionFlavor() { }

        private string name;
        
        [DomainSignature]
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}