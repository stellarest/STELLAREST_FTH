using UnityEngine;
using static STELLAREST_FTH.Define;

namespace STELLAREST_FTH
{
    public class BaseObject : MonoBehaviour
    {
        [ShowOnly][SerializeField] private int _myNum = 123;
        private void Update()
        {
            // --- Formal Input ex
            if (Dev.KeyDown(EDevKey.Num00, keyInfo: $"{nameof(BaseObject)}::{nameof(Update)}"))
            {
                Dev.Log("Num0::TrueStatement01");
            }

            // --- Formal Input ex, 키 중복 테스트
            if (Dev.KeyDown(EDevKey.Num00, keyInfo: $"{nameof(BaseObject)}::{nameof(Update)}"))
            {
                Dev.Log("Num0::TrueStatement02");
            }

            // --- Action Input ex
            Dev.KeyDown(eDevKey: EDevKey.Num01, keyInfo: $"{nameof(BaseObject)}::{nameof(Update)}",
                startFlag: true, trueCase: () =>
                {
                    Dev.Log("Num01 - TrueCase01");
                },
                falseCase: () =>
                {
                    Dev.Log("Num01 - FalseCase01");
                });

            // --- Action Input ex, 키 중복 테스트
            Dev.KeyDown(eDevKey: EDevKey.Num01, keyInfo: $"{nameof(BaseObject)}::{nameof(Update)}",
                startFlag: true, trueCase: () =>
                {
                    Dev.Log("Num01 - TrueCase02");
                },
                falseCase: () =>
                {
                    Dev.Log("Num01 - FalseCase02");
                });
        }
    }
}
