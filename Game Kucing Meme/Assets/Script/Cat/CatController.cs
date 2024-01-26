using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [Range(0f, 1f)]
    public float catMoveDuration = 1;

    private Vector3 initialPosition;


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

            // Rotate the object to face the target position
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.DORotate(new Vector3(0, 0, angle), 0.5f);

            // Move the object using DoTween
            initialPosition = new Vector3(targetPosition.x, initialPosition.y, initialPosition.z);
            transform.DOMove(targetPosition, catMoveDuration).SetEase(Ease.OutQuad).OnComplete(() => transform.DOMove(initialPosition, catMoveDuration).SetEase(Ease.InQuad));
        }
    }
}
