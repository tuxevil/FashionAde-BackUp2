using SharpArch.Core.DomainModel;

namespace FashionAde.Core.Accounts
{
    public class UserSize : Entity
    {
        private string description;

        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        public UserSize() {}

        public UserSize(int id)
        {
            this.Id = id;
        }
    }
}