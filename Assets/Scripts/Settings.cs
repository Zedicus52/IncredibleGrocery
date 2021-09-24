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
    [SerializeField] private Color _green;
    [SerializeField] private Color _red;
    [SerializeField] private TMP_Text _musicText;
    [SerializeField] private TMP_Text _effectText;
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private GameObject _settingsPanel;
    private bool _effectOn = true;
    private bool _musicOn = true;
    private bool _isActive = false;
    private bool _greenColor = true;

    private void Start()
    {
        float volume = 0f;
        LoadAudioSettings("MusicOn", ref volume, ref _musicOn);
        _mixer.audioMixer.SetFloat("MusicVolume", volume);
 
       LoadAudioSettings("EffectOn", ref volume, ref _effectOn);
        _mixer.audioMixer.SetFloat("UIVolume", volume);
        
        EffectController();
        MusicController();
    }

    private void LoadAudioSettings(string settingKey, ref float volume, ref bool setting)
    {
        if (PlayerPrefs.HasKey(settingKey))
        {
            setting = PlayerPrefs.GetInt(settingKey) == 1;
            if (setting)
            {
                volume = 0f;
            }
            else
            {
                volume = -80f;
            }
        }
    }

    public void EffectController()
    {
        if (_effectOn)
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
        
        if (_musicOn)
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

    private void SetIcon(Image image, TMP_Text text, bool onEnabled)
    {
        if (onEnabled)
        {
            text.text = "ON";
            image.color = _green;
        }
        else
        {
            text.text = "OFF";
            image.color = _red;
        }
    }

   public void SettingsController()
    {
        _isActive = !_isActive;
        if(_isActive)
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
        
        PlayerPrefs.SetInt("EffectOn", _effectOn ? 0 : 1);
        PlayerPrefs.SetInt("MusicOn", _musicOn ? 0 : 1);
        
        PlayerPrefs.Save();
    }

    public void DeleteAllKey()
    {
        PlayerPrefs.DeleteKey("EffectOn");
        PlayerPrefs.DeleteKey("MusicOn");
        PlayerPrefs.DeleteKey("Balance");
    }

    
}
