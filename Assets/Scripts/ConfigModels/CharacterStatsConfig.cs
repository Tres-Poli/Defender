using System;

[Serializable]
public class CharacterConfig
{
    public CharacterType Type { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }
    public float AttackRate { get; set; }
    public float AttackRange { get; set; }
    public float Speed { get; set; }
}

public enum CharacterType
{
    Player = 0, 
    Zombie = 1,
}
