using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Storage : MonoBehaviour
{
    [SerializeField] private Order _order;
    [SerializeField] private Button _sellButton;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private List<Sprite> _allProducts;
    [SerializeField] private List<Sprite> _markersIcon;
    [SerializeField] private List<SpriteRenderer> _icons;
    [SerializeField] private List<SpriteRenderer> _markers;
    [SerializeField] private List<Sprite> _grades;
    [SerializeField] private List<StorageItem> _items;
    [SerializeField] private int _priceOneItem = 10;
    private byte _countSelectedItem = 0;
    private int[] _readyOrderID;
    private bool _correctOrder = false;
    private bool _end = false;
    public bool End { get { return _end; } }

    

    public void Initialization()
    {
        _countSelectedItem = 0;
        _readyOrderID = new int[_order.AmountProducts];
        for (int i = 0; i < _readyOrderID.Length; i++)
        {
            _readyOrderID[i] = -255;
        }
    }
   
    private bool CanSell()
    {
        if(_countSelectedItem == _order.AmountProducts)
        {
            return true;
        }
        return false;
    }
    public bool CanSelect => !CanSell();

    

    public void TrySell()
    {
        if(CanSell() == true)
        {
            _sellButton.interactable = true;
            SetIcons();
        }
        else
        {
            _sellButton.interactable = false;
        }
    }
    private void SetIcons()
    {
        for (int i = 0; i < _readyOrderID.Length; i++)
        {
            _icons[i].sprite = _allProducts[_readyOrderID[i]];
        }
    }

    public void SelectItem(int ID)
    {
        for (int i = 0; i < _readyOrderID.Length; i++)
        {
            if(_readyOrderID[i]==-255)
            {
                _readyOrderID[i] = ID;
                break;
            }
        }
        _countSelectedItem += 1;
    }

    public void UnSelectItem(int ID)
    {
        for (int i = 0; i < _readyOrderID.Length; i++)
        {
            if (_readyOrderID[i] == ID)
            {
                _readyOrderID[i] = -255;
                break;
            }
        }
        _countSelectedItem -= 1;
    }
    private void CheckProduct(int ID)
    {
        bool right = false;
        for (int i = 0; i < _order.AmountProducts; i++)
        {
            for (int j = 0; j < _readyOrderID.Length; j++)
            {
                if (_order.OrderedProductID[j] == _readyOrderID[ID])
                {
                    right = true;
                    break;
                }
            }
            if(right)
            {
                break;
            }
        }
        if (right == true)
        {
            _markers[ID].sprite = _markersIcon[0];
            _icons[ID].color = new Color(255, 255, 255, 0.3f);
            _correctOrder = true;
            _wallet.FinalSum = _priceOneItem;
        }
        else
        {
            _markers[ID].sprite = _markersIcon[1];
            _icons[ID].color = new Color(255, 255, 255, 0.3f);
            _correctOrder = false;
        }
    }

    public void Sell()
    {
        StartCoroutine(CheckOrder());
    }

    private void GradeOrder()
    {
        Nullify();
        if(_correctOrder == true)
        {
            _order.GradeIcon[1].sprite = _grades[0];
            _wallet.DoubleSum();
        }
        else
        {
            _order.GradeIcon[1].sprite = _grades[1];
        }
    }

    private void Nullify()
    {
        _sellButton.interactable = false;
        for (int i = 0; i < _order.GradeIcon.Length; i++)
        {
            _order.GradeIcon[i].sprite = null;
            _icons[i].color = new Color(255, 255, 255, 1);
            _icons[i].sprite = null;
            _markers[i].sprite = null;
        }
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].NullifyItems();
        }
        
    }

    private IEnumerator CheckOrder()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < _order.AmountProducts; i++)
        {
            CheckProduct(i);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1f);
        _end = true;
        GradeOrder();
        _wallet.UpdateBalance(_wallet.FinalSum);
        yield return new WaitForSeconds(1f);
        Nullify();
        _end = false;
        _correctOrder = false;
        _wallet.Nullify();
    }
}
