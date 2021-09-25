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
    [SerializeField] private SpriteRenderer _grade;
    [SerializeField] private Sprite _positiveGrade;
    [SerializeField] private Sprite _negativeGrade;
    [SerializeField] private List<StorageItem> _items;
    [SerializeField] private int _priceOneItem = 10;
    private byte _countSelectedItem = 0;
    private int[] _readyOrderID;
    private bool _correctOrder = false;
    private bool _end = false;
    private float _checkDelay = 0.5f;
    private float _visibleDelay = 1f;
    public bool End => _end;


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
        if(CanSell())
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
    private void CheckProduct(int id)
    {
        bool correct = false;
        for (int i = 0; i < _order.AmountProducts; i++)
        {
            for (int j = 0; j < _readyOrderID.Length; j++)
            {
                if (_order.OrderedProductID[j] == _readyOrderID[id])
                {
                    correct = true;
                    break;
                }
            }
            if(correct)
            {
                break;
            }
        }
        if (correct)
        {
            _markers[id].sprite = _markersIcon[0];
            _icons[id].color = SetAlpha(0.3f);
            _correctOrder = true;
            _wallet.FinalSum = _priceOneItem;
        }
        else
        {
            _markers[id].sprite = _markersIcon[1];
            _icons[id].color = SetAlpha(0.3f);
            _correctOrder = false;
        }
    }

    private Color SetAlpha(float alpha)
    {
        return new Color(1, 1, 1, alpha);
    }

    public void Sell()
    {
        StartCoroutine(CheckOrder());
    }

    private void GradeOrder()
    {
        Nullify();
        if(_correctOrder)
        {
            _grade.sprite = _positiveGrade;
            _wallet.DoubleSum();
        }
        else
        {
            _grade.sprite = _negativeGrade;
        }
    }

    private void Nullify()
    {
        _sellButton.interactable = false;
        for (int i = 0; i < _order.GradeIcon.Length; i++)
        {
            _order.GradeIcon[i].sprite = null;
            _grade.sprite = null;
            _icons[i].color = SetAlpha(1f);
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
        yield return new WaitForSeconds(_visibleDelay);
        for (int i = 0; i < _order.AmountProducts; i++)
        {
            CheckProduct(i);
            yield return new WaitForSeconds(_checkDelay);
        }
        yield return new WaitForSeconds(_visibleDelay);
        _end = true;
        GradeOrder();
        _wallet.UpdateBalance(_wallet.FinalSum);
        yield return new WaitForSeconds(_visibleDelay);
        Nullify();
        _end = false;
        _correctOrder = false;
        _wallet.Nullify();
    }
}
