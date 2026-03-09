using UnityEngine;

namespace ThreeD {

    public static class CharacterUtility {

        public static float GetDistanceToGround(Vector3 position, LayerMask layerMask, float maxDistance){
            if (Physics.Raycast(position, Vector3.down, out RaycastHit hitInfo
                    , maxDistance, layerMask)){
                return hitInfo.distance;
            }

            return maxDistance;
        }

    }

}