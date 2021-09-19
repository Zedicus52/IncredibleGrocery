using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageItem : MonoBehaviour
{
    private Image _image;
    private bool _isSelected = false;
    [SerializeField] private Image _marker;
    [SerializeField] private Storage _storage;
    [SerializeField] private int _id;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AudioClip _selectClip;
    private void Awake()
    {
        _image = GetComponent<Image>();
    }


    public void NullifyItems()
    {
        _isSelected = false;
        _image.color = new Color(255, 255, 255, 1);
        _marker.enabled = false ;
    }
   

    public void SelectItem(int ID)
    {
        _isSelected = !_isSelected;
        if (_storage.CanSelect)
        {
            if (_isSelected == true)
            {
                _image.color = new Color(255, 255, 255, 0.3f);
                _storage.SelectItem(ID);
                _audioManager.PlayEffect(_selectClip);
            }
            else if (_isSelected == false)
            {
                _image.color = new Color(255, 255, 255, 1);
                _storage.UnSelectItem(ID);
                _audioManager.PlayEffect(_selectClip);
            }
            _marker.enabled = _isSelected;
        }
        else if(_storage.CanSelect == false && _marker.enabled == true)
        {
            _image.color = new Color(255, 255, 255, 1);
            _storage.UnSelectItem(ID);
            _audioManager.PlayEffect(_selectClip);
            _marker.enabled = false;
        }
    }
}
