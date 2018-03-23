using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script
{
    public interface Navigator
    {
        void Navigate(Vector3 destination);
    }
}
