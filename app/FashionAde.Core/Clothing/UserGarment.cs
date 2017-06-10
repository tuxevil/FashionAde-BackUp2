using FashionAde.Core.Accounts;

namespace FashionAde.Core.Clothing
{
    public class UserGarment : Garment
    {
        private RegisteredUser user;
        private GarmentVisibility visibility;
        private ApprovalStatus approvalStasus = ApprovalStatus.Pending;

        public virtual ApprovalStatus ApprovalStatus
        {
            get { return approvalStasus; }
            set { approvalStasus = value; }
        }

        public virtual RegisteredUser User
        {
            get { return user; }
            set { user = value; }
        }

        public virtual GarmentVisibility Visibility
        {
            get { return visibility; }
            set { visibility = value; }
        }

        public virtual void SetVisibility(GarmentVisibility visibility)
        {
            this.visibility = visibility;
            if (visibility == GarmentVisibility.Public)
                this.ApprovalStatus = ApprovalStatus.Pending;
            else
                this.ApprovalStatus = ApprovalStatus.Approved;
        }

        public virtual void Approve()
        {
            this.approvalStasus = ApprovalStatus.Approved;
        }

        public virtual void Reject()
        {
            this.approvalStasus = ApprovalStatus.Rejected;
        }
 
    }

    public class MasterGarment : Garment
    {
        public MasterGarment(int id)
        {
            this.Id = id;
        }

        public MasterGarment() { }
    }
}