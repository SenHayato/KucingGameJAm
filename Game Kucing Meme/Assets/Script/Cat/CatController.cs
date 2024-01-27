using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [Range(0f, 1f)]
    public float catMoveDuration = 1;

    private Vector3 initialPosition;
    
    private bool isFacingRight = true;

    public AudioSource knockSfx;

    public SpriteRenderer spriteRenderer;
    public Sprite jumpSprite;
    public Sprite fallSprite;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse click position
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0f; // Set the z-coordinate to 0 if your object is in a 2D space

            // Calculate the direction to the target position
            Vector3 direction = targetPosition - transform.position;

            if (direction.x > 0 && isFacingRight || direction.x < 0 && !isFacingRight)
                Flip();
            
            // Rotate the object to face the target position
            //float angle = Mathf.Atan2(direction.y, direction.x * transform.localScale.x) * Mathf.Rad2Deg;
            //transform.DORotate(new Vector3(0, 0, angle), 0.5f);

            // Move the object using DoTween
            spriteRenderer.sprite = jumpSprite;
            initialPosition = new Vector3(targetPosition.x, initialPosition.y, initialPosition.z);
            transform.DOMove(targetPosition, catMoveDuration).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                knockSfx.PlayOneShot(knockSfx.clip);
                transform.DOMove(initialPosition, catMoveDuration).SetEase(Ease.InQuad);
                spriteRenderer.sprite = fallSprite;
            });

        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight; // Flip the facing direction
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Invert the x scale to flip horizontally
        transform.localScale = scale;
    }
}
