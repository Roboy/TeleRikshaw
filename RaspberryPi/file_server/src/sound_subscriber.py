#!/usr/bin/env python
import rospy
from std_msgs.msg import Int16

def callback(data):
    # TODO: write to serial port 'j' + 1 ... inf
    rospy.loginfo(rospy.get_caller_id() + "I heard %s", data.data)
    
def sound_subscriber():

    rospy.init_node('sound_subscriber', anonymous=True)
    # TODO: topic
    rospy.Subscriber("music", Int16, callback)
    rospy.spin()

if __name__ == '__main__':
    sound_subscriber()