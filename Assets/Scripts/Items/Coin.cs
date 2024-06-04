using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue;

    private Animator _animator;





    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("idle", true);
        _animator.SetBool("colected", false);
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("is player");
            var _playerStats = other.GetComponent<PlayerStatsManager>();
            if (_playerStats != null)
            {
                Debug.Log("layer != null");
                _animator.SetBool("idle", false);
                _animator.SetBool("colected", true);
                //_playerStats.GetCoins(coinValue);
            }
        }
    }


    public void DestroyCoin()
    {
        Destroy(gameObject);
    }

}
