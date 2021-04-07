using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace kakaoImti
{
    struct Value
    {
        public int listIndex;
        public int index;
    }

    class Trie
    {
        public Dictionary<char, Trie> children;
        public List<Value> values;

        public Trie()
        {
            children = new Dictionary<char, Trie>();
            values = new List<Value>();
        }

        
    }
    
    class KeywordAnalysis
    {
        Trie node;

        public KeywordAnalysis()
        {
            node = new Trie();   
        }

        public void insert(String key, Value value)
        {
            Trie root = node;

            foreach (char c in key)
            {
                if (!root.children.ContainsKey(c))
                {
                    root.children[c] = new Trie();
                    root.values.Add(value);

                }
                root = root.children[c];
            }

            root.values.Add(value);

        }


        public List<Value> findIndex(String str)
        {
            List<Value> vList = new List<Value>();
            Trie root = node;

            foreach (char c in str)
            {
                if (root.children.ContainsKey(c))
                    root = root.children[c];
                else
                    return vList;
            }

            vList = root.values;

            return vList;
        }

        public void AddData(int listIndex, List<String> keywordTexts)
        {

            for(int i = 0; i < keywordTexts.Count; i++)
            {
                Value value = new Value();
                value.index = i;
                value.listIndex = listIndex;

                List<String> texts = keywordTexts[i].Split(',').ToList();

                foreach(String text in texts)
                {
                    insert(text, value);
                }

            }
        }


    }
}
