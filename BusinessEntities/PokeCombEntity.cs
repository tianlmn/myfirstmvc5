using System.Collections.Generic;

namespace BusinessEntities
{
    public class PokeEntity
    {
        public List<PokeCombEntity> SortedList { get; set; }

    }

    public class PokeCombEntity
    {
        /// <summary>
        /// 技能组合数组
        /// </summary>
        public PokeAttrType[] SortList { get; set; }

        public List<PokeAttrType> List_100 { get; set; }

        public List<PokeAttrType> List_150 { get; set; }

        public List<PokeAttrType> List_300 { get; set; }



        public double Score { get; set; }
    }

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