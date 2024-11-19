using UnityEngine;

namespace EasySheets.Example.Medium
{
    [CreateAssetMenu(menuName = "Create WeaponData", fileName = "WeaponData", order = 0)]
    public class WeaponData : ScriptableObject
    {
        public string Id;
        public string Name;
        public int Damage;
        public float Range;
        public float FireRate;
    }
}