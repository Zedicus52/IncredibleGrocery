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
   

    public void SelectItem(int id)
    {
        _isSelected = !_isSelected;
        if (_storage.CanSelect)
        {
            if (_isSelected)
            {
                _image.color = SetAlpha(0.3f);
                _storage.SelectItem(id);
                _audioManager.PlayEffect(_selectClip);
            }
            else
            {
                _image.color = SetAlpha(1f);
                _storage.UnSelectItem(id);
                _audioManager.PlayEffect(_selectClip);
            }
            _marker.enabled = _isSelected;
        }
        else if(!_storage.CanSelect && _marker.enabled)
        {
            _image.color = SetAlpha(1f);
            _storage.UnSelectItem(id);
            _audioManager.PlayEffect(_selectClip);
            _marker.enabled = false;
        }
    }
    private Color SetAlpha(float alpha)
    {
        return new Color(1, 1, 1, alpha);
    }
}
