using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cal4U_Sisa
{
    internal class TagLista
    {
        private List<string> tags = new List<string>();
        public TagLista() 
        {
            tags = new List<string>();  
        }
        public void lisaa(string tag)
        {
            tags.Add(tag);
        }
        public void tyhjenna()
        {
            tags.Clear();
        }
        public List<string> palauta()
        {
            return tags;
        }
    }
}
