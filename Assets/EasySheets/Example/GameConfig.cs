using EasySheets.Core;
using UnityEngine;

namespace EasySheets.Example
{
    [CreateAssetMenu(menuName = "Create GameConfig", fileName = "GameConfig", order = 0)]
    public class GameConfig : ParsedScriptableObject
    {
        public string WinText;
        public string LoseText;
        public int MaxCharactersLevel;
        
        
        protected override void PopulateData(ParsedData data)
        {
            
            Debug.Log("Data populated");
        }
    }
}
