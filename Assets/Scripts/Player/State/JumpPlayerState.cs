using UnityEngine;
using UnityEngine.InputSystem;

namespace ThreeD {

    public class JumpPlayerState : PlayerState, IPlayerState {

        public JumpPlayerState(PlayerController playerController, Animator animator, PlayerInput playerInput)
            : base(playerController, animator, playerInput){ }

        public void Enter(){
            _animator.SetTrigger(PlayerController.PlayerAniParamJump);
        }
        public void Update(){ }
        public void Exit(){ }

    }

}