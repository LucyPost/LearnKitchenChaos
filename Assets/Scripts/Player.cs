using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {

    [SerializeField] private float speed = 7f;
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    [SerializeField] private Joystick joystick;

    [SerializeField] private bool isOnIce;
    [SerializeField] private bool isInMirror = false;
    public static Player Instance { get; private set; }

    public event EventHandler OnPickedSomething;

    private KitchenObject kitchenObject;
    private bool isWalking;
    private float MaxSpeed;
    private float acceleration = 5.0f;
    private float friction = 0.5f;
    private Vector3 velocity;
    private UnityEngine.Vector3 lastInputdirction = UnityEngine.Vector3.zero;
    private BaseCounter selectedCounter;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one instance of Player in the scene");
        }
        if(isInMirror) {
            return;
        }
        Instance = this;
        MaxSpeed = speed;
        if (isOnIce) {
            speed = 0;
            velocity = Vector3.zero;
        }
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void Update() {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public void Run() {
        speed = 14.0f;
    }
    public void StopRun() {
        speed = 7.0f;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (!GameManager.Instance.IsGamePlaying()) return;
        if(selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e) {
        if (!GameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null) {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void HandleMovement() {
        //UnityEngine.Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        UnityEngine.Vector2 inputVector = new UnityEngine.Vector2(
            Math.Abs(joystick.Horizontal) > 0.45 ? joystick.Horizontal : 0,
            Math.Abs(joystick.Vertical) > 0.45 ? joystick.Vertical : 0
            );

        UnityEngine.Vector3 moveDir = new UnityEngine.Vector3(inputVector.x, 0, inputVector.y);
        

        if(isInMirror) {
            moveDir = new UnityEngine.Vector3(-inputVector.x, 0, inputVector.y);
        }

        if(moveDir.magnitude > 0) {
            lastInputdirction = moveDir;
            isWalking = true;
        } else {
            isWalking = false;
        }

        float moveDistance = speed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        
        if (isOnIce) {
            if(moveDir.magnitude == 0) {
                velocity = velocity * (1 - friction * Time.deltaTime);
            }
            velocity += moveDir * acceleration * Time.deltaTime;
            if(velocity.magnitude > MaxSpeed) {
                velocity = velocity.normalized * MaxSpeed;
            }
            
            moveDistance = velocity.magnitude * Time.deltaTime;
            moveDir = velocity.normalized;
        }

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + UnityEngine.Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        transform.forward = UnityEngine.Vector3.Slerp(transform.forward, lastInputdirction, rotationSpeed * Time.deltaTime);
        if (isOnIce) {
            if (canMove) {
                transform.position += velocity * Time.deltaTime;
                return;
            
            } else {
                velocity = Vector3.zero;
            }
        }

        if (!canMove) {

            UnityEngine.Vector3 moveDirX = new UnityEngine.Vector3(moveDir.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + UnityEngine.Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove) {

                moveDir = moveDirX;

            } else {

                UnityEngine.Vector3 moveDirZ = new UnityEngine.Vector3(0, 0, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + UnityEngine.Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove) {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove) {
            transform.position += moveDir * speed * Time.deltaTime;
        }
    }

    private void HandleInteractions() {
        if (isInMirror) {
            //return;
        }

        float interactionDistance = 2f;

        if(Physics.Raycast(transform.position, lastInputdirction, out RaycastHit hit, interactionDistance, countersLayerMask)) {
            if(hit.transform.TryGetComponent(out BaseCounter counter)) {
                if(counter != selectedCounter) {
                    SetSelectedCounter(counter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter counter) {
        selectedCounter = counter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetkitechenObjectFollowtransform() {
        return kitchenObjectHoldPoint;
    }

    public void SetKitechenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
        //if(kitchenObject != null) {
        //    OnPickedSomething?.Invoke(this, EventArgs.Empty);
        //}
        OnPickedSomething?.Invoke(this, EventArgs.Empty);
    }

    public KitchenObject GetKitchenObject() {
        return this.kitchenObject;
    }

    public void ClearKitchenObject() {
        this.kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return this.kitchenObject != null;
    }
}