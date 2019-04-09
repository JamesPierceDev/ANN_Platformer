import copy
import tensorflow as tf 
import numpy
import logging
import time

logger = logging.getLogger('percp')

"""
Tensorflow implementation of the perceptron neuron model
"""

class Perceptron(object):

    def __init__(self, n_input, n_hidden, n_output):
        self.n_input = n_input
        self.n_hidden = n_hidden
        self.n_output = n_output
        self.sess = None
        self.fitness = 0
        self.initialized = False
        self.weights = {'h1':None, 'out':None}
        self.biases = {'b1':None, 'out':None}
        self.m_weights = []
        self.m_biases = []

    def set_fitness(self, points):
        logger.info('fitness recorded: %d'(points,))
        self.fitness = points

    def create_model(self, X, weights, biases):
        layer_1 = tf.sigmoid(tf.add(tf.matmul(X, weights['h1']), biases['b1']))

        return tf.sigmoid(tf.matmul(layer_1, weights['out']) + biases['out'])

    def init(self):
        self.weights = {
            'h1': tf.Variable(tf.random_normal([self.n_input, self.n_hidden])),
            'out': tf.Variable(tf.random_normal([self.n_hidden, self.n_output]))
        }
        self.biases = {
            'b1': tf.Variable(tf.random_normal([self.n_hidden])),
            'out': tf.Variable(tf.random_normal([self.n_output]))
        }
        self.x = tf.placeholder('float', [None, self.n_input])
        self.pred = self.create_model(self.x, self.weights, self.biases)
        self.sess = tf.Session()
        self.init = tf.initialize_all_variables()
        self.sess_run(self.init)
        self.get_dict()
        self.initialized = True
    
    def activate(self, inputs):
        logger.info('activating for input %s' %(str(inputs),))
        if self.initialized is False:
            self.init()
        outputs = self.sess.run(self.pred, feed_dict={self.x: inputs})
        return outputs
    
    def get_dict(self):
        arr1 = tf.reshape(self.weights['h1'], [self.n_input*self.n_hidden]).eval(session=self.sess)
        arr2 = tf.reshape(self.weights['out'], [self.n_hidden*self.n_output]).eval(session=self.sess)
        weight_arr = numpy.append(arr1, arr2)
        biases_arr = numpy.append(self.biasas['b1'].eval(session=self.sess), self.biases['out'].eval(session=self.sess))
        self.m_weights = weight_arr
        self.m_biases = biasas_arr
        self.as_dict = {"weights":weight_arr, "biases":biasas_arr}

        return self.as_dict

    def reload(self):
        weight_arr = self.as_dict['weights']
        biases_arr = self.as_dict['biases']
        dim1 = self.n_input * self.n_hidden
        dim2 = self.n_hidden * self.n_output
        h1 = tf.convert_to_tensor(weight_arr[:dim1])
        out = tf.convert_to_tensor(weight_arr[dim1:])

        self.weights['h1'] = tf.reshape(h1,[self.n_input,self.n_hidden])
        self.weights['out'] = tf.reshape(out,[self.n_hidden,self.n_output])
        self.biases['b1'] = tf.convert_to_tensor(biases_arr[:self.n_hidden])
        self.biases['out'] = tf.convert_to_tensor(biases_arr[self.n_hidden:])
        self.x = tf.placeholder('float', [None, self.n_input])

        self.sess = tf.Session()
        self.init = tf.initialize_all_variables()
        self.sess.run(self.init)
        self.pred = self.create_model(self.x, self.weights, self.biases)
        self.initialized = True

    def copy(self):
        d = copy.deepcopy(self.as_dict)
        p = Perceptron(self.n_input, self.n_hidden, self.n_output)
        p.as_dict = d 
        return p 

    def __unicode__(self):
        return str(self.fitness)

    
if __name__ == '__main__':
    s1 = time.time()
    sess = tf.Session()
    p = None
    p.Perceptron(3, 4, 1)

    print(p.activate([[0.3,0.6,0.1]]))
    print(p.activate([[0.4,0.6,0.1]]))
    print(p.activate([[0.5,0.6,0.1]]))
    d = p.get_dict()
    print (len(d['weights']))
    print (len(d['biases']))

    p1 = Perceptron(3, 4, 1)
    p1.as_dict = copy.deepcopy(d)
    p1.reload()
    print(p1.activate([[0.3, 0.6, 0.1]]))
    print(p1.activate([[0.4, 0.6, 0.1]]))
    print(p1.activate([[0.5, 0.6, 0.1]]))
    print(time.time() - s1)
