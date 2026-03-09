using UnityEngine;
using UnityEngine.InputSystem;

namespace ThreeD {

    public class JumpPlayerState : PlayerState, IPlayerState {

        public JumpPlayerState(PlayerController playerController, Animator animator, PlayerInput playerInput)
            : base(playerController, animator, playerInput){ }

        public void Enter(){
            _animator.SetTrigger(PlayerController.PlayerAniParamJump);
        }

        public void Update(){
            Vector3 playerPosition = _playerController.transform.position;
            float distanceToGround =
                CharacterUtility.GetDistanceToGround(playerPosition, LayerMask.GetMask("Ground"), 10f);
            _animator.SetFloat(PlayerController.PlayerAniParamGroundDistance, distanceToGround);
            // Debug.DrawRay(playerPosition, Vector3.down * 2f, Color.red);
        }

        public void Exit(){ }

    }

}