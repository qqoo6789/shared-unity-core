/*
 * @Author: xiang huan
 * @Date: 2022-09-06 15:36:33
 * @Description: 霸体效果
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/SkillEffect/SEEndureCore.cs
 * 
 */

public class SEEndureCore : SkillEffectBase
{
    private EntityBattleDataCore _battleData;

    public override void OnAdd()
    {
        base.OnAdd();

        if (RefOwner.TryGetComponent(out _battleData))
        {
            _battleData.AddBattleState(BattleDefine.eBattleState.Endure);
        }
    }

    public override void OnRemove()
    {
        if (_battleData != null)
        {
            _battleData.RemoveBattleState(BattleDefine.eBattleState.Endure);
            _battleData = null;
        }
        base.OnRemove();
    }
}