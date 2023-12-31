using System.Collections;
using _GAME.Scripts.Pool;
using _GAME.Scripts.UI;
using DG.Tweening;
using UnityEngine;

namespace _GAME.Scripts.Play.Chest
{
    public class Lid : MonoBehaviour
    {
        public ObjectPooler pool;
        public ParticleSystem particleForChestOpen;
        public ParticleSystem particleForChestMoveDown;
        private Chest _chest;
        private Animator _animator;
        private GameObject _createdCoinObject;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _chest = GetComponentInParent<Chest>();
        }

        private void OpenLidAnim()
        {
            _animator.SetBool("isOpen", true);
        }

        private void PlayParticle()
        {
            particleForChestOpen.Play();
        }

        public void ChestOpeningStatus()
        {
            OpenLidAnim();
            PlayParticle();
            StartCoroutine(ChestMoveDown());
        }

        public void CoinCollect() //animation event
        { 
            _createdCoinObject = pool.SpawnFromPool("coin", transform.position, Quaternion.identity);
            TotalCoinPanel.Instance.MoveCoinToUI(_createdCoinObject, transform.position, _chest.income);
        }

        private IEnumerator ChestMoveDown()
        {
            yield return new WaitForSeconds(0.8f);
            _chest.transform.DOMoveY(-20, 1);
            yield return new WaitForSeconds(1f);
            _chest.gameObject.SetActive(false);
        }
    }
}