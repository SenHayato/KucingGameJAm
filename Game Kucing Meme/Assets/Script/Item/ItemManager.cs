using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [Header("Cat Component")]
    public CatController cat;

    [Header("Human Component")]
    public HumanController human;

    [Header("Item Component")]
    public List<Item> items;
    public Transform fallPoint;

    public int currentLevel = 1;

    private void Awake()
    {
        // EVENT LISTENER
        foreach (var item in items)
            item.onItemClicked += OnItemClicked;

        ShowCurrentLevelItem();
    }

    [NaughtyAttributes.Button("Update Current Level Value", NaughtyAttributes.EButtonEnableMode.Always)]
    private void ShowCurrentLevelItem()
    {
        // Clean items
        foreach (var item in items)
        {
            item.gameObject.SetActive(false);
        }

        // Shuffle the items list
        for (int i = items.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            // Swap elements
            Item temp = items[i];
            items[i] = items[randomIndex];
            items[randomIndex] = temp;
        }

        // Now, iterate through the shuffled list
        for (int i = 0; i < currentLevel; i++)
        {
            if (i < items.Count)
                items[i].gameObject.SetActive(true);
        }
    }

    private void OnItemClicked(Item item, GameObject go)
    {
        Vector3 fallPosition = new Vector3(go.transform.position.x, fallPoint.transform.position.y, go.transform.position.z);
        go.transform.DOMove(fallPosition, item.itemFallDuration).SetDelay(cat.catMoveDuration);

        StartCoroutine(MoveHuman(item.initialPosition, item.returnToPlaceDuration * .8f));
    }

    private IEnumerator MoveHuman(Vector3 targetPosition, float waitForSeconds)
    {
        yield return new WaitForSeconds(waitForSeconds);
        human.MoveHuman(targetPosition);
    }
}
