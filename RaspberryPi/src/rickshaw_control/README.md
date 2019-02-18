# Package `rickshaw_control`

## Description
This package accomplishes the processing of the control commands between Unity and ROS for the Rickshaw.

Unity sends RC car commands to the Rickshaw by publishing to the following topics via [rosbridge_server](http://wiki.ros.org/rosbridge_server) and [ROS#](https://github.com/siemens/ros-sharp):
* `cmd_vel`   
 
Furthermore, the node [`speed_handler`](https://github.com/Roboy/TeleRikshaw/blob/devel/RaspberryPi/src/rickshaw_control/src/speed_handler.py) calculates and publishes the speed of the Rickshaw to be displayed by Unity.

## Procedure
* setup the connection between ROS and Unity

```
$ roslaunch file_server unity_communication.launch
```

* launch the processing node

```
$ rosrun rickshaw_control speed_handler.py
```

Now you can control the Rickshaw by moving the Vive handlebar and by pressing the buttons on the Vive controllers.