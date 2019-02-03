#!/usr/bin/env python
import rospy
import serial
from std_msgs.msg import String

arduinoSerialData =  serial.Serial('/dev/ttyACM0', 9600)

def callback(data):
    # TODO: write to serial port "C" + 35 ... 145
    arduinoSerialData.write(data.data + "\n")
    #rospy.loginfo(str(data.data) + "\n")
    
def camera_direction_subscriber():

    rospy.init_node('camera_direction_subscriber', anonymous=True)
    rospy.Subscriber("direction_camera", String, callback)
    rospy.spin()  

if __name__ == '__main__':
    camera_direction_subscriber()