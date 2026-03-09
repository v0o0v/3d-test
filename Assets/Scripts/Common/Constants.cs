using UnityEngine;

namespace ThreeD {

    public class Constants {

        public const float Gravity = -9.81f;
        public static LayerMask GroundLayerMask => LayerMask.GetMask("Ground");

    }

}