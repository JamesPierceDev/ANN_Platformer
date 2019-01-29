#Tensorflow examples


#import
import tensorflow as tf 

#create a tensorflow constant
const = tf.constant(2.0, name="const")

#create tensorflow variables
b = tf.Variable(2.0, name="b")
c = tf.Variable(1.0, name="c")

#create operations
d = tf.add(b, c, name="d")
e = tf.add(c, const, name="e")
a = tf.multiply(d, e, name="a")

#setup the variable init
init_op = tf.global_variables_initializer()

#start the session
with tf.Session() as sess:
    #init the variables
    sess.run(init_op)
    #computer the output of the graph
    a_out = sess.run(a)
    print("Variable a is {}".format(a_out))

    