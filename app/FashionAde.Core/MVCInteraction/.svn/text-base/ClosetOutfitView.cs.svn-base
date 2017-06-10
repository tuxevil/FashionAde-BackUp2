using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.OutfitCombination;
using FashionAde.Core.OutfitEngine;
using FashionAde.Core.ThirdParties;
using SharpArch.Core.DomainModel;

namespace FashionAde.Core.MVCInteraction
{
    public class ClosetOutfitView
    {
        private ClosetOutfit closetOutfit;
        private float averageFriendRating;
        private float averageUserRating;
        private float averageWearRating;
        private float editorRating;
        private DateTime wornDate;
        private string location;
        private float myRating;
        private PreCombination preCombination;
        private bool disabled;        
        private bool showAddToMyCloset;
        private List<ClosetOutfitGarmentView> _outfitGarments = new List<ClosetOutfitGarmentView>();
        private OutfitUpdater outfitUpdater;
        private int eventTypes;
        private string shortEventTypes;

        public virtual int Id { get; set; }

        public virtual float AverageFriendRating
        {
            get { return averageFriendRating; }
            set { averageFriendRating = value; }
        }

        public virtual float AverageUserRating
        {
            get { return averageUserRating; }
            set { averageUserRating = value; }
        }

        public virtual float AverageWearRating
        {
            get { return averageWearRating; }
            set { averageWearRating = value; }
        }

        public virtual float EditorRating
        {
            get { return editorRating; }
            set { editorRating = value; }
        }

        public virtual DateTime WornDate
        {
            get { return wornDate; }
            set { wornDate = value; }
        }

        public virtual string Location
        {
            get { return location; }
            set { location = value; }
        }

        public virtual List<ClosetOutfitGarmentView> OutfitGarments
        {
            get { return _outfitGarments; }
            set { _outfitGarments = value; }
        }

        public virtual int ClosetOutfitId
        {
            get { return closetOutfit.Id; }
        }

        public virtual ClosetOutfit ClosetOutfit
        {
            get { return closetOutfit; }
            set { closetOutfit = value; }
        }

        public virtual float MyRating
        {
            get { return myRating; }
            set { myRating = value; }
        }

        public virtual PreCombination PreCombination
        {
            get { return preCombination; }
            set { preCombination = value; }
        }

        public virtual bool Disabled
        {
            get { return disabled; }
            set { disabled = value; }
        }

        public virtual bool ShowAddToMyCloset
        {
            get { return showAddToMyCloset; }
            set { showAddToMyCloset = value; }
        }

        public virtual OutfitUpdater OutfitUpdater
        {
            get { return outfitUpdater; }
            set { outfitUpdater = value; }
        }

        public virtual string ShortEventTypes
        {
            get { return shortEventTypes; }
            set { shortEventTypes = value; }
        }

        public virtual int EventTypes
        {
            get { return eventTypes; }
            set { eventTypes = value; }
        }

        public ClosetOutfitView(int closetOutfit, float averageFriendRating, float averageUserRating, float averageWearRating, float editorRating, DateTime wornDate, string location, float myRating, int preCombination, int eventTypes)
        {
            this.Id = closetOutfit;
            this.closetOutfit = new ClosetOutfit(closetOutfit);
            this.averageFriendRating = averageFriendRating;
            this.averageWearRating = averageWearRating;
            this.averageUserRating = averageUserRating;
            this.editorRating = editorRating;
            this.wornDate = wornDate;
            this.location = location;
            this.myRating = myRating;
            this.preCombination = new PreCombination(preCombination);
            this.eventTypes = eventTypes;
        }
    }
    
}
