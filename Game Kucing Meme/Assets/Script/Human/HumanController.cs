using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    [Range(0f, 1f)]
    public float humanMoveDuration = 1;

    public SpriteRenderer spriteRenderer;
    public Sprite spriteHandOn;
    public Sprite spriteHandOff;
    private bool isFacingRight = true;

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
        spriteRenderer.sprite = spriteHandOn;
        Vector3 direction = targetPosition - transform.position;

        if (direction.x > 0 && !isFacingRight || direction.x < 0 && isFacingRight)
            Flip();

        transform.DOMove(targetPosition, humanMoveDuration).OnComplete(() =>
        {
            spriteRenderer.sprite = spriteHandOff;
            transform.DOMove(currentEndPosition, humanMoveDuration);
            currentEndPosition = (currentEndPosition == endPosition1) ? endPosition2 : endPosition1;
        });
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight; // Flip the facing direction
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Invert the x scale to flip horizontally
        transform.localScale = scale;
    }
}
