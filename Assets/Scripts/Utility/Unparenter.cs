using System;
using UnityEngine;

namespace Utility
{
    public class Unparenter : MonoBehaviour
    {
        private void Start()
        {
            transform.parent = null;
        }
    }
}