using ThreeD.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ThreeD {

    public class MovePlayerState : PlayerState, IPlayerState {

        private float _moveSpeed;
        
        public MovePlayerState(PlayerController playerController, Animator animator, PlayerInput playerInput)
            : base(playerController, animator, playerInput){ }

        public void Enter(){
            _animator.SetBool(PlayerController.PlayerAniParamMove, true);
            _moveSpeed = 0f;
        }

        public void Update(){
            Vector2 moveVector = _playerInput.actions["Move"].ReadValue<Vector2>();
            
            if (moveVector != Vector2.zero){
                base.Rotate(moveVector.x, moveVector.y);
            }
            else{
                _playerController.SetState(PlayerController.EPlayerState.Idle);
            }
            
            //이동 스피드 설정
            var isRun = _playerInput.actions["Run"].IsPressed();
            if (isRun && _moveSpeed < 1f){
                _moveSpeed += Time.deltaTime;
                _moveSpeed = Mathf.Clamp01(_moveSpeed);
            }else if (!isRun && _moveSpeed > 0f){
                _moveSpeed -= Time.deltaTime;
                _moveSpeed = Mathf.Clamp01(_moveSpeed);
            }
            _animator.SetFloat(PlayerController.PlayerAniParamMoveSpeed, _moveSpeed);
        }

        public void Exit(){
            _animator.SetBool(PlayerController.PlayerAniParamMove, false);
        }

    }

}