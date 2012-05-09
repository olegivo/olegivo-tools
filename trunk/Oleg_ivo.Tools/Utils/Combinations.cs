using System.Collections.Generic;

namespace Oleg_ivo.Tools.Utils
{
    internal abstract class Combinations
    {

    }

    public class Combination
    {
        private readonly List<object> items = new List<object>();
        private Combination parentCombination;
        private readonly List<Combination> childCombinations = new List<Combination>();
        private readonly List<object> combinationItems = new List<object>();
        private Dictionary<int, List<Combination>> combinationsByCount = new Dictionary<int, List<Combination>>();

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="T:Oleg_ivo.Tools.Utils.Combination" />.
        /// </summary>
        private Combination(Combination parentCombination) : this(parentCombination.Items)
        {
            if (parentCombination!=null)
            {
                this.parentCombination = parentCombination;
                parentCombination.ChildCombinations.Add(this);
                CombinationItems.AddRange(parentCombination.CombinationItems);
            }
        }

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="T:Oleg_ivo.Tools.Utils.Combination" />.
        /// </summary>
        public Combination(IEnumerable<object> items)
        {
            Items.AddRange(items);
        }

        ///<summary>
        /// ������������ ����������
        ///</summary>
        public Combination ParentCombination
        {
            get { return parentCombination; }
        }

        /// <summary>
        /// ��������, �� ������ ������� �������� ����������
        /// </summary>
        public List<object> Items
        {
            get { return items; }
        }

        /// <summary>
        /// �������� ����������
        /// </summary>
        public List<object> CombinationItems
        {
            get { return combinationItems; }
        }

        /// <summary>
        /// �������� ����������
        /// </summary>
        public List<Combination> ChildCombinations
        {
            get { return childCombinations; }
        }

        /// <summary>
        /// ������� ���������� �� �� �����
        /// </summary>
        public Dictionary<int, List<Combination>> CombinationsByCount
        {
            get { return combinationsByCount; }
        }

        /// <summary>
        /// ������ ���������� �������� ������ ����������, ������ �� ��������� ������ ����������
        /// </summary>
        /// <returns></returns>
        private int Max()
        {
            int index = -1;
            if (CombinationItems.Count > 0)
                index = Items.LastIndexOf(CombinationItems[CombinationItems.Count - 1]);
            return index;
        }

        /// <summary>
        /// ������� �������� ����������
        /// </summary>
        /// <returns>�������� ����������</returns>
        public List<Combination> CreateChildCombinations()
        {
            return CreateChildCombinations(Items.Count);
        }

        /// <summary>
        /// ������� �������� ����������
        /// </summary>
        /// <param name="maxCombinationCount">������������ ���������� ��������� ����������� ����������</param>
        /// <returns>�������� ����������</returns>
        private List<Combination> CreateChildCombinations(int maxCombinationCount)
        {
            for (int i = Max() + 1; i < maxCombinationCount; i++)
            {
                Combination combination = new Combination(this);
                combination.CombinationItems.Add(Items[i]);
                combination.CreateChildCombinations(maxCombinationCount);
            }
            if (ParentCombination == null)
                for (int i = 1; i < Items.Count; i++)
                {
                    List<Combination> list = GetCombinationsByCount(i);
                    combinationsByCount.Add(i, list);
                }
            return ChildCombinations;
        }

        private List<Combination> GetCombinationsByCount(int count)
        {
            List<Combination> list = new List<Combination>();
            if (CombinationItems.Count == count)
                list.Add(this);
            foreach (Combination combination in ChildCombinations)
                list.AddRange(combination.GetCombinationsByCount(count));
            return list;
        }

        #region Overrides of Object

        /// <summary>
        /// ���������� ������ <see cref="T:System.String" />, ������� ������������ ������� ������ <see cref="T:System.Object" />.
        /// </summary>
        /// <returns>
        /// ������ <see cref="T:System.String" />, �������������� ������� ������ <see cref="T:System.Object" />.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            string res = "";
            foreach (object item in CombinationItems)
            {
                if (!string.IsNullOrEmpty(res))
                    res += ",";
                res += item.ToString();
            }
            return res;
        }

        #endregion
    }
}