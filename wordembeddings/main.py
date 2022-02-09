from sklearn.manifold import TSNE
from matplotlib import pyplot as plt
from sklearn.neighbors import KNeighborsClassifier
from classes import *

# Pre-processing input training data (reviews)
######################
#change this if you want to use another dataset
wordlist_train = open('Data/RestaurantReviewsTrainingSet.txt', 'r+').readlines()
######################
wordlist_train = [i.replace('\n', '') for i in wordlist_train]
wordlist_train = [i.replace('\t', '') for i in wordlist_train]

stop_words = open('Data/StopWords.txt', 'r+').readlines()
stop_words = [i.replace('\n', '') for i in stop_words]
stop_words = [i.replace('\t', '') for i in stop_words]


# Here we delete unnecessary characters,stop words,etc and create a list of document objects
document_list_train = []
index = 0
for sentence in wordlist_train:
    temp_string = ""
    label = int(sentence[-1])
    for character in sentence:
        if character.isalpha() == True or (character.isspace() == True):
           temp_string = temp_string + character
    temp_string = temp_string.lower()
    temp_words = temp_string.split(" ")
    final_words = []
    for string in temp_words:
        if string not in stop_words :
            final_words.append(string)
    document = Document(final_words,label,index)
    document_list_train.append(document)
    index = index + 1

# We use the vectors of each document to create feature and label list which is feed in to the knn classifier
features_train = [document.vector for document in document_list_train]
labels_train = [document.label for document in document_list_train]

#################################
# Choose how many neighbours the knn classifier should look at for classifying
n = 11
#################################

knn_model = KNeighborsClassifier(n_neighbors=n)
knn_model.fit(features_train,labels_train) #KNN classifer is trained using the the trainig dataset

# input the test data and pre-processing it (same as before)
#########################
#Change if you want to change the test dataset
wordlist_test = open('Data/RestaurantReviewsTestSet.txt', 'r+').readlines()
#########################
wordlist_test = [i.replace('\n', '') for i in wordlist_test]
wordlist_test = [i.replace('\t', '') for i in wordlist_test]


document_list_test = []

index = 0
for sentence in wordlist_test:
    temp_string = ""
    label = int(sentence[-1])
    for character in sentence:
        if character.isalpha() == True or (character.isspace() == True):
           temp_string = temp_string + character
    temp_string = temp_string.lower()
    temp_words = temp_string.split(" ")
    final_words = []
    for string in temp_words:
        if string not in stop_words :
            final_words.append(string)
    document = Document(final_words, label, index)
    document_list_test.append(document)
    index = index + 1

labels_test = [document.label for document in document_list_test] # Extracting labels of the documents(ground truth)


result = [knn_model.predict(document.vector.reshape(1,-1)) for document in document_list_test] # We predict the data

#Computing metrics
tp= 0
fp = 0
fn = 0
tn = 0

for i in range(0,len(labels_test)):
    if (result[i][0] == 1 and labels_test[i] == 1):
        tp += 1
    elif(result[i][0] == 1 and labels_test[i] == 0):
        fp += 1
    elif(result[i][0] == 0 and labels_test[i] == 1):
        fn += 1
    elif(result[i][0] == 0 and labels_test[i] == 0):
        tn += 1

precision = tp/(tp+fp)
recall = tp/(tp+fn)
accuracy = (tp+tn)/(tp+fn+tn+fp)
f1 = 2 * precision * recall/(recall + precision)

print(f'Precision {precision}\nRecall {recall}\nAccuracy {accuracy}\nF1 {f1}\n')



#TSNE
#Here I use the 10khighlyfrequentwords.txt for the TSNE

#############################
#The location of the file should be the same if you dont change anything
wordlist_train_tsne = open('Data/10k_highlyfrequentwords.txt', 'r+').readlines()
#############################

# Obtain all the words in the file and make it to a list and get the vector for each word using gensim model
wordlist_train_tsne = [i.replace('\n', '') for i in wordlist_train_tsne]
wordlist_train_tsne = [i.replace('\t', '') for i in wordlist_train_tsne]
wordlist_train_tsne = [word for word in wordlist_train_tsne if word in model_gensim]
training_vectors_tsne = np.array([model_gensim[word] for word in wordlist_train_tsne if word in model_gensim])


#Here some test words to see if TSNE is working

positive_words =["great", "excellent", "delicious"]
negative_words =["horrible", "awful", "terrible"] # I used the word terrible instead of the word "disgusting" because tsne model is fitted
# on the file named 10k_highlyfrequentwords.txt' with 10k words in it.
food_related_words = ["salad", "tomato","onion","potato"]
kitchen_related_words = ["knife","refrigerator","fridge","dishes","grill"]

#Some words in the 19k_highlyfrequentwords.txt is missing in the gensim model so we do the following to obtain correct indices later
positive_words_vectors =np.array([model_gensim[word] for word in positive_words if word in model_gensim])
negative_words_vectors =np.array([model_gensim[word] for word in negative_words if word in model_gensim])
food_words_vectors =np.array([model_gensim[word] for word in food_related_words if word in model_gensim])
kitchen_words_vectors =np.array([model_gensim[word] for word in kitchen_related_words if word in model_gensim])

#Here again we want to train another tsne model but this time for the entire reviews to see if it works on them too
train_reviews_vectors = np.array([document.vector for document in document_list_train])
test_reviews_vectors = np.array([document.vector for document in document_list_test])
all_review_vectors = np.concatenate((train_reviews_vectors,test_reviews_vectors),axis=0)

#Here we create tsne model for words and for our reviews OPS!!!! feed in the entire dataset and plot some of them later.
words_tsne = TSNE(n_components = 2, random_state=0).fit_transform(training_vectors_tsne)
review_tsne = TSNE(n_components = 2, random_state=0).fit_transform(all_review_vectors)

#Here we obtain the indices for plots we want to make of both words and also reviews.
my_bad_words_index = [wordlist_train_tsne.index(x) for x in negative_words]
my_good_words_index = [wordlist_train_tsne.index(x) for x in positive_words]
my_vegetables_index = [wordlist_train_tsne.index(x) for x in food_related_words]
my_kitchen_index = [wordlist_train_tsne.index(x) for x in kitchen_related_words]

my_positive_reviews_index = [1,2,6,7,8,11,14,17] #Here i just looked at documets and found some indexes look at document_list_train
my_negative_reviews_index = [0,4,5,9,10,12,15,16]

#Make the plot object and plot it :)
fig1,ax1= plt.subplots()
ax1.plot(words_tsne[my_bad_words_index, 0], words_tsne[my_bad_words_index, 1], 'ro')
ax1.plot(words_tsne[my_good_words_index, 0], words_tsne[my_good_words_index, 1], 'bo')
ax1.legend(['negative words', 'Positive words'])

fig2,ax2 =plt.subplots()
ax2.plot(words_tsne[my_vegetables_index, 0], words_tsne[my_vegetables_index, 1], 'go')
ax2.plot(words_tsne[my_kitchen_index, 0], words_tsne[my_kitchen_index, 1], 'yo')
ax2.legend(['vegetable words', 'kitchen words'])

fig3,ax3 =plt.subplots()
ax3.plot(review_tsne[my_positive_reviews_index, 0], review_tsne[my_positive_reviews_index, 1], 'mo')
ax3.plot(review_tsne[my_negative_reviews_index, 0], review_tsne[my_negative_reviews_index, 1], 'co')
ax3.legend(['positive reviews', 'negative reviews'])

plt.show()

##################
# Set save to 1 if you want to save the file
save = 0
##################
if save == 1:
    text_file = open("/Users/iman/Library/CloudStorage/Box-Box/CAS/Conversational AI/HW1/untitled folder/k11.txt", "w")
    text_file.write("Neighbours:{}   Precision:{}   Recall:{}   Accuracy:{}   f1:{}".format(n,precision,recall,accuracy,f1))
    text_file.close()
    vectors_train =  np.array([document.vector for document in document_list_train])
    np.savetxt("train.txt", vectors_train, delimiter=',')
    vectors_test =  np.array([document.vector for document in document_list_test])
    np.savetxt("test.txt", vectors_test, delimiter=',')