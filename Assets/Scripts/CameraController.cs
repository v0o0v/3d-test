using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace {

    public class CameraController : MonoBehaviour {

        [SerializeField] private float rotationSpeed = 50f, distance = 3f;

        private Transform _target;
        private Vector2 _lookVector;

        private float _azimuthAngle, _polarAngle;

        private void Awake(){
            _azimuthAngle = 0f;
            _polarAngle = 0f;
        }

        private void LateUpdate(){
            if (_target){
                //마우스 x,y 갑을 이용해 카메라 이동
                _azimuthAngle += _lookVector.x * rotationSpeed * Time.deltaTime;
                _polarAngle -= _lookVector.y * rotationSpeed * Time.deltaTime;
                _polarAngle = Mathf.Clamp(_polarAngle, -20f, 60f);
                transform.position = _target.position - GetCameraPosition(distance, _polarAngle, _azimuthAngle);
                transform.LookAt(_target);
            }
        }

        private Vector3 GetCameraPosition(float r, float polarAngle, float azimuthAngle){
            float b = r * Mathf.Cos(polarAngle * Mathf.Deg2Rad);
            float x = b * Mathf.Sin(azimuthAngle * Mathf.Deg2Rad);
            float y = r * Mathf.Sin(polarAngle * Mathf.Deg2Rad) * -1;
            float z = b * Mathf.Cos(azimuthAngle * Mathf.Deg2Rad);

            return new Vector3(x, y, z);
        }

        public void SetTarget(Transform target, PlayerInput playerInput){
            _target = target;
            
            //카메라 위치 설정
            transform.position = _target.position - GetCameraPosition(distance, _polarAngle, _azimuthAngle);
            transform.LookAt(_target);

            playerInput.actions["Look"].performed += OnActionLook;
            playerInput.actions["Look"].canceled += OnActionLook;
        }

        private void OnActionLook(InputAction.CallbackContext context){
            _lookVector = context.ReadValue<Vector2>();
            Debug.Log(_lookVector);
        }

    }

}