using SharpArch.Core.DomainModel;
using FashionAde.Core.Clothing;
namespace FashionAde.Core.OutfitEngine
{
    public abstract class RulePair<T> : Entity
    {
        private T from;
        private T to;

        public virtual T FromItem
        {
            get { return from; }
            set { from = value; }
        }

        public virtual T ToItem
        {
            get { return to; }
            set { to = value; }
        }

        public virtual StyleRule StyleRule { get; set; } 
    }

    public class PatternRule : RulePair<PatternType>
    {
    }

    public class ShapeRule: RulePair<ShapeType>
    {
    }

    public class StructureRule : RulePair<StructureType>
    {
    }
}