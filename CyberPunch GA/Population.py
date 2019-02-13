import Individual

class Population:

    individuals = []

    def size():
        return len(individuals)

    def __init__(self, population_size, initialise):
        for i in range(population_size):
            individuals[i] = Individual()

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
    

    def save_individual(self, index, indiv):
        individuals[index] = indiv