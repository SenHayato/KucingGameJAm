using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemManager : MonoBehaviour
{
    [Header("Cat Component")]
    public CatController cat;

    [Header("Human Component")]
    public HumanController human;
    private List<Coroutine> humanCoroutines;

    [Header("Item Component")]
    public List<Item> items;
    public Transform fallPoint;

    public int currentLevel = 1;
    private int itemFallCounter;

    [Header("Game Event Handler")]
    public UnityAction onGameWin;
    public UnityAction onGameOver;

    private void Awake()
    {
        humanCoroutines = new List<Coroutine>();

        // EVENT LISTENER
        foreach (var item in items)
        {
            item.onItemClicked += OnItemClicked;
            item.onItemReturn += OnItemReturned;
        }

        ShowCurrentLevelItem();
    }

    [NaughtyAttributes.Button("Update Current Level Value", NaughtyAttributes.EButtonEnableMode.Always)]
    public void ShowCurrentLevelItem()
    {
        itemFallCounter = 0;

        // Clean items
        foreach (var item in items)
        {
            item.gameObject.SetActive(false);
            item.ResetItem();
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
            if (i < items.Count)
                items[i].gameObject.SetActive(true);
    }

    private void OnItemReturned()
    {
        itemFallCounter--;

        if (IsGameOver())
            onGameOver?.Invoke();
    }

    private bool IsGameOver() => itemFallCounter <= 0;
    private bool IsGameWin() => itemFallCounter >= currentLevel;

    public void OnItemClicked(Item item, GameObject go)
    {

        // Item falling animation
        Vector3 fallPosition = new Vector3(go.transform.position.x, fallPoint.transform.position.y, go.transform.position.z);
        go.transform.DOMove(fallPosition, item.itemFallDuration).SetDelay(cat.catMoveDuration);
        
        // Game over check
        itemFallCounter++;
        if (IsGameWin())
        {
            onGameWin?.Invoke();

            foreach (var i in items)
                i.StopReturned();

            foreach (var coroutine in humanCoroutines)
                StopCoroutine(coroutine);

        } else
        {
            // Human returned back item animation
            Coroutine humanCoroutine = StartCoroutine(MoveHuman(item.initialPosition, item.returnToPlaceDuration * .8f));
            humanCoroutines.Add(humanCoroutine);
        }

    }

    private IEnumerator MoveHuman(Vector3 targetPosition, float waitForSeconds)
    {
        yield return new WaitForSeconds(waitForSeconds);
        human.MoveHuman(targetPosition);
    }
}
