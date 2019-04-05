import logging

import numpy as np 
from mlagents.trainers.ga.models import GAModel
from mlagents.trainers.policy import Policy

logger = logging.getLogger("mlagents.trainers")

class GAPolicy(Policy):
    def __init__(self, seed, brain, trainer_parameters, load):
        """
        @param seed : Random seed
        @param brain : Assigned brain object
        @param trainer_parameters : Defined training parameters
        @param load : Whether a pre-trained model will be loaded or a new one created
        """
        super(GAPolicy, self).__init__(seed, brain, trainer_parameters)

        with self.graph.as_default():
            with self.graph.as_default():
                self.model = GAModel(
                    h_size = int(trainer_parameters['hidden_units']),
                    lr = float(trainer_parameters['learning_rate']),
                    n_layers = int(trainer_parameters['num_layers']),
                    m_size = self.m_size,
                    normalize = False,
                    user_recurrent = trainer_parameters['use_recurrent'],
                    brain=brain,
                    seed = seed
                )

        if load:
            self._load_graph()
        else:
            self._initialize_graph()

        self.inference_dict = {'action': self.model.sample_action}
        self.update_dict = {'policy_loss': self.model.loss,
                            'update_batch': self.model.update}
        if self.use_recurrent:
            self.inference_dict['memory_out'] = self.model.memory_out

        self.evaluate_rate = 1.0
        self.update_rate = 0.5

    def evaluate(self, brain_info):
        """
        This function will need to implement a fitness calculator
        @param brain_info : BrainInfo to network
        """
        

    def update(self, mini_batch, num_sequences):
        """
        Updates the model.
        @param mini_batch : Batch of experiences
        @param num_sequences : Number of sequences to process
        """

