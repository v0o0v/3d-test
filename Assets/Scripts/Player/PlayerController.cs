using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ThreeD {

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour {

        [SerializeField] private Transform headTransform;

        [Header("이동")] [SerializeField] [Range(0f, 5f)]
        private float breakForce = 1f;
        public float BreakForce => breakForce;
        
        [SerializeField] private float jumpHeight = 2f;
        
        private Animator _animator;
        private PlayerInput _playerInput;
        private CharacterController _characterController;

        public static readonly int PlayerAniParamIdle = Animator.StringToHash("idle");
        public static readonly int PlayerAniParamMove = Animator.StringToHash("move");
        public static readonly int PlayerAniParamJump = Animator.StringToHash("jump");
        public static readonly int PlayerAniParamMoveSpeed = Animator.StringToHash("moveSpeed");
        public static readonly int PlayerAniParamGroundDistance = Animator.StringToHash("ground_distance");

        public enum EPlayerState { None, Idle, Move, Jump }
        
        private float _velocityY;

        public EPlayerState PlayerState{ get; private set; }
        private Dictionary<EPlayerState, IPlayerState> _playerStates;

        private void Awake(){
            _animator = GetComponent<Animator>();
            _playerInput = GetComponent<PlayerInput>();
            _characterController = GetComponent<CharacterController>();

            IPlayerState idlePlayerState = new IdlePlayerState(this, _animator, _playerInput);
            IPlayerState movePlayerState = new MovePlayerState(this, _animator, _playerInput);
            IPlayerState jumpPlayerState = new JumpPlayerState(this, _animator, _playerInput);

            _playerStates = new Dictionary<EPlayerState, IPlayerState>(){
                { EPlayerState.Idle, idlePlayerState },
                { EPlayerState.Move, movePlayerState },
                { EPlayerState.Jump, jumpPlayerState }
            };

            Camera playerCamera = Camera.main;
            if (playerCamera){
                _playerInput.camera = playerCamera;
                playerCamera.GetComponent<CameraController>()?.SetTarget(headTransform, _playerInput);
            }
        }

        private void Update(){
            if (PlayerState != EPlayerState.None) _playerStates[PlayerState].Update();
        }

        public void SetState(EPlayerState state){
            if (PlayerState == state) return;
            if (PlayerState != EPlayerState.None)
                _playerStates[PlayerState].Exit();

            PlayerState = state;
            if (PlayerState != EPlayerState.None)
                _playerStates[PlayerState].Enter();
        }
        
        //물리적 점프 적용
        public void Jump(){
            if (!_characterController.isGrounded) return;
            _velocityY = Mathf.Sqrt(jumpHeight * -2f * Constants.Gravity);
        }

        private void OnAnimatorMove(){
            Vector3 movePosition;
            if (_characterController.isGrounded){
                movePosition = _animator.deltaPosition;
            }else{
                movePosition = _characterController.velocity * Time.deltaTime;
            }
            
            _velocityY += Constants.Gravity * Time.deltaTime;
            movePosition.y = _velocityY * Time.deltaTime;
            
            _characterController.Move(movePosition);
        }

    }

}