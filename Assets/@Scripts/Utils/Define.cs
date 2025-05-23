using UnityEngine;

namespace STELLAREST_FTH
{
    public static class Define
    {
#if UNITY_EDITOR
        public enum EDevKey
        {
            None = -1,
            Num00, Num01, Num02, Num03, Num04,
            Num05, Num06, Num07, Num08, Num09,
            Max = 10,
        }
#endif

        public enum EIntDefault
        {
            NoneOfValue,
        }

        public enum EFloatDefault
        {
            NoneOfValue,
        }

        public enum EStringDefault
        {
            Null,
            Empty,
        }
    }
}
