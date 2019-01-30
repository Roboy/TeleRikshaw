FROM ros:kinetic-robot

WORKDIR /home/ros

# Install system libraries
RUN apt-get update && \
    apt-get install -y ros-kinetic-rosbridge-server git apt-utils net-tools

# Initialize ROS
RUN /bin/bash -c 'source /opt/ros/kinetic/setup.bash'

# Create catkin workspace
RUN mkdir -p catkin_ws/src
#RUN /bin/bash -c 'catkin_init_workspace'
#WORKDIR /home/ros/catkin_ws
#RUN /bin/bash -c 'source devel/setup.bash'
#RUN /bin/bash -c 'catkin_make'
#RUN roscore

# Clone TeleRickshaw repo
WORKDIR /home/ros
RUN mkdir /home/ros/repo
RUN git clone https://github.com/Roboy/TeleRikshaw.git repo
WORKDIR /home/ros/repo
RUN git checkout devel
RUN mv /home/ros/repo/RaspberryPi/src/ /home/ros/catkin_ws/src

# build catkin workspace
WORKDIR /home/ros/catkin_ws
RUN /bin/bash -c '. /opt/ros/kinetic/setup.bash; cd /home/ros/catkin_ws; catkin_make'
RUN /bin/bash -c 'source devel/setup.bash'

# Configure ROS master
RUN export ROS_MASTER_URI=0.0.0.0:11311

#CMD roslaunch file_server unity_communication.launch

