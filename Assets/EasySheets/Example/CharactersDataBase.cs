using System;
using System.Collections.Generic;
using EasySheets.Core;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;

namespace EasySheets.Example
{
    [CreateAssetMenu(menuName = "Create CharactersDataBase", fileName = "CharactersDataBase", order = 0)]
    public class CharactersDataBase : ParsedScriptableObject
    {
        public List<CharacterData> playerDataList = new List<CharacterData>();
        protected override void PopulateData(List<ValueRange> data)
        {
            var values = data[0].Values;
            for (int i = 0; i < values.Count; i++)
            {
                var characterData = new CharacterData
                {
                    name = values[i][0].ToString(),
                    description = values[i][1].ToString(),
                    stars = int.Parse(values[i][2].ToString()),
                    attackRate = float.Parse(values[i][3].ToString(), System.Globalization.CultureInfo.InvariantCulture),
                    damage = float.Parse(values[i][4].ToString(), System.Globalization.CultureInfo.InvariantCulture),
                    rarity = EnumParser.ParseEnum<Rarity>(values[i][5].ToString())
                };
                
                if (playerDataList.Count-1 < i)
                {
                    playerDataList.Add(characterData);
                }
                else
                {
                    playerDataList[i] = characterData;
                }
            }

            Debug.Log("Data populated");
        }
    }
    
    [Serializable]
    public class CharacterData
    {
        public string name;
        public string description;
        public int stars;
        public float attackRate;
        public float damage;
        public Rarity rarity;
    }

    public enum Rarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }
}