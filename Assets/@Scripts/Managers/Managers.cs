using UnityEngine;

namespace STELLAREST_FTH
{
    public class Managers : MonoBehaviour
    {
        private static Managers s_instance = null;
        public static Managers Instance
        {
            get
            {
                if (s_instance == null)
                {
                    GameObject go = GameObject.Find("@Managers");
                    if (go == null)
                        go = new GameObject { name = "@Managers" };

                    s_instance = go.AddComponent<Managers>();
                    UnityEngine.Object.DontDestroyOnLoad(go);
                }
                
                return s_instance;
            }
        }
    }
}
