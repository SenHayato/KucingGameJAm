using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public UnityAction<Item, GameObject> onItemClicked;
    public Transform initialPoint;

    private void Awake()
    {
        initialPoint = transform;
    }

    private void OnMouseDown()
    {
        onItemClicked?.Invoke(this, gameObject);
    }
}
