using UnityEngine;
using UnityEngine.InputSystem;

namespace ThreeD {

    public class IdlePlayerState : PlayerState, IPlayerState {

        public IdlePlayerState(PlayerController playerController, Animator animator, PlayerInput playerInput)
            : base(playerController, animator, playerInput){ }

        public void Enter(){
            _animator.SetBool(PlayerController.PlayerAniParamIdle, true);
            _playerInput.actions["Jump"].performed += Jump;
        }

        public void Update(){
            if (_playerInput.actions["Move"].IsPressed()){
                _playerController.SetState(PlayerController.EPlayerState.Move);
            }
        }

        public void Exit(){
            _animator.SetBool(PlayerController.PlayerAniParamIdle, false);
            _playerInput.actions["Jump"].performed -= Jump;
        }

    }

}