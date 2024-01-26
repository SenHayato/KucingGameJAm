using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    [Range(0f, 1f)]
    public float humanMoveDuration = 1;


    private Vector3 currentEndPosition;
    private Vector3 endPosition1;
    private Vector3 endPosition2;

    private void Awake()
    {
        currentEndPosition = transform.position;
        endPosition1 = transform.position;

        Vector2 temp = transform.position;
        temp.x = transform.position.x * -1;
        endPosition2 = temp;
    }

    public void MoveHuman(Vector3 targetPosition)
    {
        transform.DOMove(targetPosition, humanMoveDuration).OnComplete(() =>
        {
            transform.DOMove(currentEndPosition, humanMoveDuration);
            currentEndPosition = (currentEndPosition == endPosition1) ? endPosition2 : endPosition1;
        });
    }
}
