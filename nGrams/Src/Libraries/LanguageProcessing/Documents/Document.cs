using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageProcessing.Documents
{
    public class Document
    {
        private string rawData;
        private List<Sentence> sentenceList;

        public Document()
        {
            // Nothing to do here - not really needed.
        }

        // To do: Write this method:
        //
        // In this method, clean the rawData (storing the final result in rawData as well,
        // no reason to use a new variable name).
        public void Clean()
        {
            // Start by normalizing the text, converting the raw data string to lower-case:
            // Hint: Use the ToLower() command
            // Add code here:
            rawData = rawData.ToLower();
            // If you choose to use data from Wikipedia (or certain other sources), the text
            // will contain references, e.g. as "...their king[53]". You should remove
            // such occurrences, i.e. examples of the type <word>[nnn], where <word> is
            // any word and [nnn] is a number within brackets. Note: Remove the "[nnn]",
            // NOT the word itself. Example: ... king[53] -> ... king

            // Add code here:

            // clean the raw data string by removing special characters, e.g. 
            // " , ( , ) , { , } , [ , ] , <, >, -, ... and so on.
            // Note: Some characters will not be found on your keyboard. Thus, instead of
            // using Replace() (as was done in Problem1.1) run through every character
            // of the rawData, *keeping* the character only if it is any of the following:
            //
            // (1) a letter (a ... z),
            // (2) a digit (0 ... 9), 
            // (3) a space character,
            // (4) the character \n (newline) or the character \r (return) 
            // (5) the character \' (single quote character, as in the word don't)
            // (6) hyphen (-)
            // (7) full stop (.), exclamation point (!), or question mark (?)
            //
            // Here, use the StringBuilder class - it is *much* faster than
            // building a string character-by-character. Then, in the end,
            // obtain the rawdata using the ToString() method in the StringBuilder class.
            //
            //       Problem: This method removes certain characters, e.g. ü as in Zürich,
            //                é as in Élysées etc.
            //       See if you can fix that, otherwise you'll get some incorrect words
            //       (e.g. Zürich will be turned into Zrich etc.)
            //       Hint: You have to manually include the (additional) characters that should be kept,
            //             e.g. ü, é. 
            //

            // Add code here:

            StringBuilder rawDataSB = new StringBuilder();

            foreach (char c in rawData)
            {
                if (Char.IsLetter(c))
                {
                    rawDataSB.Append(c);
                }
                else if (Char.IsDigit(c))
                {
                    rawDataSB.Append(c);
                }
                else if (Char.IsWhiteSpace(c))
                {
                    rawDataSB.Append(c);
                }
                else if (c == '\n' || c == '\r')
                {
                    rawDataSB.Append(c);
                }
                else if (c == '\'')
                {
                    rawDataSB.Append(c);
                }
                else if (c == '-')
                {
                    rawDataSB.Append(c);
                }
                else if(c =='.' || c == '?' || c== '!' )
                {
                    rawDataSB.Append(c);
                }
                else if(c == 'ü' || c == 'é')
                {
                    rawDataSB.Append(c);
                }


            }

            string newRawData = rawDataSB.ToString();






            // Next, remove any instance of the ' character *when it is used as a quote*, e.g.
            // as in: " 'I don't know', he said ". However, make sure to *keep*
            // the ' character in contractions such as don't, won't, can't etc.
            // Hint: In the latter case, the ' is surrounded by letters, whereas in the
            // former it has a space character on one side (left or right)

            // Add code here:
            string newRawDataCopy = string.Copy(newRawData);

            int numberOfChars = newRawData.Length;
            for (int i = 0; i < numberOfChars; i++)
                if (newRawData[i] =='\'')
                {
                    if((Char.IsLetter(newRawData[i-1])==false && Char.IsLetter(newRawData[i +1]) == true) || (Char.IsLetter(newRawData[i - 1]) == true && Char.IsLetter(newRawData[i + 1]) == false))
                    {
                        newRawDataCopy.Remove(i);
                    }
                }

            rawData = newRawDataCopy;

            // Are you done now? Have you removed all unwanted characters? Have
            // you handled abbreviations (if any), for example? (e.g. U.S.A.)
            // Maybe, maybe not - check very carefully (note that you can view
            // any document after the Clean() step, by clicking on the document
            // index in the listBox on the left-hand side of the MainForm (under the Data tab).

            // (Maybe) add code here, if needed ...
        }






        // To do: Write this method
        //
        // This method should split the document into sentences. This is important
        // since we wish to retain the interpunction characters (here collectively
        // assigned as "."), so as to get n-grams of the from (.,he,sat) (3-gram) or (the,door,.) (also 3-gram).
        public void GenerateSentences()
        {
            // Step 1: Make sure to keep abbreviations such as "mr.", "mrs.", "prof." ...
            // In those cases, the full stops should *not* define a new sentence!
            // Hint: Since all characters are now lower-case, you can, for example, replace "mr." by "mrA" etc.,
            // then split into sentences, the, at the end, replace back: mrA -> mr.
            // Alternatively, you can simply spell them out, i.e. mr. -> mister, dr. -> doctor etc.

            // Add code here

            string[] words = rawData.Split(' ');
            int numberOfWords = words.Length;
            for(int i = 0;i < numberOfWords;i++)
                if(words[i].Equals("mr."))
                {
                    words[i] = "mister";
                }
                else if (words[i].Equals("dr."))
                {
                    words[i] = "doctor";
                }
                else if (words[i].Equals("mrs."))
                {
                    words[i] = "miss";
                }
                else if (words[i].Equals("prof."))
                {
                    words[i] = "professor";
                }
            string wordsAfterAbbreviation = String.Join(" ", words);

            // Step 2: Numbers are another problem, e.g. "12.6" - should not define a new sentence!
            // Again, use StringBuilder (see above) to replace x.y by xBy (temporarily), where
            // x and y are digits (no need to consider digits preceding x or following y),
            // and then change back (xBy -> x.y) after splitting ...

            string wordsNoPointInNumbers = string.Copy(wordsAfterAbbreviation);

            int numberOfChars = wordsAfterAbbreviation.Length;
            for (int i = 0; i < numberOfChars; i++)
                if (wordsAfterAbbreviation[i] == '.')
                {
                    if ((Char.IsNumber(wordsAfterAbbreviation[i - 1]) == true && Char.IsNumber(wordsAfterAbbreviation[i + 1]) == true))
                    {
                        wordsNoPointInNumbers.Replace(wordsNoPointInNumbers[i], 'B');
                    }
                }
            // Add code here


            // Step 3: Split the rawData using the interpunction list below.
            // Note: You must actively handle instance of "\r\n" (return + new line), noting
            // that sentences may span several lines!
            // Note also that, in the case of books from Gutenberg.org, it is common to
            // have sentences separated by multiple "\r\n", e.g. "\r\n\r\n". You must
            // handle this as well, so that you don't accidentally *join* text that
            // belongs to different sentences.

            string rawDataNorn = wordsNoPointInNumbers.Replace("\r\n\r\n", " ");
            rawDataNorn = wordsNoPointInNumbers.Replace("\r\n", " ");


            String[] spearator = { ".", "?","!"};

            List<string> splittedRawData = new List<string>();
            splittedRawData = rawDataNorn.Split(spearator,StringSplitOptions.RemoveEmptyEntries).ToList();




            sentenceList = new List<Sentence>();
            // Add code here, for filling the sentence list (see also the Sentence class)

            // Step 4: Remove the replacement characters used above, i.e.
            // A -> ., B -> ., (etc., in case you need more replacements of that kind

            // Add code here.

            // Note that, in the end, the list of sentences will be shown in the listbox
            // on the MainForm (the right panel under the Data tab), one sentence per row
            // (drag the window out to full length avoid word-wrapping), so that you can
            // investigate the sentences
            foreach(string data in splittedRawData)
            {
                Sentence thesentence = new Sentence();
                thesentence.SentenceString = data;
                sentenceList.Add(thesentence);
            }

        }

        public void Tokenize()
        {
            foreach (Sentence sentence in sentenceList)
            {
                sentence.Tokenize(); // This step is quite trivial here, since all the hard work was done 
                                     // above. Hence, you get this method for free! :)
            }
        }

        public string AsString()
        {
            string documentAsString = "";
            foreach (Sentence sentence in sentenceList)
            {
                documentAsString += sentence.AsString() + "\r\n";
            }
            return documentAsString;
        }

        public int GetTokenCount()
        {
            int tokenCount = 0;
            foreach (Sentence sentence in sentenceList)
            {
                tokenCount += sentence.TokenList.Count;
            }
            return tokenCount;
        }

        public string RawData
        {
            get { return rawData; }
            set { rawData = value; }
        }

        public List<Sentence> SentenceList
        {
            get { return sentenceList; }
            set { sentenceList = value; }
        }
    }
}
