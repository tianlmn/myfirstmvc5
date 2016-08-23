using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc5_first.Common;
using mvc5_first.Models;

namespace mvc5_first.Controllers
{
    public class PokeController : Controller
    {
        //
        // GET: /Poke/
        public ActionResult Index()
        {
            PokeAttrType a=PokeAttrType.地;
            PokeAttrType b;
            int c = 3;
            PokeEntity pokeEntity = new PokeEntity();
            if (Request["a"] != null)
            {
                a = (PokeAttrType) Enum.Parse(typeof (PokeAttrType), Request["a"]);
            }
            if (Request["b"] != null)
            {
                b = (PokeAttrType)Enum.Parse(typeof(PokeAttrType), Request["b"]);
            }
            else
            {
                b = a;
            }
            if (Request["c"] != null)
            {
                int.TryParse(Request["c"],out c);
            }
            PokeBusiness poke = new PokeBusiness(a,b,c);
            pokeEntity = poke.CalCombScore();
            return View("Pokemon", pokeEntity);
        }

        
    }


    public class PokeBusiness
    {
        public PokeBusiness(PokeAttrType p1, PokeAttrType p2, int combNum)
        {
            PropA = p1;
            PropB = p2;
            CombNum = combNum>6?6:combNum;
        }


        public PokeEntity CalCombScore()
        {
            PokeEntity pokeEntity = new PokeEntity();
            var attrList = GetList();
            List<PokeAttrType[]> combList = CombAlgorithm<PokeAttrType>.GetCombination(attrList, CombNum);

            combList.Sort(CompareByPokeAttack);
            var temp = combList.GetRange(0, 40);
            pokeEntity.SortedList = new List<PokeCombEntity>();

            foreach (var skills in temp)
            {
                GetWeakList(skills, pokeEntity.SortedList);
            }

            return pokeEntity;
        }

        private void GetWeakList(PokeAttrType[] skills, List<PokeCombEntity> sortList)
        {
            var entity = new PokeCombEntity();
            var weakList = new List<PokeAttrType>();
            var veryWeakList = new List<PokeAttrType>();
            double totalScore = 0;
            for (int i = 0; i < AttrCount; i++)
            {
                double score = 0;
                foreach (PokeAttrType xSkill in skills)
                {
                    double s = 1;
                    int xline = (int)xSkill;
                    if (IsBenxi(xSkill))
                    {
                        s *= Benxi;
                    }
                    double temp = GetScoreValue(XiangkeMatrix[xline, i] * s);
                    score = temp > score ? temp : score;
                }
                totalScore += score;
                if (score < 1)
                {
                    var t = (PokeAttrType) i;
                    veryWeakList.Add(t);
                }
                else if (score >= 1 && score <= 1.5)
                {
                    var t = (PokeAttrType)i;
                    weakList.Add(t);
                }

            }
            entity.SortList = skills;
            entity.WeakList = weakList;
            entity.VeryWeakList = veryWeakList;
            entity.Score = totalScore;
            sortList.Add(entity);
        }

        private PokeAttrType[] GetList()
        {
            var arr = new PokeAttrType[AttrCount];
            for (int i = 0; i < AttrCount; i++)
            {
                arr[i] = (PokeAttrType) i;
            }
            return arr;
        }

        private int CompareByPokeAttack(PokeAttrType[] x, PokeAttrType[] y)
        {
            double xs = GetTotalScore(x);
            double ys = GetTotalScore(y);

            if (xs < ys)
            {
                return 1;
            }

            if (xs > ys)
            {
                return -1;
            }
            
            return 0;
        }

        private double GetTotalScore(PokeAttrType[] skills)
        {
            double totalScore = 0;
            for (int i = 0; i < AttrCount; i++)
            {
                double score = 0;
                foreach (PokeAttrType xSkill in skills)
                {
                    double s = 1;
                    int xline = (int)xSkill;
                    if (IsBenxi(xSkill))
                    {
                        s *= Benxi;
                    }
                    double temp = GetScoreValue(XiangkeMatrix[xline, i] * s);
                    score = temp > score ? temp : score;
                }
                totalScore += score;
            }
            return totalScore;
        }

        

        private bool IsBenxi(PokeAttrType skill)
        {
            return skill == PropA || skill == PropB;
        }

        private double GetScoreValue(double score)
        {
            if (score <= 1.5)
            {
                return 0;
            }
            return score;
        }

        
        private PokeAttrType PropA { get; set; }
        private PokeAttrType PropB { get; set; }
        private const int AttrCount = 18;
        private const double Benxi = 1.5;
        private const double A = 0;
        private const double S = 0.5;
        private const double D = 1;
        private const double F = 2;
        private int CombNum = 4;

        private static readonly double[,] XiangkeMatrix = new double[AttrCount, AttrCount] 
        { 
            {D,D,D,D,D,D,D,D,D,D,D,D,S,A,D,S,D,D},
            {D,S,S,D,F,F,D,D,D,D,D,F,S,D,S,F,D,D},
            {D,F,S,D,S,D,D,D,F,D,D,D,F,D,S,D,D,D},
            {D,D,F,S,S,D,D,D,A,F,D,D,D,D,S,D,D,D},
            {D,S,F,D,S,D,D,S,F,S,D,S,F,D,S,S,D,D},
            {D,S,S,D,F,S,D,D,F,F,D,D,D,D,F,S,D,D},
            {F,D,D,D,D,F,D,S,D,S,S,S,F,A,D,F,F,D},
            {D,D,D,D,F,D,D,S,S,D,D,D,S,S,D,A,D,F},
            {D,F,D,F,S,D,D,F,D,A,D,S,F,D,D,F,D,D},
            {D,D,D,S,F,D,F,D,D,D,D,F,S,D,D,S,D,D},
            {D,D,D,D,D,D,F,F,D,D,S,D,D,D,D,S,A,D},
            {D,S,D,D,F,D,S,S,D,S,F,D,D,S,D,S,F,D},
            {D,F,D,D,D,F,S,D,S,F,D,F,D,D,D,S,D,D},
            {A,D,D,D,D,D,D,D,D,D,F,D,D,F,D,S,S,D},
            {D,D,D,D,D,D,D,D,D,D,D,D,D,D,F,S,D,A},
            {D,S,S,S,D,F,D,D,D,D,D,D,F,D,D,S,D,F},
            {D,D,D,D,D,D,S,D,D,D,F,D,D,F,D,S,S,D},
            {D,S,D,D,D,D,F,D,D,D,S,D,D,D,F,D,F,D}
        };
    }

}