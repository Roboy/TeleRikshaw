#!/usr/bin/env python
import rospy
import serial
from std_msgs.msg import Int16

arduinoSerialData =  serial.Serial('/dev/ttyACM0', 9600)

def callback(data):
    # TODO: write to serial port "C" + 35 ... 145
    arduinoSerialData.write("C" + str(data.data) + "\n")
    #rospy.loginfo(str(data.data) + "\n")
    
def camera_direction_subscriber():

    rospy.init_node('camera_direction_subscriber', anonymous=True)
    rospy.Subscriber("direction_camera", Int16, callback)
    rospy.spin()  

if __name__ == '__main__':
    camera_direction_subscriber()