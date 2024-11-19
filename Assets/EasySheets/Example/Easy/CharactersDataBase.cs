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
        
        protected override void PopulateData(ParsedDataList data)
        {
            if(data.TryGetParsedData( 0, out var parsedData))
            {
                playerDataList = EasyParser.ParseRowsData( parsedData, i => new CharacterData
                {
                    name = parsedData.GetString(i,0),
                    description = parsedData.GetString(i,1),
                    stars = parsedData.GetInt(i,2),
                    attackRate = parsedData.GetFloat(i,3),
                    damage = parsedData.GetFloat(i,4),
                    rarity = parsedData.GetEnum<Rarity>(i,5),
                    cost = parsedData.GetInt(i,7)
                });
            }
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
        public int cost;
    }

    public enum Rarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }
}