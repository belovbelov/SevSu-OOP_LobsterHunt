using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobster
{
    public class Treasure: MonoBehaviour
    {
        public static bool isFound = false;

        private void OnMouseDown()
        {
            isFound = true;
        }
    }
}
