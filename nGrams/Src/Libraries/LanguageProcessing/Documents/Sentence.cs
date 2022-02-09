using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageProcessing.Documents
{
    public class Sentence
    {
        private string sentenceString;
        private List<string> tokenList = null;

        public string SentenceString
        {
            get { return sentenceString; }
            set { sentenceString = value; }
        }

        public void Tokenize()
        {
            tokenList = sentenceString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public string AsString()
        {
            if (tokenList == null)
            {
                return sentenceString;
            }
            else
            {
                string sentenceAsString = "";
                foreach (string token in tokenList)
                {
                    sentenceAsString += token + " ";
                }
                sentenceAsString = sentenceAsString.TrimEnd(' ');
                return sentenceAsString;
            }
        }

        public List<string> TokenList
        {
            get { return tokenList; }
            set { tokenList = value; }
        }
    }
}
