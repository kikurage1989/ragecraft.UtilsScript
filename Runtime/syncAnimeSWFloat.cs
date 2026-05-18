
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace ragecraft.UtilsScript
{
    //Manualに固定する
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    //複数のコントローラーに一斉に同名boolパラメータの切替を行なうオルタネイトスイッチ
    //boolでなくfloatの0-1を切り替える
    //ReJoinで同期不良おこすので他SWでの同パラメータ指定NG 
    public class syncAnimeSWFloat : syncSW_Base
    {
        protected override void Start()
        {
            if(useAnimator)
            {
                if (setParameterName == null || setParameterName == "")
                {
                    Debug.Log("syncAnimeSWFloat:setParameterName:null");
                    return;
                }
                if (setParameterAnimator == null)
                {
                    Debug.Log("syncAnimeSWFloat:setParameterAnimator:null");
                    return;
                }
                setParameterNameID = Animator.StringToHash(setParameterName);
            }            
            base.Start();
        }

        protected override void SomeUpdate()
        {
            //なにかUdonSynced変数更新に付随した処理
            base.SomeUpdate();
            int i;
            for (i = 0; i < setParameterAnimator.Length; i++)
            {
                setParameterAnimator[i].SetFloat(setParameterNameID, udonSyncedBool[0] ? 1f : 0f);
            }
            return;
        }
    }
}