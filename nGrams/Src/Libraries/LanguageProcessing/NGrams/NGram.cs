using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageProcessing.NGrams
{
    public class NGram
    {
        private List<string> tokenList;
        private string tokenString;
        private int numberOfInstances;
        private double frequency;

        // Note: Generates a tokenString to facilitate sorting of n-grams. See also the NGramSet class
        public NGram(List<string> tokenList)
        {
            this.tokenList = new List<string>();
            tokenString = "";
            foreach (string token in tokenList)
            {
                this.tokenList.Add(token);
                tokenString += token + " ";
            }
            tokenString = tokenString.TrimEnd(' ');
            numberOfInstances = 1; // add a watch in debug mode 
        }

        public static NGram FromShortString(string nGramAsShortString)
        {
            List<string> stringSplit = 
                nGramAsShortString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> tokenList = new List<string>();
            for (int ii = 0; ii < stringSplit.Count-1; ii++)
            {
                tokenList.Add(stringSplit[ii]);
            }
            NGram nGram = new NGram(tokenList);
            nGram.NumberOfInstances = int.Parse(stringSplit[stringSplit.Count - 1]);
            return nGram;
        }

        public string AsShortString()
        {
            string nGramAsShortString = tokenString + " " + numberOfInstances.ToString();
            return nGramAsShortString;
        }

        public string AsString(string frequencyFormatString)
        {
            string nGramAsString = "";
            for (int ii = 0; ii < tokenList.Count; ii++)
            {
                nGramAsString += tokenList[ii].PadRight(18) + " "; 
            }
            nGramAsString += numberOfInstances.ToString().PadLeft(8) +
                    " " + frequency.ToString(frequencyFormatString).PadRight(3 + frequencyFormatString.Length);
            return nGramAsString;
        }

        public List<string> TokenList
        {
            get { return tokenList; }
            set { tokenList = value; }
        }

        public string TokenString
        {
            get { return tokenString; }  // ska inte vara en set oxå här ?
        }

        public int NumberOfInstances
        {
            get { return numberOfInstances; }
            set { numberOfInstances = value; }
        }

        public double Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }
    }
}
