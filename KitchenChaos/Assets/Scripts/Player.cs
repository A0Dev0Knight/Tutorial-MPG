using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IKitchenObjectParent
{

    public static Player Instance {
        get; 
        
        private set;
    }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float MoveSpeed = 10f;
    [SerializeField] private float RotationSpeed = 10f;
    [SerializeField] private float SprintSpeed = 20f;

    [SerializeField] private GameInput GameInput;
    [SerializeField] private LayerMask LayerMask;

    [SerializeField] private Transform kitchenObjectHoldPoint;



    private bool isWalking;
    private Vector3 lastInteractDirection;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one player instance brodda!");
        }
        Instance = this;
    }
    private void Start()
    {
        GameInput.OnInteractAction += GameInput_OnInteractAction;
        GameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = GameInput.GetInputVectorNormalised();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDirection = moveDir;
        }
        float interactDistance = 1f;
        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactDistance, LayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }
    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.GetInputVectorNormalised();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float playerDistance = MoveSpeed * Time.deltaTime;
        float playerHeight = 2f;
        float playerWidth = .5f;

        bool canWalk = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, moveDir, playerDistance);

        if (!canWalk)
        {
            // Test if player can move on the OX axis
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            moveDirX = moveDirX.normalized;
            bool canWalkX = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, moveDirX, playerDistance);

            if (canWalkX)
            {
                // Player moved along OX axis
                transform.position += moveDirX * playerDistance;
            }
            else
            {
                // Player can move only on OZ axis now
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                moveDirZ = moveDirZ.normalized;
                bool canWalkZ = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, moveDirZ, playerDistance);

                if (canWalkZ)
                {
                    // Player moved along OZ axis
                    transform.position += moveDirZ * playerDistance;
                }
            }
        }
        else
        {
            // Player can move in any position
            transform.position += moveDir * playerDistance;
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * RotationSpeed);

        isWalking = moveDir != Vector3.zero;

    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter,
        });

    }

    public Transform GetCounterTopPoint()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
