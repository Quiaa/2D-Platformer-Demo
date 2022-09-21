using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Stats")]
public class Stats : ScriptableObject
{
    public string Name;
    public CharacterAttributes characterAttributes;
    public CharacterExperience characterExperience;
    //public Sprite Image;
    public class CharacterExperience
    {
        int Level;

        public CharacterExperience(int baseLevel)
        {
            this.Level = baseLevel; 
        }
        public void LevelUp(int Level)
        {
            if (this.Level != 100)
            {
                this.Level++;
            }
        }
        public int GetLevel()
        {
            return this.Level;
        }
    }
    public class CharacterAttributes
    {
        public int baseDamage = 50;
        public int baseHP = 200;
        public int Speed = 5;
        public int HP;
        public int Damage;
    }
    void OnEnable()
    {
        characterExperience = new CharacterExperience(1);

        characterAttributes = new CharacterAttributes();
        characterAttributes.Damage = characterAttributes.baseDamage + (characterAttributes.baseDamage / 10) * characterExperience.GetLevel();
        characterAttributes.HP = characterAttributes.baseHP + (characterAttributes.baseHP / 10) * characterExperience.GetLevel();
    }
}