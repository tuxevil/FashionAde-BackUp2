using SharpArch.Core.DomainModel;
using System;

namespace FashionAde.Core
{
    [Serializable]
    public class EventType : Entity
    {
        #region Constructors

        public EventType() { }
        public EventType(int id) { this.Id = id; }

        #endregion

        private string name;
        private string image;
        private string description;
        private int binaryNumber;
        private string shortName;

        [DomainSignature]
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual string Image
        {
            get { return image; }
            set { image = value; }
        }

        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        public virtual int BinaryNumber
        {
            get { return binaryNumber; }
            set { binaryNumber = value; }
        }

        public virtual string ShortName
        {
            get { return shortName; }
            set { shortName = value; }
        }
    }
}