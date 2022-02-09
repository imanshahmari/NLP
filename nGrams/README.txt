In the Src folder you will find the skeleton code. Open
the NGramsSolution.sln (in the NGramsSolution folder)

Several methods (or parts thereof) have been removed from
the complete solution to the problem. Thus, initially, even
though the code compiles (so that you can, for example, read
in the data), it does not really *do* anything. You need to 
add code first. To find out what to add, you can search for 
"To do" using Edit - Find and Replace - Find in Files, in 
the C# IDE. You need to write code for cleaning the
data, splitting it into sentences, and then tokenizing.

Note that you must generate (and include) your own data set,
as mentioned in the problem formulation.

To run the code (once it has been completed, by you), start
the program, load the documents (you can use multi-select to
load many documents at once). You can view the documents
(initially, and after each pre-processing step) by clicking
on the document index in the listbox at the left-hand side of
the data tab page. After that, move to the Analysis tab where
you should generate n-grams (n = 1,2,3, and 4), after writing
the necessary code for the "Generate n-grams" button event handler.

Note that I did not bother to thread the code, so the GUI will 
freeze up for a while, when the n-grams are being generated (and,
possibly, also during sentence generation and tokenization).
If you wish, you can run that operation in a separate thread; see
also Appendix A.4 in the compendium (not required, though).

I did not write code for saving the lists of (top) n-grams,
you'll have to do that yourselves.