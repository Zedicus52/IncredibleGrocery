using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] private List<Sprite> _allProducts;
    [SerializeField] private SpriteRenderer[] _iconsProducts;
    private int[] _orderedProductsID;
    private int _amountProducts;
    private const int _maxAmountProducts = 4;
    public int AmountProducts => _amountProducts;
    public int[] OrderedProductID => _orderedProductsID;

    public SpriteRenderer[] GradeIcon => _iconsProducts;

    public void CreateOrder()
    {
        _amountProducts = Random.Range(1,_maxAmountProducts);
        _orderedProductsID = new int[_amountProducts];
        GenerateOrder();
        SetIcons();
    }
    

    private void GenerateOrder()
    {
        Debug.Log("Amount Products " + _amountProducts);
        for (int i = 0; i < _amountProducts; i++)
        {
            bool dontRepeat = true;
            do
            {
                int id = Random.Range(0, _allProducts.Count);
                for (int j = 0; j < _amountProducts; j++)
                {
                    if (id == _orderedProductsID[j])
                    {
                        dontRepeat = false;
                        break;
                    }
                    _orderedProductsID[i] = id;
                }
            } while (dontRepeat);
            Debug.Log("Ordered product " + _orderedProductsID[i]);
        }
    }
    private void SetIcons()
    {
        for (int i = 0; i < _amountProducts; i++)
        {
            _iconsProducts[i].sprite = _allProducts[_orderedProductsID[i]];
        }
    }
}
