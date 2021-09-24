using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TMP_Text _balanceText;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AudioClip _addMoney;
    private int _finalSum = 0;
    private int _balance = 0;
    public int FinalSum 
    { 
        get => _finalSum; 
        set => _finalSum += value;
    }
    public int Balance => _balance;

    private void Start()
    {
        _balance = PlayerPrefs.GetInt("Balance");
        _balanceText.text = "$ " + _balance;
    }

    public void DoubleSum()
    {
        _finalSum *= 2;
    }

    public void UpdateBalance(int sum)
    {
        _balance += sum;
        _balanceText.text = "$ "+_balance;
        _audioManager.PlayEffect(_addMoney);
    }

    public void Nullify()
    {
        _finalSum = 0;
    }
}
