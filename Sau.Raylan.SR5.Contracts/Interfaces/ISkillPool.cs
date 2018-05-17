namespace Sau.Raylan.SR5.Contracts.Interfaces
{
    public interface ISkillPool
    {
        int this[SkillType type] { get; set; }
        string Display(SkillType type);
    }
}
