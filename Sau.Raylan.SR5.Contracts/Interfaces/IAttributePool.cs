namespace Sau.Raylan.SR5.Contracts.Interfaces
{
    public interface IAttributePool
    {
        int this[AttributeType type] { get; set; }
        string Display(AttributeType type);
        int LimitValue(LimitType type);
        string LimitDisplay(LimitType type);
    }
}