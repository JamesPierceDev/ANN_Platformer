#GA Test

import numpy as np

#X - (current state, distance to cactus) y - (action to do 1/0) 
#Current state(0=neutral, 1=jumping) action(0=nothing, 1=jump)


#X = np.loadtxt("training_data.txt",delimiter=",")
#X = np.array(([1, 10], [0, 2], [1, 25]), dtype=float)
#y = np.array(([1], [0.5], [0]), dtype=float)
#xPred = np.array([0, 5], dtype=float)

X = np.array(([2,9],[1,5],[3,6]), dtype=float)
y = np.array(([92],[86],[89]), dtype=float)
xPred = np.array(([4,8]), dtype=float)

#scaling units
X = X/np.amax(X, axis=0) #Max of array
xPred = xPred/np.amax(xPred, axis=0)
y = y/100

class Neural_Network(object):
    def __init__(self):
        #params
        self.inputSize = 2
        self.outputSize = 1
        self.hiddenSize = 3

        #weights
        self.W1 = np.random.randn(self.inputSize, self.hiddenSize)
        self.W2 = np.random.randn(self.hiddenSize, self.outputSize)

    def forward(self, X):
        #Forward propagation
        self.z = np.dot(X, self.W1)
        self.z2 = self.sigmoid(self.z)
        self.z3 = np.dot(self.z2, self.W2)
        o = self.sigmoid(self.z3)
        return o 

    def sigmoid(self, s):
        #sigmoid activation function
        return 1/(1+np.exp(-s))

    def sigmoidPrime(self, s):
        #derivative of sigmoid
        return s * (1 - s)

    def backward(self, X, y, o):
        #back propagation
        self.o_error = y - o
        self.o_delta = self.o_error*self.sigmoidPrime(o)

        self.z2_error = self.o_delta.dot(self.W2.T)
        self.z2_delta = self.z2_error*self.sigmoidPrime(self.z2)

        self.W1 = X.T.dot(self.z2_delta)
        self.W2 = self.z2.T.dot(self.o_delta)

    def train(self, X, y):
        o = self.forward(X)
        self.backward(X, y, o)

    def saveWeights(self):
        np.savetxt("W1.txt", self.W1, fmt="%s")
        np.savetxt("W2.txt", self.W2, fmt="%s")

    def predict(self):
        print("Predicted data based on trained weights: ")
        print("Input (scaled): \n", str(xPred))
        print("Output: \n", str(self.forward(xPred)))

NN = Neural_Network()
for i in range(500000): #train the Net over 1000 epochs
    print("# " + str(i), "\n")
    print("Input (scaled): \n", str(X))
    print("Actual Output: \n", str(y))
    print("Predicted Output: \n", str(NN.forward(X)))
    print("Loss: \n", str(np.mean(np.square(y - NN.forward(X)))))
    print("\n")
    NN.train(X, y)

NN.saveWeights()
NN.predict()