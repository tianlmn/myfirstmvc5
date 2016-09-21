using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using DataAccessLayer;
using mvc5_first.Common;

namespace BusinessLayer
{
    public class PokemonBusinessLayer
    {
        public PokemonBusinessLayer() { }
        public PokemonBusinessLayer(PokeAttrType p1, PokeAttrType p2, int combNum)
        {
            PropA = p1;
            PropB = p2;
            CombNum = combNum > 6 ? 6 : combNum;
        }


        public List<PokemonEntity> GetPokemonEntityList()
        {
            var pokemonDal = new PokemonDal();
            var pokemons = pokemonDal.PokemonEntities.ToList();
            return pokemons;
        }

        public void AddPoke(PokemonEntity entity)
        {
            var pokemonDal = new PokemonDal();
            var pokemons = pokemonDal.PokemonEntities.ToList();
            if (!pokemons.Exists(p => p.PokemonName == entity.PokemonName.Trim()))
            {
                pokemonDal.PokemonEntities.Add(entity);
            }
            pokemonDal.SaveChanges();

        }

        public UserStatus GetUserValidity(UserDetails u)
        {
            if (u.UserName == "admin" && u.Password == "admin")
            {
                return UserStatus.AuthenticatedAdmin;
            }
            else if (u.UserName == "xiaof" && u.Password == "xiaof")
            {
                return UserStatus.AuthentucatedUser;
            }
            else
            {
                return UserStatus.NonAuthenticatedUser;
            }
        }


        public void UploadPokemon(List<PokemonEntity> list)
        {
            var pokeDal = new PokemonDal();
            pokeDal.PokemonEntities.AddRange(list);
            pokeDal.SaveChanges();
        }

        public PokeEntity CalCombScore()
        {
            PokeEntity pokeEntity = new PokeEntity();
            var attrList = GetList();
            List<PokeAttrType[]> combList = CombAlgorithm<PokeAttrType>.GetCombination(attrList, CombNum);

            combList.Sort(CompareByPokeAttack);
            var temp = combList.GetRange(0, 100);
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
            var list100 = new List<PokeAttrType>();
            var list150 = new List<PokeAttrType>();
            var list300 = new List<PokeAttrType>();
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
                    double temp = XiangkeMatrix[xline, i] * s;
                    score = temp > score ? temp : score;
                }
                totalScore += (score > 1.5 ? score : 0);
                if (score <= 1)
                {
                    var t = (PokeAttrType)i;
                    list100.Add(t);
                }
                else if (score <= 1.5)
                {
                    var t = (PokeAttrType)i;
                    list150.Add(t);
                }
                else if (score <= 3)
                {
                    var t = (PokeAttrType)i;
                    list300.Add(t);
                }

            }
            entity.SortList = skills;
            entity.List_100 = list100;
            entity.List_150 = list150;
            entity.List_300 = list300;
            entity.Score = totalScore;
            sortList.Add(entity);
        }

        private static PokeAttrType[] GetList()
        {
            var arr = new PokeAttrType[AttrCount];
            for (int i = 0; i < AttrCount; i++)
            {
                arr[i] = (PokeAttrType)i;
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