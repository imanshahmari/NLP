using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageProcessing.Documents;

namespace LanguageProcessing.NGrams
{

    public class NGramComparer : IComparer<NGram>
    {
        public int Compare(NGram nGram1, NGram ngram2)
        {
            return nGram1.TokenString.CompareTo(ngram2.TokenString);
        }
    }

    public class NGramSet
    {
        private int n;
        private List<NGram> itemList;
        private List<string> tokenStringList;

        public NGramSet(int n)
        {
            this.n = n;
            itemList = new List<NGram>();
            tokenStringList = new List<string>();
        }

        // To do: Write this method
        //       
        public void Generate(List<Document> documentList)
        {
            itemList = new List<NGram>();


            for (int i = 0; i < documentList.Count; i++)
            {
                for (int j = 0; j < documentList[i].SentenceList.Count; j++)
                {
                    for (int k = 0; k < documentList[i].SentenceList[j].TokenList.Count; k++)
                    {
                        if (n == 1)
                        {
                            List<string> gramOneList = new List<string>();
                            gramOneList.Add(documentList[i].SentenceList[j].TokenList[k].ToString());

                            NGram gramOne = new NGram(gramOneList);
                            itemList.Add(gramOne);
                        }

                        if (n == 2)
                        {


                            List<string> gramTwoList = new List<string>();
                            if (documentList[i].SentenceList[j].TokenList.Count > 1 && k + 1 < documentList[i].SentenceList[j].TokenList.Count)
                            {
                                gramTwoList.Add(documentList[i].SentenceList[j].TokenList[k].ToString());
                                gramTwoList.Add(documentList[i].SentenceList[j].TokenList[k + 1].ToString());
                            }

                            if (gramTwoList.Count > 0)
                            {
                                NGram gramTwo = new NGram(gramTwoList);
                                itemList.Add(gramTwo);
                            }

                        }

                        if (n == 3)
                        {
                            List<string> gramThreeList = new List<string>();
                            if (documentList[i].SentenceList[j].TokenList.Count > 2 && k + 2 < documentList[i].SentenceList[j].TokenList.Count)
                            {
                                gramThreeList.Add(documentList[i].SentenceList[j].TokenList[k].ToString());
                                gramThreeList.Add(documentList[i].SentenceList[j].TokenList[k + 1].ToString());
                                gramThreeList.Add(documentList[i].SentenceList[j].TokenList[k + 2].ToString());
                            }

                            if (gramThreeList.Count > 0)
                            {
                                NGram gramThree = new NGram(gramThreeList);
                                itemList.Add(gramThree);
                            }

                        }

                        if (n == 4)
                        {
                            List<string> gramFourList = new List<string>();
                            if (documentList[i].SentenceList[j].TokenList.Count > 3 && k + 3 < documentList[i].SentenceList[j].TokenList.Count)
                            {
                                gramFourList.Add(documentList[i].SentenceList[j].TokenList[k].ToString());
                                gramFourList.Add(documentList[i].SentenceList[j].TokenList[k + 1].ToString());
                                gramFourList.Add(documentList[i].SentenceList[j].TokenList[k + 2].ToString());
                                gramFourList.Add(documentList[i].SentenceList[j].TokenList[k + 3].ToString());
                            }
                            if (gramFourList.Count > 0)
                            {
                                NGram gramFour = new NGram(gramFourList);
                                itemList.Add(gramFour);
                            }
                        }
                    }
                }
            }



            List<NGram> itemListNew = new List<NGram>();

            NGramComparer itemListComparer = new NGramComparer();
            itemList.Sort((a, b) => itemListComparer.Compare(a, b));

            List<string> distinctWords = new List<string>();
            List<string> allWords = new List<string>();

            for (int i = 0; i < itemList.Count - 1; i++)
            {
                if (itemList[i].TokenString != itemList[i + 1].TokenString)
                {
                    itemListNew.Add(itemList[i]);
                }

            }
            int index = 0;
            for (int i = 0; i < itemList.Count - 1; i++)
            {
                while (itemList[i].TokenString == itemList[i + 1].TokenString)
                {
                    itemListNew[index].NumberOfInstances++;
                    i++;

                }
                index++;

            }

            itemList = itemListNew;
            // Loop over all documents (outer loop) and
            // all sentences in each document (inner loop),
            // and form n-grams from the token list of each sentence
            // 
            // Use the NGramComparer to Sort the NGrams when needed, i.e.  
            // itemList.Sort(comparer), where comparer is an instance of NGramComparer
            //
            // NGramComparer comparer = new NGramComparer();
            //
            // Alternatively, you can avoid using the NGramComparer, and instead just
            // sort as in the SortAlphabetically method below
            //
            // Easiest way: Add ALL Ngrams, with a count of 1 for each instance, 
            // then reduce the list, counting the
            // number of copies of each Ngram, and keeping just one copy + the count
            //
            // Example (1-grams)
            //
            // ...
            // book
            // book
            // book
            // books
            // books
            // ...
            //
            // -> NGram: book, numberOfInstances = 3
            // -> NGram: books, numberOfInstances = 2
            // ... and so on. (see also the NGram class)
            //
            // The end result should be a list of NGrams, one instance per
            // NGram along with the number of occurrences (Frequency) of the NGram.
            if( n==1)
            {
                Console.WriteLine("1gram" + itemList.Count);
            }
            else if (n == 2)
            {
                Console.WriteLine("2gram" + itemList.Count);
            }
            else if (n == 3)
            {
                Console.WriteLine("3gram" + itemList.Count);
            }
            else if (n == 4)
            {
                Console.WriteLine("4gram" + itemList.Count);
            }



        }

        // To do: Write this method
        //
        // Sum the total number of tokens (given the itemList, where each
        // NGram has a NumberOfInstances). Then compute the relative
        // frequencies (count(ngram)/totalCount) for each Ngram.
        public void ComputeFrequencies()
        {
            int totalCount = 0;
            for (int i = 0; i < itemList.Count; i++)
            {
                NGram ng = itemList[i];
                totalCount += ng.NumberOfInstances;
            }
            for (int i = 0; i < itemList.Count; i++)
            {
                NGram ng = itemList[i];
                ng.Frequency = Convert.ToDouble(ng.NumberOfInstances) / Convert.ToDouble(totalCount);
            }
        }

        // Not currently used ...
        public void SortAlphabetically()
        {
            itemList.Sort((a, b) => a.TokenString.CompareTo(b.TokenString));
        }

        // This method you get for free... :)
        // It sorts the NGrams in descending order of frequency (and, for NGrams with
        // the same frequency, then in alphabetical order).
        public void SortInFrequency()
        {
            itemList = itemList.OrderByDescending(a => a.Frequency).ThenBy(b => b.TokenString).ToList();
        }

        public int N
        {
            get { return n; }
            set { n = value; }
        }

        public List<NGram> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }

        public List<string> TokenStringList
        {
            get { return tokenStringList; }
        }
    }
}
