import collections
from collections import OrderedDict
import copy
import logging
import numpy as np
import random
import tensorflow as tf 
from threading import Thread 
from Perceptron import Perceptron 
import time

logger = logging.getLogger('GA.learner')

class Learner(object):

    def __init__(self, genomeUnits, selection, mutationProb):
        self.genomes = []
        self.state = 'STOP'
        self.genome = 0
        self.generation = 0
        self.shouldCheckExp = False
        self.genomeUnits = genomeUnits
        self.selection = selection
        self.mutationProb = mutationProb
        self.interupted = False

    def beginLearning(self, stop_event):
        self.stop_event = stop_event
        while(len(self.genomes) < self.genomeUnits):
            self.genomes.append(self.buildGenome(3, 1))

        self.executeGeneration()

    def executeGen(self):
        if (self.state == 'STOP'):
            return

        self.generation += 1
        logger.info('Executing generation %d'%(self.generation,))

        self.genome = 0

        while(self.genome < len(self.genomes) and not self.interupted):
            self.executeGen()
        self.genify()

    def genify(self):

        self.genomes = self.select()

        bestGenomes = self.genomes
        
        while len(self.genomes) < self.genomeUnits - 2:
            genA = random.choice(bestGenomes).copy()
            genB = random.choice(bestGenomes).copy()

            newGenome = self.mutate(self.crossover(genA, genB))
            self.genomes.append(newGenome)

        while len(self.genomes) < self.genomeUnits:
            gen = random.choice(bestGenomes).copy()
            newGenome = self.mutate(gen)
            self.genomes.append(newGenome)

        logger.info('Completed generation %d' %(self.generation,))

        self.executeGen()

    def select(self):
        d = dict(enumerate(self.genomes))
        f = []
        s = []
        selected = OrderedDict(sorted(d.items(), key= lambda t: t[1].fitness, reverse=True)).values()
        selected = selected[:self.selection]
        tf.reset_default_graph()

        for select in selected:
            fit = select.copy()
            fit.reload()
            s.append(fit)
            f.append(select.fitness)

        selected = None 
        logger.info('Fitness: #### %s' %(str(f),))
        return s        

    def executeGenome(self):
        if (self.state == 'STOP'):
            return
        
        genome = self.genomes[self.genome]
        self.genome += 1
        logger.info('Executing genome %d' %(self.genome,))

        if (self.shouldCheckExp):
            if not self.checkExperience(genome):
                genome.fitness = 0
                logger.info('Genome %d has no min. experience'%(self.genome))
                return

    def checkExperience(self, genome):
        step, start, stop = (0.7, 0.0, 7)

        inputs = [[0.0, 0.8, 0.5]]
        outputs = {}
        for k in np.arange(start, stop, step):
            inputs[0][0] = k
            activation = genome.activate(inputs)
            outputs.update({state:True})
        if len(outputs.keys()) > 1:
            return True
        return False

     # Load genomes saved from saver
    def loadGenomes(self, genomes, deleteOthers):
        if deleteOthers:
            self.genomes = []
  
        loaded = 0
        for k in genomes:
            self.genomes.append(genome)
            loaded +=1
  
        logger.info('Loaded %d genomes!' %(loaded,))

    # Builds a new genome based on the 
    # expected number of inputs and outputs
    def buildGenome(self, inputs, outputs):
        logger.info('Build genome %d' %(len(self.genomes)+1,))
        #Intialize one genome network with one layer perceptron
        network = Perceptron(inputs, 4,outputs)

        return network;


    #SPECIFIC to Neural Network.
    # Crossover two networks
    def crossOver(self, netA, netB):
        #Swap (50% prob.)
        if (random.random() > 0.5):
            temp = netA
            netA = netB
            netB = temp
  
        # get dict from net
        netA_dict = netA.as_dict
        netB_dict = netB.as_dict

        # Cross over bias
        netA_biases = netA_dict['biases']
        netB_biases = netB_dict['biases']
        cutLocation = int(len(netA_biases) * random.random())
        netA_updated_biases = np.append(netA_biases[(range(0,cutLocation)),],
            netB_biases[(range(cutLocation, len(netB_biases))),]) 
        netB_updated_biases = np.append(netB_biases[(range(0,cutLocation)),],
            netA_biases[(range(cutLocation, len(netA_biases))),]) 
        netA_dict['biases'] = netA_updated_biases
        netB_dict['biases'] = netB_updated_biases
        netA.reload()
        netB.reload()

        return netA

   def mutate(self, net):
        # Mutate
        # get dict from net
        net_dict = net.as_dict
        self.mutateDataKeys(net_dict, 'biases', self.mutationProb)
        self.mutateDataKeys(net_dict, 'weights', self.mutationProb)
        net.reload()
        return net

    def mutateDataKeys(self, a, key, mutationRate):
        for k in range(0,len(a[key])):
        # Should mutate?
            if (random.random() > mutationRate):
                continue
            a[key][k] += a[key][k] * (random.random() - 0.5) * 1.5 + (random.random() - 0.5)

    
if __name__ == "__main__":
    from game_manipulator import GameManipulator
    gm = GameManipulator()
    learner = Learner(gm,12, 4, 0.2)

    #p = Perceptron(3,4,1)
    j =0
    for i in range (0,100):
        tf.reset_default_graph()
        #sess = tf.Session()
        p = Perceptron(3,4,1)
        exp = learner.checkExperience(p)
        print exp
        if exp:
            j+=1
    print j
