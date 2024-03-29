using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    [HideInInspector]
    public Vector3 initialScale;

    public bool isGameOver;

    public GameObject knockPrefab;

    public List<Transform> particles;

    [Header("Audio Component")]
    public AudioClip itemFallClip;
    public List<AudioClip> itemRestoreClip;
    public AudioSource audioSource;
    public bool isClicked;

    public Transform fallpoint;

    private void Awake()
    {
        initialPosition = transform.position;
        initialScale = transform.localScale;
    }

    private void OnEnable()
    {
        if (fallpoint != null)
            if (Mathf.Round(transform.position.y) == fallpoint.position.y)
                ResetItem();
    }

    private void Start()
    {
        DeactivateParticles();
        if (initialPosition == transform.position)
        {
            isClicked = false;
        }
    }

    private void OnMouseEnter()
    {
        transform.DOScale(transform.localScale * 1.2f, .2f);
    }

    private void OnMouseExit()
    {
        transform.DOScale(initialScale, .2f);
    }

    private void OnMouseDown()
    {
        if (!isClicked)
        {
            StartCoroutine(ReturnToPlace());
            onItemClicked?.Invoke(this, gameObject);
            Debug.Log(gameObject.name);
            isClicked = true;
        }
    }

    public void StopReturned()
    {
        isGameOver = true;
    }

    public void ResetItem()
    {
        transform.position = initialPosition;
        isGameOver = false;
        isClicked = false;
    }

    private IEnumerator ReturnToPlace()
    {
        yield return new WaitForSeconds(returnToPlaceDuration);
        if (!isGameOver)
        {
            isClicked = false;
            ActivateParticles();
            ShakeParticles(itemFallDuration);
            audioSource.PlayOneShot(itemRestoreClip[Random.Range(0, itemRestoreClip.Count)]);
            transform.DOMove(initialPosition, itemFallDuration).OnComplete(() =>
            {
                DeactivateParticles();
            });
            onItemReturn?.Invoke();
        }
    }

    private void ShakeParticles(float duration)
    {
        foreach (var item in particles)
        {
            item.DOShakePosition(duration);
        }
    }

    private void ActivateParticles()
    {
        foreach (var item in particles)
        {
            item.gameObject.SetActive(true);
        }
    }

    private void DeactivateParticles()
    {
        foreach (var item in particles)
        {
            item.gameObject.SetActive(false);
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
