using System;
using System.Collections.Generic;
using EasySheets.Core;
using UnityEngine;

namespace EasySheets.Example.Hard.TalentDatabase
{
    [CreateAssetMenu(menuName = "Create TalentsDatabase", fileName = "TalentsDatabase", order = 0)]
    public class TalentsDatabase : ParsedScriptableObject
    {
        public List<Talent> Talents = new List<Talent>();
        protected override void PopulateData(ParsedDataList data)
        {
            if (data.TryGetParsedData(0, out var talentsData))
            {
                EasyParser.ParseSubRowsData(talentsData, Talents, (row, subRow, index) =>
                {
                    var talent = new Talent
                    {
                        Id = talentsData.GetString(row, 0),
                        Name = talentsData.GetString(row, 1),
                        Chance = talentsData.GetFloat(row, 2),
                        Cooldown = talentsData.GetFloat(row, 3),
                        Duration = talentsData.GetFloat(row, 4),
                        Cost = talentsData.GetInt(row, 5),
                        Actions = talentsData.GetEnumList<ActionType>(row, 6),
                        Conditions = new List<Condition>(),
                        Effects = new List<Effect>()
                    };
                    if (subRow)
                    {
                        talent = Talents[index];
                        talent.Conditions.Add(new Condition
                        {
                            Type = talentsData.GetEnum<ConditionType>( row, 7),
                            Value = talentsData.GetFloat(row, 8)
                        });
                        talent.Effects.Add(new Effect
                        {
                            Type = talentsData.GetEnum<EffectType>( row, 9),
                            Value = talentsData.GetFloat(row, 10)
                        });
                    }
                    return talent;
                });
            }
        }
    }
    
    [Serializable]
    public class Talent
    {
        public string Id;
        public string Name;
        public float Chance;
        public float Cooldown;
        public float Duration;
        public int Cost;
        
        public List<Condition> Conditions;
        public List<ActionType> Actions;
        public List<Effect> Effects;
    }
    [Serializable]
    public class Condition
    {
        public ConditionType Type;
        public float Value;
    }
    [Serializable]
    public class Effect
    {
        public EffectType Type;
        public float Value;
    }
    

    
    public enum ConditionType
    {
        None,
        IfMoreHp,
        IfLessHp
    }
    
    public enum ActionType
    {
        None,
        Always,
        FightStart,
        AfterAttack,
        AfterDefend
    }
    
    public enum EffectType
    {
        None,
        Damage,
        Heal,
        Buff,
        Debuff
    }
}
