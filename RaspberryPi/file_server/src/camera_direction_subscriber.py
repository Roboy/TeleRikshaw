#!/usr/bin/env python
import rospy
from std_msgs.msg import Int16

def callback(data):
    # TODO: write to serial port 'c' + 35 ... 145
    rospy.loginfo(rospy.get_caller_id() + "I heard %s", data.data)
    
def camera_direction_subscriber():

    rospy.init_node('camera_direction_subscriber', anonymous=True)
    # TODO: topic
    rospy.Subscriber("test", Int16, callback)
    rospy.spin()

if __name__ == '__main__':
    camera_direction_subscriber()