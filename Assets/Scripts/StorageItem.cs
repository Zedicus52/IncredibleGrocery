using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageItem : MonoBehaviour
{
    
    [SerializeField] private Image _marker;
    [SerializeField] private Storage _storage;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AudioClip _selectClip;
    private Image _image;
    private bool _isSelected = false;
    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    
    public void NullifyItems()
    {
        _isSelected = false;
        _image.color = Color.white;
        _marker.enabled = false ;
    }
   

    public void SelectItem(int id)
    {
        _isSelected = !_isSelected;
        if (_storage.CanSelect)
        {
            if (_isSelected)
            {
                _image.color = Utils.SetAplha(Color.white, 0.3f);
                _storage.SelectItem(id);
                _audioManager.PlayEffect(_selectClip);
            }
            else
            {
                _image.color = Utils.SetAplha(Color.white, 1f);
                _storage.UnSelectItem(id);
                _audioManager.PlayEffect(_selectClip);
            }
            _marker.enabled = _isSelected;
        }
        else if(!_storage.CanSelect && _marker.enabled)
        {
            _image.color = Utils.SetAplha(Color.white, 1f);
            _storage.UnSelectItem(id);
            _audioManager.PlayEffect(_selectClip);
            _marker.enabled = false;
        }
    }
    
}
