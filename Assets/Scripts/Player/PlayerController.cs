using System.Collections.Generic;
using DefaultNamespace;
using ThreeD.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ThreeD {

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour {

        [SerializeField] private Transform headTransform;
        
        private Animator _animator;
        private PlayerInput _playerInput;
        private CharacterController _characterController;

        public static readonly int PlayerAniParamMove = Animator.StringToHash("move");
        
        public enum EPlayerState { None, Idle, Move }

        public EPlayerState PlayerState{ get; private set; }
        private Dictionary<EPlayerState, IPlayerState> _playerStates;

        private void Awake(){
            _animator = GetComponent<Animator>();
            _playerInput = GetComponent<PlayerInput>();
            _characterController = GetComponent<CharacterController>();

            IPlayerState idlePlayerState = new IdlePlayerState(this, _animator, _playerInput);
            IPlayerState movePlayerState = new MovePlayerState(this, _animator, _playerInput);

            _playerStates = new Dictionary<EPlayerState, IPlayerState>(){
                { EPlayerState.Idle, idlePlayerState },
                { EPlayerState.Move, movePlayerState }
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

    }

}