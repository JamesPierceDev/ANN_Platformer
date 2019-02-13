
import Population
import Individual

class Algorithm:
    uniformRate = 0.5
    mutationRate = 0.010
    tournamentSize = 5
    elitism = false

    def evolvePopulation(p):
        newPopulation = new Population(p.size(), false)

        if elitism:
            newPopulation.saveIndividual(0, pop.getFittest())
    
        elitismOffset = 0
        if elitism
            elitismOffset = 1
        else
            elitismOffset = 0

        for i in range(p.size()):
            indiv1 = tournamementSelect(p)
            indiv2 = tournamementSelect(p)
            newIndiv = crossover(indiv1, indiv2)
            newPopulation.saveIndividual(i, newIndiv)

        for i in range(newPopulation.size()):
            mutate(newPopulation.getIndividual(i))

        return newPopulation
        
    def crossover(indiv1, indiv2):
        newSolution = Individual

        for i in range(indiv1.size()):
            if random.randrange() <= uniformRate:
                newSolution.setGene(i, indiv1.getGene(i))
            else:
                newSolution.setGene(i, indiv2.getGene(i))
        return newSolution