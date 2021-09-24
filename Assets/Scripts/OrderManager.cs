using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private GameObject _orderInfo;
    [SerializeField] private GameObject _cashierOrder;
    [SerializeField] private float _timeToMember = 5;
    [SerializeField] private Animator _storageAnimator;
    [SerializeField] private Storage _storage;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AudioClip _orderOn;
    [SerializeField] private AudioClip _orderOff;
    private Animator _buyerAnimator;
    private Order _order;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Buyer"))
        {
            _order = _orderInfo.GetComponent<Order>();
            _buyerAnimator = GameObject.FindGameObjectWithTag("Buyer").GetComponent<Animator>();
            StartCoroutine(CreateOrder());
            other.enabled = false;
            _storage.Initialization();
        }
    }

   

    private IEnumerator CreateOrder()
    {
        _order.CreateOrder();
        _orderInfo.SetActive(true);
        _audioManager.PlayEffect(_orderOn);
        yield return new WaitForSeconds(_timeToMember);
        _orderInfo.SetActive(false);
        _audioManager.PlayEffect(_orderOff);
        yield return new WaitForSeconds(0.25f);
        _storageAnimator.SetTrigger("Open");
        yield return new WaitUntil(() => _storage.End);
        _orderInfo.SetActive(true);
        _audioManager.PlayEffect(_orderOn);
        _cashierOrder.SetActive(false);
        _audioManager.PlayEffect(_orderOff);
        yield return new WaitForSeconds(0.5f);
        _orderInfo.SetActive(false);
        _audioManager.PlayEffect(_orderOff);
        _buyerAnimator.SetTrigger("End");
        
    }
}
