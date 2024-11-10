using System;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;

namespace EasySheets.Core
{
    [Serializable]
    public class RangeData
    {
        [SerializeField] private string _pageName;
        [SerializeField] private string _range;
        
        [Header("Only for documentation purposes")]
        [TextArea]
        [SerializeField] private string _description;

        public string range => _pageName + "!" + _range;

        public ValueRange valueRange { get; set; }
    }

    
    
}