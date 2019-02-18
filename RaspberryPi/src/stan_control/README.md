# Package `stan_control`

## Description
This package accomplishes the processing of the control commands between Unity and ROS for the RC car.

Unity sends RC car commands to the RaspberryPi by publishing to the following topics via [rosbridge_server](http://wiki.ros.org/rosbridge_server) and [ROS#](https://github.com/siemens/ros-sharp):
* `direction_car`
* `speed_car`
* `music`

There is one ROS node subscribing to each topic and writing the corresponding command to the serial port of the Arduino:
* [`direction_subscriber`](https://github.com/Roboy/TeleRikshaw/blob/devel/RaspberryPi/src/stan_control/src/direction_subscriber.py)
* [`sound_subscriber`](https://github.com/Roboy/TeleRikshaw/blob/devel/RaspberryPi/src/stan_control/src/sound_subscriber.py)
* [`speed_handler`](https://github.com/Roboy/TeleRikshaw/blob/devel/RaspberryPi/src/stan_control/src/speed_handler.py)  

The node `speed_handler` additionally calculates and publishes the speed of the RC car.

Moreover, this package contains a launch file to publish and convert the video stream as ROS topic `/raspicam_node/image_repub/compressed`.

## Procedure
* setup the connection between ROS and Unity:
```
$ roslaunch file_server unity_communication.launch
```
* launch the subscriber nodes only:
```
$ roslaunch stan_control subscribers.launch
```
* launch the video stream nodes only:
```
$ roslaunch stan_control raspicam.launch
```
* start capturing of video stream:
```
$ rosservice call /raspicam_node/start_capture
```
* you can also launch subscribers and video stream nodes alltogether:
```
$ roslaunch stan_control launch_all.launch
```
* then you just need to start capturing the video stream:
```
$ rosservice call /raspicam_node/start_capture
```
Now you can control the RC car by moving the Vive handlebar and by pressing the buttons on the Vive controllers. Simultaneously, the video stream of the RaspberryPi camera is displayed in Unity in real-time.