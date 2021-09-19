using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Image _effectImage;
    [SerializeField] private Image _musicImage;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Color[] _colors;
    [SerializeField] private TMP_Text _musicText;
    [SerializeField] private TMP_Text _effectText;
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private GameObject _settingsPanel;
    private bool _effectOn = true;
    private bool _musicOn = true;
    private bool _isActive = false;
    private bool _greenColor = true;

    public void Start()
    {
        if (PlayerPrefs.HasKey("MusicOn"))
        {
            if (PlayerPrefs.GetInt("MusicOn") == 1)
            {
                _musicOn = true;
                _mixer.audioMixer.SetFloat("MusicVolume", 0f);
            }
            else if (PlayerPrefs.GetInt("MusicOn") == 0)
            {
                _musicOn = false;
                _mixer.audioMixer.SetFloat("MusicVolume", -80f);
            }
        }
        else
        {
            PlayerPrefs.SetInt("MusicOn", 1);
        }

        if (PlayerPrefs.HasKey("EffectOn"))
        {
            if (PlayerPrefs.GetInt("EffectOn") == 1)
            {
                _effectOn = true;
                _mixer.audioMixer.SetFloat("UIVolume", 0f);
            }
            else if (PlayerPrefs.GetInt("EffectOn") == 0)
            {
                _effectOn = false;
                _mixer.audioMixer.SetFloat("UIVolume", -80f);
            }
        }
        else
        {
            PlayerPrefs.SetInt("EffectOn", 1);
        }
        EffectController();
        MusicController();
    }

    public void EffectController()
    {
        if (_effectOn == true)
        {
            _mixer.audioMixer.SetFloat("UIVolume", 0f);
            _greenColor = true;
        }
        else 
        {
            _mixer.audioMixer.SetFloat("UIVolume", -80f);
            _greenColor = false;
        }
        _effectOn = !_effectOn;
        SetIcon(_effectImage, _effectText, _greenColor);
    }


    public void MusicController()
    {
        
        if (_musicOn == true)
        {
            _mixer.audioMixer.SetFloat("MusicVolume", 0f);
            _greenColor = true;
        }
        else 
        {
            _mixer.audioMixer.SetFloat("MusicVolume", -80f);
            _greenColor = false;
        }
        _musicOn = !_musicOn;
        SetIcon(_musicImage, _musicText, _greenColor);
    }

    private void SetIcon(Image image, TMP_Text text, bool enabled)
    {
        if (enabled == true)
        {
            text.text = "ON";
            image.color = _colors[0];
        }
        else
        {
            text.text = "OFF";
            image.color = _colors[1];
        }
    }

   public void SettingsController()
    {
        _isActive = !_isActive;
        if(_isActive == true)
        {
            _settingsPanel.SetActive(_isActive);
            Time.timeScale = 0.0f;
        }
        else
        {
            _settingsPanel.SetActive(_isActive);
            Time.timeScale = 1.0f;
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Balance", _wallet.Balance);
        if(_effectOn == true)
        {
            PlayerPrefs.SetInt("EffectOn", 0);
        }
        else if(_effectOn == false)
        {
            PlayerPrefs.SetInt("EffectOn", 1);
        }
        if(_musicOn == true)
        {
            PlayerPrefs.SetInt("MusicOn", 0);
        }
        else if(_musicOn == false)
        {
            PlayerPrefs.SetInt("MusicOn", 1);
        }
        PlayerPrefs.Save();
    }

    public void DeleteAllKey()
    {
        PlayerPrefs.DeleteKey("EffectOn");
        PlayerPrefs.DeleteKey("MusicOn");
        PlayerPrefs.DeleteKey("Balance");
    }

    
}
