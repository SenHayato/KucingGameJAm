using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class Item : MonoBehaviour
{
    public UnityAction<Item, GameObject> onItemClicked;
    public UnityAction onItemReturn;

    [Range(0f, 1f)]
    public float itemFallDuration = 1;

    [Range(0, 20)]
    public float returnToPlaceDuration = 10;

    [HideInInspector]
    public Vector3 initialPosition;

    public bool isGameOver;

    public GameObject knockPrefab;

    [Header("Audio Component")]
    public AudioClip itemFallClip;
    public List<AudioClip> itemRestoreClip;
    public AudioSource audioSource;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void OnMouseDown()
    {
        StartCoroutine(ReturnToPlace());
        onItemClicked?.Invoke(this, gameObject);
    }

    public void StopReturned()
    {
        isGameOver = true;
    }

    public void ResetItem()
    {
        transform.position = initialPosition;
        isGameOver = false;
    }

    private IEnumerator ReturnToPlace()
    {
        yield return new WaitForSeconds(returnToPlaceDuration);
        if (!isGameOver)
        {
            audioSource.PlayOneShot(itemRestoreClip[Random.Range(0, itemRestoreClip.Count)]);
            transform.DOMove(initialPosition, itemFallDuration);
            onItemReturn?.Invoke();
        }
    }

    public void KnockEffect(float delay)
    {
        Invoke(nameof(KnockDelay), delay);
    }

    private void KnockDelay()
    {
        GameObject obj = Instantiate(knockPrefab, initialPosition, Quaternion.identity);
        Destroy(obj, 0.2f);
    }

    public void PlayItemFallSFX()
    {
        audioSource.PlayOneShot(itemFallClip);
    }
}
