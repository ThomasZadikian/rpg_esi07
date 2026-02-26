namespace RPG_ESI07.Domain.Entities;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // weapon, armor, accessory, consumable
    public string? Category { get; set; } // potion_hp, potion_mp, etc.
    public string? StatModifiers { get; set; } // JSON: {"strength": 5, "maxHP": 20}
    public int? EffectValue { get; set; } // Pour consommables
    public string? Description { get; set; }
    public int Price { get; set; } = 0;

    // Navigation properties
    public ICollection<PlayerInventory> PlayerInventories { get; set; } = new List<PlayerInventory>();
}