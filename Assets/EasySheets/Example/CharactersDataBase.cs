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
        protected override void PopulateData(ParsedData data)
        {
            for (int i = 0; i < data.rowCount; i++)
            {
                var characterData = new CharacterData
                {
                    name = data.GetString(i,0),
                    description = data.GetString(i,1),
                    stars = data.GetInt(i,2),
                    attackRate = data.GetFloat(i,3),
                    damage = data.GetFloat(i,4),
                    rarity = data.GetEnum<Rarity>(i,5)
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