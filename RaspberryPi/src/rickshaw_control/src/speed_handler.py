#!/usr/bin/env python
import rospy
from std_msgs.msg import String
from nav_msgs.msg import Odometry
import math

speed_mps = 0.0
speed_kmh = 0.0

pub = rospy.Publisher('rickshaw_speed', String, queue_size=10)

def callback(data):
    speed_mps = data.twist.twist.linear.x
    speed_kmh = speed_mps * 3.6
    pub.publish(speed_kmh)
    
def speed_handler():
    rospy.init_node('speed_handler_rickshaw', anonymous=True)
    # TODO odometry
    rospy.Subscriber("odom", Odometry, callback)
    rospy.spin()

if __name__ == '__main__':
    try:
        speed_handler()
    except rospy.ROSInterruptException:
        pass
