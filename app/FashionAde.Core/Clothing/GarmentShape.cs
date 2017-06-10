namespace FashionAde.Core.Clothing
{
    public enum ShapeType
    {
        Slim100 = 1,
        Slim200 = 2,
        Slim300 = 3,
        Slim400 = 4,
        Slim500 = 5,
        Slim600Plus = 6,
        Fitted100 = 11,
        Fitted200 = 12,
        Fitted300 = 13,
        Fitted400 = 14,
        Fitted500 = 15,
        Fitted600Plus = 16,
        Loose100 = 21,
        Loose200 = 22,
        Loose300 = 23,
        Loose400 = 24,
        Loose500 = 25,
        Loose600Plus = 26,
    }

    public class Shape : Tag
    {
        public virtual ShapeType Type { get; set; }
    }
}