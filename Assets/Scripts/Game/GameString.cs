/// <summary>
/// 游戏中的固定字符串
/// </summary>
public class GameString
{
    /// <summary>
    /// 游戏难度译名
    /// </summary>
    public readonly string[] GameMode = new string[]
    {
        "新手调查员",
        "探索者",
        "噩梦之旅",
    };

    public readonly string NoHp = "程佑受伤过重。";

    public readonly string NoSan = "程佑失去了理智，永久陷入了癫狂。";

    /// <summary>
    /// 背包已满时提示
    /// </summary>
    public readonly string BagFull = "背包已经放不下东西了。";

    /// <summary>
    /// 调查无法进入的门
    /// </summary>
    public readonly string LockedDoor = "锁死了。";

    public readonly string[][] UseSpecialItem = new string[][]
    {
        new string[] { "令人难以理解的现象再次在程佑眼前发生，泛黄的信封逐渐褪去颜色，不一会儿便变得崭新如初，潦草的字迹也逐渐消失，直至隐去。" },
    };
}
