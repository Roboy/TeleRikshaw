FROM ros:kinetic-robot

WORKDIR /home/ros

# Install system libraries
RUN apt-get update && \
    sudo apt-get install ros-kinetic-rosbridge-server

# Initialize ROS
RUN /bin/bash -c 'source /opt/ros/kinetic/setup.bash'

# Create catkin workspace
RUN mkdir -p catkin_ws/src && \
    cd catkin_ws && \
    catkin_make

# Clone TeleRickshaw repo
WORKDIR /home
RUN git clone https://github.com/Roboy/TeleRikshaw && \
    git checkout devel && \
    mv /home/TeleRickshaw/RaspberryPi/src/ /home/ros/catkin_ws/src

# build catkin workspace
WORKDIR /home/ros/catkin_ws
RUN catkin_make 
RUN /bin/bash -c 'source devel/setup.bash'

# Configure ROS master
RUN export ROS_MASTER_URI=0.0.0.0:11311

CMD roslaunch file_server unity_communication.launch

