using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] private List<Sprite> _allProducts;
    [SerializeField] private SpriteRenderer[] _iconsProducts;
    private int[] _orderedProductsID;
    private int _amountProducts;
    private const int _maxAmountProducts = 4;
    public int AmountProducts { get { return _amountProducts; } }
    public int[] OrderedProductID { get { return _orderedProductsID; } }

    public SpriteRenderer[] GradeIcon { get { return _iconsProducts; } }

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
            int id;
            for (; ;)
            {
                bool dontRepeat = true;
                id = Random.Range(0, _allProducts.Count);
                for (int j = 0; j < _amountProducts; j++)
                {
                    if (id == _orderedProductsID[j])
                    {
                        dontRepeat = false;
                        break;
                    }
                }
                if (dontRepeat)
                {
                    break;
                }
            }
            _orderedProductsID[i] = id;
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
