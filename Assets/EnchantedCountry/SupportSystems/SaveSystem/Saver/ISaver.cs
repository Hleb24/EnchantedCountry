using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public interface ISaver {
        void Save(Scrolls scrolls);

        Scrolls Load(out bool isNewGame);
    }
}
