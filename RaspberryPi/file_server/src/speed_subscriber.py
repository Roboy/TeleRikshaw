#!/usr/bin/env python
import rospy
from std_msgs.msg import Int16

def callback(data):
    # TODO: write to serial port 's' + 0 ... 510
    rospy.loginfo(rospy.get_caller_id() + "I heard %s", data.data)
    
def speed_subscriber():

    rospy.init_node('speed_subscriber', anonymous=True)
    # TODO: topic
    rospy.Subscriber("test", Int16, callback)
    rospy.spin()

if __name__ == '__main__':
    speed_subscriber()