using UnityEngine;
using static STELLAREST_FTH.Define;

namespace STELLAREST_FTH
{
    public class BaseObject : MonoBehaviour
    {
        [ShowOnly][SerializeField] private int _myNum = 123;
        private void Update()
        {
            // add first
            if (Dev.KeyDown(EDevKey.Num00, keyInfo: $"{nameof(BaseObject)}::{nameof(Update)}"))
            {
                Dev.Log("Num0::TrueCase01");
            }

            // false
            if (Dev.KeyDown(EDevKey.Num00, keyInfo: $"{nameof(BaseObject)}::{nameof(Update)}"))
            {
                Dev.Log("Num0::TrueCase02");
            }

            // add first
            if (Dev.KeyDown(EDevKey.Num01, keyInfo: $"{nameof(BaseObject)}::{nameof(Update)}"))
            {
                Dev.Log("Num1::TrueCase01");
            }

            // false
            if (Dev.KeyDown(EDevKey.Num01, keyInfo: $"{nameof(BaseObject)}::{nameof(Update)}"))
            {
                Dev.Log("Num1::TrueCase02");
            }
        }
    }
}
