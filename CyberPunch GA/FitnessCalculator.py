

class FitnessCalculator:

    solution = [Individual.geneSize]

    def setSolution(newSolution):
        solution = newSolution

    def getFitness(Individual indiv):
        fitness = 0

        for i range(indiv.size()):
            if indiv.getGene(i) == solution[i]:
                fitness++
        
        return fitness

    def getMaxFitness():
        maxFitness = len(solution)
        return maxFitness

    