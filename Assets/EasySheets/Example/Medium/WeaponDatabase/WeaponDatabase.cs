using System.Collections.Generic;
using EasySheets.Core;
using UnityEngine;

namespace EasySheets.Example.Medium
{
    [CreateAssetMenu(menuName = "Create ParseInToSeparateObjects", fileName = "ParseInToSeparateObjects", order = 0)]
    public class WeaponDatabase : ParsedScriptableObject
    {
        public List<WeaponData> Weapons;
        
        protected override void PopulateData(ParsedDataList data)
        {
            if (data.TryGetParsedData(0, out var weaponsData))
            {
                EasyParser.ParseRowsData(weaponsData, Weapons, i =>
                {
                    var weapon = Weapons.Find(x => x.Id == weaponsData.GetString(i, 0));
                    if (weapon == null)
                    {
                        return null;
                    }

                    weapon.Name = weaponsData.GetString(i, 1);
                    weapon.Damage = weaponsData.GetInt(i, 2);
                    weapon.Range = weaponsData.GetFloat(i, 3);
                    weapon.FireRate = weaponsData.GetFloat(i, 4);
                    return weapon;
                }, true);
            }
        }
    }
}
