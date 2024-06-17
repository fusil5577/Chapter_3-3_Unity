using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f; // �̵� �ӵ�
    public float moveDistance = 1f; // �̵� �Ÿ�
    public float moveDelay = 0.12f; // �̵� ������ �ð�
    private bool canMove = true; // �̵� ���� ����

    private Rigidbody rigidbody;
    private AnimationController animationController;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animationController = GetComponent<AnimationController>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && canMove)
        {
            StartCoroutine(Move(Vector3.back));
        }
    }

    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && canMove)
        {
            StartCoroutine(Move(Vector3.forward));
        }
    }

    public void OnMoveForward(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && canMove)
        {
            StartCoroutine(Move(Vector3.left));
        }
    }

    public void OnMoveBack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && canMove)
        {
            StartCoroutine(Move(Vector3.right));
        }
    }

    private IEnumerator Move(Vector3 direction)
    {
        canMove = false;
        Vector3 targetPosition = transform.position + direction * moveDistance;
        float elapsedTime = 0;

        animationController.PlayMoveAnimation(direction);

        while (elapsedTime < moveDelay)
        {
            rigidbody.MovePosition(Vector3.Lerp(transform.position, targetPosition, (elapsedTime / moveDelay)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rigidbody.MovePosition(targetPosition);

        yield return new WaitForSeconds(moveDelay);
        canMove = true;
    }
}