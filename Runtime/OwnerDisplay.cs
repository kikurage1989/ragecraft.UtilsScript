
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro; //TextMeshProを扱う際に必要
using System;//Stringに使用

namespace ragecraft.UtilsScript
{
    public class OwnerDisplay : UdonSharpBehaviour
    {
        [SerializeField] TextMeshPro paraText; //TextMeshProの変数宣言
        [SerializeField] GameObject[] targetUdonObject;
        void Update()
        {
            string dis_text = "";

            for (int i = 0; i < targetUdonObject.Length; i++)
            {
                if (targetUdonObject[i] != null)
                {
                    dis_text += targetUdonObject[i].name + " ";
                    dis_text += "オーナー:" + Networking.GetOwner(targetUdonObject[i]).displayName + " ID:" + Networking.GetOwner(targetUdonObject[i]).playerId + "\n";
                }
            }
            paraText.text = dis_text;
        }
    }
}