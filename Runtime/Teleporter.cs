
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
namespace ragecraft.UtilsScript
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class Teleporter : UdonSharpBehaviour
    {
        [SerializeField] private Transform teleportPointTo;
        void Start()
        {
            
        }
        public override void Interact()
        {
            if(teleportPointTo == null)
            {
                Debug.Log(this.gameObject.name + ":teleportPointTo:null");
                return;
            }
            Networking.LocalPlayer.TeleportTo(teleportPointTo.position, teleportPointTo.rotation);
        }
    }
}
