public class SkillDefine
{
    public static int MAX_CAPTURE_TIMEOUT = 45;         // 捕捉超时时间
    public static int MAX_CAPTURE_ROPE_HP = 3;          // 最大绳子Hp
    public static int PER_CAPTURE_ROPE_DAMAGE = -1;     // 单次对捕捉绳的伤害
    public static int MIN_CAPTURE_ROPE_LEN = 2;         // 最短捕获绳长度
}

// todo 
public enum eSkillId : int
{
    Capture = 205007, //捕获
    CaptureHook = 205008, //捕获-> 钩怪
    CaptureShoot = 205009, //捕获-> 射怪
    CaptureMonsterAttack = 205010 // 怪物打人
}

public enum eSkillFlyerType : int
{
    Normal = 1,
    Rope = 2,
    FollowRope = 3
}
