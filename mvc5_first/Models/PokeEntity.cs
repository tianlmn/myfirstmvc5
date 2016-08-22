using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc5_first.Models
{
    public class PokeEntity
    {
        public List<PokeAttrType[]> SortedList { get; set; }

    }

    //public enum PokeAttrType
    //{
    //    Putong,
    //    Huo,
    //    Shui,
    //    Dian,
    //    Cao,
    //    Bing,
    //    Gedou,
    //    Du,
    //    Dimian,
    //    Feixing,
    //    Chaoneng,
    //    Chong,
    //    Yan,
    //    Gui,
    //    Long,
    //    Gang,
    //    E,
    //    Xian
    //}

    public enum PokeAttrType
    {
        普,
        火,
        水,
        电,
        草,
        冰,
        斗,
        毒,
        地,
        飞,
        超,
        虫,
        岩,
        鬼,
        龙,
        钢,
        恶,
        仙
    }

}