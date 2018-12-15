#!/usr/bin/env python
import rospy
import serial
from std_msgs.msg import Int16

arduinoSerialData =  serial.Serial('/dev/ttyACM0', 9600)

def callback(data):
    # TODO: write to serial port "B" + 1 ... 2000
    arduinoSerialData.write("B" + str(data.data) + "\n")
    #rospy.loginfo(str(data.data) + "\n")
    
def honk_subscriber():

    rospy.init_node('honk_subscriber', anonymous=True)
    rospy.Subscriber("honking", Int16, callback)
    rospy.spin()

if __name__ == '__main__':
    honk_subscriber()