using UnityEngine;

[CreateAssetMenu(menuName = "Loot/Currency Definition")]
public class CurrencyDefinition : ScriptableObject
{
    public CurrencyType currencyType;

    public GameObject prefab;
}