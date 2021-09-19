using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] private List<Sprite> _allProducts;
    [SerializeField] private SpriteRenderer[] _iconsProducts;
    private int[] _orderedProductsID;
    private int _amountProducts;
    public int AmountProducts { get { return _amountProducts; } }
    public int[] OrderedProductID { get { return _orderedProductsID; } }

    public SpriteRenderer[] GradeIcon { get { return _iconsProducts; } }

    public void CreateOrder()
    {
        _amountProducts = Random.Range(1,4);
        _orderedProductsID = new int[_amountProducts];
        GenerateOrder();
        SetIcons();
    }
    

    private void GenerateOrder()
    {
        Debug.Log("Amount Products " + _amountProducts);
        for (int i = 0; i < _amountProducts; i++)
        {
            int ID;
            for (; ; )
            {
                bool dontRepeat = true;
                ID = Random.Range(0, _allProducts.Count);
                for (int j = 0; j < _amountProducts; j++)
                {
                    if (ID == _orderedProductsID[j])
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
            _orderedProductsID[i] = ID;
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
