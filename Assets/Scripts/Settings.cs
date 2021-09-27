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
    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;
    [SerializeField] private TMP_Text _musicText;
    [SerializeField] private TMP_Text _effectText;
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _effectButton;
    private Button _settingsButton;
    [SerializeField] private AudioClip _clickAudio;
    private bool _effectOn = true;
    private bool _musicOn = true;
    private bool _isActive = false;
    private bool _on = true;

    private void Start()
    {
        _settingsButton = GetComponent<Button>();
        float volume = 0f;
        LoadAudioSettings("MusicOn", ref volume, ref _musicOn);
        _mixer.audioMixer.SetFloat("MusicVolume", volume);
 
       LoadAudioSettings("EffectOn", ref volume, ref _effectOn);
        _mixer.audioMixer.SetFloat("UIVolume", volume);
        
        EffectController();
        MusicController();
        
        _settingsButton.onClick.AddListener(SettingsController);
        _settingsButton.onClick.AddListener(Play);
        _effectButton.onClick.AddListener(EffectController);
        _effectButton.onClick.AddListener(Play);
        _musicButton.onClick.AddListener(MusicController);
        _musicButton.onClick.AddListener(Play);
        _saveButton.onClick.AddListener(Save);
        
    }

    private void Play()
    {
        _audioManager.PlayEffect(_clickAudio);
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
            _on = true;
        }
        else 
        {
            _mixer.audioMixer.SetFloat("UIVolume", -80f);
            _on = false;
        }
        _effectOn = !_effectOn;
        SetIcon(_effectImage, _effectText, _on);
    }


    public void MusicController()
    {
        
        if (_musicOn)
        {
            _mixer.audioMixer.SetFloat("MusicVolume", 0f);
            _on = true;
        }
        else 
        {
            _mixer.audioMixer.SetFloat("MusicVolume", -80f);
            _on = false;
        }
        _musicOn = !_musicOn;
        SetIcon(_musicImage, _musicText, _on);
    }

    private void SetIcon(Image image, TMP_Text text, bool onEnabled)
    {
        if (onEnabled)
        {
            text.text = "ON";
            image.sprite = _onSprite;
        }
        else
        {
            text.text = "OFF";
            image.sprite = _offSprite;
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
