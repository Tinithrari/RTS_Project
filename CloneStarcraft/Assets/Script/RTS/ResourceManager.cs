using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{

    public static class ResourceManager
    {
        public static float RotateAmount { get { return 10; } }
        public static float ScrollSpeed { get { return 25; } }
        public static float RotateSpeed { get { return 100; } }
        public static int ScrollWidth { get { return 15; } }
        public static float MinCameraHeight { get { return 10; } }
        public static float MaxCameraHeight { get { return 40; } }
        public static GUISkin SelectBoxSkin { get; private set; }
        private static Bounds invalidBounds = new Bounds(Vector3.negativeInfinity, new Vector3());
        public static Bounds InvalidBounds { get { return invalidBounds; } }

        public static void StoreSelectBoxSkin(GUISkin skin)
        {
            SelectBoxSkin = skin;
        }
    }
}
