
from random import random

class Individual:

    def __init__(self):
        self.defaultGeneLength = 64
        self.genes = []
        self.fitness = 0

    def size(self):
        return len(self.genes)

    def gen_individual(self):
        for i in range(size()):
            gene = random.range(0, 2)
            self.genes[i] = gene

    def set_default_gene_length(self, length):
        self.defaultGeneLength = length

    def get_default_gene_length(self):
        return self.defaultGeneLength

    def get_gene(self, index):
        return self.genes[index]

    def set_gene(self, index, value):
        self.genes[index] = value
        self.fitness = 0

    def get_fitness(self):
        if self.fitness == 0:
            #self.fitness = FitnessCalculator.get_fitness(self)
        return self.fitness