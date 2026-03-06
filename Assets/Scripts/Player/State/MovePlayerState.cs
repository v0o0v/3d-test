using ThreeD.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ThreeD {

    public class MovePlayerState : PlayerState, IPlayerState {

        public MovePlayerState(PlayerController playerController, Animator animator, PlayerInput playerInput)
            : base(playerController, animator, playerInput){ }

        public void Enter(){
            _animator.SetBool(PlayerController.PlayerAniParamMove, true);
        }

        public void Update(){
            Vector2 moveVector = _playerInput.actions["Move"].ReadValue<Vector2>();
            
            if (moveVector != Vector2.zero){
                
            }
            else{
                _playerController.SetState(PlayerController.EPlayerState.Idle);
            }
        }

        public void Exit(){
            _animator.SetBool(PlayerController.PlayerAniParamMove, false);
        }

    }

}