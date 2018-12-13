#!/usr/bin/env python
import rospy
from std_msgs.msg import Int16

def callback(data):
    # TODO: write to serial port 'd' + 40 ... 140
    rospy.loginfo(rospy.get_caller_id() + "I heard %s", data.data)
    
def direction_subscriber():

    rospy.init_node('direction_subscriber', anonymous=True)
    # TODO: topic
    rospy.Subscriber("test", Int16, callback)
    rospy.spin()

if __name__ == '__main__':
    direction_subscriber()