import numpy as np
import gensim
##########################
model_gensim = gensim.models.KeyedVectors.load_word2vec_format('/Users/iman/Downloads/GoogleNews-vectors-negative300.bin', binary=True)
##########################
class Document:
    def __init__(self, token_list: list, label: int, index=int):
        """
        :param token_list: list of words in the document after preprocessing
        :param label: every document has a label which can be postive 1 or negative 0
        :param index: every document has an id called index to acess it easier
        """
        self.token_list = token_list
        self.label = label
        self.vector = np.array([])
        self.document_index = index
        self.compute_vector() #when intilizing a document we call the computevector method which is in the class

    def __str__(self):
        if self.label == 1:
            klass = "Positive Document"
        else:
            klass = "Negative Document"
        return klass

    def compute_vector(self):
        """
        this method computes the average of the all words in the document given the documents tokenlist using the gensim model
        and it also normlizes the vectrs to speed up the process
        :return: doesnt return anything it sets the document vecotor to the computed vector
        """
        temp_vector = np.zeros([model_gensim.vector_size])
        for word in self.token_list:
            try:
                temp_vector = temp_vector + model_gensim[word]
            except Exception:
                pass
        temp_vector = temp_vector / len(self.token_list)
        temp_vector_normed = (temp_vector - temp_vector.mean(axis=0)) / temp_vector.std(axis=0)

        self.vector= temp_vector_normed