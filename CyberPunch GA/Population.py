import Individual

class Population:

    individuals = []

    def __init__(self, populationSize, initialise):
        individuals = new Individual[populationSize]

        if initialise:
            for i in range(size()):
                newIndividual = new Individual()
                newIndividual.genIndividual()
                saveIndividual(i, newIndividual)

    def getIndividual(index):
        return individuals[index]

    def getFittest():
        fittest = individuals[0]

        for i in range(size()):
            if fittest.getFitness() <= getIndividual(i).getFitness():
                fittest = getIndividual(i)
        
        return fittest
    
    def size():
        return len(individuals)

    def saveIndividual(index, indiv):
        individuals[index] = indiv