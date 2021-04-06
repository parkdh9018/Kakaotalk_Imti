using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace kakaoImti
{
    struct Value
    {
        public int positionCol;
        public int positionRow;
        public int index;

    }

    class Trie
    {

        private Dictionary<char, Trie> children;
        private List<Value> values;

        public Trie()
        {
            children = new Dictionary<char, Trie>();
            values = new List<Value>();

        }

        public void insert(Trie node, String key, Value value)
        {
            foreach(char c in key)
            {
                if(!children.ContainsKey(c))
                    node.children[c] = new Trie();

                node = node.children[c];
            }
            node.values.Add(value);
        }

        
        public List<Value> findIndex(Trie node, String str)
        {
            List<Value> vList = new List<Value>();
            
            foreach(char c in str)
            {
                if (node.children.ContainsKey(c))
                    node = node.children[c];
                else
                    return vList;
            }

            vList = node.values;

            return vList;
        }

        
    }
    
    class KeywordAnalysis
    {
        Trie trie;

        public KeywordAnalysis()
        {
            trie = new Trie();
        }
        
        public void AddData(int positonRow, int postionCol, List<String> texts)
        {
            Trie trie = new Trie();

            for(int i = 0; i < texts.Count; i++)
            {
                Value value = new Value();
                value.index = i;
                value.positionCol = postionCol;
                value.positionRow = positonRow;

                trie.insert(trie, texts[i], value);
            }
        }

        public List<Value> findImoticon(String keyword)
        {
            return trie.findIndex(trie, keyword);
            
        }
    }
}
