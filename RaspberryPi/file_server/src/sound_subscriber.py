#!/usr/bin/env python
import rospy
import serial
from std_msgs.msg import Int16

arduinoSerialData =  serial.Serial('/dev/ttyACM0', 9600)

def callback(data):
    # TODO: write to serial port "J" + 1 ... inf
    arduinoSerialData.write("J" + str(data.data) + "\n")
    #rospy.loginfo(str(data.data) + "\n")
    
def sound_subscriber():

    rospy.init_node('sound_subscriber', anonymous=True)
    rospy.Subscriber("music", Int16, callback)
    rospy.spin()

if __name__ == '__main__':
    sound_subscriber()