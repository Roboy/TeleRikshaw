# Arduino code for the RC car

Here, you can find the basic Arduino code for controlling the RC car. The Arduino code also contains the `ros_lib` for communication between ROS and Arduino via [rosserial_arduino](http://wiki.ros.org/rosserial_arduino). This allows to launch nodes from inside the Arduino code and to publish and subscribe to topics for example.    
Thus, the Arduino control functions of the RC car are called within the Arduino callback functions of the ROS subscribers.

Take a look at [these tutorials](http://wiki.ros.org/rosserial_arduino/Tutorials) for more information.