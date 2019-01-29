import tensorflow as tf
from tensorflow.keras import layers

print(tf.VERSION)

model = tf.keras.Sequential([
    #Adds a densely-connected layer with 64 units to model
    layers.Dense(64, activation='relu'),
    #Add another
    layers.Dense(64, activation='relu'),
    #Add a softmax layer with 10 output units
    layers.Dense(10, activation='softmax')])
model.compile(optimizer=tf.train.AdamOptimizer(0.001),
                    loss='categorical_crossentropy',
                    metrics=['accuracy'])
