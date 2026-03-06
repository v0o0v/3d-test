using ThreeD.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ThreeD {

    public class IdlePlayerState : PlayerState, IPlayerState {

        public IdlePlayerState(Animator animator, PlayerInput playerInput, PlayerController playerController)
            : base(animator, playerInput, playerController){ }

        public void Enter(){
        }   
        public void Update(){
        }
        public void Exit(){
        }

    }

}