#Unity ML-Agents
# ML-Agent Learning (Genetic Algorithm)
# @Author - James Pierce
# Contains an implementation of Genetic Algorithm

import logging
import os

import numpy as np 
import tensorflow as tf 

from mlagents.envs import AllBrainInfo
from mlagents.trainers.ga.policy import GAPolicy
from mlagents.trainer.buffer import Buffer
from mlagents.trainers.trainer import Trainer 

logger = logging.getLogger("mlagents.trainers")

class GATrainer(Trainer):
    """GATrainer is an implementation of Genetic Algorithm"""

    def __init__(self, brain, trainer_parameters, training, load, seed, run_id):
        super(GATrainer, self).__init__(brain, trainer_parameters, training, run_id)
        self.policy = GAPolicy(seed, brain, trainer_parameters, load)
        self.n_sequences = 1
        self.cumulative_rewards = {}
        self.episode_steps = {}
        self.stats = {'Fitness' : [], 'Environment/Eposide Length': [],
                        'Environment/Cumulative Reward': []}

        self.summary_path = trainer_parameters['summary_path']
        self.batches_per_epoch = trainer_parameters['batches_per_epoch']
        if not os.path.exists(self.summary_path):
            os.makedirs(self.summary_path)

        self.demonstration_buffer = Buffer()
        self.evaluation_buffer = Buffer()
        self.summary_writer = tf.summary.FileWriter(self.summary_path)

@property
def parameters(self):
    return self.trainer_parameters

@property
def get_max_steps(self):
    return float(self.trainer_parameters['max_steps'])

@property
def get_step(self):
    return self.policy.get_current_step()