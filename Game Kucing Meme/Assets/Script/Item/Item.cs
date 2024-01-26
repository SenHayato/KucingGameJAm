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
    

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void OnMouseDown()
    {
        onItemClicked?.Invoke(this, gameObject);
        Invoke(nameof(ReturnToPlace), returnToPlaceDuration);
    }

    private void ReturnToPlace()
    {
        transform.DOMove(initialPosition, itemFallDuration);
    }


}
