========================================
Step-by-step guide to Word2Vec in Python
as required for Assignment 1.4:
========================================

In addition to reading the text below, you may also wish to
search for "Word2Vec python", or something like that. There
are plenty of tutorials online.

----------------------------------------------------------------------------------------------------------
Python:
----------------------------------------------------------------------------------------------------------

Installing Python:
* Problem with Python 3.10, a bit problematic to install numpy
  https://stackoverflow.com/questions/2812520/dealing-with-multiple-python-versions-and-pip
* Suggestion: Install Python 3.8 instead
* To install a package under Python 3.8 (if other Python versions are available),
  use py -<version> -m pip install <package>

----------------------------------------------------------------------------------------------------------
Word2Vec:
----------------------------------------------------------------------------------------------------------

Obtaining Word2Vec:
* To run Word2Vec, the gensim package is needed: https://pypi.org/project/gensim/
* Gensim requires numpy and scipy, install first:
  py -3.8 -m pip install numpy
  py -3.8 -m pip install scipy
* Then install Gensim:
  py -3.8 -m pip install gensim
* Then download Word2Vec (standard trained model from Google) from
  https://drive.google.com/u/0/uc?id=0B7XkCwpI5KDYNlNUTTlSS21pQmM&export=download
  NOTE: Large file, around 3.6 Gb uncompressed.
* Start Python:
* Import gensim and load Word2Vec. 
>>> import gensim
>>> model = gensim.models.KeyedVectors.load_word2vec_format('<file-path>',binary=True)
  where <file-path> is the search path to the Word2Vec file, GoogleNews-vectors-negative300.bin

Usage examples: 

* Example 1: Finding the cosine similarity (see Chapter 4) between two related words
>>> import numpy as np
>>> cosine_similarity = np.dot(model['riddle'], model['enigma'])/(np.linalg.norm(model['riddle'])* np.linalg.norm(model['enigma']))
>>> cosine_similarity 
0.57354695

* Example 2: Finding the words that are most similar to a given word (in terms of cosine similarity)
>>> model.most_similar('riddle',topn=5)
[('mystery', 0.6793243288993835), ('riddles', 0.6546879410743713), ('conundrum', 0.6521844863891602), ('conundrums', 0.6042738556861877), ('mysteries', 0.6019241809844971)]

Every token embedding is a 300-dimensional vector. Thus, given a sequence of tokens (e.g. a sentence) it is
easy to form the average vector, as required for this problem.

Then implement kNN classification, either in C# or in Python; see also Section 3.2.4 in 
the compendium.

--------------------------------------------------------------------------------------------------------------------------------
tSNE visualization:
--------------------------------------------------------------------------------------------------------------------------------
* tSNE visualization requires sklearn https://pypi.org/project/scikit-learn/  
  Install:
  py -3.8 -m pip install sklearn
* Visualization with tSNE: Needs TSNE from sci-kit and plotly
  Command prompt
  py -3.8 -m pip install sklearn
  py -3.8 -m pip install plotly

* Define text file with a word list (one word (or, rather, token - could be multiple words) per line
* Then, to make a tSNE plot for those words (this is an *example* - can be implemented in different ways):

>>> from sklearn.manifold import TSNE               
>>> import numpy as np                                
>>> from plotly.offline import init_notebook_mode, iplot, plot
>>> import plotly.graph_objs as go
>>> wordlist = open('WordSamples.txt','r+').readlines()
>>> wordlist = [i.replace('\n','') for i in wordlist]

* Extract embedding vectors and labels:

>>> labels = [word for word in wordlist if word in model]
>>> vectors =  [model[word] for word in wordlist if word in model]
>>> x_vals = [v[0] for v in vectors]
>>> y_vals = [v[1] for v in vectors]

Select the first m+1 words in the wordlist to *plot* (even though the other
words are needed when *generating* the tSNE information: tSNE needs many
comparison words to give a good result; see
https://stackoverflow.com/questions/33317896/scikit-learn-tsne-transform-returns-strange-results-when-applied-to-word-vectors

>>> x_vals = x_vals[0:m]  
>>> y_vals = y_vals[0:m]

... where m is the number of plotted words.

* Generate tSNE
>>> tsne = TSNE(n_components=2,random_state=0)
>>> vectors = tsne.fit_transform(vectors)

* Make the plot
>>> trace = go.Scatter(x=x_vals,y=y_vals,mode='text+markers',text=labels,textposition='top center')
>>> data = [trace]
>>> plot(data, filename='word-embedding-plot.html')