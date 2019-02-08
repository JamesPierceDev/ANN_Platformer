import random
import FitnesCalculator

class Individual:

    geneSize = 16
    genes = [16]
    fitness = 0

    def genIndividual():
        for i in range(len(genes)):
            g = random.randrange(1, 16)
            genes[i] = g

    def getFitness():
        if fitness == 0:
            #fitness = CalculateFitness(self)
        return fitness

    def getGene(index):
        return genes[index]