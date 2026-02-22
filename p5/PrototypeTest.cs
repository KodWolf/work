using System;
using System.Collections.Generic;

public class Weapon : ICloneable
{
    public string Name { get; set; }
    public int Damage { get; set; }

    public Weapon(string name, int damage)
    {
        Name = name;
        Damage = damage;
    }

    public object Clone()
    {
        return new Weapon(Name, Damage);
    }
}

public class Armor : ICloneable
{
    public string Name { get; set; }
    public int Defense { get; set; }

    public Armor(string name, int defense)
    {
        Name = name;
        Defense = defense;
    }

    public object Clone()
    {
        return new Armor(Name, Defense);
    }
}

public class Skill : ICloneable
{
    public string Name { get; set; }
    public int Power { get; set; }

    public Skill(string name, int power)
    {
        Name = name;
        Power = power;
    }

    public object Clone()
    {
        return new Skill(Name, Power);
    }
}

public class Character : ICloneable
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Intelligence { get; set; }
    public Weapon Weapon { get; set; }
    public Armor Armor { get; set; }
    public List<Skill> Skills { get; set; }

    public Character(string name)
    {
        Name = name;
        Health = 100;
        Strength = 10;
        Agility = 10;
        Intelligence = 10;
        Skills = new List<Skill>();
    }

    public object Clone()
    {
        Character clone = new Character(Name);
        clone.Health = Health;
        clone.Strength = Strength;
        clone.Agility = Agility;
        clone.Intelligence = Intelligence;

        if (Weapon != null)
            clone.Weapon = (Weapon)Weapon.Clone();

        if (Armor != null)
            clone.Armor = (Armor)Armor.Clone();

        foreach (Skill skill in Skills)
        {
            clone.Skills.Add((Skill)skill.Clone());
        }

        return clone;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Персонаж: {Name}");
        Console.WriteLine($"Характеристики: Здоровье={Health}, Сила={Strength}, Ловкость={Agility}, Интеллект={Intelligence}");

        if (Weapon != null)
            Console.WriteLine($"Оружие: {Weapon.Name} (Урон {Weapon.Damage})");

        if (Armor != null)
            Console.WriteLine($"Броня: {Armor.Name} (Защита {Armor.Defense})");

        if (Skills.Count > 0)
        {
            Console.WriteLine("Способности:");
            foreach (Skill s in Skills)
                Console.WriteLine($"  - {s.Name} (Сила {s.Power})");
        }
        Console.WriteLine();
    }
}

class PrototypeTest
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Тестирование Prototype ===\n");

        Character original = new Character("Маг");
        original.Health = 80;
        original.Intelligence = 15;
        original.Weapon = new Weapon("Посох", 20);
        original.Armor = new Armor("Мантия", 5);
        original.Skills.Add(new Skill("Огненный шар", 30));
        original.Skills.Add(new Skill("Лечение", 15));

        Console.WriteLine("Оригинал:");
        original.ShowInfo();

        Character clone = (Character)original.Clone();
        clone.Name = "Маг-клон";
        clone.Health = 60;
        clone.Weapon.Damage = 15;
        clone.Skills[0].Power = 25;

        Console.WriteLine("Клон (измененный):");
        clone.ShowInfo();

        Console.WriteLine("Оригинал (не изменился):");
        original.ShowInfo();

        Console.ReadKey();
    }
}