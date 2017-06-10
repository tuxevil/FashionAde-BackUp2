using SharpArch.Core.DomainModel;

namespace FashionAde.Core.OutfitEngine
{
    public class ColorBlendingRules
    {
        private bool monotoneColor;
        private bool analogousColor;
        private bool complimentaryColor;
        private bool neutralPrimaryColor;
        private bool neutralSecondaryColor;
        private bool neutralColor;

        /// <summary>
        /// Garments all of one color.
        /// </summary>
        public virtual bool MonotoneColor
        {
            get { return monotoneColor; }
            set { monotoneColor = value; }
        }

        /// <summary>
        /// .  Garments all in a single color family (e.g., red, red-orange, orange, orange-yellow, yellow).
        /// </summary>
        public virtual bool AnalogousColor
        {
            get { return analogousColor; }
            set { analogousColor = value; }
        }

        /// <summary>
        /// For example, an orange garment with a blue garment, or a red garment with a green garment.
        /// </summary>
        public virtual bool ComplimentaryColor
        {
            get { return complimentaryColor; }
            set { complimentaryColor = value; }
        }

        /// <summary>
        /// For example, black with a single primary colored garment.
        /// </summary>
        public virtual bool NeutralPrimaryColor
        {
            get { return neutralPrimaryColor; }
            set { neutralPrimaryColor = value; }
        }

        /// <summary>
        /// For example, brown with aqua.
        /// </summary>
        public virtual bool NeutralSecondaryColor
        {
            get { return neutralSecondaryColor; }
            set { neutralSecondaryColor = value; }
        }

        /// <summary>
        /// For example, black with white, brown with black, navy with beige.
        /// </summary>
        public virtual bool NeutralColor
        {
            get { return neutralColor; }
            set { neutralColor = value; }
        }
    }
}