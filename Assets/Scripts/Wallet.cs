using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wallet : MonoBehaviour
{
    private int _finalSum = 0;
    private int _balance = 0;
    [SerializeField] private TMP_Text _balanceText;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AudioClip _addMoney;
    public int FinalSum { get { return _finalSum; } set { _finalSum += value; } }
    public int Balance { get { return _balance; } }

    private void Start()
    {
        _balance = PlayerPrefs.GetInt("Balance");
        _balanceText.text = "$ " + _balance.ToString();
    }

    public void DoubleSum()
    {
        _finalSum *= 2;
    }

    public void UpdateBalance(int sum)
    {
        _balance += sum;
        _balanceText.text = "$ "+_balance.ToString();
        _audioManager.PlayEffect(_addMoney);
    }

    public void Nullify()
    {
        _finalSum = 0;
    }
}
