# Package `file_server`

## Description
This package accomplishes the communication between Unity and ROS.

Unity sends RC car commands to the RaspberryPi by publishing to the following topics via [rosbridge_server](http://wiki.ros.org/rosbridge_server) and [ROS#](https://github.com/siemens/ros-sharp):
* `direction_car`
* `speed`
* `direction_camera`
* `honking`
* `music`

There is one ROS node subscribing to each topic and writing the corresponding command to the serial port of the Arduino:
* [`camera_direction_subscriber`](https://github.com/Roboy/TeleRikshaw/blob/devel/RaspberryPi/file_server/src/camera_direction_subscriber.py)
* [`direction_subscriber`](https://github.com/Roboy/TeleRikshaw/blob/devel/RaspberryPi/file_server/src/direction_subscriber.py)
* [`honk_subscriber`](https://github.com/Roboy/TeleRikshaw/blob/devel/RaspberryPi/file_server/src/honk_subscriber.py)
* [`sound_subscriber`](https://github.com/Roboy/TeleRikshaw/blob/devel/RaspberryPi/file_server/src/sound_subscriber.py)
* [`speed_subscriber`](https://github.com/Roboy/TeleRikshaw/blob/devel/RaspberryPi/file_server/src/speed_subscriber.py)

## Procedure
1. setup the connection between ROS and Unity

```
$ roslaunch file_server unity_communication.launch
```

1. launch the subscriber nodes

```
$ roslaunch file_server subscribers.launch
```

Now you can control the RC car by moving the Vive handlebar and by pressing the buttons on the Vive controllers.