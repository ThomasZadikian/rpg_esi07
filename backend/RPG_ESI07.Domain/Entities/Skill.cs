namespace RPG_ESI07.Domain.Entities;

public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MPCost { get; set; }
    public int? BaseDamage { get; set; } // Si sort offensif
    public int? HealAmount { get; set; } // Si sort soin
    public string EffectType { get; set; } = string.Empty; // damage, heal, buff, debuff
    public string? ElementType { get; set; } // fire, lightning, neutral
    public string? Description { get; set; }

    // Navigation properties
    public ICollection<PlayerSkill> PlayerSkills { get; set; } = new List<PlayerSkill>();
}