#!/usr/bin/env python
import rospy
import serial
from std_msgs.msg import String

arduinoSerialData =  serial.Serial('/dev/ttyACM0', 9600)

def callback(data):
    # TODO: write to serial port "B" + 1 ... 2000
    arduinoSerialData.write(data.data + "\n")
    #rospy.loginfo(str(data.data) + "\n")
    
def honk_subscriber():

    rospy.init_node('honk_subscriber', anonymous=True)
    rospy.Subscriber("honking", String, callback)
    rospy.spin()

if __name__ == '__main__':
    honk_subscriber()