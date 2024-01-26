using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [Header("Cat Component")]
    public CatController catController;
    
    [Header("Item Component")]
    public List<Item> items;
    public Transform fallPoint;

    [Range(0f, 1f)]
    public float itemFallDuration = 1;


    private void Awake()
    {
        // EVENT LISTENER
        foreach (var item in items)
            item.onItemClicked += OnObjectClicked;
    }

    private void OnObjectClicked(Item item, GameObject go)
    {
        Vector3 fallPosition = new Vector3(go.transform.position.x, fallPoint.transform.position.y, go.transform.position.z);
        go.transform.DOMove(fallPosition, itemFallDuration).SetDelay(catController.catMoveDuration);
    }
}
