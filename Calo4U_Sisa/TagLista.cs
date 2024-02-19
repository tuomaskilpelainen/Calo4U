using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo4U_Sisa
{
    internal class TagLista
    {
        private List<string> Tags;
        public TagLista()
        {
            Tags = new List<string>();
        }
        public void Lisaa(string tag)
        {
            Tags.Add(tag);
        }
        public void Tyhjenna()
        {
            Tags.Clear();
        }
        public List<string> Hae()
        {
            return Tags;
        }
    }
}
