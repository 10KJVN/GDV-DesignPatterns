using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CurrencyFactory
{
    private List<CurrencyDefinition> currencies;

    public CurrencyFactory(List<CurrencyDefinition> currencies)
    {
        this.currencies = currencies;
    }

    public CurrencyDefinition CreateCurrency(CurrencyType type)
    {
        CurrencyDefinition currency = currencies.FirstOrDefault(x => x.currencyType == type);

        if (currency == null)
        {
            Debug.LogWarning($"No prefab found for {type}.");

            return null;
        }

        return currency;
    }
}